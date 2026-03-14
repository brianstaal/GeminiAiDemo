using Google.GenAI;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System.Text;

AnsiConsole.Clear();

AnsiConsole.Write(
    new FigletText("Gemini AI Demo App")
        .LeftJustified()
        .Color(Color.Purple));

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();


var geminiApiKey = configuration["GoogleGemini:ApiKey"];
var geminiModel = configuration["GoogleGemini:Model"];

if (string.IsNullOrEmpty(geminiApiKey) || string.IsNullOrEmpty(geminiModel))
{
    Console.WriteLine("Google Gemini ApiKey or Model is not set. Please set it in the configuration & secrets.");
    return;
}

var rootDir = AppContext.BaseDirectory;
//var instructionFile = Path.Combine(rootDir, "AppData", "instructions.txt");
var pdfFile = Path.Combine(rootDir, "AppData", "Beierholm.pdf");
var outputSchema = Path.Combine(rootDir, "AppData", "output.schema.json");

//var sb = new StringBuilder();
//sb.Append(System.IO.File.ReadAllText(instructionFile));

// Create a new Gemini client
var client = new Client(apiKey:geminiApiKey);
var pdfFileRef = await client.Files.UploadAsync(filePath: pdfFile);
var outputRef = await client.Files.UploadAsync(filePath: outputSchema);

var response = await client.Models.GenerateContentAsync(model: geminiModel, contents: "Extract data from Beierholm.pdf by the given output.schema.json, and return a json");

var responseText = response.Text;
Console.WriteLine(responseText);

// Todo: Add the pfdFile + outputFormatFile gemini and give the instructionContent as the system prompt. Then get the response and write it to the console.




AnsiConsole.MarkupLine("[green]Starting....[/]");