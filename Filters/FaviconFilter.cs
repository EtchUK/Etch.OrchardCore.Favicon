using Etch.OrchardCore.Favicon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OrchardCore.Admin;
using OrchardCore.ContentManagement;
using OrchardCore.Entities;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;
using System;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Favicon.Filters
{
    public class FaviconFilter : IAsyncResultFilter
    {
        #region Dependencies

        private readonly ILogger<FaviconFilter> _logger;
        private readonly IResourceManager _resourceManager;
        private readonly ISiteService _siteService;

        #endregion

        #region Constructor

        public FaviconFilter(ILogger<FaviconFilter> logger, IResourceManager resourceManager, ISiteService siteService)
        {
            _logger = logger;
            _resourceManager = resourceManager;
            _siteService = siteService;
        }

        #endregion

        #region Implementation

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // should only run on the front-end for a full view
            if (!((context.Result is ViewResult || context.Result is PageResult) && !AdminAttribute.IsApplied(context.HttpContext)))
            {
                await next.Invoke();
                return;
            }

            try
            {
                var pathBase = context.HttpContext.Request.PathBase;
                var siteSettings = await _siteService.GetSiteSettingsAsync();
                var faviconSettings = siteSettings.As<ContentItem>("FaviconSettings")?.As<FaviconSettings>();

                if (faviconSettings != null && faviconSettings.HasAppleTouchIcon)
                {
                    _resourceManager.RegisterLink(new LinkEntry { Href = $"{pathBase}/icon.png", Rel = "apple-touch-icon" });
                }

                if (faviconSettings != null && faviconSettings.HasFavicon)
                {
                    _resourceManager.RegisterLink(new LinkEntry { Href = $"{pathBase}/favicon.ico", Rel = "shortcut icon" });
                }

                if (faviconSettings != null && faviconSettings.HasWebAppManifest)
                {
                    _resourceManager.RegisterLink(new LinkEntry { Href = $"{pathBase}/site.webmanifest", Rel = "manifest" });
                }
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error defining meta/link tags for favicon feature");
            }

            await next.Invoke();
        }

        #endregion
    }
}
