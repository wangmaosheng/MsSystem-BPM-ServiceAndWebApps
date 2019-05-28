define(function (require, exports, module) {
    require("axios");
    var layer = require("layer");
    require('icheck');
    //权限实体
    var permission = {
        //判断是否授权 ==> 请求数据的时候
        isAuth: function (response) {
            if (response) {
                if (response.data == 2) {
                    layer.msg('未授权！不可访问', { icon: 2, time: 1500 });
                    return false;
                } else {
                    return true;
                }
            } else {
                return false;
            }
        },
        authCallback: function (response, callback) {
            if (response.data == 2) {
                layer.msg('未授权！不可访问', { icon: 2, time: 1500 });
                return false;
            } else if (response.data) {
                if (callback) {
                    callback();
                }
                return true;
            } else {
                layer.msg('操作失败！请联系管理员', { icon: 2, time: 1500 });
                return false;
            }
        },
        //权限操作响应判断
        authResponse: function (response) {
            if (response.data == 2) {
                layer.msg('未授权！不可访问', { icon: 2, time: 1500 });
                return false;
            } else if (response.data) {
                layer.msg('操作成功！', { icon: 1, time: 1500 }, function () {
                    if (parent.refresh) {
                        var index = parent.layer.getFrameIndex(window.name);
                        parent.refresh(index);
                    } else {
                        location.reload(true);
                    }
                });
                return true;
            } else {
                layer.msg('操作失败！请联系管理员', { icon: 2, time: 1500 });
                return false;
            }
        },
        init: function (action, controller, areas) {

        },
        initEvent: function() {
            $('#formReturn').on('click', function() {
                var index = parent.layer.getFrameIndex(window.name);
                parent.layer.close(index);
            });
        }
    };

    var page = {
        init: function () {
            page.initEvent();
        },
        initEvent: function () {
            //修改全局checkbox/radio
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue'
            });
            $('#ms_refresh').on('click', function () {
                location.reload(true);
            });
            var layerindex;
            //定义一个请求拦截器
            axios.interceptors.request.use(function (config) {
                layerindex = layer.load();
                return config;
            });
            //定义一个响应拦截器
            axios.interceptors.response.use(function (config) {
                layer.close(layerindex);
                return config;
            });
        }
    };
    //键盘事件
    var keyboard = {
        init: function () {
            keyboard.initEvent();
        },
        initEvent: function () {
            $(document).keyup(function (event) {
                switch (event.keyCode) {
                    //ESC 关闭弹框
                    case 27:
                    case 96:
                        keyboard.closeDialog();
                        break;
                }
            });
        },
        closeDialog: function () {
            try {
                var index = parent.layer.getFrameIndex(window.name);
                if (index) {
                    parent.layer.close(index);
                }
            } catch (e) {
                console.info(e);
            } 
        }
    };


    var open = function (options) {
        var defaults = {
            type: 2,
            title: '',
            area: ['800px', '600px'],
            content: '',
            maxmin: false,
            success: function (layero, index) {
                layero.find('iframe').focus();
            }
        };
        var resoptions = $.extend(defaults, options);
        layer.open(resoptions);
    };

    $(function() {
        page.init();

        //全选
        $('#ms_checkall').on('ifChecked', function (event) {
            //$('input').iCheck('check');
            $('.ibox-content table tbody td input[type=checkbox]').iCheck('check');
        });
        //反选
        $('#ms_checkall').on('ifUnchecked', function (event) {
            $('.ibox-content table tbody td input[type=checkbox]').iCheck('uncheck');
        });

    });

    keyboard.init();

    exports.permission = permission;
    exports.page = page;
    exports.open = open;

});