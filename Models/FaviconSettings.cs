using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;
using OrchardCore.ContentFields.Fields;
using System.Linq;
using Etch.OrchardCore.Fields.Colour.Fields;

namespace Etch.OrchardCore.Favicon.Models
{
    public class FaviconSettings : ContentPart
    {
        #region Android Icon

        public string AndroidLargeIconPath
        {
            get
            {
                return this.Get<MediaField>("AndroidLargeIcon")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        public string AndroidSmallIconPath
        {
            get
            {
                return this.Get<MediaField>("AndroidSmallIcon")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        public bool HasAndroidLargeIcon
        {
            get { return this.Get<MediaField>("AndroidLargeIcon")?.Paths?.Any() ?? false; }
        }

        public bool HasAndroidSmallIcon
        {
            get { return this.Get<MediaField>("AndroidSmallIcon")?.Paths?.Any() ?? false; }
        }

        #endregion

        #region Apple Touch Icon

        public string AppleTouchIconPath
        {
            get
            {
                return this.Get<MediaField>("AppleTouchIcon")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        public bool HasAppleTouchIcon
        {
            get { return this.Get<MediaField>("AppleTouchIcon")?.Paths?.Any() ?? false; }
        }

        #endregion

        #region Browser Config

        public string BrowserConfigPath
        {
            get
            {
                return this.Get<MediaField>("BrowserConfig")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        public bool HasBrowserConfig
        {
            get { return this.Get<MediaField>("BrowserConfig")?.Paths?.Any() ?? false; }
        }

        #endregion

        #region Favicon

        public string DefaultFaviconPath
        {
            get
            {
                return this.Get<MediaField>("Favicon")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        public string FaviconFallbackPath
        {
            get
            {
                return this.Get<MediaField>("FaviconFallback")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        public bool HasDefaultFavicon
        {
            get { return this.Get<MediaField>("Favicon")?.Paths?.Any() ?? false; }
        }

        public bool HasFaviconFallback
        {
            get { return this.Get<MediaField>("FaviconFallback")?.Paths?.Any() ?? false; }
        }

        public bool HasLargeFavicon
        {
            get { return this.Get<MediaField>("LargeFavicon")?.Paths?.Any() ?? false; }
        }

        public string LargeFaviconPath
        {
            get
            {
                return this.Get<MediaField>("LargeFavicon")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        #endregion

        #region SafariPinnedTab

        public bool HasSafariPinnedTab
        {
            get { return this.Get<MediaField>("SafariPinnedTab")?.Paths?.Any() ?? false; }
        }

        public bool HasSafariPinnedTabColour
        {
            get { return !string.IsNullOrWhiteSpace(SafariPinnedTabColour?.Value); }
        }

        public ColourField SafariPinnedTabColour
        {
            get; set;
        }

        public string SafariPinnedTabPath
        {
            get
            {
                return this.Get<MediaField>("SafariPinnedTab")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        #endregion

        #region Theme

        public bool HasThemeColour
        {
            get { return !string.IsNullOrWhiteSpace(ThemeColour?.Value); }
        }

        public ColourField ThemeColour
        {
            get; set;
        }

        #endregion

        #region Tile

        public bool HasTile
        {
            get { return this.Get<MediaField>("Tile")?.Paths?.Any() ?? false; }
        }

        public bool HasTileColour
        {
            get { return !string.IsNullOrWhiteSpace(TileColour?.Value); }
        }

        public ColourField TileColour
        {
            get; set;
        }

        public string TilePath
        {
            get
            {
                return this.Get<MediaField>("Tile")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        #endregion

        #region Web App Manifest

        public string WebAppManifestPath
        {
            get
            {
                return this.Get<MediaField>("WebAppManifest")?.Paths?.FirstOrDefault() ?? null;
            }
        }

        public bool HasWebAppManifest
        {
            get { return this.Get<MediaField>("WebAppManifest")?.Paths?.Any() ?? false; }
        }

        #endregion
    }
}
