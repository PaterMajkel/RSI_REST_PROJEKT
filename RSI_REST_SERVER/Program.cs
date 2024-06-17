using RSI_REST_SERVER.Middleware;
using RSI_REST_SERVER.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApiDocument();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Rsi Api", Version = "v1" });
});

builder.Services.AddSingleton<IEventSrv, EventSrv>();

var app = builder.Build();

//app.UseMiddleware<MyResponseMiddleware>();

//app.UseMiddleware<BasicAuthMiddleware>();
builder.Services.AddCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rsi Api");
    });
    app.UseCors(x => x
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .SetIsOriginAllowed(origin => true) // allow any origin
                   .AllowCredentials()); // allow credentials
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
