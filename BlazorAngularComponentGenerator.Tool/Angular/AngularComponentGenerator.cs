using BlazorAngularComponentGenerator.Build.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BlazorAngularComponentGenerator.Build.Angular
{
    public class AngularComponentGenerator
    {
        public string OutputPath { get; }
        public string IntermediateOutputPath { get; }
        public string AssemblyName { get; set; }
        public string JavaScriptComponentOutputDirectory { get; }

        public AngularComponentGenerator(string outputPath, string intermediateOutputPath, string assemblyName, string javaScriptComponentOutputDirectory)
        {
            OutputPath = outputPath;
            IntermediateOutputPath = intermediateOutputPath;
            AssemblyName = assemblyName;
            JavaScriptComponentOutputDirectory = javaScriptComponentOutputDirectory;
        }

        // TODO
        // Turn this into a Console app (self contained, single file)
        // Use MSBuild targets to run the console app 
        // Invoke the console app using all of the paths from above.
        // Since the console app lifetime will be tied to the MSBuild task, the IntermediateOutputPath with teh TagHelper cache will still exist
        // See here: https://natemcmaster.com/blog/2017/11/11/msbuild-task-with-dependencies/

        public bool Execute()
        {
            var assemblyFilePath = $"{OutputPath}/{AssemblyName}.dll";
            HashSet<string> componentNames;

            try
            {
                componentNames = new(RazorComponentReader.ReadWithAttributeFromAssembly(assemblyFilePath, "GenerateAngularAttribute"));
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception occurred while reading the specified assembly: {e.Message}");
                return false;
            }

            var tagHelperCacheFileName = $"{IntermediateOutputPath}/{AssemblyName}.TagHelpers.output.cache";
            List<RazorComponentDescriptor> componentDescriptors;

            try
            {
                componentDescriptors = RazorComponentDescriptorReader.ReadWithNamesFromTagHelperCache(tagHelperCacheFileName, componentNames);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception occurred while reading the tag helper output cache: {e.Message}");
                return false;
            }

            var blazorAdapterDirectory = Path.Combine(JavaScriptComponentOutputDirectory, "blazor-adapter");
            var blazorAdapterFilePath = Path.Combine(blazorAdapterDirectory, "blazor-adapter.component.ts");

            Directory.CreateDirectory(blazorAdapterDirectory);

            using (var blazorAdapterStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BlazorAngularComponentGenerator.Tool.js.blazor_adapter.blazor-adapter.component.ts"))
            using (var blazorAdapterFile = File.Create(blazorAdapterFilePath))
            {
                blazorAdapterStream.CopyTo(blazorAdapterFile);
            }

            foreach (var componentDescriptor in componentDescriptors)
            {
                try
                {
                    AngularComponentWriter.Write(JavaScriptComponentOutputDirectory, componentDescriptor);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Could not write an Angular component from Razor component '{componentDescriptor.Name}': {e.Message}");
                    return false;
                }
            }

            Console.WriteLine("Angular component generation complete.");
            return true;
        }
    }
}
