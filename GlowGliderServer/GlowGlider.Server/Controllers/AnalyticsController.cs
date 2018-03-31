using System.Threading.Tasks;
using GlowGlider.Server.Data;
using GlowGlider.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GlowGlider.Server.Controllers
{
    [Route("/api/[controller]")]
    public class AnalyticsController : Controller
    {
        private readonly IAnalyticsRepository _repository;

        public AnalyticsController(IAnalyticsRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] AnalyticsData data)
        {
            if (TokenBuilder.TokenFor(data) != data.Token)
            {
                return new BadRequestResult();
            }

            await _repository.StoreDataAsync(data);

            return new OkResult();
        }
    }
}