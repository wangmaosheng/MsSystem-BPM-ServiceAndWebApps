<%@ Page Language="C#" %>
<% 
    string dictid = Request.QueryString["dictid"];
    if (!dictid.IsGuid())
    {
        Response.Write("[]");
        Response.End();
    }
    var dicts = new Business.Platform.Dictionary().GetChilds(dictid.ToGuid());
    System.Text.StringBuilder json = new StringBuilder();
    json.Append("[");
    foreach (var dict in dicts)
    {
        json.Append("{");
        json.AppendFormat("\"id\":\"{0}\",", dict.ID.ToString());
        json.AppendFormat("\"title\":\"{0}\"", dict.Title.ToString());
        json.Append("},");
    }
    Response.Write(json.ToString().TrimEnd(',') + "]");
    Response.End();
%>