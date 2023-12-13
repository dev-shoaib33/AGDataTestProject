using AGDataTestProject.Interfaces;
using AGDataTestProject.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();  
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetSection("MongoDb:ConnectionString").Value));
builder.Services.AddSingleton(s =>
{
    var client = s.GetRequiredService<IMongoClient>();
    return client.GetDatabase(builder.Configuration.GetSection("MongoDb:Database").Value);
});
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
