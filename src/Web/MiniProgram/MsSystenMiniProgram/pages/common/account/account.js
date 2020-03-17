// pages/common/account/account.js

const app = getApp();

Page({

  /**
   * 页面的初始数据
   */
  data: {
    userInfo: null,
    list: [
      {
        id: 'timecard',
        name: '打卡',
        open: false,
        pages: [
          { path: "normal", title: '正常出勤' },
          { path: "history", title: '打卡记录' }
        ]
      }, {
        id: 'overwork',
        name: '各类申请',
        open: false,
        pages: [
          { path: "create", title: '加班申请' },
          { path: "list", title: '休假申请' }
        ]
      }, {
        id: 'sample',
        name: '申请审批',
        open: false,
        pages: [
          { path: "index", title:'子菜单'  }
        ]
      }
    ]
  },
  //事件处理函数
  kindToggle: function (e) {
    var id = e.currentTarget.id, list = this.data.list;
    for (var i = 0, len = list.length; i < len; ++i) {
      if (list[i].id == id) {
        list[i].open = !list[i].open
      } else {
        list[i].open = false
      }
    }
    this.setData({
      list: list
    });
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.setData({
      userInfo: app.globalData.userInfo,
    })
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