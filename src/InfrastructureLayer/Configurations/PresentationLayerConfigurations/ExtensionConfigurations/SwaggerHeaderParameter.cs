using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace InfrastructureLayer.Configurations.PresentationLayerConfigurations.ExtensionConfigurations
{
    public class SwaggerHeaderParameter : IOperationFilter
    {
        public void Apply(
            OpenApiOperation operation,
            OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "TenantId",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Nullable = true,
                    Default = null,
                    MaxLength = 450
                }
            });
        }
    }
}
