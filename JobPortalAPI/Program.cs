using Dapper;
using JobPortalAPI.Extensions;
using JobPortalAPI.Settings;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDatabaseServices(builder.Configuration); //for database connection
builder.Services.AddAutoMapper(typeof(MappingProfile));  //for dto mapping

builder.Services.AddAuthServices(builder.Configuration); //for jwt authentication
builder.Services.AddServices(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerServices();
SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
