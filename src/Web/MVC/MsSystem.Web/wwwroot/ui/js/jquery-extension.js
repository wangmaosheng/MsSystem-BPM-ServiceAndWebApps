(function ($) {
    //类自定义扩展
    $.extend({
        //获取URL参数值
        getUrlParam: function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return decodeURI(r[2]);
            return null;
        },
        /**
         * 当前时间戳
         * @return <int>    unix时间戳(秒) 
         */
        curTime: function () {
            return Date.parse(new Date()) / 1000;
        },
        /**       
         * 日期 转换为 Unix时间戳
         * @param <string> 日期格式
         * @return <int>    unix时间戳(秒)
         */
        dateToUnix: function (string) {
            var f = string.split(' ', 2);
            var d = (f[0] ? f[0] : '').split('-', 3);
            var t = (f[1] ? f[1] : '').split(':', 3);
            return (new Date(
                parseInt(d[0], 10) || null,
                (parseInt(d[1], 10) || 1) - 1,
                parseInt(d[2], 10) || null,
                parseInt(t[0], 10) || null,
                parseInt(t[1], 10) || null,
                parseInt(t[2], 10) || null
            )).getTime() / 1000;
        },
        /**       
         * 时间戳转换日期       
         * @param <int> unixTime  待时间戳(秒)       
         * @param <bool> isFull  返回完整时间(Y-m-d 或者 Y-m-d H:i:s)
         * @param <int> timeZone  时区       
         */
        unixToDate: function (unixTime, isFull, timeZone) {
            if (typeof (timeZone) == 'number') {
                unixTime = parseInt(unixTime) + parseInt(timeZone) * 60 * 60;
            }
            var time = new Date(unixTime * 1000);
            var ymdhis = "";
            ymdhis += time.getUTCFullYear() + "-";
            ymdhis += (time.getUTCMonth() + 1) + "-";
            ymdhis += time.getUTCDate();
            if (isFull === true) {
                ymdhis += " " + time.getHours() + ":";
                ymdhis += time.getMinutes() + ":";
                ymdhis += time.getSeconds();
            }
            return ymdhis;
        },
        /**
         * 计算时间间隔
         * @param {} 开始时间(字符串)
         * @param {} 结束时间
         * @returns {} 
         */
        toTimeSpan: function (datestr, date2) {
            var res = datestr.replace(/-/g, "/");
            var date1 = new Date(res);
            if (date2 == undefined) {
                date2 = new Date();
            }
            var date3 = date2.getTime() - date1.getTime();
            //计算出相差天数  
            var days = Math.floor(date3 / (24 * 3600 * 1000));
            //计算出小时数  
            var leave1 = date3 % (24 * 3600 * 1000);   //计算天数后剩余的毫秒数  
            var hours = Math.floor(leave1 / (3600 * 1000));
            //计算相差分钟数  
            var leave2 = leave1 % (3600 * 1000);        //计算小时数后剩余的毫秒数  
            var minutes = Math.floor(leave2 / (60 * 1000));
            //计算相差秒数  
            var leave3 = leave2 % (60 * 1000);      //计算分钟数后剩余的毫秒数  
            var seconds = Math.round(leave3 / 1000);
            var str = '';
            if (days > 0) {
                str += days + '天';
            }
            if (hours > 0) {
                str += hours + '时';
            }
            if (minutes > 0) {
                str += minutes + '分';
            }
            if (seconds > 0) {
                str += seconds + '秒';
            }
            return str;
        },
        /**
         * 随机生成GUID
         * @returns {} 
         */
        getRandomGuid: function() {
            return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
                return v.toString(16);
            });
        },
        /**
         * 获取表单中的数据
         * @param form 表单
         */
        formCollection: function (form) {
            var formel = $(form).find('[name]');
            var result = {};
            formel.each(function (key, val) {
                var cur = $(this);
                var names = cur.attr("name");
                var type = $(this).attr("type");
                if (type == "checkbox") {
                    if ($(this).prop("checked")) {
                        result[names] = true;
                    }
                } else if (type == "radio") {
                    if ($(this).prop("checked")) {
                        result[names] = $(this).val();
                    }
                } else {
                    result[names] = cur.val();
                }
            });
            return result;
        },
        /**
         * 表单设置不可用
         * @param {any} form
         */
        setFormDisabled: function (form) {
            if (form == undefined) {
                form = document.forms[0];
            }
            var legs = form.length;
            for (var i = legs - 1; i >= 0; i--) {
                $(form[i]).prop("disabled", true);
            }
        },
        getCurrentTime: function () {
            var date = new Date();
            var year = date.getFullYear();
            var month = date.getMonth() + 1;
            var day = date.getDate();
            var hour = date.getHours();
            var minute = date.getMinutes();
            var second = date.getSeconds();
            return year + "-" + month + "-" + day + " " + hour + ':' + minute + ':' + second;
        }
    });

    //Jquery方法自定义扩展
    $.fn.extend({

    });
})(jQuery);

//JS方法扩展
//时间格式转换扩展
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