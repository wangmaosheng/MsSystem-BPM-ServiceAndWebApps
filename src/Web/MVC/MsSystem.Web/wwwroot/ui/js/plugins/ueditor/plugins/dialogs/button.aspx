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
                    <th>宽度:</th>
                    <td><input type="text" id="width" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>高度:</th>
                    <td><input type="text" id="height"  class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>文本:</th>
                    <td><input type="text" id="value"  class="mytext" style="width:150px" /></td>
                </tr>
            </table>    
        </div>

        <div id="text_event" class="panel">
          <%Server.Execute("events.aspx"); %>
        </div>
    </div>
</div>

<script type="text/javascript">
    var oNode = null, thePlugins = 'formbutton';
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
        //biddingFileds(attJSON, oNode ? $(oNode).attr("id") : "", $("#bindfiled"));
        if (oNode)
        {
            $text = $(oNode);
            $("#width").val($text.attr('width'));
            $("#height").val($text.attr('height'));
            $("#value").val($text.attr('value'));

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
        var width = $("#width").val();
        var height = $("#height").val();
        var value = $("#value").val();

        var html = '<input type="button" class="mybutton" type1="flow_button" isflow="1" value="' + value + '" ';
        if (width)
        {
            html += 'style="width:' + width + '" ';
        }
        if (height)
        {
            html += 'height="' + height + '" ';
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