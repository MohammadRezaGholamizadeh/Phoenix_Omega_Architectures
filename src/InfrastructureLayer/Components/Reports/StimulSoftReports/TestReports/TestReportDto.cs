namespace InfrastructureLayer.Reports.StimulSoftReports.TestReports
{
    public partial class TestReportAppHandler
    {
        public class TestReportDto
        {
            public TestReportDto()
            {
                Counts = new List<CountDto>();
            }
            public List<CountDto> Counts { get; set; }
        }



    }
}
