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
<br />
<table cellpadding="0" cellspacing="1" border="0" width="95%" class="formtable">
    <tr>
        <th>默认值：</th>
        <td><input type="text" class="mytext" id="defaultvalue" style="width:250px; margin-right:6px;"/><select class="myselect" onchange="setDefaultValue(document.getElementById('defaultvalue'), this.value);" style="width:100px"><%=workFlowFrom.GetDefaultValueSelect("") %></select></td>
    </tr> 
    <tr>
        <th>字号：</th>
        <td><input type="text" id="fontsize" class="mytext" style="width:150px" /> (例:18px, 18pt)</td>
    </tr>
    <tr>
        <th>颜色：</th>
        <td><input type="text" id="fontcolor" class="mytext" style="width:150px" /> (例:#cccccc, red)</td>
    </tr>
    <tr>
        <th>样式：</th>
        <td><input type="checkbox" id="fontbold" value="1" style="vertical-align:middle;" /><label for="fontbold" style="vertical-align:middle;">粗体</label>
            <input type="checkbox" id="fontstyle" value="1" style="vertical-align:middle;" /><label for="fontstyle" style="vertical-align:middle;">斜体</label>
        </td>
    </tr>
</table>
<script type="text/javascript">
    var oNode = null, thePlugins = 'formlabel';
    var attJSON = parent.formattributeJSON;
    $(function ()
    {
        if (UE.plugins[thePlugins].editdom)
        {
            oNode = UE.plugins[thePlugins].editdom;
        }
        //biddingFileds(attJSON, oNode ? $(oNode).attr("id") : "", $("#bindfiled"));
        if (oNode)
        {
            $text = $(oNode);
            $("#defaultvalue").val($text.attr('defaultvalue'));
            $("#fontsize").val($text.attr('fontsize'));
            $("#fontcolor").val($text.attr('fontcolor'));
            $("#fontbold").prop("checked", "1" == $text.attr('fontbold'));
            $("#fontstyle").prop("checked", "1" == $text.attr('fontstyle'));
        }
    });
    dialog.oncancel = function ()
    {
        if (UE.plugins[thePlugins].editdom)
        {
            delete UE.plugins[thePlugins].editdom;
        }
    };
    dialog.onok = function ()
    {
        //var bindfiled = $("#bindfiled").val();
        //var id = attJSON.dbconn && attJSON.dbtable && bindfiled ? attJSON.dbtable + '.' + bindfiled : "";
        var defaultvalue = $("#defaultvalue").val();
        var fontsize = $("#fontsize").val();
        var fontcolor = $("#fontcolor").val();
       
        var html = '<input type="text" type1="flow_label" value="Label标签" ';
        if (defaultvalue)
        {
            html += 'defaultvalue="' + defaultvalue + '" ';
        }
        if (fontsize)
        {
            html += 'fontsize="' + fontsize + '" ';
        }
        if (fontcolor)
        {
            html += 'fontcolor="' + fontcolor + '" ';
        }
        if ($("#fontbold").prop("checked"))
        {
            html += 'fontbold="1" ';
        }
        if ($("#fontstyle").prop("checked"))
        {
            html += 'fontstyle="1" ';
        }
        html += '/>';
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