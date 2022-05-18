// based on https://github.com/dotnet/command-line-api/blob/main/docs/Your-first-app-with-System-CommandLine.md
using Library.Data;
using Library.Models;
using System.CommandLine;

const string CommandLineExample = @"dotnet Search --name partialSearchWithinCameraName";
const string CommandLineHelp = @"Run the program by entering the following values: " + CommandLineExample;

// Create some options:
var nameOption = new Option<string>(
        "--name",
        description: $"Performs a partial search within the field '{nameof(CameraLocation.Name)}' of a {nameof(CameraLocation)}.");

// Add the options to a root command:
var rootCommand = new RootCommand
{
    nameOption
};

// set the handler, which executes the code based on the arguments supplied
rootCommand.SetHandler((string cameraName) =>
{
    if (string.IsNullOrWhiteSpace(cameraName))
    {
        Console.WriteLine(CommandLineHelp);
        return;
    }

    // GetCurrentDirectory works when debugging, but not when calling the application via the 'dotnet run' command.
    var currentDirectory =
        Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ??
        Directory.GetCurrentDirectory();

    var csvFilename = Path.Combine(currentDirectory, @"Data\cameras-defb.csv");
    IDataExtractor dataExtractor = new CSVDataExtractor(csvFilename);

    var cameraLocations = dataExtractor.GetData<CameraLocation>();

    // partial search
    var cameraLocationsSearchResults = 
        cameraLocations
            .Where(e => 
                e.Name != null &&
                e.Name.Contains(cameraName))
            .ToList();

    cameraLocationsSearchResults.ForEach(e =>
    {
        Console.WriteLine(e);
    });

}, nameOption);

// Parse the incoming args and invoke the handler
return rootCommand.Invoke(args);
