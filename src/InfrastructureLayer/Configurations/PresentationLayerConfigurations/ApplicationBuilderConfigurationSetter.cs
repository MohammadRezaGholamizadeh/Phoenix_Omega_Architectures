using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ServiceLayer.Setups.BusinessExceptions;
using System.Net.Mime;
using System.Text.Json;

namespace InfrastructureLayer.Configurations.PresentationLayerConfigurations
{
    public class ApplicationBuilderConfigurationSetter
    {
        private readonly IApplicationBuilder _app;

        public ApplicationBuilderConfigurationSetter(IApplicationBuilder app)
        {
            _app = app;
        }

        public ApplicationBuilderConfigurationSetter UseRouting()
        {
            _app.UseRouting();
            return this;
        }

        public ApplicationBuilderConfigurationSetter UseAuthorization()
        {
            _app.UseAuthorization();
            return this;
        }

        public ApplicationBuilderConfigurationSetter UseAuthentication()
        {
            _app.UseAuthentication();
            return this;
        }

        public ApplicationBuilderConfigurationSetter UseEndpointsToConfigHealthCheck()
        {
            _app.UseEndpoints(_ =>
                 {
                     _.MapHealthChecks("/health");
                 });
            return this;
        }

        public ApplicationBuilderConfigurationSetter UseEndpointsToMapControllers()
        {
            _app.UseEndpoints(_ =>
            {
                _.MapHealthChecks("/health");
                _.MapControllers();
            });
            return this;
        }

        public ApplicationBuilderConfigurationSetter UseHttpsRedirection()
        {
            _app.UseHttpsRedirection();
            return this;
        }

        public ApplicationBuilderConfigurationSetter UseHealthChecks()
        {
            _app.UseHealthChecks("/health");
            return this;
        }

        public ApplicationBuilderConfigurationSetter UseCors()
        {
            _app.UseCors(cors =>
            {
                cors.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
            return this;
        }
        public ApplicationBuilderConfigurationSetter UseHangfireDashboard()
        {
            _app.UseHangfireDashboard();
            return this;
        }

        public ApplicationBuilderConfigurationSetter UseExceptionHandler()
        {
            var webHostEnvironment = _app.ApplicationServices
                                         .GetRequiredService<IWebHostEnvironment>();

            var jsonOptions = _app.ApplicationServices
                                  .GetService<IOptions<JsonOptions>>()?
                                  .Value.JsonSerializerOptions;

            _app.UseExceptionHandler(_ =>
                _.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

                var isAssignToBusinessException =
                    exception?.GetType().IsAssignableTo(typeof(BusinessException));

                const string errorInProduction = "UnknownError";

                var result = new ExceptionErrorDto();
                if (!webHostEnvironment.IsDevelopment())
                {
                    if (isAssignToBusinessException is false)
                    {
                        result.Error = errorInProduction;
                        result.Description = null;
                    }
                    else
                    {
                        result.Error =
                            exception?
                            .GetType()
                            .Name
                            .Replace("Exception", string.Empty);
                        result.Description = null;
                    }
                }
                else
                {
                    result.Error =
                        exception?
                        .GetType()
                        .Name
                        .Replace("Exception", string.Empty);
                    result.Description =
                        exception?.ToString();
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                await context.Response
                             .WriteAsync(JsonSerializer.Serialize(result, jsonOptions));
            }));

            if (webHostEnvironment.IsProduction())
                _app.UseHsts();

            return this;
        }

        public ApplicationBuilderConfigurationSetter UseSwaggerAndSwaggerUI()
        {

            var provider =
                _app.ApplicationServices
                .GetRequiredService<IApiVersionDescriptionProvider>();
            var environment =
                _app.ApplicationServices
                    .GetRequiredService<IHostEnvironment>();

            if (environment.IsDevelopment())
            {
                _app.UseSwagger(options =>
                {
                    options.RouteTemplate = "docs/{documentName}/swagger.json";
                });

                _app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = "docs";

                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }

                    options.DocumentTitle = $"Phoenix Omega Architecture API Documentation";
                });
            }
            return this;
        }

        //public ApplicationBuilderConfigurationSetter InitializeDataBase()
        //{
        //    var dbInitializer =
        //        _app.ApplicationServices
        //           .GetRequiredService<IMigrationRunner>();

        //    dbInitializer.Initialize(null);
        //    return this;
        //}
    }
    public class ExceptionErrorDto
    {
        public string? Error { get; set; }
        public string? Description { get; set; }
    }
}
