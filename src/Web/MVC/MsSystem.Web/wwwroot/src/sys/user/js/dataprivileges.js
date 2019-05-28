define(function (require, exports, module) {
    require("ztree");
    require('jquery-extension');
    require("vue");
    require("vue-filters");
    require("axios");
    require("slimscroll");
    require("toastr");
    require("pace");
    var layer = require("layer");
    var utils = require('utils');
    var vm = new Vue({
        el: '#msapp',
        data: {
            'UserId': null
        },
        created: function () {

        },
        mounted: function () {
            this.init();
        },
        methods: {
            init: function () {
                this.UserId = $.getUrlParam('UserId');
                utils.permission.initEvent();
                $('#selectSystem').change(function () {
                    var val = $(this).val();
                    if (val > 0) {
                        vm.getFormData(val);
                    }
                });
            },
            getFormData: function (systemid) {
                var vthis = this;
                axios.get('/Sys/User/GetDataPrivileges?UserId=' + vthis.UserId + '&SystemId=' + systemid).then(function (response) {
                    if (!utils.permission.isAuth(response)) {
                        return;
                    }
                    vthis.initTree(response.data.ZTrees);
                }).catch(function (error) {
                    console.log(error);
                });
            },
            saveFormData: function () {
                var vthis = this;
                var param = {
                    "SysDept": vthis.SysDept
                };
                axios.post('/Sys/User/SaveDataPrivileges', param).then(function (response) {
                    utils.permission.authResponse(response);
                });
            },
            initTree: function (json) {
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
                var deptsTree = $.fn.zTree.init($("#deptsTree"), setting, json);
                deptsTree.expandAll(true);
                $('#depts').show();
                $('#savedepts').click(function () {
                    var deptsTree = $.fn.zTree.getZTreeObj("deptsTree");
                    var nodes = deptsTree.getCheckedNodes(true);
                    var array = [];
                    for (var i = 0; i < nodes.length; i++) {
                        array.push(nodes[i].id);
                    }
                    debugger;
                    var param = {
                        "UserId": parseInt(vm.UserId),
                        'SystemId': $('#selectSystem').val(),
                        'Depts': array
                    };
                    axios.post('/Sys/User/SaveDataPrivileges', param).then(function (response) {
                        if (response.data) {
                            layer.msg('保存成功！', { icon: 1, time: 1500 }, function () {
                                layer.closeAll();
                            });
                        } else {
                            layer.msg('操作失败！请联系管理员', { icon: 2, time: 1000 });
                        }
                    });
                });
            }
        }
    });
});