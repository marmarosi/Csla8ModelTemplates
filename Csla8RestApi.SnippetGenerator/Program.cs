using Csla8RestApi.SnippetGenerator;
using Csla8RestApi.SnippetGenerator.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Config.json", optional: false);

IConfiguration config = builder.Build();

// Get base configuration.
var data = new BaseData
{
    Author = GetAbsolutePath(config.GetValue<string>("Author")),
    TargetBasePath = GetAbsolutePath(config.GetValue<string>("TargetBasePath")),
    TestBasePath = GetAbsolutePath(config.GetValue<string>("TestBasePath"))
};
var snippetMapPath = GetAbsolutePath(".\\SnippetMaps");

// Get snippet declarations.
using var declarationStream = File.OpenRead(GetAbsolutePath(".\\SnippetMaps\\declarations.json"));
data.Declarations = (await JsonSerializer.DeserializeAsync<List<Declaration>>(declarationStream))!;

// Get snippet categories.
using var categoryStream = File.OpenRead(GetAbsolutePath(".\\SnippetMaps\\categories.json"));
data.Summary = (await JsonSerializer.DeserializeAsync<List<Category>>(categoryStream))!;

// Get the project path of template sources.
data.TemplateSources = config.GetSection("TemplateSources").Get<List<TemplateSource>>()!;
foreach (var templateSource in data.TemplateSources)
    templateSource.SourceBasePath = GetAbsolutePath(templateSource.SourceBasePath);

// Get templates.
data.SnippetTemplate = await File.ReadAllTextAsync("Templates\\Snippet.xml");
data.LiteralTemplate = await File.ReadAllTextAsync("Templates\\Literal.xml");

// Generate snippets.
ProcessResource(snippetMapPath, data);

// Generate summary.
Summary.Generate(data);
DocsData.Generate(data);

#region Helper methods

void ProcessResource(
    string snippetMapPath,
    BaseData data
    )
{
    var snippetMaps = Directory.GetFiles(snippetMapPath, "*.txt");
    foreach (var snippetMap in snippetMaps)
        Snippet.Generate(snippetMap, data);

    var folders = Directory.GetDirectories(snippetMapPath);
    foreach (var folder in folders)
        ProcessResource(folder, data);
}

string GetAbsolutePath(
    string? path
    )
{
    if (string.IsNullOrWhiteSpace(path))
        path = Directory.GetCurrentDirectory();
    if (path.StartsWith('.'))
        path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\", path);
    return path;
}

#endregion
