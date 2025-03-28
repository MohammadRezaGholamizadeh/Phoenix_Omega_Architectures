using Hangfire;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.ColorService.Contracts;
using ServiceLayer.Services.ColorService.Contracts.Dtos;

namespace PresentationLayer.Controllers.Colors
{
    [Route("api/v{version:apiVersion}/colors")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorAppService;
        private readonly ILogger<ColorsController> _logger;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public ColorsController(
            IColorService colorAppService,
            ILogger<ColorsController> logger,
            IBackgroundJobClient backgroundJobClient)
        {
            _colorAppService = colorAppService;
            _logger = logger;
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost]
        public async Task<int> Add(AddColorDto addColorDto)
        {
            return await _colorAppService.Add(addColorDto);
        }

        [HttpPost("HangFireTest")]
        public async Task<int> TestHangfireJob(AddColorDto addColorDto)
        {
            var jobId =
                _backgroundJobClient
                .Schedule(() => Console.WriteLine("Test Hangfire Delayed !!! "),
                                TimeSpan.FromSeconds(5));
            return await _colorAppService.Add(addColorDto);
        }
    }
}
