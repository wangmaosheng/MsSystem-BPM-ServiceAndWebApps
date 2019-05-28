define(function (require, exports, module) {
    require('ztree');
    require("vue");
    require("vue-filters");
    require("axios");
    require("validate");
    require("toastr");
    require("pace");
    var layer = require("layer");
    window.layer = layer;
    var utils = require('utils');
    var vm = new Vue({
        el: '#msapp',
        data: {
            SysDept: {},
            ParentMenus: [],
            deptid: null
        },
        created: function () {

        },
        mounted: function () {
            this.init();
        },
        methods: {
            init: function () {
                utils.permission.initEvent();
                $('#form-role').validate();
                this.getFormData();
                $('#formSave').on('click', function () {
                    vm.saveFormData();
                });
            },
            getFormData: function () {
                var vthis = this;
                vthis.deptid = $.getUrlParam('id');
                axios.get('/Sys/Dept/Get?Id=' + vthis.deptid).then(function (response) {
                    if (!utils.permission.isAuth(response)) {
                        return;
                    }
                    vthis.$set(vthis, 'SysDept', response.data.SysDept);
                    vthis.$set(vthis, 'ParentMenus', response.data.ParentMenus);
                    vthis.$nextTick(function () {
                        vthis.initZTree();
                    });
                }).catch(function (error) {
                    console.log(error);
                });
            },
            saveFormData: function () {
                var isvalidate = $('#form-role').valid();
                if (isvalidate === false) {
                    return;
                }
                var vthis = this;
                var url = parseInt(vthis.SysDept.DeptId) > 0 ? '/Sys/Dept/Edit' : '/Sys/Dept/Add';
                var param = {
                    "SysDept": vthis.SysDept
                };
                axios.post(url, param).then(function (response) {
                    utils.permission.authResponse(response);
                });
            },
            clearMenu: function () {
                $("#menuselected").val('');
                vm.SysDept.ParentId = 0;
            },
            selectMenu: function () {
                $("#menuContent").toggle(200);
            },
            initZTree: function () {
                //设置默认选中
                $.each(this.ParentMenus, function (i, item) {
                    if (item.checked) {
                        $("#menuselected").val(item.name);
                        return false;
                    } else {
                        return true;
                    }
                });
                var zNodes = this.ParentMenus;
                var setting = {
                    view: {
                        dblClickExpand: false
                    },
                    data: {
                        simpleData: {
                            enable: true
                        }
                    },
                    callback: {
                        beforeClick: beforeClick
                    }
                };
                function beforeClick(treeId, treeNode) {
                    vm.SysDept.ParentId = parseInt(treeNode.id);
                    $("#menuselected").val(treeNode.name);
                    $("#menuContent").toggle();
                }
                var treeobj = $.fn.zTree.init($("#treeMenu"), setting, zNodes);
                treeobj.expandAll(true);
            }
        }
    });
    window.selectIcon = function (index, icon) {
        layer.close(index);
        vm.resource.Icon = icon;
    };
});