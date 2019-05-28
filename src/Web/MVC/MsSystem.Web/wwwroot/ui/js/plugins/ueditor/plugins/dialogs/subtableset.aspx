<%@ Page Language="C#" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" src="../common.js"></script>
    <%=WebMvc.Common.Tools.IncludeFiles %>
</head>
<body>
<% 
    WebMvc.Common.Tools.CheckLogin();
    Business.Platform.WorkFlowForm workFlowFrom = new Business.Platform.WorkFlowForm();
    string defaultOptions = workFlowFrom.GetDefaultValueSelect("");
%>
<table cellpadding="0" cellspacing="1" border="0" width="98%" style="margin-top:6px;" class="formtable" id="maintable">
    <tr>
        <th style="width:100px;">编辑模式：</th>
        <td><select class="myselect" id="editmode" style="width:227px" onchange="editmode_change(this.value);"><%=workFlowFrom.GetEditmodeOptions("") %></select></td>
    </tr>
    <tr id="mode_text" style="display:none;">
        <td colspan="2">
            <table cellpadding="0" cellspacing="1" border="0" width="100%" class="formtable">
                <tr>
                    <th style="width:100px;">默认值：</th>
                    <td><input type="text" class="mytext" id="text_defaultvalue" style="width:250px; margin-right:6px;"/><select class="myselect" onchange="setDefaultValue(document.getElementById('text_defaultvalue'), this.value);" style="width:100px"><%=workFlowFrom.GetDefaultValueSelect("") %></select></td>
                </tr>
                <tr>
                    <th>值类型：</th>
                    <td><select class="myselect" id="text_valuetype" ><%=workFlowFrom.GetValueTypeOptions("") %></select></td>
                </tr>
                <tr>
                    <th>最大字符数：</th>
                    <td><input type="text" id="text_maxlength" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>宽度：</th>
                    <td><input type="text" id="text_width" class="mytext" style="width:150px" /></td>
                </tr>
            </table>
        </td>
    </tr>
    
    <tr id="mode_textarea" style="display:none;">
        <td colspan="2">
            <table cellpadding="0" cellspacing="1" border="0" width="100%" class="formtable">
                <tr>
                    <th style="width:100px;">默认值：</th>
                    <td><input type="text" class="mytext" id="textarea_defaultvalue" style="width:250px; margin-right:6px;"/><select class="myselect" onchange="setDefaultValue(document.getElementById('textarea_defaultvalue'), this.value);" style="width:100px"><%=workFlowFrom.GetDefaultValueSelect("") %></select></td>
                </tr>
                <tr>
                    <th>值类型：</th>
                    <td><select class="myselect" id="textarea_valuetype" ><%=workFlowFrom.GetValueTypeOptions("") %></select></td>
                </tr>
                <tr>
                    <th>最大字符数：</th>
                    <td><input type="text" id="textarea_maxlength" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>宽度：</th>
                    <td><input type="text" id="textarea_width" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>高度：</th>
                    <td><input type="text" id="textarea_height" class="mytext" style="width:150px" /></td>
                </tr>
            </table>
        </td>
    </tr>

    <tr id="mode_select" style="display:none;">
        <td colspan="2" style="padding:0; margin:0;">
            <div class="mytab">
	        <ul>
		        <li class="mytabli2" for="select_base">属性</li>
		        <li class="mytabli1" for="select_default" onclick="select_default();">默认值</li>
	        </ul>
            <div style="clear:both;"></div>
            </div>
            <div id="select_base" class="mytab_div" style="display:block; padding-bottom:8px;">
            <table cellpadding="0" cellspacing="1" border="0" width="98%" class="formtable" style="margin:0 auto;">
                <tr>
                    <th style="width:110px;">宽度：</th>
                    <td><input type="text" id="select_width" class="mytext" style="width:150px" />
                        <input type="checkbox" name="select_hasempty" id="select_hasempty" value="1" style="vertical-align:middle; margin-left:15px;" />
                        <label style="vertical-align:middle;" for="select_hasempty">是否添加空选项</label>
                    </td>
                </tr>
                <tr>
                    <th><input type="radio" value="select_dsdict" name="select_ds" style="vertical-align:middle;" id="select_ds1" /><label style="vertical-align:middle;" for="select_ds1">数据源(字典):</label></th>
                    <td><input type="text" class="mydict" id="select_ds_dict" more="0" value="" style="width:200px;" /></td>
                </tr>
                <tr>
                    <th><input type="radio" value="select_dssql" name="select_ds" style="vertical-align:middle;" id="select_ds2"/><label style="vertical-align:middle;" for="select_ds2">数据源(SQL):</label></th>
                    <td>
                        <div><textarea cols="1" rows="1" class="mytextarea"  id="select_ds_sql" style="width:99%; height:70px;"></textarea></div>
                        <div style="margin:3px 0;">格式：SELECT 值字段,标题字段 FROM 表名 WHERE 条件</div>
                    </td>
                </tr>
                <tr>
                    <th><input type="radio" value="select_dsstring" name="select_ds" style="vertical-align:middle;" id="select_ds3"/><label style="vertical-align:middle;" for="select_ds3">数据源(表达式):</label></th>
                    <td>
                        <div><textarea cols="1" rows="1" class="mytextarea" id="select_ds_string" style="width:99%; height:70px;"></textarea></div>
                        <div>
                            <div style="margin:3px 0;">格式：值0,标题0;值1,标题1</div>
                            <div>示例：0,事假;1,病假;2,年假;3,婚假</div> 
                        </div>
                    </td>
                </tr>
            </table>
            </div>
            <div id="select_default" class="mytab_div" style="display:none; height:290px; overflow:auto;">
            </div>
        </td>
    </tr>

    <tr id="mode_checkbox" style="display:none;">
        <td colspan="2">
            <div class="mytab">
	        <ul>
		        <li class="mytabli2" for="checkbox_base">属性</li>
		        <li class="mytabli1" for="checkbox_default" onclick="checkbox_default();">默认值</li>
	        </ul>
            <div style="clear:both;"></div>
            </div>
            <div id="checkbox_base" class="mytab_div" style="display:block; padding-bottom:8px;">
            <table cellpadding="0" cellspacing="1" border="0" width="98%" class="formtable" style="margin:0 auto;">
                <tr>
                    <th style="width:110px;"><input type="radio" value="checkbox_dsdict" name="checkbox_ds" style="vertical-align:middle;" id="checkbox_ds1" /><label style="vertical-align:middle;" for="checkbox_ds1">数据源(字典):</label></th>
                    <td><input type="text" class="mydict" id="checkbox_ds_dict" more="0" value="" style="width:200px;" /></td>
                </tr>
                <tr>
                    <th><input type="radio" value="checkbox_dssql" name="checkbox_ds" style="vertical-align:middle;" id="checkbox_ds2"/><label style="vertical-align:middle;" for="checkbox_ds2">数据源(SQL):</label></th>
                    <td>
                        <div><textarea cols="1" rows="1" class="mytextarea"  id="checkbox_ds_sql" style="width:99%; height:70px;"></textarea></div>
                        <div style="margin:3px 0;">格式：SELECT 值字段,标题字段 FROM 表名 WHERE 条件</div>
                    </td>
                </tr>
                <tr>
                    <th><input type="radio" value="checkbox_dsstring" name="checkbox_ds" style="vertical-align:middle;" id="checkbox_ds3"/><label style="vertical-align:middle;" for="checkbox_ds3">数据源(表达式):</label></th>
                    <td>
                        <div><textarea cols="1" rows="1" class="mytextarea" id="checkbox_ds_string" style="width:99%; height:70px;"></textarea></div>
                        <div>
                            <div style="margin:3px 0;">格式：值0,标题0;值1,标题1</div>
                            <div>示例：0,事假;1,病假;2,年假;3,婚假</div> 
                        </div>
                    </td>
                </tr>
            </table>
            </div>
            <div id="checkbox_default" class="mytab_div" style="display:none; height:290px; overflow:auto;">
            </div>
        </td>
    </tr>

    <tr id="mode_radio" style="display:none;">
        <td colspan="2">
            <div class="mytab">
	        <ul>
		        <li class="mytabli2" for="radio_base">属性</li>
		        <li class="mytabli1" for="radio_default" onclick="radio_default();">默认值</li>
	        </ul>
            <div style="clear:both;"></div>
            </div>
            <div id="radio_base" class="mytab_div" style="display:block; padding-bottom:8px;">
            <table cellpadding="0" cellspacing="1" border="0" width="98%" class="formtable" style="margin:0 auto;">
                <tr>
                    <th style="width:110px;"><input type="radio" value="radio_dsdict" name="radio_ds" style="vertical-align:middle;" id="radio_ds1" /><label style="vertical-align:middle;" for="radio_ds1">数据源(字典):</label></th>
                    <td><input type="text" class="mydict" id="radio_ds_dict" more="0" value="" style="width:200px;" /></td>
                </tr>
                <tr>
                    <th><input type="radio" value="radio_dssql" name="radio_ds" style="vertical-align:middle;" id="radio_ds2"/><label style="vertical-align:middle;" for="radio_ds2">数据源(SQL):</label></th>
                    <td>
                        <div><textarea cols="1" rows="1" class="mytextarea"  id="radio_ds_sql" style="width:99%; height:70px;"></textarea></div>
                        <div style="margin:3px 0;">格式：SELECT 值字段,标题字段 FROM 表名 WHERE 条件</div>
                    </td>
                </tr>
                <tr>
                    <th><input type="radio" value="radio_dsstring" name="radio_ds" style="vertical-align:middle;" id="radio_ds3"/><label style="vertical-align:middle;" for="radio_ds3">数据源(表达式):</label></th>
                    <td>
                        <div><textarea cols="1" rows="1" class="mytextarea" id="radio_ds_string" style="width:99%; height:70px;"></textarea></div>
                        <div>
                            <div style="margin:3px 0;">格式：值0,标题0;值1,标题1</div>
                            <div>示例：0,事假;1,病假;2,年假;3,婚假</div> 
                        </div>
                    </td>
                </tr>
            </table>
            </div>
            <div id="radio_default" class="mytab_div" style="display:none; height:290px; overflow:auto;">
            </div>
        </td>
    </tr>

    <tr id="mode_datetime" style="display:none;">
        <td colspan="2">
            <table cellpadding="0" cellspacing="1" border="0" width="100%" class="formtable">
                <tr>
                    <th style="width:100px;">默认值：</th>
                    <td><input type="text" class="mytext" id="datetime_defaultvalue" style="width:250px; margin-right:6px;"/><select class="myselect" onchange="setDefaultValue(document.getElementById('datetime_defaultvalue'), this.value);" style="width:100px"><%=workFlowFrom.GetDefaultValueSelect("") %></select></td>
                </tr>
               
                <tr>
                    <th>宽度：</th>
                    <td><input type="text" id="datetime_width" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>选择范围：</th>
                    <td>
                        <input type="text" id="datetime_min" class="mycalendar" style="width:100px;" />
                        &nbsp;至&nbsp;<input type="text" id="datetime_max" class="mycalendar" style="width:100px;" />
                    </td>
                </tr>
                <tr>
                    <th>是否选时间：</th>
                    <td><input type="checkbox" id="datetime_istime" value="1" style="vertical-align:middle;" /><label for="datetime_istime" style="vertical-align:middle;">是否允许选择时间</label></td>
                </tr>
            </table>
        </td>
    </tr>

    <tr id="mode_org" style="display:none;">
        <td colspan="2">
            <table cellpadding="0" cellspacing="1" border="0" width="100%" class="formtable">
                <tr>
                    <th style="width:100px;">默认值：</th>
                    <td><select class="myselect" id="org_defaultvalue" style="width:227px"><%=defaultOptions %></select></td>
                </tr>
                <tr>
                    <th>宽度：</th>
                    <td><input type="text" id="org_width" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>选择范围：</th>
                    <td>
                        <div>
                            <%=workFlowFrom.GetOrgRangeRadios("org_rang","") %>
                        
                        <input type="radio" name="org_rang" value="2" id="org_rang_3" style="vertical-align:middle;"/><label for="org_rang_3" style="vertical-align:middle;">自定义</label>：<input type="text" id="org_rang1" class="mymember" style="width:150px;"/></div>
                    </td>
                </tr>
                <tr>
                    <th>选择类型：</th>
                    <td><%=workFlowFrom.GetOrgSelectTypeCheckboxs("org_type","") %></td>
                </tr>
                <tr>
                    <th>多选：</th>
                    <td><input type="checkbox" value="1" id="org_more"  style="vertical-align:middle;" /><label for="org_more" style="vertical-align:middle;">是否可以多选</label></td>
                </tr>
            </table>
        </td>
    </tr>

    <tr id="mode_dict" style="display:none;">
        <td colspan="2">
            <table cellpadding="0" cellspacing="1" border="0" width="100%" class="formtable">
                <tr>
                    <th style="width:100px;">宽度：</th>
                    <td><input type="text" id="dict_width" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>选择范围：</th>
                    <td>
                        <div style="padding-top:5px;"><input type="text" id="dict_rang" class="mydict" style="width:200px;"/></div>
                    </td>
                </tr>
                <tr>
                    <th>是否多选：</th>
                    <td><input type="checkbox" id="dict_more" value="1" style="vertical-align:middle;" /><label for="dict_more" style="vertical-align:middle;">是否允许多选</label></td>
                </tr>
            </table>
        </td>
    </tr>

    <tr id="mode_files" style="display:none;">
        <td colspan="2">
            <table cellpadding="0" cellspacing="1" border="0" width="100%" class="formtable">
                <tr>
                    <th style="width:100px;">宽度：</th>
                    <td><input type="text" id="files_width" class="mytext" style="width:150px" /></td>
                </tr>
                <tr>
                    <th>文件类型：</th>
                    <td>
                        <div style="margin-top:3px;"><input type="text" id="files_filetype" class="mytext" style="width:97%" /></div>
                        <div style="margin-top:3px;">格式：*.jpg;*.png;*.gif;*.doc;*.docx</div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>

