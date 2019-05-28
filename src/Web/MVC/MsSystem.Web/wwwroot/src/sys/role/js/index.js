define(function (require, exports, module) {
    require("jquery-pager");
    require("ztree");
    require("jquery-extension");
    require("bootstrap");
    require("slimscroll");
    require("toastr");
    require("axios");
    require('pace');
    var layer = require("layer");
    var utils = require('utils');
    window.layer = layer;
    window.refresh = function(index) {
        layer.close(index);
        location.reload();
    };
    var page = {
        init: function () {
            var resourcesTree;
            var expandDepth = 2;
            var setting = {
                check: {
                    enable: true
                },
                data: {
                    simpleData: {
                        enable: true
                    }
                }
            };
            $('.jquery-pager').pager({
                currentPageName: 'PageIndex',
                pageSizeName: 'PageSize'
            });
            $('#Role_Index_1').on('click', function () {
                $('form').submit();
            });
            $('#Role_Index_2').on('click', function () {
                var systemid = $('#SystemId').val();
                if (systemid > 0) {
                    utils.open({
                        title: '新增角色',
                        content: '/Sys/Role/Show?systemid=' + systemid
                    });
                } else {
                    layer.msg('请选择所属系统！', { icon: 5, time: 1500 });
                }
            });
            $('#Role_Index_3').on('click', function() {
                var arrid = $('.ibox-content table tbody tr td input[type=checkbox]:checked');
                if (arrid.length == 1) {
                    utils.open({
                        title: '编辑角色',
                        content: '/Sys/Role/Show?id=' + arrid.val()
                    });
                } else {
                    layer.msg('请选择一条数据！', { icon: 2, time: 1500 });
                }
            });
            $('#Role_Index_4').on('click', function() {
                var ids = [];
                $('.ibox-content table tbody tr td input[type=checkbox]:checked').each(function () {
                    ids.push($(this).val());
                });
                if (ids.length <= 0) {
                    layer.msg('请至少选择一条数据！', { icon: 5, time: 1500 });
                    return;
                }
                layer.confirm('确认要删除？', { icon: 3, title: '提示' }, function (index) {
                    axios.post('/Sys/Role/Delete', ids).then(function (response) {
                        utils.permission.authResponse(response);
                    });
                    location.reload();
                });
            });
            $('.ibox-content table tbody tr').dblclick(function () {
                var id = $(this).find('td input[type=checkbox]').val();
                utils.open({
                    title: '编辑角色',
                    content: '/Sys/Role/Show?id=' + id
                });
            });
            $('a[name=showroleinfo]').click(function () {
                var id = $(this).attr('data-roleid');
                utils.open({
                    title: '编辑角色',
                    content: '/Sys/Role/Show?id=' + id
                });
            });
            $('.ibox-content table a[name=showres]').click(function () {
                var _this = $(this);
                var roleid = _this.data('roleid');
                $('input[name=hroleid]').val(roleid);
                axios.get('/Sys/Role/Box?roleid=' + roleid).then(function (response) {
                    var json = response.data;
                    resourcesTree = $.fn.zTree.init($("#resourcesTree"), setting, json);
                    if (expandDepth !== 0) {
                        resourcesTree.expandAll(false);
                        var nodes = resourcesTree.getNodesByFilter(function (node) {
                            return (node.level < expandDepth);
                        });
                        if (nodes.length > 0) {
                            for (var i = 0; i < nodes.length; i++) {
                                resourcesTree.expandNode(nodes[i], true, false);
                            }
                        }
                    } else {
                        resourcesTree.expandAll(true);
                    }
                });
                layer.open({
                    type: 1,
                    title: '分配资源',
                    maxmin: false,
                    area: ['300px', '500px'],
                    content: $('#resource')
                });
            });
            $('.ibox-content table a[name=showuser]').click(function () {
                var roleid = $(this).attr('data-roleid');
                utils.open({
                    title: '分配用户',
                    content: '/Sys/Role/Users?roleid=' + roleid
                });
            });
            $('#saveroleres').click(function () {
                var resourcesTree = $.fn.zTree.getZTreeObj("resourcesTree");
                var nodes = resourcesTree.getCheckedNodes(true);
                var array = [];
                $.each(nodes, function (i, item) {
                    array.push(item.id);
                });
                var roleid = $('input[name=hroleid]').val();
                var param = {
                    "RoleId": parseInt(roleid),
                    'ResourceIds': array
                };
                axios.post('/Sys/Role/BoxSave', param).then(function (response) {
                    utils.permission.authCallback(response, function () {
                        layer.msg('保存成功！', { icon: 1, time: 1500 }, function () {
                            layer.closeAll();
                        });
                    });
                });
            });
        }
    };
    page.init();
});