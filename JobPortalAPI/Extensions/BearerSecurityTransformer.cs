using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace JobPortalAPI.Extensions
{
    internal sealed class BearerSecurityTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider) : IOpenApiDocumentTransformer
    {
        public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var authSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();

            if (authSchemes.Any(s => s.Name == "Bearer"))
            {
                var bearerScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token below."
                };

                document.Components ??= new OpenApiComponents();
                document.AddComponent("Bearer", bearerScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                };

                foreach (var path in document.Paths.Values)
                {
                    foreach (var operation in path.Operations.Values)
                    {
                        operation.Security ??= [];
                        operation.Security.Add(securityRequirement);
                    }
                }
            }
        }
    }
}
