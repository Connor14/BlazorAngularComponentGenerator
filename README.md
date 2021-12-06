![Nuget](https://img.shields.io/nuget/v/BlazorAngularComponentGenerator?style=flat-square)

# BlazorAngularComponentGenerator

Generate Angular components from Blazor components.

This project was modified from the .NET Foundation's original sample code here: https://github.com/aspnet/samples/tree/main/samples/aspnetcore/blazor/JSComponentGeneration

For more information, see the following:
* Microsoft .NET Blog: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-net-6-rc-1/#generate-angular-and-react-components-using-blazor
* Microsoft documentation: https://docs.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-6.0#generate-angular-and-react-components

## Running the Demo

_Note that the demo does **not** use the NuGet package_

* Start `BlazorApp` using `dotnet watch`
* Start `angular-app-with-blazor` using `npm start`
* Navigate to the Angular app at `http://localhost:4200`
* Open the browser's dev tools and wait for `blazor.webassembly.js` to load
* Interact with the app!

## Getting Started

### Blazor WebAssembly Setup

* Install the `BlazorAngularComponentGenerator` [NuGet package](https://www.nuget.org/packages/BlazorAngularComponentGenerator/) to your Blazor project
* Add the `BlazorAngularComponentGenerator.Attributes` namespace your `_Imports.razor` file
```C#
@using BlazorAngularComponentGenerator.Attributes
```
* Decorate your Razor component files with
```C#
@attribute [GenerateAngular]
```
* Register your components for Angular in `Program.cs`
```C#
using BlazorAngularComponentGenerator.Extensions;
using BlazorApp;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.RegisterForAngular<YourComponent>();

await builder.Build().RunAsync();
```
* Build your project
* Find the generated Angular components in `bin/Debug/net6.0/js`
* Run your Blazor WebAssembly project and take note of the URL

### Angular Setup

* Create a `proxy.conf.json` file in your Angular app's `src` folder with the following configuration:
```JSON
{
  "/": {
    "target": "<url pointing to your Blazor app>",
    "secure": false
  }
}
```
* Add the `src/proxy.conf.json` to `angular.json` at `projects.<your app>.architect.serve.options.proxyConfig`. The following is a truncated `angular.json` example:
```JSON
{
  "$schema": "./node_modules/@angular/cli/lib/config/schema.json",
  "version": 1,
  "newProjectRoot": "projects",
  "projects": {
    "angular-app-with-blazor": {
      "architect": {
        "serve": {
          "options": {
            "proxyConfig": "src/proxy.conf.json"
          },
        },
      }
    }
  },
}
```
* Set up the Blazor WebAssembly framework. Place the following after `app-root` in the `body` tag in `index.html`
```HTML
<script>
  let resolveBlazorReadyPromise = null;

  // The Promise that will resolve when Blazor is ready for custom components
  BlazorReadyPromise = new Promise(function (resolve, reject) {
    resolveBlazorReadyPromise = resolve;
  });

  // The JavaScriptInitializer function that will run for each component when Blazor is ready for that component
  window.initializeBlazorComponent = function (component, params) {
    resolveBlazorReadyPromise();
  };
</script>
<script src="_framework/blazor.webassembly.js"></script>
```
* Copy your generated Angular components to your `app` folder
* Update your `app.module.ts` to include your new component declarations and the `CUSTOM_ELEMENTS_SCHEMA`. The following is a truncated `app.module.ts` example
```TypeScript
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';

// ...

@NgModule({
  declarations: [
    AppComponent,
    // <your components>
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA // Tells Angular we will have custom tags in our templates
  ]
})
export class AppModule { }
```
* Run your Angular application

## Building the NuGet package

* Navigate to the `BlazorAngularComponentGenerator` directory
* Run `dotnet pack -c Release -p:NuspecFile=BlazorAngularComponentGenerator.nuspec`

## Notes

* This project is my first foray into MSBuild related development. Parts of the project likely don't follow best practices and may not work 100% reliably.
* At times you may need to do a `rebuild` in order for the MSBuild tasks to run and copy the files correctly
