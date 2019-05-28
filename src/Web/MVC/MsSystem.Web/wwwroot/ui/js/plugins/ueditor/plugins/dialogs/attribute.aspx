<%@ Page Language="C#" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" src="../../dialogs/internal.js"></script>
    <script type="text/javascript" src="../common.js"></script>
    <%=WebMvc.Common.Tools.IncludeFiles %>
</head>
<body>
<%
    WebMvc.Common.Tools.CheckLogin();
    Business.Platform.DBConnection bdbConn = new Business.Platform.DBConnection();
    Business.Platform.WorkFlowForm bworkflowform = new Business.Platform.WorkFlowForm();
    string link_DBConnOptions = bdbConn.GetAllOptions();
    string typeOptions = bworkflowform.GetTypeOptions("");
    string validatePromptType = new Business.Platform.WorkFlowForm().GetValidatePropTypeRadios("validatealerttype","","");
%>
<br />
<table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable">
    <tr>
        <th style="width:80px;">表单名称:</th>
        <td><input type="text" class="mytext" id="name" style="width:223px"/></td>
    </tr>
    <tr>
        <th>数据连接:</th>
        <td><select class="myselect" id="dbconn" onchange="db_change(this)" style="width:227px"><%=link_DBConnOptions %></select></td>
    </tr>
    <tr>
        <th>数据表:</th>
        <td><select class="myselect" id="dbtable" onchange="table_change(this)" style="width:227px"></select></td>
    </tr>
    <tr>
        <th>主键:</th>
        <td><select class="myselect" id="dbpk" style="width:227px"></select></td>
    </tr>
    <tr>
        <th>标题字段:</th>
        <td><select class="myselect" id="dbtitle" style="width:227px"></select></td>
    </tr>
    <tr>
        <th>程序库分类:</th>
        <td>
            <select class="myselect" id="type" style=""><option value=""></option><%=typeOptions %></select>
        </td>
    </tr>
    <tr>
        <th>任务标题:</th>
        <td><input type="checkbox" value="1" id="autotitle" style="vertical-align:middle;"/><label for="autotitle" style="vertical-align:middle;">自动生成任务标题</label></td>
    </tr>
    <tr>
        <th>验证提示:</th>
        <td><%=validatePromptType %></td>
    </tr>
    <tr>
        <td colspan="2" style="height:23px; text-align:center; color:blue;">提示：属性设置完成点击确定后即可在编辑器区域设计表单</td>
    </tr>
</table>

<script type="text/javascript">
    var attJSON = parent.formattributeJSON;
    var dbconn = attJSON.dbconn || "";
    var dbtable = attJSON.dbtable || "";
    var dbtablepk = attJSON.dbtablepk || "";
    var dbtabletitle = attJSON.dbtabletitle || "";
    var isnew = "1" == "<%=Request["new"]%>";
    $(function ()
    {
        if (!isnew)
        {
            table_change($("#dbtable").get(0), "");
            $("#dbpk").val(dbtablepk);
            $("#dbtitle").val(dbtabletitle);
            $("#dbconn").val(dbconn);
            $("#name").val(attJSON.name || "");
            $("#type").val(attJSON.apptype || "");
            $("#autotitle").prop("checked", attJSON.autotitle)
            $("#typeselect").val(attJSON.apptype || "");
            $("input[name='validatealerttype'][value='" + attJSON.validatealerttype + "']").prop('checked', true);
        }
        db_change($("#dbconn").get(0), isnew ? "" : dbtable);
    });
    function db_change(obj, table)
    {
        if (!obj || !obj.value) return;
        $("#dbtable").html(getTableOps(obj.value, table));
    }
    function table_change(obj, fields)
    {
        if (!obj || !obj.value) return;
        var conn = $("#dbconn").val();
        var opts = getFieldsOps(conn, obj.value, fields);
        $("#dbpk").html(opts);
        $("#dbtitle").html(opts);
    }
    
    function typeChange(value)
    {
        $("#typeselect option").each(function ()
        {
            if ($(this).val() == value)
            {
                $("#type").val(!$(this).val() ? "" : $(this).text());
                return false;
            }
        });
    }
    dialog.onok = function ()
    {
        var json = {};
        json.name = $("#name").val();
        json.dbconn = $("#dbconn").val();
        json.dbtable = $("#dbtable").val();
        json.dbtablepk = $("#dbpk").val();
        json.dbtabletitle = $("#dbtitle").val();
        json.apptype = $("#type").val();
        json.autotitle = $("#autotitle").prop("checked");
        json.validatealerttype = $(":checked[name='validatealerttype']").val() || "1";
        
        parent.formattributeJSON.name = json.name;
        parent.formattributeJSON.dbconn = json.dbconn;
        parent.formattributeJSON.dbtable = json.dbtable;
        parent.formattributeJSON.dbtablepk = json.dbtablepk;
        parent.formattributeJSON.dbtabletitle = json.dbtabletitle;
        parent.formattributeJSON.apptype = json.apptype;
        parent.formattributeJSON.autotitle = json.autotitle;
        parent.formattributeJSON.validatealerttype = json.validatealerttype;
        
        if (isnew)
        {
            parent.formattributeJSON.id = "";
            editor.setContent("");
        }
    }
</script>
</body>
</html>

