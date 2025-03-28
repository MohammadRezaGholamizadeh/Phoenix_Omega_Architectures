using InfrastructureLayer.Components.Reports.StimulSoftReports.TestReports.Contracts;
using InfrastructureLayer.Components.StimulsoftReportComponents;

namespace InfrastructureLayer.Reports.StimulSoftReports.TestReports
{
    public partial class TestReportAppHandler : TestReportHandler
    {
        public async Task<Stream> GetPdf()
        {
            var values = new TestReportDto()
            {
                Counts =
                   new List<CountDto>()
                   {
                        new CountDto()
                        {
                            Count = 10
                        },
                        new CountDto()
                        {
                            Count = 20
                        },
                        new CountDto()
                        {
                            Count = 30
                        },new CountDto()
                        {
                            Count = 40
                        },
                        new CountDto()
                        {
                            Count = 50
                        },
                        new CountDto()
                        {
                            Count = 60
                        },new CountDto()
                        {
                            Count = 70
                        },
                        new CountDto()
                        {
                            Count = 80
                        },
                        new CountDto()
                        {
                            Count = 90
                        }
                   }
            };

            var resourcePath =
                $"{typeof(TestReportAppHandler).Namespace}.MrtFiles.TestReport.mrt";

            var result =
                await StimulsoftReportComponent
                      .CreateInstance()
                      .SetLicense()
                      .SetResourceStream(resourcePath)
                      .AddBusinessObject("Counts", values.Counts)
                      .RunExportPdfByBusinessObjectsData();

            return result;

        }

        public class CountDto
        {
            public int Count { get; set; }
        }



    }
}
