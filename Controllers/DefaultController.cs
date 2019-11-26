using Etch.OrchardCore.Favicon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OrchardCore.ContentManagement;
using OrchardCore.Entities;
using OrchardCore.FileStorage;
using OrchardCore.Media;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Favicon.Controllers
{
    public class DefaultController : Controller
    {
        #region Dependencies

        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly IMediaFileStore _mediaFileStore;
        private readonly ISiteService _siteService;

        #endregion

        #region Constructor

        public DefaultController(IContentTypeProvider contentTypeProvider, IMediaFileStore mediaFileStore, ISiteService siteService)
        {
            _contentTypeProvider = contentTypeProvider;
            _mediaFileStore = mediaFileStore;
            _siteService = siteService;
        }

        #endregion

        #region Actions

        [HttpGet]
        [Route("icon.png")]
        public async Task<IActionResult> AppleTouchIcon()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasAppleTouchIcon)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.AppleTouchIconPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.FaviconPath), contentType ?? "image/png");
        }

        [HttpGet]
        [Route("browserconfig.xml")]
        public async Task<IActionResult> BrowserConfig()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasBrowserConfig)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.BrowserConfigPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.BrowserConfigPath), contentType ?? "application/xml");
        }

        [HttpGet]
        [Route("favicon.ico")]
        public async Task<IActionResult> Favicon()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasFavicon)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.FaviconPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.FaviconPath), contentType ?? "image/x-icon");
        }

        [HttpGet]
        [Route("tile.png")]
        public async Task<IActionResult> Tile()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasTile)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.TilePath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.TilePath), contentType ?? "image/png");
        }

        [HttpGet]
        [Route("tile-wide.png")]
        public async Task<IActionResult> TileWide()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasTileWide)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.TileWidePath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.TileWidePath), contentType ?? "image/x-icon");
        }

        [HttpGet]
        [Route("site.webmanifest")]
        public async Task<IActionResult> WebAppManifest()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasWebAppManifest)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.WebAppManifestPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.WebAppManifestPath), contentType ?? "application/manifest+json");
        }

        #endregion

        #region Helper Methods

        private async Task<FaviconSettings> GetSettings()
        {
            return (await _siteService.GetSiteSettingsAsync()).As<ContentItem>(typeof(FaviconSettings).Name)?.As<FaviconSettings>() ?? null;
        }

        #endregion
    }
}
