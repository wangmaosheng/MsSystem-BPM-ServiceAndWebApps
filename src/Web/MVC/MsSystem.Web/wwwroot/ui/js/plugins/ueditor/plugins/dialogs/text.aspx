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
                    <td><select class="myselect" id="bindfiled" style="width:255px"></select></td>
                </tr>
                <tr>
                    <th>默认值:</th>
                    <td><input type="text" class="mytext" id="defaultvalue" style="width:290px; margin-right:6px;"/><select class="myselect" onchange="setDefaultValue(document.getElementById('defaultvalue'), this.value);" style="width:150px"><%=workFlowFrom.GetDefaultValueSelect("") %></select></td>
                </tr>
                <tr>
                    <th>宽度:</th>
                    <td><input type="text" id="width" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>最大字符数:</th>
                    <td><input type="text" id="maxlength"  class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>输入类型:</th>
                    <td><%=workFlowFrom.GetInputTypeRadios("inputtype","","") %></td>
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
    var oNode = null, thePlugins = 'formtext';
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
            if ($text.attr('width1')) $("#width").val($text.attr('width1'));
            $("input[name='inputtype'][value='" + $text.attr('type') + "']").prop('checked', true);
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
        var width = $("#width").val();
        var defaultvalue = $("#defaultvalue").val();
        var maxlength = $("#maxlength").val();
        var inputtype = $(":checked[name='inputtype']").val();
        var valuetype = $("#valuetype").val();

        var html = '<input type="' + (inputtype || 'text') + '" id="' + id + '" type1="flow_text" name="' + id + '" value="文本框" ';
        if (width)
        {
            html += 'style="width:' + width + '" ';
            html += 'width1="' + width + '" ';
        }
        html += 'defaultvalue="'+defaultvalue+'" ';
        if (parseInt(maxlength) > 0)
        {
            html += 'maxlength="' + maxlength + '" ';
        }
        if (valuetype)
        {
            html += 'valuetype="' + valuetype + '" ';
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