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
            resource: {},
            buttons: [],
            resourceid: 0,
            ParentMenus: [],
            systemid: 0
        },
        created: function () {

        },
        mounted: function () {
            this.init();
        },
        methods: {
            init: function () {
                utils.permission.initEvent();
                $('#form-resource').validate();
                this.getFormData();
                $('#formSave').on('click', function() {
                    vm.saveFormData();
                });
            },
            getFormData: function () {
                var vthis = this;
                vthis.resourceid = $.getUrlParam('id');
                var pid = $.getUrlParam('pid');
                var cid = $.getUrlParam('cid');
                var systemid = $.getUrlParam('systemid');
                vthis.systemid = systemid;
                axios.get('/Sys/Resource/Get?Id=' + vthis.resourceid + '&pid=' + pid + '&systemid=' + systemid).then(function (response) {
                    if (!utils.permission.isAuth(response)) {
                        return;
                    }
                    vthis.$set(vthis, 'resource', response.data.SysResource);
                    vthis.$set(vthis, 'buttons', response.data.ButtonViewModels);
                    vthis.$set(vthis, 'ParentMenus', response.data.ParentMenus);
                    vthis.$nextTick(function () {
                        vthis.initZTree();
                        if (vthis.resource.ResourceId === 0 && cid > 0) {
                            vthis.resource.ParentId = cid;
                        }
                    });
                }).catch(function (error) {
                    console.log(error);
                });
            },
            saveFormData: function () {
                var isvalidate = $('#form-resource').valid();
                if (isvalidate === false) {
                    return;
                }
                var vthis = this;
                vthis.resource.SystemId = vthis.systemid;
                var url = parseInt(vthis.resourceid) > 0 ? '/Sys/Resource/Edit' : '/Sys/Resource/Add';
                var param = {
                    "SysResource": vthis.resource,
                    "ButtonDto": vthis.buttons
                };
                axios.post(url, param).then(function (response) {
                    utils.permission.authResponse(response);
                });
            },
            clearMenu: function() {
                $("#menuselected").val('');
                vm.resource.ParentId = 0;
            },
            selectMenu: function () {
                $("#menuContent").toggle(200);
            },
            clearIcon: function() {
                vm.resource.Icon = null;
            },
            selectIcon: function() {
                layer.open({
                    type: 2,
                    title: '选择图标',
                    maxmin: false,
                    area: ['700px', '560px'],
                    content: '/Sys/Resource/Icon',
                    success: function (layero, index) {
                        layero.find('iframe').focus();
                    }
                });
            },
            initZTree: function () {
                //设置默认选中
                $.each(this.ParentMenus, function(i, item) {
                    if (item.checked) {
                        $("#menuselected").val(item.name);
                        return false;
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
                    vm.resource.ParentId = parseInt(treeNode.id);
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
        if (icon) {
            vm.resource.Icon = icon;
        }
    };
});