<style type="text/css">
    .wrapper {zoom:1;width:97%; margin:0 auto; padding:8px 3px 0 0;position:relative;}
    .tabhead {float:left;}
    .tabbody {width:100%; height:240px; position:relative; clear:both; padding:3px 0 0 2px;}
    .tabbody .panel {position:absolute; background:#fff; overflow:hidden; display:none; padding:3px; }
    .tabbody .panel.focus {width:98%; display:block;}
    #event_script { width:99%; height:160px; font-family:Verdana;}
</style>
<table cellpadding="0" cellspacing="1" border="0" width="100%" class="formtable">
    <tr>
        <th style="width:80px;">事件:</th>
        <td>
            <select class="myselect" id="event_name" onchange="event_change(this.value);"><option value=""></option><%=new Business.Platform.WorkFlowForm().GetEventOptions("","","") %></select>
        </td>
    </tr>
    <tr>
        <th>事件脚本:</th>
        <td>
            <div style="font-family:Verdana;">function methodName(srcObj){</div>
            <textarea class="mytextarea" id="event_script" onblur="saveEvent();"></textarea>
            <div style="font-family:Verdana;">}</div>
        </td>
    </tr>
</table>