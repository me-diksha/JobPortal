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

//adding cors policy for allowing browser to make connection with our api
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowVueApp");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
