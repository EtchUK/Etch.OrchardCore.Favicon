using Etch.OrchardCore.Favicon.Filters;
using Etch.OrchardCore.Favicon.Helpers;
using Etch.OrchardCore.Favicon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace Etch.OrchardCore.Favicon
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<FaviconSettings>();

            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IFaviconColourValueMigrator, FaviconColourValueMigrator>();

            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(FaviconFilter));
            });
        }
    }
}
