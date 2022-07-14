using Microsoft.AspNetCore.Mvc;
using TEGEvents.Core;
using TEGEvents.Infrastructure;

namespace TEGEventsApp
{
    [ApiController]
    [Route("[controller]")]
    public class TEGEventsVenuesController : ControllerBase
    {
        private readonly ILogger<TEGEventsVenuesController> _logger;

        public TEGEventsVenuesController(ILogger<TEGEventsVenuesController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public async Task<TEGEventVenue> Get()
        {
            TEGEventsApiHelper.InitializeClient();
            return await TEGEventsProcessor.LoadEvents();
        }
    }
}
