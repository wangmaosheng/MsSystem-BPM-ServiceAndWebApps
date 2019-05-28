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
        <th>默认值:</th>
        <td><input type="text" class="mytext" id="defaultvalue" style="width:250px; margin-right:6px;"/><select class="myselect" onchange="setDefaultValue(document.getElementById('defaultvalue'), this.value);" style="width:100px"><%=workFlowFrom.GetDefaultValueSelect("") %></select></td>
    </tr>
    <tr>
        <th>宽度：</th>
        <td><input type="text" id="width" class="mytext" style="width:150px" /></td>
    </tr>
    <tr>
        <th>选择范围：</th>
        <td>
            <div>
                <%=workFlowFrom.GetOrgRangeRadios("org_rang","") %>
            </div>
            <div style="padding-top:5px;"><input type="radio" name="org_rang" value="2" id="org_rang_3" style="vertical-align:middle;"/><label for="org_rang_3" style="vertical-align:middle;">自定义</label>：<input type="text" id="org_rang1" class="mymember" style="width:150px;"/></div>
        </td>
    </tr>
    <tr>
        <th>选择类型：</th>
        <td><%=workFlowFrom.GetOrgSelectTypeCheckboxs("org_type","") %></td>
    </tr>
    <tr>
        <th>多选：</th>
        <td><input type="checkbox" value="1" id="more"  style="vertical-align:middle;" /><label for="more" style="vertical-align:middle;">是否可以多选</label></td>
    </tr>
</table>
<script type="text/javascript">
    var oNode = null, thePlugins = 'formorg';
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
            $("#defaultvalue").val($text.attr('defaultvalue'));
            if ($text.attr('width1')) $("#width").val($text.attr('width1'));
            $("#more").prop("checked", "1" == $text.attr('more'));
            $("#org_rang1").val($text.attr('org_rang1'));
            new RoadUI.Member().setValue($("#org_rang1"));
            $("input[name='org_rang'][value='" + $text.attr('org_rang') + "']").prop('checked', true);
            var org_type = $text.attr("org_type");
            if (org_type)
            {
                $("input[name='org_type']").each(function ()
                {
                    if (org_type.indexOf(',' + $(this).val() + ',') >= 0)
                    {
                        $(this).prop('checked', true);
                    }
                });
            }
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
        var bindfiled = $("#bindfiled").val();
        var id = attJSON.dbconn && attJSON.dbtable && bindfiled ? attJSON.dbtable + '.' + bindfiled : "";
        var width = $("#width").val();
        var defaultvalue = $("#defaultvalue").val();
        var more = $("#more").prop("checked") ? "1" : "0";
        var org_rang = $(":checked[name='org_rang']").val();
        var org_rang1 = $("#org_rang1").val();
        var org_type = ",";
        $(":checked[name='org_type']").each(function ()
        {
            org_type += $(this).val() + ",";
        });

        var html = '<input type="text" type1="flow_org" id="' + id + '" name="' + id + '" value="组织机构选择框" ';
        if (width)
        {
            html += 'style="width:' + width + '" ';
            html += 'width1="' + width + '" ';
        }
        html += 'defaultvalue="' + defaultvalue + '" ';
        html += 'more="' + more + '" ';
        if (org_rang)
        {
            html += 'org_rang="' + org_rang + '" ';
            if (org_rang == "2")
            {
                html += 'org_rang1="' + org_rang1 + '" ';
            }
        }
        
        html += 'org_type="' + org_type + '" ';
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