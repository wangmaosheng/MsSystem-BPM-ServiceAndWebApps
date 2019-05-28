<%@ Page Language="C#" %>
<% 
    string sql = Request["sql"];
    string conn = Request["conn"];
    if (sql.IsNullOrEmpty() || !conn.IsGuid())
    {
        Response.Write("[]");
        Response.End();
    }
    var dbconn = new Business.Platform.DBConnection().Get(conn.ToGuid());
    var dt = new Business.Platform.DBConnection().GetDataTable(dbconn, sql);
    if (dt == null || dt.Rows.Count == 0)
    {
        Response.Write("[]");
        Response.End();
    }
    System.Text.StringBuilder json = new StringBuilder();
    json.Append("[");
    foreach (System.Data.DataRow dr in dt.Rows)
    {
        json.Append("{");
        json.AppendFormat("\"id\":\"{0}\",", dr[0]);
        json.AppendFormat("\"title\":\"{0}\"", dt.Columns.Count > 1 ? dr[1] : dr[0]);
        json.Append("},");
    }
    Response.Write(json.ToString().TrimEnd(',') + "]");
    Response.End();
%>