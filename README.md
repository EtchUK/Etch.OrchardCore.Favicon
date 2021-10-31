# Etch.OrchardCore.Favicon

Manage browser related features such as icons (e.g. favicon, apple-touch-icon, etc...), tiles & configuration files (e.g. `site.webmanifest`, `browserconfig.xml`).

## Build Status

[![Build Status](https://secure.travis-ci.org/etchuk/Etch.OrchardCore.Favicon.png?branch=master)](http://travis-ci.org/etchuk/Etch.OrchardCore.Favicon) [![NuGet](https://img.shields.io/nuget/v/Etch.OrchardCore.Favicon.svg)](https://www.nuget.org/packages/Etch.OrchardCore.Favicon)

## Orchard Core Reference

This module is referencing a stable build of Orchard Core ([`1.1.0`](https://www.nuget.org/packages/OrchardCore.Module.Targets/1.1.0)).

## Installing

This module is available on [NuGet](https://www.nuget.org/packages/Etch.OrchardCore.Favicon). Add a reference to your Orchard Core web project via the NuGet package manager. Search for "Etch.OrchardCore.Fields", ensuring include prereleases is checked.

Alternatively you can [download the source](https://github.com/etchuk/Etch.OrchardCore.Favicon/archive/master.zip) or clone the repository to your local machine. Add the project to your solution that contains an Orchard Core project and add a reference to Etch.OrchardCore.Favicon.

## Usage

### Configure Media Library

Users can define which assets are included in responses for pre-configured endpoints. To enable uploading these assets to the media library your site must be [configured to allow the file extensions](https://orchardcore.readthedocs.io/en/dev/docs/reference/modules/Media/). Ensure `.ico`, `.manifest`, `.png` & `.xml` are included in the `AllowedFileExtensions` collection, as shown below.

```
{
  "OrchardCore": {
    "OrchardCore.Media": {
      "AllowedFileExtensions": [
        // Images
        ".jpg",
        ".jpeg",
        ".png",
        ".gif",
        ".ico",
        ".svg",

        // configuration
        ".xml",
        ".webmanifest"
      ],
      "SupportedSizes": [ 100, 135, 180, 210, 375, 425, 600, 768, 1024, 1280, 1440, 1920, 2560 ]
    }
  }
}
```

### Enabling Feature

Enable the feature via a recipe or the admin dashboard for your tenant. Once enabled this will add a new content type, "FaviconSettings" that has a stereotype of ["CustomSettings"](https://orchardcore.readthedocs.io/en/dev/docs/reference/modules/CustomSettings/). This will make a new menu option available ("Favicon") within the admin dashboard under "Configuration" -> "Settings". Use this section to upload and select different files that will represent the following browser features.

#### Favicon

This should ideally be a `.ico` file and when selected will render when a request is made to `/favicon.ico`. Once defined a `link` attribute will render in the `head` section to let the browser know the location of the favicon.

#### Apple Touch Icon

This should ideally be a `.png` file and when selected will render when a request is made to `/icon.png`. Once defined a `link` attribute will render in the `head` section to let the browser know the location of the apple touch icon. If you're using a web app manifest then it's recommended that the contents points to `icon.png` as shown in the [web app manifest](#web-app-manifest) section.

### Tiles

Both the "Tile" and "Tile Wide" should ideally be `.png` files and when selected will render when a request is made to `tile.png` or `tile-wide.png`. If you're using a browser config then it's recommended that the contents of this file point to `tile.png` & `tile-wide.png` as shown in the [browser config](#browser-config) section.

#### Browser Config

This must be an `.xml` file and when selected will render when a request is made to `/browserconfig.xml`. When [tiles](#tiles) have been defined these should be included in the contents of the XML file, as shown below. Alternatively you can omit selecting a browser config file and select tile images and the browserconfig will be generated replicating the example shown below.

```
<?xml version="1.0" encoding="utf-8"?>
<browserconfig>
  <msapplication>
    <tile>
      <square70x70logo src="tile.png"/>
      <square150x150logo src="tile.png"/>
      <wide310x150logo src="tile-wide.png"/>
      <square310x310logo src="tile.png"/>
    </tile>
  </msapplication>
</browserconfig>
```

For more in-depth information about the browserconfig.xml file, see [MSDN](<https://docs.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/platform-apis/dn320426(v=vs.85)>).

#### Web App Manifest

This must be an `.webmanifest` file and when selected will render when a request is made to `/site.webmanifest`. Once defined a `link` attribute will render in the `head` section to let the browser know about the manifest file and it's location. When an [apple touch icon](#apple-touch-icon) has been defined, this should be included in the contents of the manifest, as shown below.

```
{
  "short_name": "",
  "name": "",
  "icons": [{
    "src": "icon.png",
    "type": "image/png",
    "sizes": "192x192"
  }],
  "start_url": "/?utm_source=homescreen",
  "background_color": "#fafafa",
  "theme_color": "#fafafa"
}
```

Head over to [MDN to find out more](https://developer.mozilla.org/en-US/docs/Web/Manifest) about the web app manifest.

## Credits

Knowledge about the files & URLs were obtained from the awesome [HTML5 boilerplate project](https://html5boilerplate.com/).
