define(function (require, exports, module) {
    require("ztree");
    require("bootstrap");
    require("jquery-extension");
    require('pace');
    require("axios");
    require("vue");

    var layer = require("layer");
    var tableTrees;
    var setting = {
        callback: {
            onClick: zTreeOnClick
        },
        check: {
            enable: true
        },
        data: {
            simpleData: {
                enable: true
            }
        }
    };
    var vm = new Vue({
        el: '#msapp',
        data: {
            'TableSearch': {},
            'TableColumn': [],
            'TableName': null
        },
        mounted: function() {
            this.init();
        },
        methods: {
            init: function () {
                this.TableSearch = {
                    'Database': 'mssystem',
                    'DataSource': '192.168.178.81',
                    'UserId': 'root',
                    'Password': '123456',
                    'TableName': null,
                    'Namespace': 'MsSystem.Sys'
                };
            },
            getTables: function () {
                var url = '/Sys/CodeBuilder/GetTablesAsync';
                axios.get(url, { params: vm.TableSearch }).then(function (response) {
                    var json = response.data;
                    tableTrees = $.fn.zTree.init($("#tableTrees"), setting, json);
                    tableTrees.expandAll(true);
                });
            },
            getTableColumn: function (tablename) {
                this.TableSearch.TableName = tablename;
                var url = '/Sys/CodeBuilder/GetTableColumnsAsync';
                axios.get(url, { params: vm.TableSearch }).then(function (response) {
                    var res = response.data;
                    vm.TableColumn = res;
                    vm.TableName = res[0].TABLE_NAME;
                });
            },
            createCode: function (type) {
                if (tableTrees == undefined) {
                    layer.msg("请拉取表数据！");
                    return;
                }
                var nodes = tableTrees.getCheckedNodes(true);
                var array = [];
                for (var i = 0; i < nodes.length; i++) {
                    array.push(nodes[i].name);
                }
                if (array.length == 0) {
                    layer.msg("请选择表！");
                    return;
                }
                var url = '/Sys/CodeBuilder/CreateFileAsync';
                for (var j = 0; j < array.length; j++) {
                    //请求伪造构建
                    var form = document.createElement('form');
                    form.method = "get";
                    form.action = url;
                    document.body.appendChild(form);
                    var hdatabase = document.createElement('input');
                    hdatabase.type = 'hidden';
                    hdatabase.name = 'Database';
                    hdatabase.value = vm.TableSearch.Database;
                    form.appendChild(hdatabase);

                    var hdatasource = document.createElement('input');
                    hdatasource.type = 'hidden';
                    hdatasource.name = 'DataSource';
                    hdatasource.value = vm.TableSearch.DataSource;
                    form.appendChild(hdatasource);

                    var huserid = document.createElement('input');
                    huserid.type = 'hidden';
                    huserid.name = 'UserId';
                    huserid.value = vm.TableSearch.UserId;
                    form.appendChild(huserid);

                    var hpwd = document.createElement('input');
                    hpwd.type = 'hidden';
                    hpwd.name = 'Password';
                    hpwd.value = vm.TableSearch.Password;
                    form.appendChild(hpwd);

                    var htablebname = document.createElement('input');
                    htablebname.type = 'hidden';
                    htablebname.name = 'TableName';
                    htablebname.value = array[j];
                    form.appendChild(htablebname);

                    var hns = document.createElement('input');
                    hns.type = 'hidden';
                    hns.name = 'Namespace';
                    hns.value = vm.TableSearch.Namespace;
                    form.appendChild(hns);

                    var htype = document.createElement("input");
                    htype.type = "hidden";
                    htype.name = "Type";
                    htype.value = type;
                    form.appendChild(htype);
                    form.submit();
                }
            }
        }
    });
    function zTreeOnClick(event, treeId, treeNode) {
        vm.getTableColumn(treeNode.name);
    };
});