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
                    <th style="width:80px;">绑定字段：</th>
                    <td><select class="myselect" id="bindfiled" style="width:227px"></select></td>
                </tr>
                <tr>
                    <th>默认值：</th>
                    <td><input type="text" class="mytext" id="defaultvalue" style="width:290px; margin-right:6px;"/><select class="myselect" onchange="setDefaultValue(document.getElementById('defaultvalue'), this.value);" style="width:150px"><%=workFlowFrom.GetDefaultValueSelect("") %></select></td>
                </tr>
                <tr>
                    <th>宽度：</th>
                    <td><input type="text" id="width" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>选择范围：</th>
                    <td>
                        <span>
                        <input type="text" id="min" class="mycalendar" style="width:100px;" />
                        &nbsp;至&nbsp;<input type="text" id="max" class="mycalendar" style="width:100px;" />
                        </span>
                        <span>
                            <input type="checkbox" id="daybefor" value="1" style="vertical-align:middle;" /><label for="daybefor" style="vertical-align:middle;">今天以前</label>
                        </span>
                        <span>
                            <input type="checkbox" id="dayafter" value="1" style="vertical-align:middle;" /><label for="dayafter" style="vertical-align:middle;">今天以后</label>
                        </span>
                        <span>
                            <input type="checkbox" id="currentmonth" value="1" style="vertical-align:middle;" /><label for="currentmonth" style="vertical-align:middle;">当前月</label>
                        </span>
                    </td>
                </tr>
                <tr>
                    <th>时间：</th>
                    <td><input type="checkbox" id="istime" value="1" style="vertical-align:middle;" /><label for="istime" style="vertical-align:middle;">允许选择时间</label>
                    </td>
                </tr>
            </table>
        </div>

        <div id="text_event" class="panel">
           <%Server.Execute("events.aspx"); %>
        </div>
    </div>
</div>
<script type="text/javascript">
    var oNode = null, thePlugins = 'formdatetime';
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
            $("#min").val($text.attr('mindate'));
            $("#max").val($text.attr('maxdate'));
            $("#istime").prop('checked', "1" == $text.attr('istime'));
            $("#daybefor").prop('checked', "1" == $text.attr('daybefor'));
            $("#dayafter").prop('checked', "1" == $text.attr('dayafter'));
            $("#currentmonth").prop('checked', "1" == $text.attr('currentmonth'));

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
        var defaultvalue = $("#defaultvalue").val();
        var id = attJSON.dbconn && attJSON.dbtable && bindfiled ? attJSON.dbtable + '.' + bindfiled : "";
        var width = $("#width").val();
        var istime = $("#istime").prop('checked') ? "1" : "0";
        var min = $("#min").val();
        var max = $("#max").val();
        var daybefor = $("#daybefor").prop('checked') ? "1" : "0";
        var dayafter = $("#dayafter").prop('checked') ? "1" : "0";
        var currentmonth = $("#currentmonth").prop('checked') ? "1" : "0";
        
        var html = '<input type="text" type1="flow_datetime" id="' + id + '" name="' + id + '" value="日期时间选择" ';
        if (width)
        {
            html += 'style="width:' + width + '" ';
            html += 'width1="' + width + '" ';
        }
        html += 'defaultvalue="' + defaultvalue + '" ';
        if (parseInt(max) > 0)
        {
            html += 'maxdate="' + max + '" ';
        }
        if (parseInt(min) > 0)
        {
            html += 'mindate="' + min + '" ';
        }
        html += 'istime="' + istime + '" ';
        html += 'daybefor="' + daybefor + '" ';
        html += 'dayafter="' + dayafter + '" ';
        html += 'currentmonth="' + currentmonth + '" ';

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