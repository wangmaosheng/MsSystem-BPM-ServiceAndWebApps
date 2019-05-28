define(function (require, exports, module) {
    require("jquery-pager");
    require("jquery-extension");
    require('pace');
    var layer = require("layer");
    window.layer = layer;
    var page = {
        init: function () {
            page.initEvent();
        },
        initEvent: function() {
            $('.jquery-pager').pager({
                currentPageName: 'PageIndex',
                pageSizeName: 'PageSize'
            });
            $('#saveuser').on('click', function () {
                var ids = [];
                $('.ibox-content table tbody tr td input[type=checkbox]:checked').each(function () {
                    ids.push($(this).val());
                });
                if (ids.length <= 0) {
                    layer.msg('请至少选择一条数据！', { icon: 5, time: 1500 });
                    return;
                }
                var index = parent.layer.getFrameIndex(window.name);
                if (parent.getUsers) {
                    parent.getUsers(index, ids);
                }
            });
        }
    };
    page.init();
});