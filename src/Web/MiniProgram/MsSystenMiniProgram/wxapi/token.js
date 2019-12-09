const CONFIG = require('../config.js')
/**
 * 获取AccessToken操作
 */
const getToken = function() {
  var token = '';
  initGatewayToken(function (res) {
    token = res;
  })
  return token;
}
/**
 * 判断是否已经认证
 */
const isAuthenticated = function(){
  var gw = CONFIG.gateway;
  var timestamp = Date.parse(new Date());
  var expiration = timestamp + 100 * 60 * 1000; //缓存100分钟
  var data_expiration = wx.getStorageSync(gw.tokenTimeKey);
  if (data_expiration){
    if (timestamp > data_expiration) {
      return false;
    } else {
      var data_token = wx.getStorageSync(gw.tokenkey);
      if (data_token){
        return getToken().length > 0;
      }else{
        return false;
      }
    }
  }else{
    return false;
  }
};
/**
 * 缓存AccessToken操作
 */
const initGatewayToken = function(cb) {
  var gw = CONFIG.gateway;
  var timestamp = Date.parse(new Date());
  var expiration = timestamp + 100 * 60 * 1000; //缓存100分钟
  var data_expiration = wx.getStorageSync(gw.tokenTimeKey);
  if (data_expiration) {
    if (timestamp > data_expiration) { //已过期
      wx.clearStorageSync()
      requestGateWayAccessToken(function(token){
        wx.setStorageSync(gw.tokenkey, token)
        wx.setStorageSync(gw.tokenTimeKey, expiration)
        if (cb){
          cb(token);
        }
      });
    }else{
      //未过期
      var data_token = wx.getStorageSync(gw.tokenkey);
      if (data_token.length > 0) {
        if (cb) {
          cb(data_token);
        }
        return;
      }
    }
  } else {
    requestGateWayAccessToken(function (token) {
      wx.setStorageSync(gw.tokenkey, token)
      wx.setStorageSync(gw.tokenTimeKey, expiration)
      if (cb) {
        cb(token);
      }
    });
  }
}
/**
 * 请求获取AccessToken
 */
const requestGateWayAccessToken = function(cb) {
  var gw = CONFIG.gateway;
  wx.request({
    url: gw.url + gw.tokenurl,
    method: 'POST',
    header: {
      'content-type': 'application/x-www-form-urlencoded' // 默认值
    },
    dataType: 'json',
    data: {
      client_id: gw.client_id,
      client_secret: gw.client_secret,
      grant_type: gw.grant_type,
      scopes: gw.scopes
    },
    success: function (response) {
      if (response.statusCode == 200) {
        //回调
        cb(response.data.access_token);
      }
    },
    fail:function(){
      console.error("网络不通！！！");
    }
  })
}

module.exports = {
  getToken,
  initGatewayToken,
  isAuthenticated
}