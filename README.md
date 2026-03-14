# GeminiAiDemo

The GeminiAiDemo is a .NET application that demonstrates how to integrate with Google's Gemini AI models.

It allows you to interact with the Gemini API using your API key and explore the capabilities of the AI models.

The demo show an example on how to give the AI a invoice PDF file and a schema to extract the data from the PDF file and return it in a structured format.

## Apikey
Login to https://aistudio.google.com/api-keys and create an API key

## Prerequisites
```
dotnet user-secrets init
dotnet user-secrets set GoogleGemini:Token <your-google-gemini-token>
```

## Update appsettings.json
Set the model you wish to use

## Run the demo
```
dotnet run
```

## Note

At the time of writing this demo, the following Gemini models was tested and the token usage for the demo data was measured.
The token usage may vary based on the input data and the model used.

#### Total tokens to extract data from the invoicesample.pdf
- gemini-3.1-flash-lite-preview = 1574 tokens
- gemini-3-flash-preview = 3611 tokens
- gemini-3.1-pro-preview = 5144 tokens
