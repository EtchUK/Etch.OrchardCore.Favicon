using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK Ltd.",
    Category = "Content",
    Description = "Manage browser related features (e.g. favicon, apple-touch-icon, browserconfig.xml, etc...)",
    Name = "Favicons",
    Version = "0.2.1",
    Website = "https://etchuk.com",
    Dependencies = new string[] { "OrchardCore.ContentFields", "OrchardCore.Media" }
)]