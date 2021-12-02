using BlazorAngularComponentGenerator.Build.Angular;

// Create a generator
var generator = new AngularComponentGenerator(args[0], args[1], args[2], args[3]);

// Do the generation
var result = generator.Execute();

// Return a result indicating success or failure
return result ? 0 : 1;