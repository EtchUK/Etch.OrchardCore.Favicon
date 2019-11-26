using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK Ltd.",
    Category = "Content",
    Description = "Manage browser related icons (e.g. favicon, apple-touch-icon, etc...)",
    Name = "Favicons",
    Version = "0.0.1",
    Website = "https://etchuk.com",
    Dependencies = new string[] { "OrchardCore.ContentFields", "OrchardCore.Media" }
)]