var workflow;
var categoryTree;
var formTree;
var rolesTree;
var usersTree;
var chatUserTree;

var viewrolesTree;
var viewusersTree;

function dblClickExpand(treeId, treeNode) {
    return treeNode.level > 0;
}
$(function () {
    var basicpage = {
        setting: {
            view: {
                dblClickExpand: dblClickExpand
            },
            data: {
                simpleData: {
                    enable: true
                }
            }
        },
        initCategoryTree: function () {
            var index = layer.load();
            axios.get('/WF/Category/GetCategoryTreeAsync').then(function (response) {
                categoryTree = $.fn.zTree.init($("#categoryTree"), basicpage.setting, response.data);
                layer.close(index);
            });
        },
        initFormTree: function () {
            var index = layer.load();
            axios.get('/WF/Form/GetFormTreeAsync').then(function (response) {
                formTree = $.fn.zTree.init($("#formTree"), basicpage.setting, response.data);
                layer.close(index);
            });
        },
        init: function () {
            basicpage.initEvent();
        },
        initEvent: function () {
            //流程分类树
            $('#selectcategoryTree').click(function () {
                basicpage.initCategoryTree();
                utils.open({
                    type: 1,
                    title: '选择分类',
                    maxmin: false,
                    area: ['300px', '500px'],
                    content: $('#categorybox')
                });
            });
            $('#savecategory').click(function () {
                var treeNode = categoryTree.getSelectedNodes();
                $('input[name=CategoryId]').val(treeNode[0].id);
                $('input[name=CategoryName]').val(treeNode[0].name);
                $('#form-flow').valid();
                layer.closeAll();
            });
            $('#selectformTree').click(function () {
                basicpage.initFormTree();
                utils.open({
                    type: 1,
                    title: '选择表单',
                    maxmin: false,
                    area: ['300px', '500px'],
                    content: $('#formbox')
                });
            });
            $('#saveformtree').click(function () {
                var treeNode = formTree.getSelectedNodes();
                $('input[name=FormId]').val(treeNode[0].id);
                $('input[name=FormName]').val(treeNode[0].name);
                $('#form-flow').valid();
                layer.closeAll();
            });
        }
    };

    basicpage.init();

    var property = {
        toolBtns: ["start round mix", "end round", "task", "view"],
        haveHead: true,
        headLabel: true,
        headBtns: [],
        haveTool: true,
        haveGroup: false,
        useOperStack: true
    };
    var remark = {
        cursor: "选择指针",
        direct: "结点连线",
        start: "入口结点",
        end: "结束结点",
        task: "任务结点",
        node: "条件结点",//自动结点
        chat: "会签",
        state: "状态结点",
        plug: "附加插件",
        fork: "分支结点",
        join: "联合结点",
        complex: "复合结点",
        group: "组织划分框编辑开关",
        view: "通知节点"
    };
    workflow = $.createGooFlow($("#workflowpanel"), property);
    if ($('input[name=FlowId]').val() && $('input[name=FlowId]').val() !== '00000000-0000-0000-0000-000000000000') {
        workflow.loadData(JSON.parse($('input[name=FlowContent]').val()));
    }
    workflow.reinitSize($(this).width() - 400, $(this).height() - 400);
    window.onresize = function () {
        workflow.reinitSize($(this).width() - 400, $(this).height() - 200);
    };
    workflow.setNodeRemarks(remark);
    workflow.onItemDel = function (id, type) {
        if (confirm("确定要删除该单元吗?")) {
            this.blurItem();
            return true;
        } else {
            return false;
        }
    };
    workflow.onItemFocus = function (id, model) {
        var obj;
        $("#ele_model").val(model);
        $("#ele_id").val(id);
        if (model === "line") {
            obj = this.$lineData[id];
            $("#ele_type").val(obj.M);
            $("#ele_from").val(obj.from);
            $("#ele_to").val(obj.to);
        } else if (model === "node") {
            obj = this.$nodeData[id];
            $("#ele_type").val(obj.type);
            $("#ele_from").val("");
            $("#ele_to").val("");
        }
        $("#ele_name").val(obj.name);
        $('#mynodeeventtitle,#mynodeeventcontent').hide();
        return true;
    };
    workflow.onItemBlur = function (id, model) {
        document.getElementById("propertyForm").reset();
        $('.form_title:not(:first),.form_content:not(:first)').hide();
        $('#linename').text('');
        return true;
    };
    workflow.onItemDbClick = function (id, model) {
        var nodeobj = workflow.getItemInfo(id, model);
        if (model === 'node') {
            if (nodeobj.type === 'task' || nodeobj.type === 'node' || nodeobj.type === 'chat' || nodeobj.type === 'view') {
                pageprop.toggleNodeEvent(nodeobj.id, nodeobj.type);
                return false;
            } 
        } else if (model === 'line') {
            pageprop.toggleLineEvent(nodeobj.id);
            return false;
        }
    };
    workflow.onItemAdd = function (id, type, json) {
        var flag = true;
        $.each(workflow.$nodeData, function (index, item) {
            if (item.type === 'start round mix') {
                flag = false;
            }
        });
        if (json.type === 'start round mix' && flag === false) {
            alert('开始节点只能有一个');
            return false;
        } else {
            flag = true;
            $.each(workflow.$nodeData, function (index, item) {
                if (item.type === 'end round') {
                    flag = false;
                }
            });
            if (json.type === 'end round' && flag === false) {
                alert('结束节点只能有一个');
                return false;
            }
            return true;
        }
    };
    $('#formSave').click(function () {
        if ($('#form-flow').valid() === false) {
            return;
        }
        var flowmodel = {
            FlowCode: $('input[name=FlowCode]').val(),
            FlowName: $('input[name=FlowName]').val(),
            Memo: $('input[name=Memo]').val(),
            Enable: 0,
            FlowContent: null,
            CategoryId: $('input[name=CategoryId]').val(),
            FlowId: $('input[name=FlowId]').val(),
            FormId: $('input[name=FormId]').val()
        };
        if ($('input[name=Enable]').is(":checked")) {
            flowmodel.Enable = 1;
        }
        flowmodel.FlowContent = JSON.stringify(workflow.exportData());
        var url = flowmodel.FlowId;
        if (flowmodel.FlowId) {
            url = '/WF/WorkFlow/UpdateAsync';
        } else {
            flowmodel.FlowId = null;
            url = '/WF/WorkFlow/InsertAsync';
        }
        var layerIndex = layer.load();
        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            data: { workflow: flowmodel },
            url: url,
            success: function (data) {
                layer.close(layerIndex);
                if (data) {
                    layer.msg('操作成功！', { icon: 1, time: 1500 }, function () {
                        utils.menu.closeCurrentTab();
                    });
                } else {
                    layer.msg('操作失败！', { icon: 5, time: 1500 });
                }
            }
        });
    });
    $('input[name=FlowName]').on('input', function () {
        workflow.setTitle($(this).val());
    });
    //节点属性PAGE
    var pageprop = {
        init: function () {
            pageprop.initEvent();
            pageprop.TaskEvent();
            pageprop.ChatEvent();
        },
        toggleNodeEvent: function (id, type) {
            if (type === 'task') {
                $('#mynodeeventtitle,#mynodeeventcontent').show();
                if (workflow.getItemInfo(id, 'node').setInfo) {
                    var nodeobj = workflow.getItemInfo(id, 'node');
                    $('#NodeDesignate').val(nodeobj.setInfo.NodeDesignate);
                    var node_ids;
                    if (nodeobj.setInfo.NodeDesignate === 'SPECIAL_ROLE') {
                        $('#selectrolebox').show();
                        $('#selectuserbox,#selectsqlcode').hide();
                        node_ids = nodeobj.setInfo.Nodedesignatedata.roles;
                        pageprop.__getRoleTree(node_ids, function (ids, names, data) {
                            $('#selectrolebox input[type=hidden]').val(ids.join(','));
                            $('#selectrolebox textarea').val(names.join(','));
                        });
                    } else if (nodeobj.setInfo.NodeDesignate === 'SPECIAL_USER') {
                        $('#selectuserbox').show();
                        $('#selectrolebox,#selectsqlcode').hide();
                        node_ids = nodeobj.setInfo.Nodedesignatedata.users;
                        pageprop.__getUserTree(node_ids, function (ids, names, data) {
                            $('#selectuserbox input[type=hidden]').val(ids.join(','));
                            $('#selectuserbox textarea').val(names.join(','));
                        });
                    } else if (nodeobj.setInfo.NodeDesignate === 'SQL') {
                        $('#selectsqlcode').show();
                        $('#selectuserbox,#selectrolebox').hide();
                        node_ids = nodeobj.setInfo.Nodedesignatedata.sql;
                        $('#selectsqlcode textarea').val(node_ids);
                    }
                    else {
                        $('#selectrolebox,#selectuserbox,#selectsqlcode').hide();
                    }
                }
            } else if (type === 'chat') {
                $('#mynodechattitle,#mynodechatcontent').show();
                if (workflow.getItemInfo(id, 'node').setInfo) {
                    var chatnodeobj = workflow.getItemInfo(id, 'node');
                    var chatnode_ids = chatnodeobj.setInfo.Nodedesignatedata.users;
                    $('#ChatType').val(chatnodeobj.setInfo.ChatData.ChatType);
                    $('#ChatParallelCalcType').val(chatnodeobj.setInfo.ChatData.ChatParallelCalcType);
                    pageprop.__getUserTree(chatnode_ids, function (ids, names, data) {
                        $('#selectchatusertrees').parent().find('input[type=hidden]').val(ids.join(','));
                        $('#selectchatusertrees').parent().find('textarea').val(names.join(','));
                    });
                }
            } else if (type == 'view') {
                $('#myviewtitle,#myviewcontent').show();
                if (workflow.getItemInfo(id, 'node').setInfo) {
                    var nodeobj = workflow.getItemInfo(id, 'node');
                    $('#NodeViewDesignate').val(nodeobj.setInfo.NodeDesignate);
                    var node_ids;
                    if (nodeobj.setInfo.NodeDesignate === 'SPECIAL_ROLE') {
                        $('#selectviewrolebox').show();
                        $('#selectviewuserbox,#selectviewsqlcode').hide();
                        node_ids = nodeobj.setInfo.Nodedesignatedata.roles;
                        pageprop.__getRoleTree(node_ids, function (ids, names, data) {
                            $('#selectviewrolebox input[type=hidden]').val(ids.join(','));
                            $('#selectviewrolebox textarea').val(names.join(','));
                        });
                    } else if (nodeobj.setInfo.NodeDesignate === 'SPECIAL_USER') {
                        $('#selectviewuserbox').show();
                        $('#selectviewrolebox,#selectviewsqlcode').hide();
                        node_ids = nodeobj.setInfo.Nodedesignatedata.users;
                        pageprop.__getUserTree(node_ids, function (ids, names, data) {
                            $('#selectviewuserbox input[type=hidden]').val(ids.join(','));
                            $('#selectviewuserbox textarea').val(names.join(','));
                        });
                    } else if (nodeobj.setInfo.NodeDesignate === 'SQL') {
                        $('#selectviewsqlcode').show();
                        $('#selectviewuserbox,#selectviewrolebox').hide();
                        node_ids = nodeobj.setInfo.Nodedesignatedata.sql;
                        $('#selectviewsqlcode textarea').val(node_ids);
                    }
                    else {
                        $('#selectviewrolebox,#selectviewuserbox,#selectviewsqlcode').hide();
                    }
                }
            }
        },
        toggleLineEvent: function (id) {
            $('#mylineeventtitle,#mylineeventcontent').show();
            if (workflow.getItemInfo(id, 'line').setInfo) {
                $('#selectlinebox textarea').val(workflow.getItemInfo(id, 'line').setInfo.CustomSQL);
            }
        },
        __getRoleTree: function (node_ids, callback) {
            var index = layer.load();
            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                url: '/WF/Config/GetRoleTreesAsync',
                data: { ids: node_ids },
                success: function (data) {
                    var ids = [];
                    var names = [];
                    $.each(data, function (index, item) {
                        $.each(node_ids, function (i, id) {
                            if (id == item.id) {
                                ids.push(item.id);
                                names.push(item.name);
                            }
                        });
                    });
                    layer.close(index);
                    if (callback) {
                        callback(ids, names, data);
                    }
                }
            });
        },
        __getUserTree: function (node_ids, callback) {
            var index = layer.load();
            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                url: '/WF/Config/GetUserTreeAsync',
                data: { ids: node_ids },
                success: function (data) {
                    var ids = [];
                    var names = [];
                    $.each(data, function (index, item) {
                        $.each(node_ids, function (i, id) {
                            if (id == item.id) {
                                ids.push(item.id);
                                names.push(item.name);
                            }
                        });
                    });
                    layer.close(index);
                    if (callback) {
                        callback(ids, names, data);
                    }
                }
            });
        },
        ChatEvent: function () {
            $('#selectchatusertrees').click(function () {
                var idstr = $(this).parent().find('input[type=hidden]').val();
                var ids = idstr.split(',');
                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/WF/Config/GetUserTreeAsync',
                    data: { ids: ids },
                    success: function (data) {
                        chatUserTree = $.fn.zTree.init($("#chatusersTree"), pageprop.setting, data);
                        chatUserTree.expandAll(true);
                    }
                });
                utils.open({
                    type: 1,
                    title: '选择用户',
                    maxmin: false,
                    area: ['300px', '500px'],
                    content: $('#chatusers')
                });
            });
            $('#savechatusers').click(function () {
                var nodes = chatUserTree.getCheckedNodes(true);
                var ids = [];
                var names = [];
                for (var i = 0; i < nodes.length; i++) {
                    ids.push(nodes[i].id);
                    names.push(nodes[i].name);
                }
                $('#selectchatusertrees').parent().find('input[type=hidden]').val(ids.join(','));
                $('#selectchatusertrees').parent().find('textarea').val(names.join(','));
                layer.closeAll();
            });
            $('#ChatType').change(function () {
                var val = $(this).val();
                if (val == 0) {
                    $('#ChatParallelCalcType').parent().parent().show();
                } else {
                    $('#ChatParallelCalcType').parent().parent().hide();
                }
            });
        },
        TaskEvent: function () {
            $('#NodeDesignate').change(function () {
                var val = $(this).val();
                if (val == 'SPECIAL_USER') {
                    $('#selectuserbox').show();
                    $('#selectsqlcode,#selectrolebox').hide();
                } else if (val == 'SPECIAL_ROLE') {
                    $('#selectrolebox').show();
                    $('#selectsqlcode,#selectuserbox').hide();
                } else if (val == 'SQL') {
                    $('#selectsqlcode').show();
                    $('#selectuserbox,#selectrolebox').hide();
                } else {
                    $('#selectuserbox,#selectrolebox,#selectsqlcode').hide();
                }
            });
            $('#NodeViewDesignate').change(function () {
                var val = $(this).val();
                if (val == 'SPECIAL_USER') {
                    $('#selectviewuserbox').show();
                    $('#selectviewsqlcode,#selectviewrolebox').hide();
                } else if (val == 'SPECIAL_ROLE') {
                    $('#selectviewrolebox').show();
                    $('#selectviewsqlcode,#selectviewuserbox').hide();
                } else if (val == 'SQL') {
                    $('#selectviewsqlcode').show();
                    $('#selectviewuserbox,#selectviewrolebox').hide();
                } else {
                    $('#selectviewuserbox,#selectviewrolebox,#selectviewsqlcode').hide();
                }
            });
            $('#selectroletrees').click(function () {
                var idstr = $('#selectrolebox input[type=hidden]').val();
                var ids = idstr.split(',');
                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/WF/Config/GetRoleTreesAsync',
                    data: { ids: ids },
                    success: function (data) {
                        rolesTree = $.fn.zTree.init($("#rolesTree"), pageprop.setting, data);
                        rolesTree.expandAll(true);
                    }
                });
                utils.open({
                    type: 1,
                    title: '选择角色',
                    maxmin: false,
                    area: ['300px', '500px'],
                    content: $('#roles')
                });
            });
            $('#selectviewroletrees').click(function () {
                var idstr = $('#selectviewrolebox input[type=hidden]').val();
                var ids = idstr.split(',');
                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/WF/Config/GetRoleTreesAsync',
                    data: { ids: ids },
                    success: function (data) {
                        viewrolesTree = $.fn.zTree.init($("#viewrolesTree"), pageprop.setting, data);
                        viewrolesTree.expandAll(true);
                    }
                });
                utils.open({
                    type: 1,
                    title: '选择角色',
                    maxmin: false,
                    area: ['300px', '500px'],
                    content: $('#viewroles')
                });
            });
            $('#selectuserpage').click(function () {
                var idstr = $('#selectuserbox input[type=hidden]').val();
                var ids = idstr.split(',');
                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/WF/Config/GetUserTreeAsync',
                    data: { ids: ids },
                    success: function (data) {
                        usersTree = $.fn.zTree.init($("#usersTree"), pageprop.setting, data);
                        usersTree.expandAll(true);
                    }
                });
                utils.open({
                    type: 1,
                    title: '选择用户',
                    maxmin: false,
                    area: ['300px', '500px'],
                    content: $('#users')
                });
            });
            $('#selectviewuserpage').click(function () {
                var idstr = $('#selectviewuserbox input[type=hidden]').val();
                var ids = idstr.split(',');
                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/WF/Config/GetUserTreeAsync',
                    data: { ids: ids },
                    success: function (data) {
                        viewusersTree = $.fn.zTree.init($("#viewusersTree"), pageprop.setting, data);
                        viewusersTree.expandAll(true);
                    }
                });
                utils.open({
                    type: 1,
                    title: '选择用户',
                    maxmin: false,
                    area: ['300px', '500px'],
                    content: $('#viewusers')
                });
            });
            $('#saveroles').click(function () {
                var nodes = rolesTree.getCheckedNodes(true);
                var ids = [];
                var names = [];
                for (var i = 0; i < nodes.length; i++) {
                    ids.push(nodes[i].id);
                    names.push(nodes[i].name);
                }
                $('#selectrolebox input[type=hidden]').val(ids.join(','));
                $('#selectrolebox textarea').val(names.join(','));
                layer.closeAll();
            });
            $('#saveviewroles').click(function () {
                var nodes = viewrolesTree.getCheckedNodes(true);
                var ids = [];
                var names = [];
                for (var i = 0; i < nodes.length; i++) {
                    ids.push(nodes[i].id);
                    names.push(nodes[i].name);
                }
                $('#selectviewrolebox input[type=hidden]').val(ids.join(','));
                $('#selectviewrolebox textarea').val(names.join(','));
                layer.closeAll();
            });
            $('#saveusers').click(function () {
                var nodes = usersTree.getCheckedNodes(true);
                var ids = [];
                var names = [];
                for (var i = 0; i < nodes.length; i++) {
                    ids.push(nodes[i].id);
                    names.push(nodes[i].name);
                }
                $('#selectuserbox input[type=hidden]').val(ids.join(','));
                $('#selectuserbox textarea').val(names.join(','));
                layer.closeAll();
            });
            $('#saveviewusers').click(function () {
                var nodes = viewusersTree.getCheckedNodes(true);
                var ids = [];
                var names = [];
                for (var i = 0; i < nodes.length; i++) {
                    ids.push(nodes[i].id);
                    names.push(nodes[i].name);
                }
                $('#selectviewuserbox input[type=hidden]').val(ids.join(','));
                $('#selectviewuserbox textarea').val(names.join(','));
                layer.closeAll();
            });
        },
        initEvent: function () {
            $('#formReturn').on('click', function () {
                utils.menu.closeCurrentTab();
            });
            $('#savenodeprop').click(function () {
                var nodeModel = $('#ele_model').val();
                if (nodeModel === 'node') {//node类型时候
                    var nodeType = $('#ele_type').val();
                    var selfinfodata = {};
                    if (nodeType === 'task') {
                        selfinfodata = {
                            NodeDesignate: $('#NodeDesignate').val(),
                            Nodedesignatedata: {
                                users: [],
                                roles: [],
                                orgs: [],
                                sql: ''
                            }
                        };
                        if (selfinfodata.NodeDesignate == 'SPECIAL_ROLE') {
                            selfinfodata.Nodedesignatedata.roles = $('#selectrolebox input[type=hidden]').val().split(',');
                        } else if (selfinfodata.NodeDesignate == 'SPECIAL_USER') {
                            selfinfodata.Nodedesignatedata.users = $('#selectuserbox input[type=hidden]').val().split(',');
                        } else if (selfinfodata.NodeDesignate == 'SQL') {
                            selfinfodata.Nodedesignatedata.sql = $('#selectsqlcode textarea').val();
                        }
                    } else if (nodeType === 'chat') {
                        selfinfodata = {
                            NodeDesignate: 'SPECIAL_USER',
                            Nodedesignatedata: {
                                users: [],
                                roles: [],
                                orgs: []
                            },
                            IsChat: true,
                            ChatData: {
                                ChatType: $('#ChatType').val(),
                                ChatParallelCalcType: $('#ChatParallelCalcType').val()
                            }
                        };
                        selfinfodata.Nodedesignatedata.users = $('#selectchatusertrees').parent().find('input[type=hidden]').val().split(',');
                    } else if (nodeType == 'view') {
                        selfinfodata = {
                            NodeDesignate: $('#NodeViewDesignate').val(),
                            Nodedesignatedata: {
                                users: [],
                                roles: [],
                                orgs: [],
                                sql: ''
                            }
                        };
                        if (selfinfodata.NodeDesignate == 'SPECIAL_ROLE') {
                            selfinfodata.Nodedesignatedata.roles = $('#selectviewrolebox input[type=hidden]').val().split(',');
                        } else if (selfinfodata.NodeDesignate == 'SPECIAL_USER') {
                            selfinfodata.Nodedesignatedata.users = $('#selectviewuserbox input[type=hidden]').val().split(',');
                        } else if (selfinfodata.NodeDesignate == 'SQL') {
                            selfinfodata.Nodedesignatedata.sql = $('#selectviewsqlcode textarea').val();
                        }
                    }
                    workflow.setName($('#ele_id').val(), $('#ele_name').val(), 'node', selfinfodata);
                } else if (nodeModel === 'line') {
                    var sql = $('#selectlinebox textarea').val();
                    var setInfo = {
                        CustomSQL: sql
                    };
                    workflow.setName($('#ele_id').val(), $('#ele_name').val(), 'line', setInfo);
                }
            });
        },
        setting: {
            check: {
                enable: true
            },
            data: {
                simpleData: {
                    enable: true
                }
            }
        }
    };
    pageprop.init();
});