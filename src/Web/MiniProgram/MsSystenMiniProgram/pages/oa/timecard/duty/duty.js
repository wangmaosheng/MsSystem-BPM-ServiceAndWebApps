var util = require('../../../../utils/util.js');
var amapFile = require('../../../../utils/amap-wx.js');
var app = getApp();
// pages/oa/timecard/duty/duty.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    latitude: 0,
    longitude: 0,
    nowTime: '',
    locationName:'等待获取',
    locationInfo:'等待获取',
    timecardType: ['上班','下班'],
    timecardTypeIndex: 0,
    isLoading: true
  },
  bindTypeChange:function(e){
    this.setData({
      timecardTypeIndex:e.detail.value
    });
  },
  /**
   * 生命周期函数--监听页面加载
   */
  getLocation:function(){
    var that = this;
    that.setData({
      isLoading: true
    })
    var amap = new amapFile.AMapWX({ key: '8ebbe699d71eed6674889848604e411a' });
    amap.getRegeo({
      success: function (data) {
        var _locationName = data[0].name;
        var _locationInfo = data[0].desc;
        var lat = data[0].latitude;
        var lon = data[0].longitude;
        that.setData({
          locationName: _locationName,
          locationInfo: _locationInfo,
          latitude: lat,
          longitude: lon,
          isLoading: false
        })
      },
      fail: function (info) {
        console.info(info)
      }
    })
  },
  onLoad: function (options) {
    this.getLocation();
  },
  reLocate:function(){
    this.getLocation();
  },
  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {
    this.setData({
      isLoading: true,
      nowTime: util.currentTime()
    });
  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {
    // 时间显示
    var that = this;
    setInterval(function () {
      that.setData({
        nowTime: util.currentTime()
      });
    }, 1000)
  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

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

  }
})