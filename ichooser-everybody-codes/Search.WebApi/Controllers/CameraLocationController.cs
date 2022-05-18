using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Search.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CameraLocationController : ControllerBase
    {
        private readonly ILogger<CameraLocationController> _logger;

        public CameraLocationController(ILogger<CameraLocationController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Gets a list of <seealso cref="CameraLocation"/> by searching partially within <seealso cref="CameraLocation.Camera"/>. When <paramref name="cameraName"/> does not contain a value, all <seealso cref="CameraLocation"/> entries are returned.
        /// </summary>
        /// <param name="cameraName">The name of the camera (or <seealso cref="CameraLocation.Camera"/>)</param>
        /// <returns></returns>
        [HttpGet(Name = "Search")]
        public IEnumerable<CameraLocation> Get(string? cameraName)
        {
            // GetCurrentDirectory works when debugging, but not when calling the application via the 'dotnet run' command.
            var currentDirectory =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ??
                Directory.GetCurrentDirectory();

            var csvFilename = Path.Combine(currentDirectory, @"Data\cameras-defb.csv");
            IDataExtractor dataExtractor = new CSVDataExtractor(csvFilename);

            var cameraLocations = dataExtractor.GetData<CameraLocation>();

            // cameraName not supplied?
            if (string.IsNullOrWhiteSpace(cameraName))
            {
                _logger.LogInformation($"No value supplied for '{nameof(cameraName)}'; returning all {nameof(CameraLocation)}");
                return cameraLocations;
            }

            // partial search
            var cameraLocationsSearchResults =
                cameraLocations
                    .Where(e =>
                        e.Name != null &&
                        e.Name.Contains(cameraName))
                    .ToList();

            return cameraLocationsSearchResults;
        }
    }
}