<%@ Page Language="C#" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" src="../../dialogs/internal.js"></script>
    <script type="text/javascript" src="../common.js"></script>
    <%=WebMvc.Common.Tools.IncludeFiles %>
</head>
<body>
<% 
    WebMvc.Common.Tools.CheckLogin();
    Business.Platform.WorkFlowForm workFlowFrom = new Business.Platform.WorkFlowForm(); 
%>
<table cellpadding="0" cellspacing="1" border="0" width="98%" class="formtable" style="margin-top:6px;">
    <tr>
        <th style="width:100px;">从表：</th>
        <td><select class="myselect" id="secondtable" style="width:227px" onchange="table_change(this)"></select>
           
        </td>
        <th>从表主键：</th>
        <td><select class="myselect" id="secondtableprimarykey" style="width:227px"></select></td>
    </tr>
    
    <tr>
        <th style="width:100px;">主表字段：</th>
        <td><select class="myselect" id="primarytablefiled" style="width:227px"></select></td>
        <th>与主表关联字段：</th>
        <td><select class="myselect" id="secondtablerelationfield" style="width:227px"></select></td>
    </tr>
    <!--
    <tr>
        <th>序号列:</th>
        <td><input type="checkbox" value="1" id="secondtableindex" style="vertical-align:middle;"/><label for="autotitle" style="vertical-align:middle;">是否显示序号列</label></td>
    </tr>
    -->
</table>
<div style="width:98%; margin:5px auto 0 auto; height:370px; overflow:auto;">
    <table cellpadding="0" cellspacing="1" border="0" width="98%" class="listtable" id="listtable">
        <thead>
            <tr>
                <th style="width:30%;"><input type="checkbox" style="vertical-align:middle;" onclick="checkall(this);" />显示列</th>
                <th style="width:25%;">显示名称</th>
                <th style="width:30%;">编辑模式</th>
                <th style="width:5%;">合计</th>
                <th style="width:10%;">显示顺序</th>
            </tr>
        </thead>
        <tbody>
            
        </tbody>
    </table>
