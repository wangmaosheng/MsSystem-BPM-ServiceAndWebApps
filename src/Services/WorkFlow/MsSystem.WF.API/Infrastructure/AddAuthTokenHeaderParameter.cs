//using Swashbuckle.AspNetCore.Swagger;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using System.Collections.Generic;

//namespace MsSystem.WF.API.Infrastructure
//{
//    public class AddAuthTokenHeaderParameter : IOperationFilter
//    {

//        public void Apply(Operation operation, OperationFilterContext context)
//        {

//            if (operation.Parameters == null)
//            {
//                operation.Parameters = new List<IParameter>();
//            }
//            operation.Parameters.Add(new NonBodyParameter()
//            {
//                Name = "token",
//                In = "header",
//                Type = "string",
//                Description = "token认证信息",
//                Required = true
//            });
//        }
//    }
//}
