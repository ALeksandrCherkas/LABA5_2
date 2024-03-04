using WebApplication1;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;



var builder = WebApplication.CreateBuilder();
var logger_factory = LoggerFactory.Create(config =>
{
    config.AddFile($"{Directory.GetCurrentDirectory()}\\Logs\\Log.txt");
}).CreateLogger("Logger");

var app = builder.Build();

app.UseMiddleware<LoggingMiddleware>(logger_factory);
app.UseMiddleware<FormMiddleware>();
app.Run();
