UE.compule = {
    getDefaultValue: function (defaultValue)
    {
        return defaultValue || "";//默认值方式更新，已不用这种方法来得到默认值。
        /*
        var dValue = "";
        if (defaultValue && $.trim(defaultValue).length > 0)
        {
            switch ($.trim(defaultValue))
            {
                case "10"://当前步骤用户ID
                    dValue = "u_@Business.Platform.Users.CurrentUserID.ToString()";
                    break;
                case "11"://当前步骤用户姓名
                    dValue = "@Business.Platform.Users.CurrentUserName";
                    break;
                case "12"://当前步骤用户部门ID
                    dValue = "@Business.Platform.Users.CurrentDeptID";
                    break;
                case "13"://当前步骤用户部门名称
                    dValue = "@Business.Platform.Users.CurrentDeptName";
                    break;
                case "14"://流程发起者ID
                    dValue = "u_@(new Business.Platform.WorkFlowTask().GetFirstSnderID(FlowID.ToGuid(), GroupID.ToGuid(), true))";
                    break;
                case "15"://流程发起者姓名
                    dValue = "@(new Business.Platform.Users().GetName(new Business.Platform.WorkFlowTask().GetFirstSnderID(FlowID.ToGuid(), GroupID.ToGuid(), true)))";
                    break;
                case "20"://短日期
                    dValue = "@Utility.DateTimeNew.ShortDate";
                    break;
                case "21"://长日期
                    dValue = "@Utility.DateTimeNew.LongDate";
                    break;
                case "22"://短时间
                    dValue = "@Utility.DateTimeNew.ShortTime";
                    break;
                case "23"://长时间
                    dValue = "@Utility.DateTimeNew.LongTime";
                    break;
                case "24"://短日期时间
                    dValue = "@Utility.DateTimeNew.ShortDateTime";
                    break;
                case "25"://长日期时间
                    dValue = "@Utility.DateTimeNew.LongDateTime";
                    break;
                case "30"://流程名称
                    dValue = "@Html.Raw(BWorkFlow.GetFlowName(FlowID.ToGuid()))"
                    break;
                case "31"://步骤名称
                    dValue = "@Html.Raw(BWorkFlow.GetStepName(StepID.ToGuid(), FlowID.ToGuid(), true))"
                    break;
                default:
                    dValue = defaultValue;
                    break;
            }
        }
        return dValue;
        */
    },
    getEventScript: function ($control)
    {
        var eventsid = $control.attr("eventsid");
        $control.removeAttr("eventsid");
        if (!eventsid || $.trim(eventsid).length == 0)
        {
            return;
        }
        var events = getEvents(eventsid);
        if (!$.isArray(events))
        {
            return;
        }
        var script = '';
        for (var i = 0; i < events.length; i++)
        {
            if (!events[i].name || $.trim(events[i].name).length == 0 || !events[i].script || $.trim(events[i].script).length == 0)
            {
                continue;
            }
            var functionName = events[i].name + "_" + eventsid;
            $control.attr(events[i].name, functionName + " (this);");
            script += '<script type="text/javascript">function ' + functionName + '(srcObj){' + events[i].script + '}</script>';
        }
        $control.after(script)
    },
    getEventScriptString:function(eventsid)
    {
        var json = { attr: "", script: "" };
        if (!eventsid || $.trim(eventsid).length == 0)
        {
            return json;
        }
        var events = getEvents(eventsid);
        if (!$.isArray(events))
        {
            return json;
        }
        var attr = "", scripts = "";
        for (var i = 0; i < events.length; i++)
        {
            if (!events[i].name || $.trim(events[i].name).length == 0 || !events[i].script || $.trim(events[i].script).length == 0)
            {
                continue;
            }
            var functionName = events[i].name + "_" + eventsid;
            attr += ' ' + events[i].name + '=\'' + functionName + '(this);\''
            scripts += ' <script type="text/javascript">function ' + functionName + '(srcObj){' + events[i].script + '}</script>';
        }
        json.attr = attr;
        json.script = scripts;
        return json;
    },
    getTextHtml: function ($control)
    {
        $control.attr("isflow", "1").attr("value", "").attr("class", "mytext").attr("title", "");
        var defaultValue = $control.attr("defaultvalue");
        $control.attr("value", UE.compule.getDefaultValue(defaultValue));
        $control.removeAttr("defaultvalue").removeAttr("ondblclick").removeAttr("width1");
        UE.compule.getEventScript($control);
    },
    getTextareaHtml: function ($control)
    {
        var id = $control.attr("id");
        var maxlength = $control.attr("maxlength");
        var defaultValue = $control.attr("defaultvalue");
        var defaultValue1 = UE.compule.getDefaultValue(defaultValue);
        var textarea = '<textarea isflow="1" type1="flow_textarea" id="' + id + '" name="' + id + '" class="mytext" ';
        textarea += 'style="' + $control.attr("style") + '"';
        if (maxlength)
        {
            textarea += ' maxlength="' + maxlength + '" ';
        }
        textarea += '>';
        if (defaultValue1)
        {
            textarea += defaultValue1;
        }
        textarea += '</textarea>';
        $textarea = $(textarea);
        $textarea.attr("eventsid", $control.attr("eventsid"));
        $control.after($textarea);
        UE.compule.getEventScript($textarea);
        $control.remove();

    },
    getRadioOrCheckboxHtml: function ($control, type)
    {
        $control.attr("isflow", "1");
        var id = $control.attr("id");
        var datasource = $control.attr("datasource");
        var eventsid = $control.attr("eventsid");
        var eventsJson = { attr: "", script: "" };
        var eventArrs = "", eventScripts = "";
        var defaultvalue = $.trim($control.attr("defaultvalue") || "");
        if (eventsid)
        {
            eventsJson = UE.compule.getEventScriptString(eventsid);
            eventArrs = eventsJson.attr;
            eventScripts = eventsJson.script;
        }
        if ("0" == datasource)//数据字典
        {
            var dictid = $control.attr("dictid");
            var radios = '';
            
            if ('radio' == type)
            {
                radios = '<span>@Html.Raw(BDictionary.GetRadiosByID("' + dictid + '".ToGuid(), "' + id + '", Business.Platform.Dictionary.OptionValueField.ID, "' + defaultvalue + '", "isflow=\'1\' type1=\'flow_radio\'' + eventArrs + '"))' + eventScripts + '</span>';
            }
            else if ('checkbox' == type)
            {
                radios = '<span>@Html.Raw(BDictionary.GetCheckboxsByID("' + dictid + '".ToGuid(), "' + id + '", Business.Platform.Dictionary.OptionValueField.ID, "' + defaultvalue + '", "isflow=\'1\' type1=\'flow_checkbox\'' + eventArrs + '"))' + eventScripts + '</span>';
            }

            $control.after(radios);
            $control.remove();
        }
        else if ("1" == datasource)//自定义
        {
            var customopts = {};
            try
            {
                customopts = JSON.parse($control.attr("customopts"));
            }
            catch (e) { alert($control.attr("id") + " 自定义选项错误!"); }

            var radios = '';
            for (var i = 0; i < customopts.length; i++)
            {
                var title = customopts[i].title;
                var value = customopts[i].value;
                if (!title || !value)
                {
                    continue;
                }
                radios += '<input type="' + type + '" name="' + id + '" id="' + id + '_' + i.toString() + '" value="' + $.trim(value) + '" ' + (defaultvalue == $.trim(value)?'checked="checked"':'') + ' style="vertical-align:middle;" isflow="1" type1="flow_' + type + '"' + eventArrs + '/>';
                radios += '<label for="' + id + '_' + i.toString() + '" style="vertical-align:middle;margin-right:3px;">' + $.trim(title) + '</label>';
            }
            radios += eventScripts;
            $control.after(radios);
            $control.remove();
        }
        else if ("2" == datasource)//SQL
        {
            var sql = $control.attr("sql");
            var radios = '';
            if ("radio" == type)
            {
                radios += '<span>@Html.Raw(new Business.Platform.WorkFlowForm().GetRadioFromSql(DBConnID, "' + sql.replaceAll('"', '\"') + '", "' + id + '", "' + defaultvalue + '", "isflow=\'1\' type1=\'flow_radio\'' + eventArrs + '"))' + eventScripts + '</span>';
            }
            else if ("checkbox" == type)
            {
                radios += '<span>@Html.Raw(new Business.Platform.WorkFlowForm().GetCheckboxFromSql(DBConnID, "' + sql.replaceAll('"', '\"') + '", "' + id + '", "' + defaultvalue + '", "isflow=\'1\' type1=\'flow_checkbox\'' + eventArrs + '"))' + eventScripts + '</span>';
            }
            $control.after(radios);
            $control.remove();
        }
    },
    getSelectHtml: function ($control)
    {
        $control.attr("isflow", "1");
        var id = $control.attr("id");
        var datasource = $control.attr("datasource");
        var width1 = $control.attr("width1");
        var defaultvalue = $.trim($control.attr("defaultvalue") || "");
        var hasempty = $control.attr("hasempty");
        if ("0" == datasource)//数据字典
        {
            var dictid = $control.attr("dictid");
            var radios = '<select class="myselect" id="' + id + '" name="' + id + '" ' + (width1 ? 'style="width:' + width1 + '"' : '') + ' isflow="1" type1="flow_select">';
            if ("1" == hasempty)
            {
                radios += '<option value=""></option>';
            }
            radios += '@Html.Raw(BDictionary.GetOptionsByID("' + dictid + '".ToGuid(), Business.Platform.Dictionary.OptionValueField.ID, "' + defaultvalue + '"))';
            radios += '</select>';
            var $radios = $(radios);
            $radios.attr("eventsid", $control.attr("eventsid"));
            $control.after($radios);
            UE.compule.getEventScript($radios);
            $control.remove();
        }
        else if ("1" == datasource)//自定义
        {
            var customopts = {};
            try
            {
                customopts = JSON.parse($control.attr("customopts"));
            }
            catch (e) { alert($control.attr("id") + " 自定义选项错误!"); }

            var radios = '<select class="myselect" id="' + id + '" name="' + id + '" ' + (width1 ? 'style="width:' + width1 + '"' : '') + ' isflow="1" type1="flow_select">';
            if ("1" == hasempty)
            {
                radios += '<option value=""></option>';
            }
            for (var i = 0; i < customopts.length; i++)
            {
                var title = customopts[i].title;
                var value = customopts[i].value;
                if (!title || !value)
                {
                    continue;
                }
                radios += '<option value="' + value + '" ' + (defaultvalue == $.trim(value) ? 'selected="selected"' : '') + '>' + title + '</option>';
            }
            radios += '</select>';
            var $radios = $(radios);
            $radios.attr("eventsid", $control.attr("eventsid"));
            $control.after($radios);
            UE.compule.getEventScript($radios);
            $control.remove();
        }
        else if ("2" == datasource)//SQL
        {
            var sql = $control.attr("sql");
            var radios = '<select class="myselect" id="' + id + '" name="' + id + '" ' + (width1 ? 'style="width:' + width1 + '"' : '') + ' isflow="1" type1="flow_select">';
            if ("1" == hasempty)
            {
                radios += '<option value=""></option>';
            }
            radios += '@Html.Raw(new Business.Platform.WorkFlowForm().GetOptionsFromSql(DBConnID, "' + sql.replaceAll('"', '\"') + '", "' + defaultvalue + '"))';
            radios += '</select>';
            var $radios = $(radios);
            $radios.attr("eventsid", $control.attr("eventsid"));
            $control.after($radios);
            UE.compule.getEventScript($radios);
            $control.remove();
        }
    },
    getOrgHtml: function ($control)
    {
        $control.attr("isflow", "1").attr("value", "").attr("class", "mymember").attr("title", "");;
        var defaultValue = $control.attr("defaultvalue");
        $control.attr("value", UE.compule.getDefaultValue(defaultValue));
        var org_type = $control.attr("org_type");
        if (org_type)
        {
            $control.attr("dept", org_type.indexOf(",0,") >= 0 ? "1" : "0");
            $control.attr("station", org_type.indexOf(",1,") >= 0 ? "1" : "0");
            $control.attr("user", org_type.indexOf(",2,") >= 0 ? "1" : "0");
            $control.attr("workgroup", org_type.indexOf(",3,") >= 0 ? "1" : "0");
            $control.attr("unit", org_type.indexOf(",4,") >= 0 ? "1" : "0");
        }
        var org_rang = $control.attr("org_rang");
        var rootid = '';
        switch (org_rang)
        {
            case "0": //发起者部门
                rootid = '@BWorkFlowTask.GetFirstSnderDeptID(FlowID.ToGuid(), GroupID.ToGuid())';
                break;
            case "1": //处理者部门
                rootid = '@Business.Platform.Users.CurrentDeptID';
                break;
            case "2": //自定义
                rootid = $control.attr("org_rang1");
                break;
        }
        $control.attr("rootid", rootid).removeAttr("defaultvalue").removeAttr("width1").removeAttr("ondblclick").removeAttr("org_type").removeAttr("org_rang1").removeAttr("org_rang");
    },
    getDictHtml: function ($control)
    {
        $control.attr("isflow", "1").attr("value", "").attr("class", "mydict").attr("title", "");
        $control.removeAttr("width1").removeAttr("ondblclick");
    },
    getDateTimeHtml: function ($control)
    {
        $control.attr("isflow", "1").attr("value", "").attr("class", "mycalendar").attr("title", "");
        var defaultValue = $control.attr("defaultvalue");
        $control.attr("value", UE.compule.getDefaultValue(defaultValue));
        $control.removeAttr("width1").removeAttr("ondblclick");
        UE.compule.getEventScript($control);
    },
    getFilesHtml: function ($control)
    {
        $control.attr("isflow", "1").attr("value", "").attr("class", "myfile").attr("title", "");
        $control.removeAttr("width1").removeAttr("ondblclick");
    },
    getHiddenHtml: function ($control)
    {
        var id = $control.attr("id");
        var defaultValue = $control.attr("defaultvalue");
        var html = '<input type="hidden" id="' + id + '" name="' + id + '" type1="flow_hidden" value="' + UE.compule.getDefaultValue(defaultValue) + '"';
        html += '/>';
        $control.after(html);
        $control.remove();
    },
    getHtmlHtml: function ($control, json)
    {
        formattributeJSON = json || parent.formattributeJSON;
        formattributeJSON.hasEditor = "1";
        var id = $control.attr("id");
        var table = '', field = '';
        var idArray = id.split('.');
        if (idArray.length == 2)
        {
            table = idArray[0];
            field = idArray[1];
        }
        //var maxlength = $control.attr("maxlength");
        var textarea = '<textarea isflow="1" model="html" type1="flow_textarea" id="' + id + '" name="' + id + '" class="mytextarea" ';
        textarea += 'style="' + $control.attr("style") + '"';
        //if (maxlength)
        //{
        //    textarea += ' maxlength="' + maxlength + '" ';
        //}
        textarea += '>';

        textarea += '@Html.Raw(BWorkFlow.GetFromFieldData(initData, "' + table + '","' + field + '"))';

        textarea += '</textarea>';
        $control.after(textarea);
        $control.remove();
    },
    getLabelHtml: function ($control)
    {
        var defaultValue = $control.attr("defaultvalue");
        var label = '<label style="';
        if ($control.attr("fontsize"))
        {
            label += 'font-size:' + $control.attr("fontsize") + ';';
        }
        if ($control.attr("fontcolor"))
        {
            label += 'color:' + $control.attr("fontcolor") + ';';
        }
        if ("1" == $control.attr("fontbold"))
        {
            label += 'font-weight:bold;';
        }
        if ("1" == $control.attr("fontstyle"))
        {
            label += 'font-style:italic;';
        }
        label += '" >';
        label += UE.compule.getDefaultValue(defaultValue);
        label += '</label>';
        $control.after(label);
        $control.remove();
    },
    getGridHtml: function ($control)
    {
        var div = '<div style="margin:0 auto;';
        if ($control.attr("width1"))
        {
            div += 'width:' + $control.attr("width1") + ';';
        }
        if ($control.attr("height1"))
        {
            div += 'height:' + $control.attr("height1") + ';';
            div += 'overflow:auto;';
        }
        div += '" >';
        div += '@Html.Raw(new Business.Platform.WorkFlowForm().GetFormGridHtml(DBConnID, "' + $control.attr("dataformat") + '","' + $control.attr("datasource") + '","' + $control.attr("datasource1") + '"))';
        div += '</div>';
        $control.after(div);
        $control.remove();
    },
    getButtonHtml: function ($control)
    {
        UE.compule.getEventScript($control);
    },

    getSubTableHtml_Text: function (colnumJSON, id, i, iscount)
    {
        var editmode = colnumJSON.editmode;
        var input = '<input type="text" class="mytext" issubflow="1" type1="subflow_text" ';
        input += 'name="' + id + '_' + i + '_' + colnumJSON.name + '" ';
        input += 'id="' + id + '_' + i + '_' + colnumJSON.name + '" ';
        input += 'colname="' + colnumJSON.name + '" ';
        if (editmode.text_width)
        {
            input += 'style="width:' + editmode.text_width + '" ';
        }
        if (editmode.text_maxlength)
        {
            input += 'maxlength="' + parseInt(editmode.text_maxlength) + '" ';
        }
        input += 'value="' + UE.compule.getDefaultValue(editmode.text_defaultvalue) + '" ';
        if (iscount)
        {
            input += 'iscount="1" onblur="formrun.subtableCount(\'' + id + '\',\'' + colnumJSON.name + '\',\'countspan_' + id + '_' + colnumJSON.name + '\');" ';
        }
        if (editmode.text_valuetype)
        {
            input += 'valuetype="' + editmode.text_valuetype + '" ';
        }
        input += '/>';
        return input;
    },
    getSubTableHtml_Textarea: function (colnumJSON, id, i, iscount)
    {
        var editmode = colnumJSON.editmode;
        var textarea = '<textarea class="mytextarea" name="' + id + '_' + i + '_' + colnumJSON.name + '" id="' + id + '_' + i + '_' + colnumJSON.name + '" issubflow="1" type1="subflow_textarea" ';
        var width = editmode.textarea_width;
        var height = editmode.textarea_height;
        textarea += 'colname="' + colnumJSON.name + '" ';
        if (width && height)
        {
            textarea += 'style="width:' + width + ';height:' + height + '" ';
        }
        else if (width || height)
        {
            if (width)
            {
                textarea += 'style="width:' + width + '" ';
            }
            if (height)
            {
                textarea += 'style="height:' + height + '" ';
            }
        }

        if (editmode.text_valuetype)
        {
            input += 'valuetype="' + editmode.text_valuetype + '" ';
        }

        if (editmode.textarea_maxlength)
        {
            textarea += 'maxlength="' + parseInt(editmode.textarea_maxlength) + '" ';
        }
        textarea += '>';
        textarea += UE.compule.getDefaultValue(editmode.textarea_defaultvalue);
        textarea += '</textarea>';
        return textarea;
    },
    getSubTableHtml_OptionsFromString: function (str, type, name, colname, iscount, value)//type:0 option 1 checkbox 2 radio name:checkbox radio时的名称
    {
        var select = '';
        var strarray = str.split(';');
        for (var i = 0; i < strarray.length; i++)
        {
            var strarray1 = strarray[i].split(',');
            if (strarray1.length == 0)
            {
                continue;
            }
            var val = strarray1[0];
            var txt = val;
            if (strarray1.length > 1)
            {
                txt = strarray1[1];
            }
            type = type || 0;
            value = $.trim(value || "");
            switch (type)
            {
                case 0:
                    select += '<option value="' + val.toString().replaceAll('"', '') + '" ' + (value == val.toString().replaceAll('"', '') ? 'selected="selected"' : '') + '>' + txt + '</option>';
                    break;
                case 1:
                    select += '<input type="checkbox" colname="' + colname + '" issubflow="1" type1="subflow_checkbox" name="' + name + '" id="' + name + '" value="' + val.toString().replaceAll('"', '') + '" ' + (value.indexOf(val.toString().replaceAll('"', '')) >= 0 ? 'checked="checked"' : '') + ' style="vertical-align:middle;"/>';
                    select += '<label style="vertical-align:middle;" for="' + name + '">' + txt + '</label>';
                    break;
                case 2:
                    select += '<input type="radio" colname="' + colname + '" issubflow="1" type1="subflow_checkbox" name="' + name + '" id="' + name + '" value="' + val.toString().replaceAll('"', '') + '" ' + (value == val.toString().replaceAll('"', '') ? 'checked="checked"' : '') + ' style="vertical-align:middle;"/>';
                    select += '<label style="vertical-align:middle;" for="' + name + '">' + txt + '</label>';
                    break;
            }
        }
        return select;
    },
    getSubTableHtml_Select: function (colnumJSON, id, i, iscount)
    {
        var editmode = colnumJSON.editmode;
        var select = '<select class="myselect" name="' + id + '_' + i + '_' + colnumJSON.name + '" id="' + id + '_' + i + '_' + colnumJSON.name + '" issubflow="1" type1="subflow_select" ';
        select += 'colname="' + colnumJSON.name + '" ';
        if (editmode.select_width)
        {
            select += 'style="width:' + editmode.select_width + '" ';
        }
        select += '>';
        var ds = editmode.select_ds;
        var dvalue = editmode.select_default || "";
        switch (ds)
        {
            case "select_dsdict":
                var rootid = editmode.select_ds_dict;
                if ("1" == editmode.select_hasempty)
                {
                    select += '<option value=""></option>';
                }
                select += '@Html.Raw(BDictionary.GetOptionsByID("' + rootid + '".ToGuid(), Business.Platform.Dictionary.OptionValueField.ID, "' + dvalue + '"))';
                break;
            case "select_dssql":
                var sql = editmode.select_ds_sql;
                if ("1" == editmode.select_hasempty)
                {
                    select += '<option value=""></option>';
                }
                select += '@Html.Raw(new Business.Platform.WorkFlowForm().GetOptionsFromSql(DBConnID, "' + sql.replaceAll('"', '\"') + '", "' + dvalue + '"))';
                break;
            case "select_dsstring":
                var str = editmode.select_ds_string;
                if ("1" == editmode.select_hasempty)
                {
                    select += '<option value=""></option>';
                }
                select += UE.compule.getSubTableHtml_OptionsFromString(str, 0, "", "", "", dvalue);
                break;
        }
        select += '</select>';
        return select;
    },
    getSubTableHtml_Checkbox: function (colnumJSON, id, i, iscount)
    {
        var editmode = colnumJSON.editmode;
        var checkbox = '';
        var ds = editmode.checkbox_ds;
        var name = id + '_' + i + '_' + colnumJSON.name;
        var dvalue = editmode.checkbox_default || "";
        switch (ds)
        {
            case "checkbox_dsdict":
                var rootid = editmode.checkbox_ds_dict;
                checkbox = '<span>@Html.Raw(BDictionary.GetCheckboxsByID("' + rootid + '".ToGuid(), "' + name + '", Business.Platform.Dictionary.OptionValueField.ID, "' + dvalue + '", "issubflow=\'1\' type1=\'subflow_checkbox\' colname=\'' + colnumJSON.name + '\'"))</span>';
                break;
            case "checkbox_dssql":
                var sql = editmode.checkbox_ds_sql;
                checkbox = '@Html.Raw(new Business.Platform.WorkFlowForm().GetCheckboxFromSql(DBConnID, "' + sql.replaceAll('"', '\"') + '", "' + name + '", "' + dvalue + '", "issubflow=\'1\' type1=\'subflow_checkbox\'" colname=\'' + colnumJSON.name + '\'"))';
                break;
            case "checkbox_dsstring":
                var str = editmode.checkbox_ds_string;
                checkbox += UE.compule.getSubTableHtml_OptionsFromString(str, 1, name, colnumJSON.name, "", dvalue);
                break;
        }

        return checkbox;
    },
    getSubTableHtml_Radio: function (colnumJSON, id, i, iscount)
    {
        var editmode = colnumJSON.editmode;
        var radio = '';
        var ds = editmode.radio_ds;
        var name = id + '_' + i + '_' + colnumJSON.name;
        var dvalue = editmode.radio_default || "";
        switch (ds)
        {
            case "radio_dsdict":
                var rootid = editmode.radio_ds_dict;
                radio = '<span>@Html.Raw(BDictionary.GetRadiosByID("' + rootid + '".ToGuid(), "' + name + '", Business.Platform.Dictionary.OptionValueField.ID, "' + dvalue + '", "issubflow=\'1\' type1=\'subflow_radio\' colname=\'' + colnumJSON.name + '\'"))</span>';
                break;
            case "radio_dssql":
                var sql = editmode.radio_ds_sql;
                radio = '@Html.Raw(new Business.Platform.WorkFlowForm().GetRadioFromSql(DBConnID, "' + sql.replaceAll('"', '\"') + '", "' + name + '", "' + dvalue + '", "issubflow=\'1\' type1=\'subflow_radio\'" colname=\'' + colnumJSON.name + '\'"))';
                break;
            case "radio_dsstring":
                var str = editmode.radio_ds_string;
                radio += UE.compule.getSubTableHtml_OptionsFromString(str, 1, name, colnumJSON.name, "", dvalue);
                break;
        }

        return radio;
    },
    getSubTableHtml_DateTime: function (colnumJSON, id, i, iscount)
    {
        var editmode = colnumJSON.editmode;
        var datetime = '<input type="text" class="mycalendar" name="' + id + '_' + i + '_' + colnumJSON.name + '" id="' + id + '_' + i + '_' + colnumJSON.name + '" issubflow="1" type1="subflow_datetime" value="' + UE.compule.getDefaultValue(editmode.datetime_defaultvalue) + '" ';
        datetime += 'colname="' + colnumJSON.name + '" ';
        if (editmode.datetime_min)
        {
            datetime += 'mindate="' + editmode.datetime_min + '"';
        }
        if (editmode.datetime_max)
        {
            datetime += 'maxdate="' + editmode.datetime_max + '"';
        }
        if (editmode.datetime_width)
        {
            datetime += 'style="width:' + editmode.datetime_width + '" ';
        }
        datetime += '/>';
        return datetime;
    },
    getSubTableHtml_Org: function (colnumJSON, id, i, iscount)
    {
        var editmode = colnumJSON.editmode;
        var org = '<input type="text" class="mymember" name="' + id + '_' + i + '_' + colnumJSON.name + '" id="' + id + '_' + i + '_' + colnumJSON.name + '" issubflow="1" type1="subflow_org" value="' + UE.compule.getDefaultValue(editmode.org_defaultvalue) + '" ';
        org += 'colname="' + colnumJSON.name + '" ';
        var org_type = editmode.org_type;
        if (org_type)
        {
            org += ' dept="' + (org_type.indexOf(",0,") >= 0 ? "1" : "0") + '"';
            org += ' station="' + (org_type.indexOf(",1,") >= 0 ? "1" : "0") + '"';
            org += ' user="' + (org_type.indexOf(",2,") >= 0 ? "1" : "0") + '"';
            org += ' workgroup="' + (org_type.indexOf(",3,") >= 0 ? "1" : "0") + '"';
            org += ' unit="' + (org_type.indexOf(",4,") >= 0 ? "1" : "0") + '"';
        }
        if (editmode.org_width)
        {
            org += 'style="width:' + editmode.org_width + '" ';
        }
        org += ' more="' + editmode.org_more + '"';
        var rootid = '';
        switch (editmode.org_rang)
        {
            case "0": //发起者部门
                rootid = '@BWorkFlowTask.GetFirstSnderDeptID(FlowID.ToGuid(), GroupID.ToGuid())';
                break;
            case "1": //处理者部门
                rootid = '@Business.Platform.Users.CurrentDeptID';
                break;
            case "2": //自定义
                rootid = editmode.org_rang1;
                break;
        }
        if (rootid)
        {
            org += ' rootid="' + rootid + '"';
        }
        org += '/>';
        return org;
    },
    getSubTableHtml_Dict: function (colnumJSON, id, i, iscount)
    {
        var editmode = colnumJSON.editmode;
        var dict = '<input type="text" class="mydict" name="' + id + '_' + i + '_' + colnumJSON.name + '" id="' + id + '_' + i + '_' + colnumJSON.name + '" issubflow="1" type1="subflow_dict" ';
        dict += 'colname="' + colnumJSON.name + '" ';
        if (editmode.dict_width)
        {
            dict += 'style="width:' + editmode.dict_width + '" ';
        }
        if (editmode.dict_rang)
        {
            dict += 'rootid="' + editmode.dict_rang + '" ';
        }
        dict += 'more="' + editmode.dict_more + '" ';
        dict += '/>';
        return dict;
    },
    getSubTableHtml_Files: function (colnumJSON, id, i, iscount)
    {
        var editmode = colnumJSON.editmode;
        var files = '<input type="text" class="myfile" name="' + id + '_' + i + '_' + colnumJSON.name + '" id="' + id + '_' + i + '_' + colnumJSON.name + '" issubflow="1" type1="subflow_files" ';
        files += 'colname="' + colnumJSON.name + '" ';
        if (editmode.files_width)
        {
            files += 'style="width:' + editmode.files_width + '" ';
        }
        if (editmode.files_filetype)
        {
            files += 'filetype="' + editmode.files_filetype + '" ';
        }
        files += '/>';
        return files;
    },
    getSubTableHtml: function ($control)
    {
        var id = $control.attr("id");
        var subtableJSON = {};

        for (var i = 0; i < parent.formsubtabs.length; i++)
        {
            if (parent.formsubtabs[i].id == id)
            {
                subtableJSON = parent.formsubtabs[i];
                break;
            }
        }
        if (!subtableJSON.id || !subtableJSON.colnums || subtableJSON.colnums.length == 0)
        {
            return;
        }
        var html = '<table class="flowformsubtable" style="width:99%;margin:0 auto;" cellPadding="0" cellSpacing="1" issubflowtable="1" id="subtable_' + id + '">';
        html += '<thead>';
        html += '<tr>';

        var edittds = '';
        var counttds = [];//合计列
        var hasCountColnum = false;//是否有合计列
        var guid = RoadUI.Core.newid(false);

        //排序
        subtableJSON.colnums.sort(function (a, b)
        {
            return parseFloat(a.index) > parseFloat(b.index);
        });
        var index = 0;
        for (var i = 0; i < subtableJSON.colnums.length; i++)
        {
            var colnumJSON = subtableJSON.colnums[i];
            if ("1" != colnumJSON.isshow)
            {
                continue;
            }
            html += '<td>';
            html += colnumJSON.showname || colnumJSON.name;
            if (index == 0)
            {
                html += '<input type="hidden" name="flowsubtable_id" value="' + id + '" />';
                html += '<input type="hidden" name="flowsubtable_' + id + '_secondtable" value="' + subtableJSON.secondtable + '" />';
                html += '<input type="hidden" name="flowsubtable_' + id + '_primarytablefiled" value="' + subtableJSON.primarytablefiled + '" />';
                html += '<input type="hidden" name="flowsubtable_' + id + '_secondtableprimarykey" value="' + subtableJSON.secondtableprimarykey + '" />';
                html += '<input type="hidden" name="flowsubtable_' + id + '_secondtablerelationfield" value="' + subtableJSON.secondtablerelationfield + '" />';
            }
            html += '</td>';

            var editelement = '';
            var editmode = colnumJSON.editmode;
            var editmode1 = editmode.editmode || "text";
            var iscount = "1" == colnumJSON.issum;
            edittds += '<td colname="' + colnumJSON.name + '" iscount="' + colnumJSON.issum + '">';
            if (index == 0)
            {
                edittds += '<input type="hidden" name="hidden_guid_' + id + '" value="' + guid + '" />';
                edittds += '<input type="hidden" name="flowsubid" value="' + id + '" />';
            }
            switch (editmode1)
            {
                case "text":
                    editelement = UE.compule.getSubTableHtml_Text(colnumJSON, id, guid, iscount);
                    break;
                case "textarea":
                    editelement = UE.compule.getSubTableHtml_Textarea(colnumJSON, id, guid, iscount);
                    break;
                case "select":
                    editelement = UE.compule.getSubTableHtml_Select(colnumJSON, id, guid, iscount);
                    break;
                case "checkbox":
                    editelement = UE.compule.getSubTableHtml_Checkbox(colnumJSON, id, guid, iscount);
                    break;
                case "radio":
                    editelement = UE.compule.getSubTableHtml_Radio(colnumJSON, id, guid, iscount);
                    break;
                case "datetime":
                    editelement = UE.compule.getSubTableHtml_DateTime(colnumJSON, id, guid, iscount);
                    break;
                case "org":
                    editelement = UE.compule.getSubTableHtml_Org(colnumJSON, id, guid, iscount);
                    break;
                case "dict":
                    editelement = UE.compule.getSubTableHtml_Dict(colnumJSON, id, guid, iscount);
                    break;
                case "files":
                    editelement = UE.compule.getSubTableHtml_Files(colnumJSON, id, guid, iscount);
                    break;
            }
            edittds += editelement;
            edittds += '</td>';

            if (!hasCountColnum && "1" == colnumJSON.issum)
            {
                hasCountColnum = true;
            }

            if (iscount)
            {
                var coltitle = colnumJSON.showname || colnumJSON.name;
                counttds.push({ "id": id, "name": colnumJSON.name, "title": coltitle });
            }
            index++;
        }
        html += '<td>'

        html += '</td>'
        html += '</tr>';
        html += '</thead>';
        html += '<tbody>';
        html += '<tr type1="listtr">';
        html += edittds;
        html += '<td>'
        html += '<input type="button" class="mybutton" style="margin-right:4px;" value="增加" onclick="formrun.subtableNewRow(this);"/>';
        html += '<input type="button" class="mybutton" value="删除" onclick="formrun.subtableDeleteRow(this);"/>';
        html += '</td>'
        html += '</tr>';

        if (hasCountColnum)//添加合计行
        {
            html += '<tr type1="counttr">';
            html += '<td colspan="' + (subtableJSON.colnums.length + 1).toString() + '" align="right" style="padding-right:20px; text-align:right;">';
            for (var j = 0; j < counttds.length; j++)
            {
                html += '<span style="margin-right:10px;">' + counttds[j].title +
                    '合计：<label id="countspan_' + counttds[j].id + '_' + counttds[j].name + '">0</label></span>';
            }
            html += '</td>';
            html += '</tr>';
        }

        html += '</tbody>';
        html += '</table>';

        $control.after(html);
        $control.remove();
    }
};