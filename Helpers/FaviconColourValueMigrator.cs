using Etch.OrchardCore.Favicon.Models;
using Etch.OrchardCore.Fields.Colour.Fields;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.Entities;
using OrchardCore.Settings;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Favicon.Helpers
{
    public class FaviconColourValueMigrator : IFaviconColourValueMigrator
    {
        private readonly ISiteService _siteService;

        public FaviconColourValueMigrator(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public async Task MigrateAsync()
        {
            var siteSettings = await _siteService.LoadSiteSettingsAsync();
            var faviconSettingsContentItem = siteSettings.As<ContentItem>(nameof(FaviconSettings));

            ContentExtensions.Apply(faviconSettingsContentItem, MigrateFaviconColourPreference(faviconSettingsContentItem));

            siteSettings.Properties[nameof(FaviconSettings)] = JObject.FromObject(faviconSettingsContentItem);

            await _siteService.UpdateSiteSettingsAsync(siteSettings);
        }

        public static ContentItem MigrateFaviconColourPreference(ContentItem faviconSettingsContentItem)
        {
            var faviconSettings = faviconSettingsContentItem.As<FaviconSettings>();

            faviconSettings.SafariPinnedTabColour = new ColourField { Value = faviconSettingsContentItem.Content.FaviconSettings.SafariPinnedTabColour.Text };
            faviconSettings.ThemeColour = new ColourField { Value = faviconSettingsContentItem.Content.FaviconSettings.ThemeColour.Text };
            faviconSettings.TileColour = new ColourField { Value = faviconSettingsContentItem.Content.FaviconSettings.TileColour.Text };

            faviconSettings.Remove("SafariPinnedTabColour");
            faviconSettings.Remove("ThemeColour");
            faviconSettings.Remove("TileColour");

            faviconSettings.Apply(faviconSettings.SafariPinnedTabColour);
            faviconSettings.Apply(faviconSettings.ThemeColour);
            faviconSettings.Apply(faviconSettings.TileColour);
            faviconSettingsContentItem.Apply(nameof(FaviconSettings), faviconSettings);

            return faviconSettingsContentItem;
        }
    }

    public interface IFaviconColourValueMigrator
    {
        Task MigrateAsync();
    }
}