using WebAPI;
using WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

// To Allow Cors
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.DIScopes();

builder.Services.AddControllers(config =>
{
    config.Filters.Add<ExceptionFilter>();
    config.Filters.Add<ActionFilter>();
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDirectoryBrowser();

var app = builder.Build();

app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials());

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI(x => x.DefaultModelsExpandDepth(-1));

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
