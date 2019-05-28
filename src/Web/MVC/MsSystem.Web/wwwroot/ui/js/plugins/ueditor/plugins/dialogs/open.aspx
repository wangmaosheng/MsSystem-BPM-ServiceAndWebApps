<%@ Page Language="C#" %>
<%
    string query = "appid=" + Request.QueryString["appid"] + "&tabid=" + Request.QueryString["appid"];
    string rootid = new Business.Platform.Dictionary().GetIDByCode("FormTypes").ToString();
    WebMvc.Common.Tools.CheckLogin();
%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script type="text/javascript" src="../../dialogs/internal.js"></script>
    <script type="text/javascript" src="../common.js"></script>
    <%=WebMvc.Common.Tools.IncludeFiles %>
</head>
<body style="padding:0; margin:0; overflow:hidden;">
<form method="post">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="width:200px; vertical-align:top; padding:5px 5px 0 5px;">
                <div id="TypeDiv" style="overflow:auto;"></div> 
            </td>
            <td style="vertical-align:top; padding:2px 0 0 3px; border-left:1px solid #cccccc;">
                <div id="ListDiv" style="overflow:auto;">
                    <table cellpadding="0" cellspacing="1" border="0" style="width:100%; margin:0 auto;" class="listtable" id="data_table">
                        <thead>
                        <tr>
                            <th>表单名称</th>
                            <th>创建时间</th>
                            <th>创建人</th>
                            <th>状态</th>
                            <th></th>
                        </tr>
                        </thead>
                        <tbody id="ListDivBody">

                        </tbody>
                    </table>
                    <script type="text/javascript">
                        function openform(id)
                        {
                            $.ajax({
                                url: top.rootdir + "/WorkFlowFormDesigner/GetHtml?id=" + id, async: false, cache: false,
                                success: function (html)
                                {
                                    editor.setContent(html);
                                }
                            });
                            $.ajax({
                                url: top.rootdir + "/WorkFlowFormDesigner/GetAttribute?id=" + id, dataType: "JSON", async: false, cache: false,
                                success: function (json)
                                {
                                    parent.formattributeJSON = json;
                                }
                            });
                            $.ajax({
                                url: top.rootdir + "/WorkFlowFormDesigner/Getsubtable?id=" + id, dataType: "JSON", async: false, cache: false,
                                success: function (json)
                                {
                                    parent.formsubtabs = json;
                                }
                            });
                            $.ajax({
                                url: top.rootdir + "/WorkFlowFormDesigner/GetEvents?id=" + id, dataType: "JSON", async: false, cache: false,
                                success: function (json)
                                {
                                    parent.formEvents = json;
                                }
                            });
                            dialog.close();
                        }
                        function delform(id)
                        {
                            if (!confirm('您真的要删除该表单吗?'))
                            {
                                return;
                            }
                            $.ajax({
                                url: 'delete.aspx?id=' + id, cache: false, async: false, success: function (txt)
                                {
                                    if ("1" == txt)
                                    {
                                        window.location = window.location;
                                    }
                                    else
                                    {
                                        alert(txt);
                                    }
                                }
                            });
                        }
                    </script>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var roadTree = null;
        $(function ()
        {
            var height = $(window).height();
            $('#TypeDiv').css('height', height - 10);
            $('#ListDiv').css('height', height - 10);
            roadTree = new RoadUI.Tree({ id: "TypeDiv", path: top.rootdir + "/Dict/Tree1?root=<%=rootid%>", refreshpath: top.rootdir + "/Dict/TreeRefresh", onclick: openUrl });
            openUrl({ id: "<%=rootid%>" });
        });
        function openUrl(json)
        {
            $.ajax({
                url: "open_list1.aspx?typeid=" + json.id + "&appid=<%=Request.QueryString["appid"]%>", cache: false, async: false, dataType: "html", success: function (html)
                {
                    $("#ListDivBody").html(html);
                }
            });
        }
        function reLoad(id)
        {
            roadTree.refresh(id);
        }
    </script>
    </form>
 </body>
 </html>