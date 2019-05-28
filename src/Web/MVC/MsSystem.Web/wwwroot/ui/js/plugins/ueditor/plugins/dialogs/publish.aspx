<%@ Page Language="C#"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="../../dialogs/internal.js"></script>
    <script type="text/javascript" src="../common.js"></script>
    <script type="text/javascript" src="../compule.js"></script>
    <%=WebMvc.Common.Tools.IncludeFiles %>
</head>
<body style="overflow:hidden;">
    <%WebMvc.Common.Tools.CheckLogin(); %>
    <div style="margin:0 auto; text-align:center; padding-top:38px;">
        <div>
            <img src="/Images/loading/load1.gif" alt="" id="wait" />
        </div>
        <div style="margin-top:5px;">
            正在发布...
        </div>
    </div>

    <!--发布时保存-->
    <%Server.Execute("save.aspx?publish=1"); %>

    <script type="text/javascript">
        $(function ()
        {   
            publish();
        });
        function publish()
        {
            var formattributeJSON = parent.formattributeJSON;
            var formid = "";
            if (!formattributeJSON || !formattributeJSON.id || $.trim(formattributeJSON.id).length == 0)
            {
                alert('您未设置表单相关属性!');
                dialog.close();
                return;
            }
            else
            {
                formattributeJSON.hasEditor = "0";
                formid = formattributeJSON.id;
                var html = editor.getContent();
                var $controls = $("[type1^='flow_']", editor.document);
                for (var i = 0; i < $controls.size() ; i++)
                {
                    var $control = $controls.eq(i);
                    var type1Arr = $control.attr('type1').split('_');
                    var controlType = type1Arr.length > 1 ? type1Arr[1] : "";
                    switch (controlType)
                    {
                        case 'text':
                            UE.compule.getTextHtml($control);
                            break;
                        case 'password':
                            UE.compule.getTextHtml($control);
                            break;
                        case 'textarea':
                            UE.compule.getTextareaHtml($control);
                            break;
                        case 'radio':
                            UE.compule.getRadioOrCheckboxHtml($control, 'radio');
                            break;
                        case 'checkbox':
                            UE.compule.getRadioOrCheckboxHtml($control, 'checkbox');
                            break;
                        case 'select':
                            UE.compule.getSelectHtml($control);
                            break;
                        case 'org':
                            UE.compule.getOrgHtml($control);
                            break;
                        case 'dict':
                            UE.compule.getDictHtml($control);
                            break;
                        case 'datetime':
                            UE.compule.getDateTimeHtml($control);
                            break;
                        case 'files':
                            UE.compule.getFilesHtml($control);
                            break;
                        case 'hidden':
                            UE.compule.getHiddenHtml($control);
                            break;
                        case 'html':
                            UE.compule.getHtmlHtml($control, formattributeJSON);
                            break;
                        case "subtable":
                            UE.compule.getSubTableHtml($control);
                            break;
                        case "label":
                            UE.compule.getLabelHtml($control);
                            break;
                        case "grid":
                            UE.compule.getGridHtml($control);
                            break;
                        case "button":
                            UE.compule.getButtonHtml($control);
                            break;
                    }
                }
                $.ajax({
                    url: top.rootdir + "/WorkFlowFormDesigner/Publish", type: "POST",
                    data: {
                        id: formid,
                        html: editor.getContent(),
                        name: formattributeJSON.name,
                        att: JSON.stringify(formattributeJSON)
                    },
                    async: false, cache: false, success: function (txt)
                    {
                        alert(txt);
                        editor.setContent(html);
                        dialog.close();
                    }, error: function (txt)
                    {
                        alert('发布表单发生了错误!');
                        editor.setContent(html);
                        dialog.close();
                    }
                });
               
            }
        }
    </script>
</body>
</html>
