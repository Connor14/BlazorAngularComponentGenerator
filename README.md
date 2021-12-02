# BlazorAngularComponentGenerator

Generate Angular components from Blazor components.

Modified from the original source: https://github.com/aspnet/samples/tree/main/samples/aspnetcore/blazor/JSComponentGeneration

### Getting Started

_More instructions to come..._

* Install the `BlazorAngularComponentGenerator` NuGet package
* Decorate your components with the `[GenerateAngular]` attribute
* Register your components with Blazor
* Build your project
* Copy your components to your Angular project
* Run the Blazor app and Angular apps
* ...

### Running the Demo

_Note that the demo does **not** use the NuGet package_

* Start `BlazorApp` using `dotnet watch`
* Start `angular-app-with-blazor` using `npm start`
* Navigate to the Angular app at `http://localhost:4200`
* Open the browser's dev tools and wait for `blazor.webassembly.js` to load
* Interact with the app!

### Building the NuGet package

* Navigate to the `BlazorAngularComponentGenerator` directory
* Run `dotnet pack -c Release -p:NuspecFile=BlazorAngularComponentGenerator.nuspec`

### Notes

* This project is my first foray into MSBuild related development. Parts of the project likely don't follow best practices and may not work 100% reliably.
* At times you may need to do a `rebuild` in order for the MSBuild tasks to run and copy the files correctly
