using InfrastructureLayer.Components.Reports.StimulSoftReports.TestReports.Contracts;
using Microsoft.AspNetCore.Mvc;
using MimeMapping;

namespace PresentationLayer.Controllers.Infrastructures.StimulSoftReports
{
    [Route("api/v{version:apiVersion}/stimulsoft-reports")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StimulSoftReportTestControllers : ControllerBase
    {
        private readonly TestReportHandler _testReportHandler;

        public StimulSoftReportTestControllers(TestReportHandler testReportHandler)
        {
            _testReportHandler = testReportHandler;
        }

        [HttpGet]
        public async Task<FileResult> GetAllPdf()
        {
            var pdf = await _testReportHandler.GetPdf();

            return File(pdf, MimeUtility.GetMimeMapping("pdf"));
        }
    }
}