</div>
<script type="text/javascript">
    var oNode = null, thePlugins = 'formsubtable';
    var attJSON = parent.formattributeJSON;
    var dbconn = attJSON.dbconn || "";
    var dbtable = attJSON.dbtable || "";
    var initJSON = {};
    $(function ()
    {
        var secondtable = '';
        var primarytablefiled = '';
        var secondtableprimarykey = '';
        var secondtablerelationfield = '';
        if (UE.plugins[thePlugins].editdom)
        {
            oNode = UE.plugins[thePlugins].editdom;
        }
        if (oNode)
        {
            $text = $(oNode);
            var id = $text.attr("id");
            for (var i = 0; i < parent.formsubtabs.length; i++)
            {
                if (parent.formsubtabs[i].id == id)
                {
                    initJSON = parent.formsubtabs[i];
                    break;
                }
            }
            
            if (initJSON)
            {
                secondtable = initJSON.secondtable;
                primarytablefiled = initJSON.primarytablefiled;
                secondtableprimarykey = initJSON.secondtableprimarykey;
                secondtablerelationfield = initJSON.secondtablerelationfield;
            }
        }

        $("#secondtable").html(getTableOps(dbconn, secondtable));
        $("#primarytablefiled").html(getFieldsOps(dbconn, dbtable, primarytablefiled));
        if (secondtable.length > 0)
        {
            table_change($("#secondtable").get(0), secondtableprimarykey, secondtablerelationfield);
        }

    });
    function table_change(obj, fields, fields1)
    {
        if (!obj || !obj.value) return;
        var conn = dbconn;
        var table = obj.value;
        var opts = getFieldsOps(conn, table, fields);
        var opts1 = getFieldsOps(conn, table, fields1);
        $("#secondtableprimarykey").html(opts);
        $("#secondtablerelationfield").html(opts1);
        addTableFields(opts, table);
    }
    function addTableFields(opts, table)
    {
        var $tbody = $("#listtable tbody");
        $tbody.html('');
        $(opts).each(function (index)
        {
            var filed = $(this).val();
            var filedNoNote = $(this).text();
            if (filed.length == 0)
            {
                return true;
            }
            var isshow = false;
            var issum = false;
            var showname = "";
            var showtype = "";
            var index = "";
            var editmode = {};
            if (initJSON && initJSON.colnums)
            {
                for (var i = 0; i < initJSON.colnums.length; i++)
                {
                    if (initJSON.colnums[i].name == table + "_" + filed)
                    {
                        var colnumjson = initJSON.colnums[i];
                        isshow = "1" == colnumjson.isshow;
                        issum = "1" == colnumjson.issum;
                        showname = colnumjson.showname;
                        editmode = colnumjson.editmode;
                        index = colnumjson.index || "";
                        if (editmode && editmode.title) showtype = editmode.title;
                        break;
                    }
                }
            }
            
            var tr = '<tr>';
            tr += '<td style="background-color:#ffffff; height:28px;"><input type="checkbox" name="field" value="' + filed + '" id="field_' + filed + '" ' + (isshow ? 'checked="checked"' : '') + ' style="vertical-align:middle;" /><label style="vertical-align:middle;" for="field_' + filed + '">' + filedNoNote + '</label></td>';
            tr += '<td style="background-color:#ffffff;"><input type="text" class="mytext" name="name_' + filed + '" value="" />' + '</td>';
            tr += '<td style="background-color:#ffffff;"><input type="hidden" value="" id="set_' + filed + '_hidden"/><input type="text" class="mytext" readonly="readonly" style="width:100px;mraing-right:0;" name="set_' + filed + '" id="set_' + filed + '" value="'+showtype+'"/><input type="button" class="mybutton" style="border-left:none 0;margin:0;" value="设置" onclick="filedEditSet(\'' + filed + '\');"/>' + '</td>';
            tr += '<td style="background-color:#ffffff;"><input type="checkbox" name="field_count" value="' + filed + '" id="field_count_' + index + '" ' + (issum ? 'checked="checked"' : '') + ' style="vertical-align:middle;" /></td>';
            tr += '<td style="background-color:#ffffff;"><input type="text" class="mytext" id="field_index_' + filed + '" style="width:40px;" value="' + index + '"/></td>';
            tr += '</tr>';
            var $tr = $(tr);
            if (showname)
            {
                $("input[name='name_" + filed + "']", $tr).val(showname);
            }
            if (editmode)
            {
                $("input[id='set_" + filed + "_hidden']", $tr).val(JSON.stringify(editmode));
            }
            $tbody.append($tr);
            new RoadUI.Text().init($(".mytext"), $tr);
            new RoadUI.Button().init($(".mybutton"), $tr);
            
        });
    }
    function filedEditSet(field)
    {
        top.mainDialog.open({
            id: "from_set_" + field,
            url: top.rootdir + "/Scripts/FormDesinger/ueditor/plugins/dialogs/subtableset.aspx?eid=set_" + field + "&dbconn=" + dbconn,
            title: field + "-编辑模式设置", width: 700, height: 450
        });
    }
    function checkall(obj)
    {
        $("input[name='field']").prop('checked', $(obj).prop('checked'));
    }
    dialog.oncancel = function ()
    {
        if (UE.plugins[thePlugins].editdom)
        {
            delete UE.plugins[thePlugins].editdom;
        }
    };
    dialog.onok = function ()
    {
        var secondtable = $("#secondtable").val();
        var primarytablefiled = $("#primarytablefiled").val();
        var secondtableprimarykey = $("#secondtableprimarykey").val();
        var secondtablerelationfield = $("#secondtablerelationfield").val();
        var json = {};
        json.secondtable = secondtable;
        json.primarytablefiled = primarytablefiled;
        json.secondtableprimarykey = secondtableprimarykey;
        json.secondtablerelationfield = secondtablerelationfield;
        json.colnums = [];
        $("#listtable tbody tr").each(function (index)
        {
            var field = $("input[type='checkbox'][id^='field_']",$(this)).val();
            var jstr = $("input[id='set_" + field + "_hidden']", $(this)).val();
            var colnum = {};
            colnum.name = secondtable + "_" + field;
            colnum.isshow = $("input[type='checkbox'][id^='field_']", $(this)).prop("checked") ? "1" : "0";
            colnum.showname = $("input[name='name_" + field + "']", $(this)).val();
            colnum.editmode = jstr.length > 0 ? JSON.parse(jstr) : {};
            colnum.issum = $("input[type='checkbox'][name='field_count']", $(this)).prop("checked") ? "1" : "0";
            colnum.index = $("input[id='field_index_" + field + "']", $(this)).val();
            json.colnums.push(colnum);
            
        });
        var id = secondtable + "_" + primarytablefiled + "_" + secondtableprimarykey + "_" + secondtablerelationfield;
        var html = '<input type="text" isflow="1" readonly="readonly" type1="flow_subtable" id="' + id + '" style="width:99%;height:50px;" value="子表" ';
        html += '/>';
        json.id = id;
        var isadd = true;
        for (var i = 0; i < parent.formsubtabs.length; i++)
        {
            if (parent.formsubtabs[i].id == id)
            {
                parent.formsubtabs[i] = json;
                isadd = false;
                break;
            }
        }
        if (isadd)
        {
            parent.formsubtabs.push(json);
        }
        if (oNode)
        {
            $(oNode).after(html);
            domUtils.remove(oNode, false);
        }
        else
        {
            editor.execCommand('insertHtml', html);
        }
        delete UE.plugins[thePlugins].editdom;
    }
</script>
</body>
</html>