const TOKEN=require('../../wxapi/token.js')
const HTTP = require('../../wxapi/http.js')
var app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    remind: '加载中',
    angle: 0,
    needLogin:false,
    btnClass: null,
    copyTime: new Date().getFullYear(),
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this
    that.setData({
      background_color: app.globalData.globalBGColor,
      bgRed: app.globalData.bgRed,
      bgGreen: app.globalData.bgGreen,
      bgBlue: app.globalData.bgBlue
    })
    if (wx.getStorageSync('userInfo')){
      that.setData({
        needLogin:false
      });
    }else{
      that.setData({
        needLogin: true
      });
    }
  },
  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
    var that = this;
    setTimeout(function () {
      that.setData({
        remind: ''
      });
    }, 1000);
    wx.onAccelerometerChange(function (res) {
      var angle = -(res.x * 30).toFixed(1);
      if (angle > 14) { angle = 14; }
      else if (angle < -14) { angle = -14; }
      if (that.data.angle !== angle) {
        that.setData({
          angle: angle
        });
      }
    });
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    console.info('onShow');
  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {
    console.info('onHide');
  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  },
  goToIndex(){
    const isauth = TOKEN.isAuthenticated();
    if(!isauth){
      wx.showLoading({
        title:'正在离线浏览...',
        success:function(){
          wx.switchTab({
            url: '/pages/main/index'
          })
        }
      });
    }else{
      app.globalData.userInfo = wx.getStorageSync('userInfo')
      wx.switchTab({
        url: '/pages/main/index'
      })
    }
  },
  bindGetUserInfo: function (e) {
    if (!e.detail.userInfo) {
      return;
    }
    var that = this;
    if (app.globalData.isConnected) {
      wx.setStorageSync('userInfo', e.detail.userInfo);
      app.globalData.userInfo = e.detail.userInfo;
      that.login();
    } else {
      wx.showToast({
        title: '当前无网络',
        icon: 'none',
      })
    }
  },
  login: function (e) {
    const that = this;
    wx.login({
      success: function (res) {

        wx.getUserInfo({
          success:function(infores){
            app.globalData.userInfo = infores.userInfo;
          }
        });

        that.setData({
          btnClass:'hidden'
        });
        const isauth =TOKEN.isAuthenticated();
        if(isauth){
          wx.showLoading({
            title: '登录中...',
          })
          HTTP.login({ code: res.code }, function (response) {
            if (response.code == 10000) {
              // 去注册
              that.registerUser();
              return;
            }
            if (response.code != 0) {

              wx.showModal({
                title: '提示',
                content: '系统未知错误',
                showCancel: false,
                success(){
                  wx.switchTab({
                    url: '/pages/main/index'
                  })
                }
              })
              // wx.showModal({
              //   title: '提示',
              //   content: '无法登录，是否重试？',
              //   showCancel: true,
              //   success(){
              //     that.login();
              //   },
              //   fail(){
              //     that.onLoad();
              //   }
              // })
              return;
            }
            wx.setStorageSync('userid', response.Data.Id)
            app.navigateToLogin = false
            wx.switchTab({
              url: '/pages/main/index'
            })
          });
        } else {
          wx.showLoading({
            title: '正在离线浏览...',
            success:function(){
              wx.setStorageSync('userid', 1)
              app.navigateToLogin = false
              wx.switchTab({
                url: '/pages/main/index'
              });
            }
          });

        }
      }
    })
  },
  registerUser: function () {
    var that = this;
    wx.login({
      success: function (fres) {
        var code = fres.code; // 微信登录接口返回的 code 参数，下面注册接口需要用到
        wx.getUserInfo({
          success: function (res) {
            app.globalData.userInfo = res.userInfo;
            HTTP.register({ code: code, rawData: res.rawData }, function (response) {
              wx.setStorageSync('userid', response.Data.Id)
              wx.hideLoading();
              wx.switchTab({
                url: '/pages/main/index'
              })
            });
          }
        })
      }
    })
  }
})