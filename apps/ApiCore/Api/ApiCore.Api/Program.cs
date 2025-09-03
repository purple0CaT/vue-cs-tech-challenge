using ApiCore.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureBuilder();

var app = builder.Build();
app.ConfigureApplication();

app.Run();
