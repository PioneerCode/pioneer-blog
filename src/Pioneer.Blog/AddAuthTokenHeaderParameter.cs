using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pioneer.Blog
{
    /// <summary>
    /// Operation filter used to add necessary UI elements that will populate Authentication
    /// header value on outbound requests
    /// </summary>
    public class AddAuthTokenHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "Authorization",
                In = "header",
                Type = "string",
                Required = false
            });
        }
    }
}