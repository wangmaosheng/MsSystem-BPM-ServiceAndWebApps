"use strict";
window.refresh = function (index) {
    layer.close(index);
    location.reload();
};
var isrefresh = false;
function refresh() {
    if (isrefresh) {
        window.location.reload(true);
    }
}
var utils;
if (!utils) utils = {};

//权限实体
const permission = {
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
                    location.reload();
                }
            });
            return true;
        } else {
            layer.msg('操作失败！请联系管理员', { icon: 2, time: 1500 });
            return false;
        }
    },
    //权限操作编辑页响应判断
    authShowResponse: function (response) {
        if (response.data == 2) {
            layer.msg('未授权！不可访问', { icon: 2, time: 1500 });
            return false;
        } else if (response.data) {
            layer.msg('操作成功！', { icon: 1, time: 1500 }, function () {
                if (parent.location) {
                    parent.location.reload();
                }
                var index = parent.layer.getFrameIndex(window.name);
                if (index) {
                    parent.layer.close(index);
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
    initEvent: function () {
        $('#formReturn').on('click', function () {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index);
        });
    }
};

const page = {
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
        //全选
        $('#ms_checkall').on('ifChecked', function (event) {
            //$('input').iCheck('check');
            $('.ibox-content table tbody td input[type=checkbox]').iCheck('check');
        });
        //反选
        $('#ms_checkall').on('ifUnchecked', function (event) {
            $('.ibox-content table tbody td input[type=checkbox]').iCheck('uncheck');
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

        //菜单区展开与缩放
        $('.conditionbtn').click(function () {
            var ibox = $(this).parents('.ibox-title');
            if (ibox.hasClass('extend')) {
                ibox.removeClass('extend');
            } else {
                ibox.addClass('extend');
            }
        });

    }
};
//键盘事件
const keyboard = {
    init: function () {
        keyboard.initEvent();
    },
    initEvent: function () {
        $(document).keyup(function (event) {
            switch (event.keyCode) {
                //ESC 关闭弹框
                case 27:
                    keyboard.closeDialog();
                    break;
            }
        });
        $('#formReturn').on('click', function () {
            keyboard.closeDialog();
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

const open = function (options) {
    var defaults = {
        type: 2,
        title: '',
        area: ['800px', '600px'],
        content: '',
        maxmin: true,
        success: function (layero, index) {
            layero.find('iframe').focus();
        }
    };
    if (options.dataUrl) {//该操作为了兼容之前写的utils.menus.open方法中的参数
        defaults.title = options.menuName;
        defaults.content = options.dataUrl;
    }
    var resoptions = $.extend(defaults, options);
    layer.open(resoptions);
};

/**
 * 菜单操作 页面内打开
 * */
const menu = {
    open: function (options) {
        var defaults = {
            dataUrl: '',
            dataIndex: parent.$('a.J_menuItem:last').attr('data-index'),
            menuName: ''
        };
        var resoptions = $.extend(defaults, options);
        menu.menuItem(resoptions);
    },
    //计算元素集合的总宽度
    calSumWidth: function (elements) {
        var width = 0;
        $(elements).each(function () {
            width += $(this).outerWidth(true);
        });
        return width;
    },
    //滚动到指定选项卡
    scrollToTab: function (element) {
        var marginLeftVal = menu.calSumWidth($(element).prevAll()), marginRightVal = menu.calSumWidth($(element).nextAll());
        // 可视区域非tab宽度
        var tabOuterWidth = menu.calSumWidth(parent.$(".content-tabs").children().not(".J_menuTabs"));
        //可视区域tab宽度
        var visibleWidth = parent.$(".content-tabs").outerWidth(true) - tabOuterWidth;
        //实际滚动宽度
        var scrollVal = 0;
        if (parent.$(".page-tabs-content").outerWidth() < visibleWidth) {
            scrollVal = 0;
        } else if (marginRightVal <= (visibleWidth - $(element).outerWidth(true) - $(element).next().outerWidth(true))) {
            if ((visibleWidth - $(element).next().outerWidth(true)) > marginRightVal) {
                scrollVal = marginLeftVal;
                var tabElement = element;
                while ((scrollVal - $(tabElement).outerWidth()) > (parent.$(".page-tabs-content").outerWidth() - visibleWidth)) {
                    scrollVal -= $(tabElement).prev().outerWidth();
                    tabElement = $(tabElement).prev();
                }
            }
        } else if (marginLeftVal > (visibleWidth - $(element).outerWidth(true) - $(element).prev().outerWidth(true))) {
            scrollVal = marginLeftVal - $(element).prev().outerWidth(true);
        }
        parent.$('.page-tabs-content').animate({
            marginLeft: 0 - scrollVal + 'px'
        }, "fast");
    },
    menuItem: function (options) {
        var dataUrl = options.dataUrl,
            dataIndex = options.dataIndex,
            menuName = $.trim(options.menuName),
            flag = true;
        if (dataUrl == undefined || $.trim(dataUrl).length == 0) return false;

        // 选项卡菜单已存在
        parent.$('.J_menuTab').each(function () {
            if ($(this).data('id') == dataUrl) {
                if (!$(this).hasClass('active')) {
                    $(this).addClass('active').siblings('.J_menuTab').removeClass('active');
                    menu.scrollToTab(this);
                    // 显示tab对应的内容区
                    parent.$('.J_mainContent .J_iframe').each(function () {
                        if ($(this).data('id') == dataUrl) {
                            $(this).show().siblings('.J_iframe').hide();
                            return false;
                        }
                    });
                }
                flag = false;
                return false;
            }
        });

        // 选项卡菜单不存在
        if (flag) {
            var str = '<a href="javascript:;" class="active J_menuTab" data-id="' + dataUrl + '">' + menuName + ' <i title="关闭窗口" class="remove"></i></a>';
            parent.$('.J_menuTab').removeClass('active');

            // 添加选项卡对应的iframe
            var str1 = '<iframe class="J_iframe" name="iframe' + dataIndex + '" width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id="' + dataUrl + '" seamless></iframe>';
            parent.$('.J_mainContent').find('iframe.J_iframe').hide();
            parent.$('.J_mainContent').append(str1);

            // 添加选项卡
            parent.$('.J_menuTabs .page-tabs-content').append(str);
            menu.scrollToTab(parent.$('.J_menuTab.active'));
        }
        return false;
    },
    closeCurrentTab: function () {
        menu.refreshPage();
        var _this = $(window.parent.document).find('a.active.J_menuTab i');
        var closeTabId = _this.parents('.J_menuTab').data('id');
        var currentWidth = _this.parents('.J_menuTab').width();
        // 当前元素处于活动状态
        if (_this.parents('.J_menuTab').hasClass('active')) {
            // 当前元素后面有同辈元素，使后面的一个元素处于活动状态
            if (_this.parents('.J_menuTab').next('.J_menuTab').size()) {
                var activeId = _this.parents('.J_menuTab').next('.J_menuTab:eq(0)').data('id');
                _this.parents('.J_menuTab').next('.J_menuTab:eq(0)').addClass('active');

                $(window.parent.document).find('.J_mainContent .J_iframe').each(function () {
                    if ($(this).data('id') == activeId) {
                        $(this).show().siblings('.J_iframe').hide();
                        return false;
                    }
                });

                var marginLeftVal = parseInt($('.page-tabs-content').css('margin-left'));
                if (marginLeftVal < 0) {
                    $(window.parent.document).find$('.page-tabs-content').animate({
                        marginLeft: (marginLeftVal + currentWidth) + 'px'
                    }, "fast");
                }

                //  移除当前选项卡
                _this.parents('.J_menuTab').remove();

                // 移除tab对应的内容区
                $(window.parent.document).find('.J_mainContent .J_iframe').each(function () {
                    if ($(this).data('id') == closeTabId) {
                        $(this).remove();
                        return false;
                    }
                });
            }

            // 当前元素后面没有同辈元素，使当前元素的上一个元素处于活动状态
            if (_this.parents('.J_menuTab').prev('.J_menuTab').size()) {
                var activeId = _this.parents('.J_menuTab').prev('.J_menuTab:last').data('id');
                _this.parents('.J_menuTab').prev('.J_menuTab:last').addClass('active');
                $(window.parent.document).find('.J_mainContent .J_iframe').each(function () {
                    if ($(this).data('id') == activeId) {
                        $(this).show().siblings('.J_iframe').hide();
                        return false;
                    }
                });

                //  移除当前选项卡
                _this.parents('.J_menuTab').remove();

                // 移除tab对应的内容区
                $(window.parent.document).find('.J_mainContent .J_iframe').each(function () {
                    if ($(this).data('id') == closeTabId) {
                        $(this).remove();
                        return false;
                    }
                });
            }
        }
        // 当前元素不处于活动状态
        else {
            //  移除当前选项卡
            _this.parents('.J_menuTab').remove();

            // 移除相应tab对应的内容区
            $(window.parent.document).find('.J_mainContent .J_iframe').each(function () {
                if ($(this).data('id') == closeTabId) {
                    $(this).remove();
                    return false;
                }
            });
            menu.scrollToTab($('.J_menuTab.active'));
        }
        return false;
    },
    //刷新页面
    refreshPage: function () {
        var pretabaid = $(window.parent.document).find('a.active.J_menuTab i').parent().prev().attr('data-id');
        if (pretabaid) {
            var preiframe = $(window.parent.document).find('.J_mainContent .J_iframe[data-id="' + pretabaid + '"]')[0];
            var currentWin = preiframe.contentWindow;
            if (currentWin) {
                if (currentWin.refresh) {
                    currentWin.refresh();
                }
            }
        }
    }
};


$(function () {
    utils.permission = permission;
    utils.page = page;
    utils.keyboard = keyboard;
    utils.open = open;
    utils.menu = menu;

    utils.page.init();
    utils.keyboard.init();
});