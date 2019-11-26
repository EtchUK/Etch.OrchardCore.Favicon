using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.Media.Fields;
using System.Linq;

namespace Etch.OrchardCore.Favicon.Models
{
    public class FaviconSettings : ContentPart
    {
        #region Apple Touch Icon

        public string AppleTouchIconPath
        {
            get
            {
                return this.Get<MediaField>("AppleTouchIcon")?.Paths.FirstOrDefault() ?? null;
            }
        }

        public bool HasAppleTouchIcon
        {
            get { return this.Get<MediaField>("AppleTouchIcon")?.Paths.Any() ?? false; }
        }

        #endregion

        #region Browser Config

        public string BrowserConfigPath
        {
            get
            {
                return this.Get<MediaField>("BrowserConfig")?.Paths.FirstOrDefault() ?? null;
            }
        }

        public bool HasBrowserConfig
        {
            get { return this.Get<MediaField>("BrowserConfig")?.Paths.Any() ?? false; }
        }

        #endregion

        #region Favicon

        public string FaviconPath
        {
            get
            {
                return this.Get<MediaField>("Favicon")?.Paths.FirstOrDefault() ?? null;
            }
        }

        public bool HasFavicon
        {
            get { return this.Get<MediaField>("Favicon")?.Paths.Any() ?? false; }
        }

        #endregion

        #region Tile

        public string TilePath
        {
            get
            {
                return this.Get<MediaField>("Tile")?.Paths.FirstOrDefault() ?? null;
            }
        }

        public bool HasTile
        {
            get { return this.Get<MediaField>("Tile")?.Paths.Any() ?? false; }
        }

        #endregion

        #region Tile Wide

        public string TileWidePath
        {
            get
            {
                return this.Get<MediaField>("TileWide")?.Paths.FirstOrDefault() ?? null;
            }
        }

        public bool HasTileWide
        {
            get { return this.Get<MediaField>("TileWide")?.Paths.Any() ?? false; }
        }

        #endregion

        #region Web App Manifest

        public string WebAppManifestPath
        {
            get
            {
                return this.Get<MediaField>("WebAppManifest")?.Paths.FirstOrDefault() ?? null;
            }
        }

        public bool HasWebAppManifest
        {
            get { return this.Get<MediaField>("WebAppManifest")?.Paths.Any() ?? false; }
        }

        #endregion
    }
}
