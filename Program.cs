using Microsoft.Extensions.Configuration;
using Spectre.Console;

AnsiConsole.Clear();

// 1. Show Banner
AnsiConsole.Write(
    new FigletText("Gemini AI Demo App")
        .LeftJustified()
        .Color(Color.Purple));

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();


var geminiToken = configuration["GoogleGemini:Token"];

if (string.IsNullOrEmpty(geminiToken))
{
    Console.WriteLine("Google Gemini token is not set. Please set it in the configuration & secrets.");
    return;
}

var rootDir = AppContext.BaseDirectory;
var instructionFile = Path.Combine(rootDir, "AppData", "instructions.txt");
var pdfFile = Path.Combine(rootDir, "AppData", "Beierholm.pdf");
var outputFormatFile = Path.Combine(rootDir, "AppData", "output.json");

var instructionContent = File.ReadAllText(instructionFile);

// Add the pfdFile + outputFormatFile gemini and give the instructionContent as the system prompt. Then get the response and write it to the console.




AnsiConsole.MarkupLine("[green]Starting....[/]");