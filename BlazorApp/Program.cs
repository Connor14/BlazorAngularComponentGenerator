using BlazorAngularComponentGenerator.Extensions;
using BlazorApp;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.RegisterForAngular<Greeting>();
builder.RootComponents.RegisterForAngular<Counter>();

await builder.Build().RunAsync();
