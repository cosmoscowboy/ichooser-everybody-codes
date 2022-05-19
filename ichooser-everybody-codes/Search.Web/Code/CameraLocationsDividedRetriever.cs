using Library.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Search.Web.Models;

namespace Search.Web.Code
{
    public class CameraLocationsDividedRetriever
    {
        private readonly SearchWebApiSettings _searchWebApiSettings;

        public CameraLocationsDividedRetriever(
            IOptions<SearchWebApiSettings> searchWebApiSettingsOptions)
        {
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _searchWebApiSettings = searchWebApiSettingsOptions.Value ?? throw new ArgumentNullException(nameof(searchWebApiSettingsOptions));
        }

        public CameraLocationsDivided Get()
        {
            var cameraLocationsDivided = new CameraLocationsDivided();

            // get the camera locations by calling the WebApi
            using (var httpClient = new HttpClient())
            {
                string? searchResult;

                try
                {
                    var searchTask = httpClient.GetStringAsync(_searchWebApiSettings.Url);
                    searchTask.Wait();

                    searchResult = searchTask.Result;
                }
                catch (HttpRequestException httpRequestException)
                {
                    //_logger.LogCritical(httpRequestException, $"Error calling the Web API (url: {_searchWebApiSettings.Url}).");
                    throw;
                }
                catch (Exception exception)
                {
                    //_logger.LogCritical(exception, $"Unexpected error getting the camera locations from the Web API (url: {_searchWebApiSettings.Url})");
                    throw;
                }

                if (searchResult == null)
                {
                    //_logger.LogCritical($"Getting the camera locations from the Web API (url: {_searchWebApiSettings.Url}) resulted in a null value.");
                    throw new Exception("Getting the cameras from the WebApi resulted in a null value.");
                }

                // convert the searchResult into JSON
                CameraLocation[]? cameraLocations;

                try
                {
                    cameraLocations = JsonConvert.DeserializeObject<CameraLocation[]>(searchResult);
                }
                catch (Exception exception)
                {
                    //_logger.LogCritical(exception, $"Error converting the returned value from the Web API into an array of {nameof(CameraLocation)} (url: {_searchWebApiSettings.Url}; return value: {searchResult})");
                    throw;
                }

                if (cameraLocations == null)
                {
                    //_logger.LogCritical($"Converting the returned value from the Web API into an array of {nameof(CameraLocation)} resulted in a null value (url: {_searchWebApiSettings.Url}; return value: {searchResult})");
                    throw new Exception("Converting the result to the WebApi call, resulted in a null value.");
                }

                // create CameraLocationsDivided to divide the camera locations
                cameraLocationsDivided = new CameraLocationsDivided(cameraLocations);

                return cameraLocationsDivided;
            }
        }
    }
}
