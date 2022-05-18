using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Search.Web.Models;

namespace Search.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly SearchWebApiSettings _searchWebApiSettings;

        public CameraLocationsDivided? CameraLocationsDivided { get; private set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            IOptions<SearchWebApiSettings> searchWebApiSettings)
        {
            _logger = logger;
            _searchWebApiSettings = searchWebApiSettings.Value ?? throw new ArgumentNullException(nameof(searchWebApiSettings));
            CameraLocationsDivided = new CameraLocationsDivided();
        }

        public void OnGet()
        {
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
                    _logger.LogCritical(httpRequestException, $"Error calling the Web API (url: {_searchWebApiSettings.Url}).");
                    return;
                }
                catch (Exception exception)
                {
                    _logger.LogCritical(exception, $"Unexpected error getting the camera locations from the Web API (url: {_searchWebApiSettings.Url})");
                    return;
                }

                if (searchResult == null)
                {
                    _logger.LogCritical($"Getting the camera locations from the Web API (url: {_searchWebApiSettings.Url}) resulted in a null value.");
                    return;
                }

                // convert the searchResult into JSON
                CameraLocation[]? cameraLocations;

                try
                {
                    cameraLocations = JsonConvert.DeserializeObject<CameraLocation[]>(searchResult);
                }
                catch(Exception exception)
                {
                    _logger.LogCritical(exception, $"Error converting the returned value from the Web API into an array of {nameof(CameraLocation)} (url: {_searchWebApiSettings.Url}; return value: {searchResult})");
                    return;
                }

                if (cameraLocations == null)
                {
                    _logger.LogCritical($"Converting the returned value from the Web API into an array of {nameof(CameraLocation)} resulted in a null value (url: {_searchWebApiSettings.Url}; return value: {searchResult})");
                    return;
                }

                // create CameraLocationsDivided to divide the camera locations
                CameraLocationsDivided = new CameraLocationsDivided(cameraLocations);
            }
        }
    }
}