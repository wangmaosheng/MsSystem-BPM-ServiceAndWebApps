<%@ Page Language="C#"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="../../dialogs/internal.js"></script>
    <script type="text/javascript" src="../common.js"></script>
    <%=WebMvc.Common.Tools.IncludeFiles %>
</head>
<body style="overflow:hidden;">
    <%WebMvc.Common.Tools.CheckLogin(); %>
    <div style="margin:0 auto; text-align:center; padding-top:38px;">
        <div>
            <img src="/Images/loading/load1.gif" alt="" id="wait" />
        </div>
        <div style="margin-top:5px;">
            正在保存...
        </div>
    </div>

    <script type="text/javascript">
        var isPublish = "1" == "<%=Request.QueryString["publish"]%>";
        $(function ()
        {   
            save();
        });
        function save()
        {
            var formattributeJSON = parent.formattributeJSON;
            var formEvents = parent.formEvents;
            var html = editor.getContent();
            if (!formattributeJSON.name || $.trim(formattributeJSON.name).length == 0)
            {
                if (!isPublish)
                {
                    alert("您未设置表单相关属性!");
                    dialog.close();
                }
                return;
            }

            var formid = "";
            if (!formattributeJSON.id || $.trim(formattributeJSON.id).length == 0)
            {
                formid = RoadUI.Core.newid()
                formattributeJSON.id = formid;
            }
            else
            {
                formid = formattributeJSON.id;
            }

            $.ajax({
                url: top.rootdir + "/WorkFlowFormDesigner/Save", type: "POST",
                data: {
                    id: formid,
                    html: html,
                    name: formattributeJSON.name,
                    type: formattributeJSON.apptype,
                    att: JSON.stringify(formattributeJSON),
                    subtable: JSON.stringify(parent.formsubtabs),
                    formEvents: JSON.stringify(formEvents)
                }, async: false, cache: false, success: function (txt)
                {
                    if (!isPublish)
                    {
                        alert(txt);
                        dialog.close();
                    }
                }
            });
        }
    </script>
</body>
</html>
