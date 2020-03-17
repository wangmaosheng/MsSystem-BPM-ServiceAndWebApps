const CONFIG = require('../config.js')
const TOKEN = require('./token.js')
const BaseUrl = CONFIG.gateway.url+'/api';

/**
 * http请求封装
 */
const request = function (_url, _method, needAuthorize, data, successCallback, failCallback){
  var _header;
  if (needAuthorize){
    var _token = TOKEN.getToken();
    _header = { 'Authorization': 'Bearer ' + _token };
  }else{
    _header = { 'Content-Type': 'application/x-www-form-urlencoded' };
  }
  wx.request({
    url: BaseUrl + _url,
    method: _method,
    header: _header,
    data: data,
    success: (response) => {
      return typeof successCallback == "function" && successCallback(response.data)
    },
    fail:function(){
      return typeof failCallback == "function" && failCallback(false)
    },
    complete:function(){
      wx.hideLoading();
    }
  })
};

const login = function (data, successCallback, failCallback){
  request('/weixin/MiniProgram/Login', 'POST', true, data, successCallback, failCallback);
};
const register = function (data, successCallback, failCallback){
  request('/weixin/MiniProgram/Register', 'POST', true, data, successCallback, failCallback);
};

/**
 * sys系统下的接口
 */
const sys = {
  getHeartBeat: function (data, successCallback, failCallback) {
    request('/sys/Log/GetLasterDataAsync', 'GET', true, data, successCallback, failCallback);
  }
};
/**
 * oa 系统下的接口
 */
const oa = {
  /**
   * 请假保存
   */
  leaveSave: function (data, successCallback, failCallback){
    request('/oa/Leave/InsertAsync', 'POST', true, data, successCallback, failCallback);
  }
};
module.exports = {
  request,
  login,
  register,
  sys,
  oa
}