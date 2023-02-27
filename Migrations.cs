using Etch.OrchardCore.Favicon.Helpers;
using Etch.OrchardCore.Favicon.Models;
using Etch.OrchardCore.Fields.Colour.Fields;
using Etch.OrchardCore.Fields.Colour.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using Parlot.Fluent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Favicon
{
    public class Migrations : DataMigration
    {
        #region Dependencies

        private readonly IRecipeMigrator _recipeMigrator;
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IFaviconColourValueMigrator _faviconColourValueMigrator;

        #endregion

        #region Constructor

        public Migrations(IContentDefinitionManager contentDefinitionManager, IRecipeMigrator recipeMigrator, IFaviconColourValueMigrator faviconColourValueMigrator)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _recipeMigrator = recipeMigrator;
            _faviconColourValueMigrator = faviconColourValueMigrator;
        }

        #endregion

        #region Migrations

        public async Task<int> CreateAsync()
        {
            await _recipeMigrator.ExecuteAsync("create.recipe.json", this);

            return 1;
        }

        public async Task<int> UpdateFrom1Async()
        {
            await _recipeMigrator.ExecuteAsync("1.recipe.json", this);

            return 2;
        }

        public int UpdateFrom2()
        {
            _contentDefinitionManager.AlterPartDefinition("FaviconSettings", builder => builder
                .RemoveField("TileColour")
                .RemoveField("SafariPinnedTabColour")
                .RemoveField("ThemeColour"));

            return 3;
        }

        public int UpdateFrom3()
        {
            _contentDefinitionManager.AlterPartDefinition("FaviconSettings", builder => builder
                .WithField("TileColour", field => field
                    .WithDisplayName("Tile Colour")
                    .WithPosition("8")
                    .OfType(nameof(ColourField))
                    .WithSettings(new ColourFieldSettings
                    {
                        AllowCustom = true,
                        AllowTransparent = false,
                        DefaultValue = "#90ee90",
                    }))
                .WithField("SafariPinnedTabColour", field => field
                    .WithDisplayName("Safari Pinned Tab Colour")
                    .WithPosition("11")
                    .OfType(nameof(ColourField))
                    .WithSettings(new ColourFieldSettings
                    {
                        AllowCustom = true,
                        AllowTransparent = false,
                        DefaultValue = "#90EE90",
                    }))
                .WithField("ThemeColour", field => field
                    .WithDisplayName("Theme Colour")
                    .WithPosition("12")
                    .OfType(nameof(ColourField))
                    .WithSettings(new ColourFieldSettings
                    {
                        Hint = "Set the toolbar colour.",
                        AllowCustom = true,
                        AllowTransparent = false,
                        DefaultValue = "#90EE90",
                    }))
                );

            return 4;
        }

        public async Task<int> UpdateFrom4()
        {
            await _faviconColourValueMigrator.MigrateAsync();
            return 5;
        }

        #endregion
    }
}
