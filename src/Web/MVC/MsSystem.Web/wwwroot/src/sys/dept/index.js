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
    window.refresh = function (index) {
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
            $('#Dept_Index_Edit').click(function () {
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
                utils.open({
                    title: '编辑部门',
                    content: '/Sys/Dept/Show?id=' + ids[0]
                });
            });

            $('#Dept_Index_2').click(function () {
                utils.open({
                    title: '新增部门',
                    content: '/Sys/Dept/Show'
                });
            });

            $('.ibox-content table tbody tr').dblclick(function () {
                var id = $(this).find('td input[type=checkbox]').val();
                utils.open({
                    title: '编辑部门',
                    content: '/Sys/Dept/Show?id=' + id
                });
            });

            $('#Dept_Index_4').click(function () {
                var ids = [];
                $('.ibox-content table tbody tr td input[type=checkbox]:checked').each(function () {
                    ids.push($(this).val());
                });
                if (ids.length <= 0) {
                    layer.msg('请至少选择一条数据！', { icon: 5, time: 1500 });
                    return;
                }
                layer.confirm('确认要删除？', { icon: 3, title: '提示' }, function (index) {
                    axios.post('/Sys/Dept/Delete', ids).then(function (response) {
                        utils.permission.authResponse(response);
                    });
                    layer.close(index);
                });
            });

        }
    };
    page.init();
});