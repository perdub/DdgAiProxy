using DdgAiProxy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CustomClient>(new CustomClient());
builder.Services.AddSingleton<GlobalDialogManager>();
builder.Services.AddControllers();

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

var app = builder.Build();

app.UseRouting();
app.UseEndpoints((e)=>{
    e.MapControllers();
});

app.MapGet("/", () => "Hello World!");

app.Run();
