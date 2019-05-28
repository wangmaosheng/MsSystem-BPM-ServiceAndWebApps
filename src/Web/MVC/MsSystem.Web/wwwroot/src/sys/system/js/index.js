define(function (require, exports, module) {
    require("jquery-extension");
    require("jquery-pager");
    require("bootstrap");
    require("slimscroll");
    require("toastr");
    require("axios");
    require('pace');
    var layer = require("layer");
    var utils = require("utils");
    window.layer = layer;
    window.refresh = function(index) {
        layer.close(index);
        location.reload();
    };
    var page = {
        init: function () {
            utils.permission.init('Index', 'System');
            $('.jquery-pager').pager({
                currentPageName: 'PageIndex',
                pageSizeName: 'PageSize'
            });
            $('#System_Index_2').on('click', function() {
                utils.open({
                    title: '新增系统',
                    content: '/Sys/System/Show'
                });
            });
            $('#System_Index_3').on('click', function() {
                var arrid = $('.ibox-content table tbody tr td input[type=checkbox]:checked');
                if (arrid.length == 1) {
                    utils.open({
                        title: '编辑系统',
                        content: '/Sys/System/Show?id=' + arrid.val()
                    });
                } else {
                    layer.msg('请选择一条数据！', { icon: 2, time: 1500 });
                }
            });
            $('#System_Index_4').on('click', function() {
                var ids = [];
                $('.ibox-content table tbody tr td input[type=checkbox]:checked').each(function () {
                    ids.push($(this).val());
                });
                if (ids.length <= 0) {
                    layer.msg('请至少选择一条数据！', { icon: 5, time: 1500 });
                    return;
                }
                layer.confirm('确认要删除？', { icon: 3, title: '提示' }, function (index) {
                    axios.post('/Sys/System/Delete', ids).then(function (response) {
                        utils.permission.authResponse(response);
                    });
                    window.location.reload(true);
                });
            });
            $('.ibox-content table tbody tr').dblclick(function () {
                var id = $(this).find('td input[type=checkbox]').val();
                utils.open({
                    title: '编辑系统',
                    content: '/Sys/System/Show?id=' + id
                });
            });
            $('a[name=showsysteminfo]').click(function () {
                var id = $(this).attr('data-systemid');
                utils.open({
                    title: '编辑系统',
                    content: '/Sys/System/Show?id=' + id
                });
            });
        }
    };
    page.init();
});