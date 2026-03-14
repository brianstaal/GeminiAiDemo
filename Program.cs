using Google.GenAI;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using Google.GenAI.Types;

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
    AnsiConsole.MarkupLine("[red]FAILED![/]");
    AnsiConsole.MarkupLine("[red]Google Gemini ApiKey or Model is not set. Please set it in the configuration & secrets.[/]");
    return;
}

var rootDir = AppContext.BaseDirectory;
var pdfFile = Path.Combine(rootDir, "AppData", "invoicesample.pdf");
var pdfFilename = Path.GetFileName(pdfFile);
var outputSchema = Path.Combine(rootDir, "AppData", "output.schema.json");
var outputSchemaFilename = Path.GetFileName(outputSchema);

AnsiConsole.MarkupLine("[green]Starting....[/]");

// Create a new Gemini client
var client = new Client(apiKey:geminiApiKey);

// Attach files
var pdfFileRef = await client.Files.UploadAsync(filePath: pdfFile);
var outputRef = await client.Files.UploadAsync(filePath: outputSchema);

var response = await client.Models.GenerateContentAsync(
    model: geminiModel,
    contents: new Content
    {
        Parts =
        [
            Part.FromUri(pdfFileRef.Uri!, pdfFileRef.MimeType),
            Part.FromUri(outputRef.Uri!, outputRef.MimeType),
            Part.FromText($"Extract data from {pdfFilename} given the {outputSchemaFilename} => return the json result")
        ]
    });

var responseText = response.Text;
Console.WriteLine(responseText);

AnsiConsole.MarkupLine("[green]DONE![/]");