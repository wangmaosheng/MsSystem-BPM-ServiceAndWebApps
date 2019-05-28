define(function (require, exports, module) {
    require("bootstrap");
    require("slimscroll");
    require("toastr");
    require("cookie");
    require('treegrid');
    require('pace');
    require("axios");
    var layer = require("layer");
    var utils = require('utils');
    window.layer = layer;
    window.refresh = function(index) {
        layer.close(index);
        location.reload();
    };
    var page = {
        init: function () { 

            $('.tree').treegrid({
                initialState: 'collapse',
                saveState: true,
                treeColumn: 1
            });

            $(".full-height-scroll").slimScroll({ height: "100%" });
            $('#Resource_Index_3').click(function () {
                var ids = [];
                $('.table tbody tr td input[type=checkbox]').each(function () {
                    var $this = $(this);
                    if ($this.is(":checked")) {
                        ids.push($this.val());
                    }
                });
                if (ids.length != 1) {
                    layer.msg('请至少选择一条数据！', { icon: 5, time: 1500 });
                    return;
                }
                var systemid = $('#changesystem').val();
                utils.open({
                    type: 2,
                    title: '编辑菜单',
                    area: ['800px', '660px'],
                    content: '/Sys/Resource/Show?id=' + ids[0] + '&systemid=' + systemid,
                });
            });

            $('#Resource_Index_2').click(function () {
                var checkbox = $('.ibox-content table tbody tr td input[type=checkbox]:checked');
                var pid = checkbox.attr('data-pid');
                var cid = checkbox.val();
                var systemid = $('#changesystem').val();
                utils.open({
                    type: 2,
                    title: '新增菜单',
                    area: ['800px', '660px'],
                    content: '/Sys/Resource/Show?pid=' + pid + '&cid=' + cid + '&systemid=' + systemid,
                });
            });

            $('.ibox-content table tbody tr').dblclick(function () {
                var id = $(this).find('td input[type=checkbox]').val();
                var systemid = $('#changesystem').val();
                utils.open({
                    type: 2,
                    title: '编辑菜单',
                    maxmin: false,
                    area: ['800px', '660px'],
                    content: '/Sys/Resource/Show?id=' + id + '&systemid=' + systemid,
                });
            });

            $('#Resource_Index_4').click(function () {
                var ids = [];
                $('.ibox-content table tbody tr td input[type=checkbox]:checked').each(function () {
                    ids.push($(this).val());
                });
                if (ids.length <= 0) {
                    layer.msg('请至少选择一条数据！', { icon: 5, time: 1500 });
                    return;
                }
                layer.confirm('确认要删除？', { icon: 3, title: '提示' }, function (index) {
                    axios.post('/Sys/Resource/Delete', ids).then(function (response) {
                        utils.permission.authResponse(response);
                    });
                    layer.close(index);
                });
            });
            $('#Resource_Index_1').click(function () {
                location.href = '/Sys/Resource/Index?systemid=' + $('#changesystem').val();
            });
        }
    };
    page.init();
});