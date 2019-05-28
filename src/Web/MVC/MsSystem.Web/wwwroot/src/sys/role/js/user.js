define(function (require, exports, module) {
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
            page.initEvent();
        },
        initEvent: function() {
            $('.jquery-pager').pager({
                currentPageName: 'PageIndex',
                pageSizeName: 'PageSize'
            });
            $('#addUser').on('click', function () {
                layer.open({
                    type: 2,
                    title: '选择用户',
                    maxmin: false,
                    area: ['700px', '500px'],
                    content: '/Sys/Plugin/User',
                    success: function (layero, index) {
                        layero.find('iframe').focus();
                    }
                });
            });
            $('#delUser').on('click', function () {
                var roleid = $('#roleid').val();
                var ids = [];
                $('.ibox-content table tbody tr td input[type=checkbox]:checked').each(function () {
                    ids.push($(this).val());
                });
                if (ids.length <= 0) {
                    layer.msg('请至少选择一条数据！', { icon: 5, time: 1500 });
                    return;
                }
                var param = {
                    RoleId: roleid,
                    UserIds: ids
                };
                layer.confirm('确认要删除？', { icon: 3, title: '提示' }, function (index) {
                    axios.post('/Sys/Role/DeleteUser', param).then(function (response) {
                        utils.permission.authResponse(response);
                    });
                    window.location.reload(true);
                });
            });
        },
        getUsers: function(index,ids) {
            if (ids.length > 0) {
                var param = {
                    RoleId: $('#roleid').val(),
                    UserIds: ids
                };
                axios.post('/Sys/Role/AddUser', param).then(function(response) {
                    if (response.data == 2) {
                        layer.msg('未授权！不可访问', { icon: 2, time: 1500 });
                    } else if (response.data) {
                        layer.msg('操作成功！', { icon: 1, time: 1500 }, function () {
                            location.reload();
                        });
                    } else {
                        layer.msg('操作失败！请联系管理员', { icon: 2, time: 1500 });
                    }
                });
            }
        }
    };
    page.init();
    window.getUsers = function (index, ids) {
        page.getUsers(index, ids);
    };
});