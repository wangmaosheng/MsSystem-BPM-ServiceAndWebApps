define(function (require, exports, module) {
    require("jquery-extension");
    require("jquery-pager");
    require('pace');
    var utils = require('utils');
    var page = {
        init: function () {
            page.initEvent();
        },
        initEvent: function () {
            $('.jquery-pager').pager({
                currentPageName: 'PageIndex',
                pageSizeName: 'PageSize'
            });
            $('#Log_Index_1').on('click', function () {
                $('form').submit();
            });
        }
    };
    page.init();
});