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
        <th style="width:80px;">绑定字段：</th>
        <td><select class="myselect" id="bindfiled" style="width:227px"></select></td>
    </tr>
    <tr>
        <th>宽度：</th>
        <td><input type="text" id="width" class="mytext" style="width:150px;" /> </td>
    </tr>
    <tr>
        <th>高度：</th>
        <td><input type="text" id="height" class="mytext" style="width:150px" /> </td>
    </tr>
    
</table>
<script type="text/javascript">
    var oNode = null, thePlugins = 'formhtml';
    var attJSON = parent.formattributeJSON;
    $(function ()
    {
        if (UE.plugins[thePlugins].editdom)
        {
            oNode = UE.plugins[thePlugins].editdom;
        }
        biddingFileds(attJSON, oNode ? $(oNode).attr("id") : "", $("#bindfiled"));
        if (oNode)
        {
            $text = $(oNode);
            //$("#defaultvalue").val($text.attr('defaultvalue'));
            //$("input[name='model'][value='" + $text.attr('model') + "']").prop('checked', true);
            $("#width").val($text.attr('width1'));
            $("#height").val($text.attr('height1'));
            //$("#maxlength").val($text.attr('maxlength'));
            //$("#valuetype").val($text.attr('valuetype'));
        }
    });
    dialog.onok = function ()
    {
        var bindfiled = $("#bindfiled").val();
        var id = attJSON.dbconn && attJSON.dbtable && bindfiled ? attJSON.dbtable + '.' + bindfiled : "";
        //var defaultvalue = $("#defaultvalue").val();
        //var maxlength = $("#maxlength").val();
        //var model = $(":checked[name='model']").val();
        var width = $("#width").val();
        var height = $("#height").val();
        //var valuetype = $("#valuetype").val();

        var html = '<input type="text" isflow="1" type1="flow_html" id="' + id + '" name="' + id + '" value="HTML编辑器" ';
        if (width && height)
        {
            html += 'style="width:' + width + ';height:' + height + '" ';
            html += 'width1="' + width + '" ';
            html += 'height1="' + height + '" ';
        }
        else if (width || height)
        {
            if (width)
            {
                html += 'style="width:' + width + '" ';
                html += 'width1="' + width + '" ';
            }
            if (height)
            {
                html += 'style="height:' + height + '" ';
                html += 'height1="' + height + '" ';
            }
        }
        else
        {
            html += 'style="width:95%; height:150px;" ';
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