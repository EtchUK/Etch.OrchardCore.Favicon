using Etch.OrchardCore.Favicon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OrchardCore.ContentManagement;
using OrchardCore.Entities;
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
        [Route("android-chrome-192x192.png")]
        public async Task<IActionResult> AndroidSmallIcon()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasAndroidSmallIcon)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.AndroidSmallIconPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.AndroidSmallIconPath), contentType ?? DefaultMimeTypes.PngIcon);
        }

        [HttpGet]
        [Route("android-chrome-512x512.png")]
        public async Task<IActionResult> AndroidLargeIcon()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasAndroidLargeIcon)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.AndroidLargeIconPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.AndroidLargeIconPath), contentType ?? DefaultMimeTypes.PngIcon);
        }

        [HttpGet]
        [Route("apple-touch-icon.png")]
        public async Task<IActionResult> AppleTouchIcon()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasAppleTouchIcon)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.AppleTouchIconPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.AppleTouchIconPath), contentType ?? DefaultMimeTypes.PngIcon);
        }

        [HttpGet]
        [Route("browserconfig.xml")]
        public async Task<IActionResult> BrowserConfig()
        {
            var settings = await GetSettings();

            if (settings == null || (!settings.HasBrowserConfig && !settings.HasTile))
            {
                return NotFound();
            }

            if (!settings.HasBrowserConfig)
            {
                return GenerateBrowserConfig();
            }

            _contentTypeProvider.TryGetContentType(settings.BrowserConfigPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.BrowserConfigPath), contentType ?? DefaultMimeTypes.BrowserConfig);
        }

        [HttpGet]
        [Route("favicon.ico")]
        public async Task<IActionResult> Favicon()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasFaviconFallback)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.FaviconFallbackPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.FaviconFallbackPath), contentType ?? DefaultMimeTypes.Favicon);
        }

        [HttpGet]
        [Route("favicon-32x32.png")]
        public async Task<IActionResult> FaviconLarge()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasLargeFavicon)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.LargeFaviconPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.LargeFaviconPath), contentType ?? DefaultMimeTypes.PngIcon);
        }

        [HttpGet]
        [Route("favicon-16x16.png")]
        public async Task<IActionResult> FaviconDefault()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasDefaultFavicon)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.DefaultFaviconPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.DefaultFaviconPath), contentType ?? DefaultMimeTypes.PngIcon);
        }

        [HttpGet]
        [Route("icon.png")]
        public async Task<IActionResult> Icon()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasAppleTouchIcon)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.AppleTouchIconPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.AppleTouchIconPath), contentType ?? DefaultMimeTypes.PngIcon);
        }

        [HttpGet]
        [Route("safari-pinned-tab.svg")]
        public async Task<IActionResult> SafariPinnedTab()
        {
            var settings = await GetSettings();

            if (settings == null || !settings.HasSafariPinnedTab)
            {
                return NotFound();
            }

            _contentTypeProvider.TryGetContentType(settings.SafariPinnedTabPath, out var contentType);
            return File(await _mediaFileStore.GetFileStreamAsync(settings.SafariPinnedTabPath), contentType ?? DefaultMimeTypes.SvgIcon);
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
            return File(await _mediaFileStore.GetFileStreamAsync(settings.TilePath), contentType ?? DefaultMimeTypes.Tile);
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
            return File(await _mediaFileStore.GetFileStreamAsync(settings.WebAppManifestPath), contentType ?? DefaultMimeTypes.WebAppManifest);
        }

        #endregion

        #region Helper Methods

        private IActionResult GenerateBrowserConfig()
        {
            return Content(string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<browserconfig>
  <msapplication>
    <tile>
      <square70x70logo src=""{0}/tile.png""/>
      <square150x150logo src=""{0}/tile.png""/>
      <wide310x150logo src=""{0}/tile-wide.png""/>
      <square310x310logo src=""{0}/tile.png""/>
    </tile>
  </msapplication>
</browserconfig>", Request.PathBase), DefaultMimeTypes.BrowserConfig);
        }

        private async Task<FaviconSettings> GetSettings()
        {
            return (await _siteService.GetSiteSettingsAsync()).As<ContentItem>(typeof(FaviconSettings).Name)?.As<FaviconSettings>() ?? null;
        }

        #endregion
    }
}
