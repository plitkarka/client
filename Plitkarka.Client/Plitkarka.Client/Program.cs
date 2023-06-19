using Microsoft.Extensions.Options;
using Plitkarka.Client;
using Plitkarka.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddConfiguration()
            .AddApiClient();
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(Provider =>
{
    var http = new HttpClient();
    http.BaseAddress = new Uri(Provider.GetRequiredService<IOptions<HttpClientConfiguration>>().Value.BaseUrl);
    return http;
});

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

app.Run();
