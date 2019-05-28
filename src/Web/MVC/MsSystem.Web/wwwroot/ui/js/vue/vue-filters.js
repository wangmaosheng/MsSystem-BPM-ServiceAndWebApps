define(function (require) {
    require('jquery-extension');
    require("vue");

    //时间戳转换成日期
    Vue.filter('unixToDateFormat', function (unixTime, isFull, timeZone) {
        if (typeof (timeZone) == 'number') {
            unixTime = parseInt(unixTime) + parseInt(timeZone) * 60 * 60;
        }
        var time = new Date(unixTime * 1000);
        var ymdhis = "";
        ymdhis += time.getUTCFullYear() + "-";
        ymdhis += (time.getUTCMonth() + 1) + "-";
        ymdhis += time.getUTCDate();
        if (isFull === true) {
            ymdhis += " " + time.getUTCHours() + ":";
            ymdhis += time.getUTCMinutes() + ":";
            ymdhis += time.getUTCSeconds();
        }
        return ymdhis;
    });

    //数字金额转为 大写金额
    Vue.filter('moneyUpperFormat', function (n) {
        var fraction = ['角', '分'];
        var digit = [
            '零', '壹', '贰', '叁', '肆',
            '伍', '陆', '柒', '捌', '玖'
        ];
        var unit = [
            ['元', '万', '亿'],
            ['', '拾', '佰', '仟']
        ];
        var head = n < 0 ? '欠' : '';
        n = Math.abs(n);
        var s = '';
        for (var i = 0; i < fraction.length; i++) {
            s += (digit[Math.floor(n * 10 * Math.pow(10, i)) % 10] + fraction[i]).replace(/零./, '');
        }
        s = s || '整';
        n = Math.floor(n);
        for (var i = 0; i < unit[0].length && n > 0; i++) {
            var p = '';
            for (var j = 0; j < unit[1].length && n > 0; j++) {
                p = digit[n % 10] + unit[1][j] + p;
                n = Math.floor(n / 10);
            }
            s = p.replace(/(零.)*零$/, '').replace(/^$/, '零') + unit[0][i] + s;
        }
        return head + s.replace(/(零.)*零元/, '元')
            .replace(/(零.)+/g, '零')
            .replace(/^整$/, '零元整');
    });

    /**
     * 金额类型转换显示 eg: 1,000.00
     * @param n:精确分位
     */
    Vue.filter('currencyFormat', function (value, n) {
        if (value == '' || value == undefined) {
            return null;
        }
        if (n == undefined) {
            n = 2;
        }
        value = parseFloat((value + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
        var l = value.split(".")[0].split("").reverse(),
            r = value.split(".")[1];
        var t = "";
        for (i = 0; i < l.length; i++) {
            t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
        }
        var res = t.split("").reverse().join("") + "." + r;
        return res;
    });

    //按钮类型转换
    //Vue.filter('buttonTypeFormat', function (value) {
    //    var res;
    //    switch (value) {
    //        case 1:
    //            res = "查看";
    //            break;
    //        case 2:
    //            res = "新增";
    //            break;
    //        case 3:
    //            res = "编辑";
    //            break;
    //        case 4:
    //            res = "删除";
    //            break;
    //        case 5:
    //            res = "打印";
    //            break;
    //        case 6:
    //            res = "审核";
    //            break;
    //        case 7:
    //            res = "作废";
    //            break;
    //        case 8:
    //            res = "结束";
    //            break;
    //        case 9:
    //            res = "扩展";
    //            break;
    //        default:
    //            res = value;
    //            break;
    //    }
    //    return res;
    //});



    //获取文件类型
    Vue.filter('fileTypeFormat', function (file) {
        try {
            var array = file.split('.');
            var leg = array.length;
            return array[leg - 1];
        } catch (e) {
            return null;
        }
    });


});