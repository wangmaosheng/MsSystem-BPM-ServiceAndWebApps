<%@ Page Language="C#" %>
<% 
    WebMvc.Common.Tools.CheckLogin();
    string id = Request.QueryString["id"];
    if(!id.IsGuid())
    {
        Response.Write("参数错误!");
        Response.End();
    }
    Business.Platform.WorkFlowForm WFF = new Business.Platform.WorkFlowForm();
    
    var wff = WFF.Get(id.ToGuid());
    
    if(wff==null)
    {
        Response.Write("参数错误!");
        Response.End();
    }
    wff.Status = 2;
    WFF.Update(wff);

    //Business.Platform.AppLibrary APP = new Business.Platform.AppLibrary();
    //var app = APP.GetByCode(id);
    //if(app!=null)
    //{
    //    APP.Delete(app.ID);
    //}

    Business.Platform.Log.Add("删除了流程表单", wff.Serialize(), Business.Platform.Log.Types.流程相关);

    Response.Write("1");
    Response.End();
%>