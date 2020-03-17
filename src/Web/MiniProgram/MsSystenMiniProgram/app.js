const WXAPI = require('wxapi/main')
const CONFIG = require('./config.js')
const TOKEN = require('wxapi/token.js')
App({
  navigateToLogin: false,
  onLaunch: function () {
    /**
     * GatewayToken
     */
    TOKEN.initGatewayToken();
    const that = this;
    // 检测新版本
    const updateManager = wx.getUpdateManager()
    updateManager.onUpdateReady(function () {
      wx.showModal({
        title: '更新提示',
        content: '新版本已经准备好，是否重启应用？',
        success(res) {
          if (res.confirm) {
            // 新的版本已经下载好，调用 applyUpdate 应用新版本并重启
            updateManager.applyUpdate()
          }
        }
      })
    })
    /**
     * 初次加载判断网络情况
     * 无网络状态下根据实际情况进行调整
     */
    wx.getNetworkType({
      success(res) {
        const networkType = res.networkType
        if (networkType === 'none') {
          that.globalData.isConnected = false
          wx.showToast({
            title: '当前无网络',
            icon: 'loading',
            duration: 2000
          })
        }
      }
    });
    /**
     * 监听网络状态变化
     * 可根据业务需求进行调整
     */
    wx.onNetworkStatusChange(function (res) {
      if (!res.isConnected) {
        that.globalData.isConnected = false
        wx.showToast({
          title: '网络已断开',
          icon: 'loading',
          duration: 2000,
          complete: function () {
            that.goStartIndexPage()
          }
        })
      } else {
        that.globalData.isConnected = true
        wx.hideToast()
      }
    });
  },
  goLoginPageTimeOut: function () {
    if (this.navigateToLogin) {
      return
    }
    this.navigateToLogin = true
    setTimeout(function () {
      wx.navigateTo({
        url: "/pages/start/start"
      })
    }, 1000)
  },
  goStartIndexPage: function () {
    setTimeout(function () {
      wx.redirectTo({
        url: "/pages/main/index"
      })
    }, 1000)
  },
  onShow(e) {
    const _this = this
    wx.checkSession({
      fail() {
      }
    })
    this.globalData.launchOption = e
  },
  globalData: {
    globalBGColor: '#0e9aef',
    bgRed: 14,
    bgGreen: 154,
    bgBlue: 239,
    userInfo: null,//微信端用户信息
    isConnected: true,
    launchOption: undefined,
    UserIdentity: { //BPM端用户信息
      UserId: 1,
      UserName: 'wms'
    }
  }
})