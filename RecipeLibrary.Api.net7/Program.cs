using RecipeLibrary.Api.net7;
using RecipeLibrary.Core;
using RecipeLibrary.Core.Services;
using RecipeLibrary.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("mongoSettings"));
builder.Services.AddSingleton<IMongoSettings, MongoSettings>();
builder.Services.AddTransient<IMongoQueryBuilder, MongoQueryBuilder>();
builder.Services.AddTransient<IMongoConnection, MongoConnection>();
builder.Services.AddTransient<IRecipeService,RecipeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.Run();
