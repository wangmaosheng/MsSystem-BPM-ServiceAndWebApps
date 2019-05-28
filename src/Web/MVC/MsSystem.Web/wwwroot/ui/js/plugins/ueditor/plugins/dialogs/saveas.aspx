<%@ Page Language="C#" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
    if (!Request.Form["name"].IsNullOrEmpty())
    {
        string id = Request.Form["id"];
        string name = Request.Form["name"];

        if (!id.IsGuid() || name.IsNullOrEmpty())
        {
           Response.Write("<script>alert('数据验证错误!');</script>");
        }
        else
        {
            var wff = workFlowFrom.Get(id.ToGuid());
            if (wff != null)
            {
                wff.ID = Guid.NewGuid();
                wff.Name = name.Trim();
                wff.CreateTime = Utility.DateTimeNew.Now;
                wff.LastModifyTime = wff.CreateTime;
                wff.CreateUserID = Business.Platform.Users.CurrentUserID;
                wff.CreateUserName = Business.Platform.Users.CurrentUserName;
                wff.Status = 0;

                var json = LitJson.JsonMapper.ToObject(wff.Attribute);
                json["id"] = wff.ID.ToString();
                json["name"] = wff.Name;
                wff.Attribute = LitJson.JsonMapper.ToJson(json);
                
                workFlowFrom.Add(wff);
                Business.Platform.Log.Add("以另存的方式创建了表单", wff.Serialize(), Business.Platform.Log.Types.流程相关);
                Response.Write("<script>alert('表单已另存为：" + name + "');dialog.close();</script>");
            }
        }
    }
%>

<form method="post" onsubmit="return new RoadUI.Validate().validateForm(this);">
    <br /><br />
    <table cellpadding="0" cellspacing="1" border="0" width="95%"  align="center">
        <tr>
            <td>表单名称：</td>
        </tr>
        <tr>
            <td>
                <input type="hidden" id="id" name="id" value="" />
                <input type="text" class="mytext" id="name" name="name" validate="empty" style="width:75%"/></td>
        </tr>
    </table>
    <div class="buttondiv">
        <input type="submit" value="确定保存" name="save1" id="save1" class="mybutton" />
        <input type="button" value="取消关闭" class="mybutton" onclick="dialog.close();" />
    </div>
</form>
<script type="text/javascript">
    var attJSON = parent.formattributeJSON;
    $(function ()
    {
        if (!attJSON.id || $.trim(attJSON.id).length == 0)
        {
            alert('请先打开一个表单,再另存为!');
            dialog.close();
        }
        else
        {
            $("#id").val(attJSON.id);
        }
    })
</script>
</body>
</html>