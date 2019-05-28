var TOKEN = require("../wxapi/token.js")
const protocal = {
  protocol: "json",
  version: 1
};

const MessageType = {
  /** Indicates the message is an Invocation message and implements the {@link InvocationMessage} interface. */
  Invocation: 1,
  /** Indicates the message is a StreamItem message and implements the {@link StreamItemMessage} interface. */
  StreamItem: 2,
  /** Indicates the message is a Completion message and implements the {@link CompletionMessage} interface. */
  Completion: 3,
  /** Indicates the message is a Stream Invocation message and implements the {@link StreamInvocationMessage} interface. */
  StreamInvocation: 4,
  /** Indicates the message is a Cancel Invocation message and implements the {@link CancelInvocationMessage} interface. */
  CancelInvocation: 5,
  /** Indicates the message is a Ping message and implements the {@link PingMessage} interface. */
  Ping: 6,
  /** Indicates the message is a Close message and implements the {@link CloseMessage} interface. */
  Close: 7,
}


export class HubConnection {

  constructor(needAuth) {
    this.openStatus = false;
    this.methods = {};
    this.negotiateResponse = {};
    this.connection = {};
    this.url = "";
    this.invocationId = 0;
    this.callbacks = {};
    this.needAuth = (needAuth == undefined || needAuth == false)?false:true;
    var _header;
    if (this.needAuth) {
      var _token = TOKEN.getToken();
      _header = { 'Authorization': 'Bearer ' + _token };
    } else {
      _header = {};
    }
    this._header = _header;
  }


  start(url, queryString) {
    var negotiateUrl = url + "/negotiate";
    if (queryString) {
      for (var query in queryString) {
        negotiateUrl += (negotiateUrl.indexOf("?") < 0 ? "?" : "&") + (`${query}=` + encodeURIComponent(queryString[query]));
      }
    }
    wx.request({
      url: negotiateUrl,
      method: "post",
      header: this._header,
      async: false,
      success: res => {
        this.negotiateResponse = res.data;
        this.startSocket(negotiateUrl.replace("/negotiate", ""));
      },
      fail: res => {
        console.error(`requrst ${url} error : ${res}`);
        return;
      }
    });

  }

  startSocket(url) {
    url += (url.indexOf("?") < 0 ? "?" : "&") + ("id=" + this.negotiateResponse.connectionId);
    url = url.replace(/^http/, "ws");
    this.url = url;
    if (this.connection != null && this.openStatus) {
      return;
    }


    this.connection = wx.connectSocket({
      url: url,
      method: "get",
      header: this._header,
      success:function(response){
      },
      fail:function(error){
      }
    })

    this.connection.onOpen(res => {
      console.log(`websocket connectioned to ${this.url}`);
      this.sendData(protocal);
      this.openStatus = true;
      this.onOpen(res);
    });

    this.connection.onClose(res => {
      console.log(`websocket disconnection`);
      this.connection = null;
      this.openStatus = false;
      this.onClose(res);
    });

    this.connection.onError(res => {
      console.error(`websocket error msg: ${res}`);
      this.close({
        reason: res
      });
      this.onError(res)
    });

    this.connection.onMessage(res => this.receive(res));
  }


  on(method, fun) {
    if (this.methods[method]) {
      this.methods[method].push(fun);
    } else {
      this.methods[method] = [fun];
    }
  }

  onOpen(data) { 
    console.log("连接已开启")
  }

  onClose(msg) {
    console.log("连接已关闭")
  }

  onError(msg) {
    console.log(msg)
  }


  close(data) {
    if (data) {
      this.connection.close(data);
    } else {
      this.connection.close();
    }

    this.openStatus = false;
  }

  sendData(data, success, fail, complete) {
    this.connection.send({
      data: JSON.stringify(data) + "", //
      success: success,
      fail: fail,
      complete: complete,
      header:this._header
    });
  }


  receive(data) {
    if (data.data.length > 3) {
      data.data = data.data.replace('{}', "")
    }

    var message = JSON.parse(data.data.replace(new RegExp("", "gm"), ""));

    switch (message.type) {
      case MessageType.Invocation:
        this.invokeClientMethod(message);
        break;
      case MessageType.StreamItem:
        break;
      case MessageType.Completion:
        var callback = this.callbacks[message.invocationId];
        if (callback != null) {
          delete this.callbacks[message.invocationId];
          callback(message);
        }
        break;
      case MessageType.Ping:
        // Don't care about pings
        break;
      case MessageType.Close:
        console.log("Close message received from server.");
        this.close({
          reason: "Server returned an error on close"
        });
        break;
      default:
        console.warn("Invalid message type: " + message.type);
    }


  }


  send(functionName) {

    var args = [];
    for (var _i = 1; _i < arguments.length; _i++) {
      args[_i - 1] = arguments[_i];
    }

    this.sendData({
      target: functionName,
      arguments: args,
      type: MessageType.Invocation,
      invocationId: this.invocationId.toString()
    });
    this.invocationId++;
  }


  invoke(functionName) {
    var args = [];
    for (var _i = 1; _i < arguments.length; _i++) {
      args[_i - 1] = arguments[_i];
    }

    var _this = this;
    var id = this.invocationId;
    var p = new Promise(function (resolve, reject) {

      _this.callbacks[id] = function (message) {
        if (message.error) {
          reject(new Error(message.error));
        } else {
          resolve(message.result);
        }
      }

      _this.sendData({
        target: functionName,
        arguments: args,
        type: MessageType.Invocation,
        invocationId: _this.invocationId.toString()
      }, null, function (e) {
        reject(e);
      });

    });
    this.invocationId++;
    return p;

  }

  invokeClientMethod(message) {
    var methods = this.methods[message.target.toLowerCase()];
    if (methods) {
      methods.forEach(m => m.apply(this, message.arguments));
      if (message.invocationId) {
        // This is not supported in v1. So we return an error to avoid blocking the server waiting for the response.
        var errormsg = "Server requested a response, which is not supported in this version of the client.";
        console.error(errormsg);
        this.close({
          reason: errormsg
        });
      }
    } else {
      console.warn(`No client method with the name '${message.target}' found.`);
    }
  }
}