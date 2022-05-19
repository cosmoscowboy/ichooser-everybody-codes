using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Search.Web.Code;
using Search.Web.Models;

namespace Search.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CameraLocationsDividedRetriever _cameraLocationsDividedRetriever;

        public CameraLocationsDivided? CameraLocationsDivided { get; private set; }
        public bool ErrorGettingModel;

        public IndexModel(
            ILogger<IndexModel> logger,
            CameraLocationsDividedRetriever cameraLocationsDividedRetriever)
        {
            _logger = logger;
            _cameraLocationsDividedRetriever = cameraLocationsDividedRetriever ?? throw new ArgumentNullException(nameof(cameraLocationsDividedRetriever));

            // set default value
            CameraLocationsDivided = new CameraLocationsDivided();
        }

        public void OnGet()
        {
            try
            {
                CameraLocationsDivided = _cameraLocationsDividedRetriever.Get();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error getting {nameof(CameraLocationsDivided)} using {nameof(CameraLocationsDividedRetriever)}.");
                ErrorGettingModel = true;
            }
        }
    }
}