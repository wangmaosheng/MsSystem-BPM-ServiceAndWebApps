const HTTP = require('../../../wxapi/http.js');
var wxCharts = require('../../../utils/wxcharts.js');
var app = getApp();
var lineChart = null;
var timer = null;
Page({
  data: {
    background_color: app.globalData.globalBGColor,
    application:1,
    tabs: [
      { title: '权限系统',type: 1 },
      { title: 'OA系统',type :2},
      { title: '工作流',type : 3},
      { title: '微信',type : 4 },
      { title: '调度' , type: 5 },
      { title: '认证中间件',type : 0},
    ],
    isFirst:true
  },
  onClick: function (e) {
    if(this.isFirst){
      return;
    }
    this.setData({
      application: e.detail.key,
      isFirst:false
    });
    this.updateData(e);
  },
  touchHandler: function (e) {
    lineChart.showToolTip(e, {
      // background: '#7cb5ec', 
      format: function (item, category) {
        if(item.data==0){
          return category + ' ' + '心跳检查失败';
        }else{
          return category + ' ' + '心跳检查正常';
        }
      }
    });
  },
  updateData: function (e) {
    var that=this;
    if(e){
      that.stopUpdate();
    }
    timer = setTimeout(function () {
      that.updateData();
    }, 6000);
    that.loadData(function(response){
      var simulationData = response;
      var series = [{
        name: '心跳时间',
        data: simulationData.YData,
        format: function (val, name) {
          return val.toFixed(2);
        }
      }];
      lineChart.updateData({
        categories: simulationData.XData,
        series: series
      });
    });
  },
  onLoad(e){
    this.createChart();
  },
  loadData:function(cb){
    var that=this;
    HTTP.sys.getHeartBeat({ recentMinutes: 4, application: that.data.application }, function (response) {
      cb(response);
      console.info("数据获取成功")
    }, function (error) {
      console.info(error);
    })
  },
  createChart: function () {
    var windowWidth = 320;
    try {
      var res = wx.getSystemInfoSync();
      windowWidth = res.windowWidth;
    } catch (e) {
      console.error('getSystemInfoSync failed!');
    }
    this.loadData(function (response) {
      var simulationData = response;
      lineChart = new wxCharts({
        canvasId: 'lineCanvas',
        type: 'line',
        categories: simulationData.XData,
        animation: false,
        series: [{
          name: '心跳时间',
          data: simulationData.YData,
          format: function (val, name) {
            return val.toFixed(2);
          }
        }],
        xAxis: {
          disableGrid: true
        },
        yAxis: {
          title: '心跳值',
          format: function (val) {
            return val.toFixed(2);
          },
          min: 0
        },
        width: windowWidth,
        height: 200,
        dataLabel: false,
        dataPointShape: true,
        extra: {
          lineStyle: 'curve'
        }
      });
    });
  },
  stopUpdate:function(){
    clearTimeout(timer);
  }
});