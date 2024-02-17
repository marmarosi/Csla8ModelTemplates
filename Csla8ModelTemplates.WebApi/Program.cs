//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Csla8ModelTemplates.WebApi.Extensions;

// ---------- Create the app builder.
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(ConfigExtensions.Build);

// ********** Add services to the container.

builder.Services.AddCors(CorsExtensions.Setup);

builder.Services.AddSwaggerGenerator(builder.Environment);

builder.Services.AddDataAccessLayers();

builder.Services.AddCslaLibrary();

builder.Services.AddControllers();

// ---------- Build the application.
var app = builder.Build();

app.RunStorageSeeders();

// ********** Configure the HTTP request pipeline.

app.UseSwaggerDocumentation();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ---------- Start the application.
app.Run();
