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
                    _resourceManager.RegisterLink(CreateLinkEntry(context, "apple-touch-icon.png", "apple-touch-icon", "180x180"));
                }

                if (faviconSettings != null && faviconSettings.HasDefaultFavicon)
                {
                    _resourceManager.RegisterLink(CreateLinkEntry(context, "favicon-16x16.png", "icon", "16x16", "image/png"));
                }

                if (faviconSettings != null && faviconSettings.HasLargeFavicon)
                {
                    _resourceManager.RegisterLink(CreateLinkEntry(context, "favicon-32x32.png", "icon", "32x32", "image/png"));
                }

                if (faviconSettings != null && faviconSettings.HasWebAppManifest)
                {
                    _resourceManager.RegisterLink(new LinkEntry { Href = $"{pathBase}/site.webmanifest", Rel = "manifest" });
                }

                if (faviconSettings != null && faviconSettings.HasSafariPinnedTab)
                {
                    _resourceManager.RegisterLink(CreateLinkEntry(context, "safari-pinned-tab.svg", "mask-icon", null, null, faviconSettings.SafariPinnedTabColourValue));
                }

                if (faviconSettings != null && faviconSettings.HasTileColour)
                {
                    _resourceManager.AppendMeta(new MetaEntry { Name = "msapplication-TileColor", Content = faviconSettings.TileColourValue }, ",");
                }

                if (faviconSettings != null && faviconSettings.HasThemeColour)
                {
                    _resourceManager.AppendMeta(new MetaEntry { Name = "theme-color", Content = faviconSettings.ThemeColourValue }, ",");
                }
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error defining meta/link tags for favicon feature");
            }

            await next.Invoke();
        }

        #endregion

        #region Helper Methods

        private LinkEntry CreateLinkEntry(ResultExecutingContext context, string href, string rel, string sizes = null, string type = null, string colour = null)
        {
            var entry = new LinkEntry { Href = $"{context.HttpContext.Request.PathBase}/{href}", Rel = rel };

            if (!string.IsNullOrWhiteSpace(type))
            {
                entry.Type = type;
            }

            if (!string.IsNullOrWhiteSpace(sizes))
            {
                entry.AddAttribute("sizes", sizes);
            }

            if (!string.IsNullOrWhiteSpace(colour))
            {
                entry.AddAttribute("color", colour);
            }

            return entry;
        }

        #endregion
    }
}
