using Csla8RestApi.SnippetGenerator;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false);

IConfiguration config = builder.Build();

var data = new BaseData
{
    Author = GetAbsolutePath(config.GetValue<string>("Author")),
    DeclarationsPath = GetAbsolutePath(config.GetValue<string>("Declarations")),
    TargetBasePath = GetAbsolutePath(config.GetValue<string>("TargetBasePath")),
    TestBasePath = GetAbsolutePath(config.GetValue<string>("TestBasePath"))
};
using var declarationStream = File.OpenRead(data.DeclarationsPath);
var declarations = JsonSerializer.Deserialize<List<Declaration>>(declarationStream);

var resources = config.GetSection("SnippetResources").Get<List<SnippetResource>>();
foreach (var resource in resources)
{
    var mapPath = GetAbsolutePath(resource.MapBasePath);
    resource.SourceBasePath = GetAbsolutePath(resource.SourceBasePath);
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
        Snippet.Generate(data, map, resource, declarations);

    var folders = Directory.GetDirectories(mapPath);
    foreach (var folder in folders)
        ProcessResource(folder, resource, declarations);
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
