namespace JobPortalAPI.Extensions
{
    public static  class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer<BearerSecurityTransformer>();
            });
            return services;
        }
        public static WebApplication UseSwaggerUI(this WebApplication app)
        {
            app.MapOpenApi();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/openapi/v1.json", "JobPortal API V1");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
