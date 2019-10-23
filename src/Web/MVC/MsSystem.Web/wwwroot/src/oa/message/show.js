define(function (require, exports, module) {
    require('jquery-extension');
    require("vue");
    require("vue-filters");
    require("axios");
    require("toastr");
    require("validate");
    require("pace");
    var layer = require("layer");
    require("laydate");
    var utils = require('utils');

    Date.prototype.format = function (fmt) {
        var o = {
            "M+": this.getMonth() + 1,                 //月份
            "d+": this.getDate(),                    //日
            "h+": this.getHours(),                   //小时
            "m+": this.getMinutes(),                 //分
            "s+": this.getSeconds(),                 //秒
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度
            "S": this.getMilliseconds()             //毫秒
        };
        if (/(y+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(fmt)) {
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    };
    function toDateString(date, fmt) {
        var oldTime = (new Date(date)).getTime();
        var curTime = new Date(oldTime).format(fmt);
        return curTime;
    }
    var vm = new Vue({
        el: '#msapp',
        data: {
            "MessageShowDTO": {},
            "MessageType": msgtypeList,
            "UserType": usertypeList
        },
        mounted: function () {
            this.init();
        },
        methods: {
            init: function () {
                this.getFormData();
                utils.permission.initEvent();
                $('#formSave').on('click', function () {
                    vm.saveFormData();
                });
            },
            getFormData: function () {
                var vthis = this;
                vthis.Id = $.getUrlParam('id');
                axios.get('/OA/Message/Get?Id=' + vthis.Id).then(function (response) {
                    if (!utils.permission.isAuth(response)) {
                        return;
                    }
                    vthis.$set(vthis, 'MessageShowDTO', response.data);
                    vthis.MessageShowDTO.StartTime = toDateString(vthis.MessageShowDTO.StartTime, "yyyy-MM-dd hh:mm:ss");
                    vthis.MessageShowDTO.EndTime = toDateString(vthis.MessageShowDTO.EndTime, "yyyy-MM-dd hh:mm:ss");

                    var start = {
                        elem: '#StartTime',
                        type: 'datetime',
                        done: function (value, date, endDate) {
                            if (value == "") {
                                vthis.MessageShowDTO.StartTime = null;
                            }
                        }
                    };
                    var end = {
                        elem: '#EndTime',
                        type: 'datetime',
                        done: function (value, date, endDate) {
                            if (value == "") {
                                vthis.MessageShowDTO.EndTime = null;
                            }
                        }
                    };
                    laydate.render(start);
                    laydate.render(end);
                }).catch(function (error) {
                    console.log(error);
                });
            },
            saveFormData: function () {
                var isvalidate = $('#form-msg').valid();
                if (isvalidate === false) {
                    return;
                }
                var vthis = this;
                var url = parseInt(vthis.MessageShowDTO.Id) > 0 ? '/OA/Message/Update' : '/OA/Message/Add';
                axios.post(url, vthis.MessageShowDTO).then(function (response) {
                    utils.permission.authResponse(response);
                });
            }
        }
    });
});