using RSI_REST_SERVER.Middleware;
using RSI_REST_SERVER.Services.IServices;
using RSI_REST_SERVER.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Rsi Api", Version = "v1" });
});

builder.Services.AddScoped<IMessageService, MessageService>();

var app = builder.Build();

app.UseMiddleware<MyResponseMiddleware>();

app.UseWhen(context => context.Request.Path.ToString().Contains("/Secured"), appBuilder =>
{
    appBuilder.UseMiddleware<BasicAuthMiddleware>();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseOpenApi();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rsi Api");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