</table>
<div class="buttondiv">
    <input type="submit" value=" 确 定 " onclick="confirm1();" class="mybutton" />
    <input type="button" class="mybutton" value=" 取 消 " style="margin-left: 5px;" onclick="closewin();" />
</div>
<script type="text/javascript">
    var win = new RoadUI.Window();
    var eid = '<%=Request.QueryString["eid"]%>';
    var ele = win.getOpenerElement(eid);
    var elehidden = win.getOpenerElement(eid + "_hidden");
    var dbconn = '<%=Request.QueryString["dbconn"]%>';
    var editjsonpublic = {};
    $(function ()
    {
        if (elehidden.size() > 0)
        {
            var jsonstr = elehidden.val();
            if (jsonstr.length > 0)
            {
                var editjson = JSON.parse(jsonstr);
                var editmode = editjson.editmode;
                editjsonpublic = editjson;
                $("#editmode").val(editmode);
                $("#mode_" + editmode).show();
                switch (editmode)
                {
                    case "text":
                        $("#text_defaultvalue").val(editjson.text_defaultvalue);
                        $("#text_valuetype").val(editjson.text_valuetype);
                        $("#text_maxlength").val(editjson.text_maxlength);
                        $("#text_width").val(editjson.text_width);
                        break;
                    case "textarea":
                        $("#textarea_defaultvalue").val(editjson.textarea_defaultvalue);
                        $("#textarea_valuetype").val(editjson.textarea_valuetype);
                        $("#textarea_maxlength").val(editjson.textarea_maxlength);
                        $("#textarea_width").val(editjson.textarea_width);
                        $("#textarea_height").val(editjson.textarea_height);
                        break;
                    case "select":
                        $("#select_width").val(editjson.select_width);
                        $("input[name='select_ds'][value='" + editjson.select_ds + "']").prop("checked", true);
                        $("#select_ds_dict").val(editjson.select_ds_dict);
                        $("#select_ds_sql").val(editjson.select_ds_sql);
                        $("#select_ds_string").val(editjson.select_ds_string);
                        $("#select_hasempty").prop("checked", "1" == editjson.select_hasempty);
                        new RoadUI.Dict().setValue($("#select_ds_dict"));
                        break;
                    case "checkbox":
                        $("input[name='checkbox_ds'][value='" + editjson.checkbox_ds + "']").prop("checked", true);
                        $("#checkbox_ds_dict").val(editjson.checkbox_ds_dict);
                        $("#checkbox_ds_sql").val(editjson.checkbox_ds_sql);
                        $("#checkbox_ds_string").val(editjson.checkbox_ds_string);
                        new RoadUI.Dict().setValue($("#checkbox_ds_dict"));
                        break;
                    case "radio":
                        $("input[name='radio_ds'][value='" + editjson.radio_ds + "']").prop("checked", true);
                        $("#radio_ds_dict").val(editjson.radio_ds_dict);
                        $("#radio_ds_sql").val(editjson.radio_ds_sql);
                        $("#radio_ds_string").val(editjson.radio_ds_string);
                        new RoadUI.Dict().setValue($("#radio_ds_dict"));
                        break;
                    case "datetime":
                        $("#datetime_defaultvalue").val(editjson.datetime_defaultvalue);
                        $("#datetime_width").val(editjson.datetime_width);
                        $("#datetime_min").val(editjson.datetime_min);
                        $("#datetime_max").val(editjson.datetime_max);
                        $("#datetime_istime").prop("checked", "1" == editjson.datetime_istime);
                        break;
                    case "org":
                        $("#org_defaultvalue").val(editjson.org_defaultvalue);
                        $("#org_width").val(editjson.org_width);
                        $("input[name='org_rang'][value='" + editjson.org_rang + "']").prop("checked", true);
                        $("#org_rang1").val(editjson.org_rang1);
                        new RoadUI.Member().setValue($("#org_rang1"));
                        var org_type = editjson.org_type;
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
                        $("#org_more").prop("checked", "1" == editjson.org_more);
                        break;
                    case "dict":
                         $("#dict_width").val(editjson.dict_width);
                         $("#dict_rang").val(editjson.dict_rang);
                         new RoadUI.Dict().setValue($("#dict_rang"));
                         $("#dict_more").prop("checked", "1" == editjson.dict_more);
                         break;
                    case "files":
                         $("#files_width").val(editjson.files_width);
                         $("#files_filetype").val(editjson.files_filetype);
                         break;
                }
            }
        }
    });
    function editmode_change(mode)
    {
        $("#maintable tr[id^='mode_']").hide();
        $("#mode_" + mode).show();
    }
    function select_default()
    {
        var $listDiv = $("#select_default");
        var datasource = $(":checked[name='select_ds']").val();
        var dvalue = $(":checked", $listDiv).val() || (editjsonpublic.select_default || "");
        $listDiv.html('');
        if ("select_dsstring" == datasource)
        {
            var custom_string = ($("#select_ds_string").val() || "").split(';');
            for (var i = 0; i < custom_string.length; i++)
            {
                var titlevalue = custom_string[i].split(',');
                if (titlevalue.length != 2)
                {
                    continue;
                }
                var title = titlevalue[0];
                var value = titlevalue[1];
                var option = '<div><input type="radio" ' + (value == dvalue ? 'checked="checked"' : '') + ' value="' + value + '" id="defaultvalue_' + value + '" style="vertical-align:middle;" name="defaultvalue"/><label style="vertical-align:middle;" for="defaultvalue_' + value + '">' + title + '(' + value + ')</label></div>';
                $listDiv.append(option);
            }
        }
        else if ("select_dsdict" == datasource)
        {
            var dictid = $("#select_ds_dict").val();
            $.ajax({
                url: "getdictchilds.aspx?dictid=" + dictid, cache: false, async: false, dataType: "json", success: function (json)
                {
                    for (var i = 0; i < json.length; i++)
                    {
                        var title = json[i].title;
                        var value = json[i].id;
                        var option = '<div><input type="radio" ' + (value == dvalue ? 'checked="checked"' : '') + ' value="' + value + '" id="defaultvalue_' + value + '" style="vertical-align:middle;" name="defaultvalue"/><label style="vertical-align:middle;" for="defaultvalue_' + value + '">' + title + '(' + value + ')</label></div>';
                        $listDiv.append(option);
                    }
                }
            });
        }
        else if ("select_dssql" == datasource)
        {
            var sql = $("#select_ds_sql").val();
            $.ajax({
                url: "getsqljson.aspx", type: "post", data: { sql: sql, conn: dbconn }, cache: false, async: false, dataType: "json", success: function (json)
                {
                    for (var i = 0; i < json.length; i++)
                    {
                        var title = json[i].title;
                        var value = json[i].id;
                        var option = '<div><input type="radio" ' + (value == dvalue ? 'checked="checked"' : '') + ' value="' + value + '" id="defaultvalue_' + value + '" style="vertical-align:middle;" name="defaultvalue"/><label style="vertical-align:middle;" for="defaultvalue_' + value + '">' + title + '(' + value + ')</label></div>';
                        $listDiv.append(option);
                    }
                }
            });
        }
    }

    function checkbox_default()
    {
        var $listDiv = $("#checkbox_default");
        var datasource = $(":checked[name='checkbox_ds']").val();
        var checkbox_default = [];
        $(":checked", $("#checkbox_default")).each(function ()
        {
            checkbox_default.push($(this).val() || "");
        });
        var dvalue = checkbox_default.length > 0 ? checkbox_default.join('') : editjsonpublic.checkbox_default || "";
        $listDiv.html('');
        if ("checkbox_dsstring" == datasource)
        {
            var custom_string = ($("#checkbox_ds_string").val() || "").split(';');
            for (var i = 0; i < custom_string.length; i++)
            {
                var titlevalue = custom_string[i].split(',');
                if (titlevalue.length != 2)
                {
                    continue;
                }
                var title = titlevalue[0];
                var value = titlevalue[1];
                var option = '<div><input type="checkbox" ' + (dvalue.indexOf(value) >= 0 ? 'checked="checked"' : '') + ' value="' + value + '" id="defaultvalue_' + value + '" style="vertical-align:middle;" name="defaultvalue"/><label style="vertical-align:middle;" for="defaultvalue_' + value + '">' + title + '(' + value + ')</label></div>';
                $listDiv.append(option);
            }
        }
        else if ("checkbox_dsdict" == datasource)
        {
            var dictid = $("#checkbox_ds_dict").val();
            $.ajax({
                url: "getdictchilds.aspx?dictid=" + dictid, cache: false, async: false, dataType: "json", success: function (json)
                {
                    for (var i = 0; i < json.length; i++)
                    {
                        var title = json[i].title;
                        var value = json[i].id;
                        var option = '<div><input type="checkbox" ' + (dvalue.indexOf(value) >= 0 ? 'checked="checked"' : '') + ' value="' + value + '" id="defaultvalue_' + value + '" style="vertical-align:middle;" name="defaultvalue"/><label style="vertical-align:middle;" for="defaultvalue_' + value + '">' + title + '(' + value + ')</label></div>';
                        $listDiv.append(option);
                    }
                }
            });
        }
        else if ("checkbox_dssql" == datasource)
        {
            var sql = $("#checkbox_ds_sql").val();
            $.ajax({
                url: "getsqljson.aspx", type: "post", data: { sql: sql, conn: dbconn }, cache: false, async: false, dataType: "json", success: function (json)
                {
                    for (var i = 0; i < json.length; i++)
                    {
                        var title = json[i].title;
                        var value = json[i].id;
                        var option = '<div><input type="checkbox" ' + (dvalue.indexOf(value) >= 0 ? 'checked="checked"' : '') + ' value="' + value + '" id="defaultvalue_' + value + '" style="vertical-align:middle;" name="defaultvalue"/><label style="vertical-align:middle;" for="defaultvalue_' + value + '">' + title + '(' + value + ')</label></div>';
                        $listDiv.append(option);
                    }
                }
            });
        }
    }

    function radio_default()
    {
        var $listDiv = $("#radio_default");
        var datasource = $(":checked[name='radio_ds']").val();
        var dvalue = $(":checked", $listDiv).val() || (editjsonpublic.radio_default || "");
        $listDiv.html('');
        if ("radio_dsstring" == datasource)
        {
            var custom_string = ($("#radio_ds_string").val() || "").split(';');
            for (var i = 0; i < custom_string.length; i++)
            {
                var titlevalue = custom_string[i].split(',');
                if (titlevalue.length != 2)
                {
                    continue;
                }
                var title = titlevalue[0];
                var value = titlevalue[1];
                var option = '<div><input type="radio" ' + (value == dvalue ? 'checked="checked"' : '') + ' value="' + value + '" id="defaultvalue_' + value + '" style="vertical-align:middle;" name="defaultvalue"/><label style="vertical-align:middle;" for="defaultvalue_' + value + '">' + title + '(' + value + ')</label></div>';
                $listDiv.append(option);
            }
        }
        else if ("radio_dsdict" == datasource)
        {
            var dictid = $("#radio_ds_dict").val();
            $.ajax({
                url: "getdictchilds.aspx?dictid=" + dictid, cache: false, async: false, dataType: "json", success: function (json)
                {
                    for (var i = 0; i < json.length; i++)
                    {
                        var title = json[i].title;
                        var value = json[i].id;
                        var option = '<div><input type="radio" ' + (value == dvalue ? 'checked="checked"' : '') + ' value="' + value + '" id="defaultvalue_' + value + '" style="vertical-align:middle;" name="defaultvalue"/><label style="vertical-align:middle;" for="defaultvalue_' + value + '">' + title + '(' + value + ')</label></div>';
                        $listDiv.append(option);
                    }
                }
            });
        }
        else if ("radio_dssql" == datasource)
        {
            var sql = $("#radio_ds_sql").val();
            $.ajax({
                url: "getsqljson.aspx", type: "post", data: { sql: sql, conn: dbconn }, cache: false, async: false, dataType: "json", success: function (json)
                {
                    for (var i = 0; i < json.length; i++)
                    {
                        var title = json[i].title;
                        var value = json[i].id;
                        var option = '<div><input type="radio" ' + (value == dvalue ? 'checked="checked"' : '') + ' value="' + value + '" id="defaultvalue_' + value + '" style="vertical-align:middle;" name="defaultvalue"/><label style="vertical-align:middle;" for="defaultvalue_' + value + '">' + title + '(' + value + ')</label></div>';
                        $listDiv.append(option);
                    }
                }
            });
        }
    }

    function confirm1()
    {
        var mode = $("#editmode").val();
        var editjson = {};
        var edittxt = "";
        editjson.editmode = mode;
        switch (mode)
        {
            case "text":
                edittxt = "文本框";
                editjson.text_defaultvalue = $("#text_defaultvalue").val();
                editjson.text_valuetype = $("#text_valuetype").val();
                editjson.text_maxlength = $("#text_maxlength").val();
                editjson.text_width = $("#text_width").val();
                break;
            case "textarea":
                edittxt = "文本域";
                editjson.textarea_defaultvalue = $("#textarea_defaultvalue").val();
                editjson.textarea_valuetype = $("#textarea_valuetype").val();
                editjson.textarea_maxlength = $("#textarea_maxlength").val();
                editjson.textarea_width = $("#textarea_width").val();
                editjson.textarea_height = $("#textarea_height").val();
                break;
            case "select":
                edittxt = "下拉列表";
                editjson.select_width = $("#select_width").val();
                editjson.select_ds = $(":checked[name='select_ds']").val();
                editjson.select_ds_dict = $("#select_ds_dict").val();
                editjson.select_ds_sql = $("#select_ds_sql").val();
                editjson.select_ds_string = $("#select_ds_string").val();
                editjson.select_hasempty = $("#select_hasempty").prop("checked") ? "1" : "0";
                editjson.select_default = $(":checked", $("#select_default")).val() || "";
                break;
            case "checkbox":
                edittxt = "复选框";
                editjson.checkbox_ds = $(":checked[name='checkbox_ds']").val();
                editjson.checkbox_ds_dict = $("#checkbox_ds_dict").val();
                editjson.checkbox_ds_sql = $("#checkbox_ds_sql").val();
                editjson.checkbox_ds_string = $("#checkbox_ds_string").val();
                var checkbox_default = [];
                $(":checked", $("#checkbox_default")).each(function ()
                {
                    checkbox_default.push($(this).val() || "");
                });
                editjson.checkbox_default = checkbox_default.join(',');
                break;
            case "radio":
                edittxt = "单选框";
                editjson.radio_ds = $(":checked[name='radio_ds']").val();
                editjson.radio_ds_dict = $("#radio_ds_dict").val();
                editjson.radio_ds_sql = $("#radio_ds_sql").val();
                editjson.radio_ds_string = $("#radio_ds_string").val();
                editjson.radio_default = $(":checked", $("#radio_default")).val() || "";
                break;
            case "datetime":
                edittxt = "日期时间";
                editjson.datetime_defaultvalue = $("#datetime_defaultvalue").val();
                editjson.datetime_width = $("#datetime_width").val();
                editjson.datetime_min = $("#datetime_min").val();
                editjson.datetime_max = $("#datetime_max").val();
                editjson.datetime_istime = $("#datetime_istime").prop("checked") ? "1" : "0";
                break;
            case "org":
                edittxt = "组织机构";
                editjson.org_defaultvalue = $("#org_defaultvalue").val();
                editjson.org_width = $("#org_width").val();
                editjson.org_rang = $(":checked[name='org_rang']").val();
                editjson.org_rang1 = $("#org_rang1").val();
                var org_type = ",";
                $(":checked[name='org_type']").each(function ()
                {
                    org_type += $(this).val() + ",";
                });
                editjson.org_type = org_type;
                editjson.org_more = $("#org_more").prop("checked") ? "1" : "0";
                break;
            case "dict":
                edittxt = "数据字典";
                editjson.dict_width = $("#dict_width").val();
                editjson.dict_rang = $("#dict_rang").val();
                editjson.dict_more = $("#dict_more").prop("checked") ? "1" : "0";
                break;
            case "files":
                edittxt = "附件";
                editjson.files_width = $("#files_width").val();
                editjson.files_filetype = $("#files_filetype").val();
                break;
        }

        editjson.title = edittxt;
        if(ele.size()>0) ele.val(edittxt);
        if(elehidden.size()>0) elehidden.val(JSON.stringify(editjson));
        win.close();
        return false;
    }

    function closewin()
    {
        win.close();
        return false;
    }
</script>
</body>
</html>