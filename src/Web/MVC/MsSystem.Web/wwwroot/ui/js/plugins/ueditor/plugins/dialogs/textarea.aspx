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
<div class="wrapper">
    <div id="tabhead" class="tabhead">
        <span class="tab focus" data-content-id="text_attr">&nbsp;&nbsp;属性&nbsp;&nbsp;</span>
        <span class="tab" data-content-id="text_event">&nbsp;&nbsp;事件&nbsp;&nbsp;</span>
    </div>
    <div id="tabbody" class="tabbody">
        <div id="text_attr" class="panel focus">
            <table cellpadding="0" cellspacing="1" border="0" width="100%" class="formtable">
                <tr>
                    <th style="width:80px;">绑定字段:</th>
                    <td><select class="myselect" id="bindfiled" style="width:227px"></select></td>
                </tr>
                <tr>
                    <th>默认值:</th>
                    <td><input type="text" class="mytext" id="defaultvalue" style="width:290px; margin-right:6px;"/><select class="myselect" onchange="setDefaultValue(document.getElementById('defaultvalue'), this.value);" style="width:150px"><%=workFlowFrom.GetDefaultValueSelect("") %></select></td>
                </tr>
                <tr>
                    <th>最大字符数:</th>
                    <td><input type="text" id="maxlength" class="mytext" style="width:150px" /> (maxlength属性)</td>
                </tr>
                <tr>
                    <th>宽度:</th>
                    <td><input type="text" id="width" class="mytext" style="width:150px" /> </td>
                </tr>
                <tr>
                    <th>高度:</th>
                    <td><input type="text" id="height" class="mytext" style="width:150px" /> </td>
                </tr>
                <tr>
                    <th>值类型:</th>
                    <td><select class="myselect" id="valuetype" ><%=workFlowFrom.GetValueTypeOptions("") %></select></td>
                </tr>
            </table>
            </div>

        <div id="text_event" class="panel">
           <%Server.Execute("events.aspx"); %>
        </div>
    </div>
</div>
<script type="text/javascript">
    var oNode = null, thePlugins = 'formtextarea';
    var attJSON = parent.formattributeJSON;

    var parentEvents = parent.formEvents;
    var events = [];
    var eventsid = RoadUI.Core.newid(false);

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
            //$("input[name='model'][value='" + $text.attr('model') + "']").prop('checked', true);
            $("#width").val($text.attr('width1'));
            $("#height").val($text.attr('height1'));
            $("#maxlength").val($text.attr('maxlength'));
            $("#valuetype").val($text.attr('valuetype'));

            if ($text.attr('eventsid'))
            {
                eventsid = $text.attr('eventsid');
                events = getEvents(eventsid);
            }
        }

        initTabs();
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
        var defaultvalue = $("#defaultvalue").val();
        var maxlength = $("#maxlength").val();
        //var model = $(":checked[name='model']").val();
        var width = $("#width").val();
        var height = $("#height").val(); 
        var valuetype = $("#valuetype").val();

        var html = '<input type="text" id="' + id + '" name="' + id + '" type1="flow_textarea" isflow="1" value="文本域" ';
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
            html += 'style="width:80%; height:40px;" ';
        }
        if (valuetype)
        {
            html += 'valuetype="' + valuetype + '" ';
        }
        html += 'defaultvalue="' + defaultvalue + '" ';
        if (parseInt(maxlength) > 0)
        {
            html += 'maxlength="' + maxlength + '" ';
        }
        if (events.length > 0)
        {
            html += 'eventsid="' + eventsid + '" ';
            setEvents(eventsid);
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