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
                var objs = [];
                $('.ibox-content table tbody tr td input:checked').each(function () {
                    ids.push($(this).val());
                    var obj = {
                        'userid': $(this).val(),
                        'username': $(this).attr('data-username')
                    };
                    objs.push(obj);
                });
                if (ids.length <= 0) {
                    layer.msg('请至少选择一条数据！', { icon: 5, time: 1500 });
                    return;
                }
                var index = parent.layer.getFrameIndex(window.name);
                if (parent.getUsers) {
                    parent.getUsers(index, ids, objs);
                }
            });
        }
    };
    page.init();
});