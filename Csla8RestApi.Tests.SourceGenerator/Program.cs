using Csla8RestApi.Tests.SourceGenerator;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json", optional: false);

IConfiguration config = builder.Build();

var data = new BaseData
{
    SnippetBasePath = GetAbsolutePath(config.GetValue<string>("SnippetBasePath")),
    ContractBasePath = GetAbsolutePath(config.GetValue<string>("ContractBasePath")),
    DalBasePath = GetAbsolutePath(config.GetValue<string>("DalBasePath")),
    ModelBasePath = GetAbsolutePath(config.GetValue<string>("ModelBasePath")),
    ControllerBasePath = GetAbsolutePath(config.GetValue<string>("ControllerBasePath"))
};
var mapFolder = GetAbsolutePath(GetAbsolutePath(".\\TestMaps"));
ProcessMaps(mapFolder, data);

void ProcessMaps(
    string mapFolder,
    BaseData data
    )
{
    var mapPaths = Directory.GetFiles(mapFolder, "*.txt");
    foreach (var mapPath in mapPaths)
        Source.Generate(mapPath, data);

    var folders = Directory.GetDirectories(mapFolder);
    foreach (var folder in folders)
        ProcessMaps(folder, data);

}

string GetAbsolutePath(
    string path
    )
{
    if (path.StartsWith("."))
        path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\", path);
    return path;
}
