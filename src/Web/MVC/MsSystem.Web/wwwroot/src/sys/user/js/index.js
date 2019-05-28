define(function (require, exports, module) {
    require("ztree");
    require("jquery-pager");
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
            $('.jquery-pager').pager({
                currentPageName: 'PageIndex',
                pageSizeName: 'PageSize'
            });
            page.initEvent();
            page.initTree();
        },
        initTree: function() {
            var rolesTree;
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
            $('.ibox-content table a[name=showrole]').click(function () {
                var _this = $(this);
                var userid = _this.data('userid');
                $('input[name=huserid]').val(userid);
                axios.get('/Sys/User/RoleBox?userid=' + userid).then(function (response) {
                    var json = response.data;
                    rolesTree = $.fn.zTree.init($("#rolesTree"), setting, json);
                    rolesTree.expandAll(true);
                });
                utils.open({
                    type: 1,
                    title: '分配角色',
                    maxmin: false,
                    area: ['300px', '500px'],
                    content: $('#roles')
                });
            });

            $('#saveroles').click(function () {
                var rolesTree = $.fn.zTree.getZTreeObj("rolesTree");
                var nodes = rolesTree.getCheckedNodes(true);
                var array = [];
                for (var i = 0; i < nodes.length; i++) {
                    if (nodes[i].level == 1) {
                        array.push(nodes[i].id);
                    }
                }
                var userid = $('input[name=huserid]').val();
                var param = {
                    "UserId": parseInt(userid),
                    'RoleIds': array
                };
                axios.post('/Sys/User/RoleBoxSave', param).then(function (response) {
                    utils.permission.authCallback(response, function () {
                        layer.msg('保存成功！', { icon: 1, time: 1500 }, function () {
                            layer.closeAll();
                        });
                    });
                });
            });
        },
        initEvent: function () {
            $('#User_Index_1').on('click', function () {
                $('form').submit();
            });
            $('#User_Index_2').on('click', function () {
                utils.open({
                    title: '新增用户',
                    content: '/Sys/User/Show',
                    area: ['800px', '450px']
                });
            });
            $('#User_Index_3').on('click', function () {
                var arrid = $('.ibox-content table tbody tr td input[type=checkbox]:checked');
                if (arrid.length === 1) {
                    utils.open({
                        title: '编辑用户',
                        content: '/Sys/User/Show?id=' + arrid.val(),
                        area: ['800px', '450px'],
                    });
                } else {
                    layer.msg('请选择一条数据！', { icon: 2, time: 1500 });
                }
            });
            $('#User_Index_4').on('click', function() {
                var ids = [];
                $('.ibox-content table tbody tr td input[type=checkbox]:checked').each(function () {
                    ids.push($(this).val());
                });
                if (ids.length <= 0) {
                    layer.msg('请至少选择一条数据！', { icon: 5, time: 1500 });
                    return;
                }
                layer.confirm('确认要删除？', { icon: 3, title: '提示' }, function (index) {
                    axios.post('/Sys/User/Delete', ids).then(function (response) {
                        utils.permission.authResponse(response);
                    });
                    window.location.reload(true);
                });
            });
            $('.ibox-content table tbody tr').dblclick(function () {
                var id = $(this).find('td input[type=checkbox]').val();
                utils.open({
                    title: '编辑用户',
                    content: '/Sys/User/Show?id=' + id,
                    area: ['800px', '450px']
                });
            });
            $('a[name=showuserinfo]').click(function () {
                var id = $(this).attr('data-userid');;
                utils.open({
                    title: '编辑用户',
                    content: '/Sys/User/Show?id=' + id,
                    area: ['800px', '450px']
                });
            });
            $('a[name=showdataprivileges]').on('click', function() {
                var userid = $(this).attr('data-userid');
                utils.open({
                    title: '数据权限分配',
                    content: '/Sys/User/DataPrivileges?UserId=' + userid
                });
            });

            $('a[name=showdept]').on('click', function () {
                var userid = $(this).attr('data-userid');
                utils.open({
                    title: '部门分配',
                    content: '/Sys/User/Dept?userid=' + userid
                });
            });
        }
    };
    page.init();
});