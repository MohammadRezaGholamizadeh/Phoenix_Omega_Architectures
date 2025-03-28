using ServiceLayer.Setups.ServicecInterfaces;

namespace InfrastructureLayer.Components.Reports.StimulSoftReports.TestReports.Contracts
{
    public interface TestReportHandler : IService
    {
        Task<Stream> GetPdf();
    }
}
