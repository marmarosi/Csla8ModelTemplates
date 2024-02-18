using Csla8ModelTemplates.WebApi.Extensions;

// ---------- Create the app builder.
var builder = WebApplication.CreateBuilder(args);

//builder.Host.Add_JsonConfiguratioFile();

// ********** Add services to the container.

builder.Services.Add_Cors(builder.Environment);
builder.Services.Add_Swagger(builder.Environment);
builder.Services.Add_DataAccessLayers();
builder.Services.Add_Csla();
builder.Services.AddControllers();

// ---------- Build the application.
var app = builder.Build();

app.Run_StorageSeeders();

// ********** Configure the HTTP request pipeline.

app.Use_Swagger();
app.UseHttpsRedirection();
app.Use_Cors();
app.UseAuthorization();
app.MapControllers();

// ---------- Start the application.
app.Run();
