// pages/oa/leave/show/show.js
const TOKEN = require('../../../../wxapi/token.js')
const HTTP = require('../../../../wxapi/http.js')

var app = getApp();

Page({

  /**
   * 页面的初始数据
   */
  data: {
    leaveReasonIndex: 0,
    leaveReason: ['事假', '年假', '婚假', '产假/陪产假', '丧假', '探亲假'],
    leaveReasonObj: [
      { id: '0', name: '事假' }, 
      { id: '1', name: '年假' },
      { id: '2', name: '婚假' },
      { id: '3', name: '产假/陪产假' },
      { id: '4', name: '丧假' },
      { id: '5', name: '探亲假' },
    ],
    Title: '',
    Reason: '',
    LeaveType: 0,
    StartTime: 0,
    EndTime: 0,
    CreateUserId: 0
  },
  bindLeaveChange: function (e) {
    this.setData({
      leaveReasonIndex: e.detail.value
    })
  },
  bindStartTimeChange:function(e){
    this.setData({
      StartTime: e.detail.value
    })
  },
  bindEndTimeChange: function (e) {
    this.setData({
      EndTime: e.detail.value
    })
  },
  bindInput:function(e){
    var _name = e.currentTarget.id;
    var _val = e.detail.value;
    this.setData({
      [_name] : _val
    });
  },
  formSave: function(){
    var that = this;
    var leave = {
      Title: that.data.Title,
      Reason: that.data.Reason,
      LeaveType: that.data.leaveReasonIndex,
      StartTime: that.data.StartTime,
      EndTime: that.data.EndTime,
      CreateUserId: that.data.CreateUserId
    };
    HTTP.oa.leaveSave(leave,function(res){
      debugger;
    },function(res){
      debugger;
    })
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var mydate=new Date();
    this.setData({
      CreateUserId: app.globalData.UserIdentity.UserId,
      StartTime: mydate.getFullYear()+'-' + (mydate.getMonth() + 1)+'-' + mydate.getDate(),
      EndTime: mydate.getFullYear() + '-' + (mydate.getMonth() + 1) + '-' + (mydate.getDate() + 1)
    });
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

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