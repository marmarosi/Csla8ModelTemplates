using Csla8RestApi.SnippetGenerator;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false);

IConfiguration config = builder.Build();

var author = GetAbsolutePath(config.GetValue<string>("Author"));

var declarationPath = GetAbsolutePath(config.GetValue<string>("Declarations"));
using var declarationStream = File.OpenRead(declarationPath);
var declarations = JsonSerializer.Deserialize<List<Declaration>>(declarationStream);

var resources = config.GetSection("SnippetResources").Get<List<SnippetResource>>();

foreach (var resource in resources)
{
    var mapPath = GetAbsolutePath(resource.MapBasePath);
    resource.SourceBasePath = GetAbsolutePath(resource.SourceBasePath);
    resource.TargetBasePath = GetAbsolutePath(resource.TargetBasePath);
    ProcessResource(mapPath, resource, declarations);
}

void ProcessResource(
    string mapPath,
    SnippetResource resource,
    List<Declaration> declarations
    )
{
    var maps = Directory.GetFiles(mapPath, "*.txt");
    foreach (var map in maps)
        Snippet.Generate(map, author, resource, declarations);

    var folders = Directory.GetDirectories(mapPath);
    foreach (var folder in folders)
        ProcessResource(folder, resource, declarations);
}

string GetAbsolutePath(
    string path
    )
{
    if (path.StartsWith("."))
        path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\", path);
    return path;
}
