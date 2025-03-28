namespace InfrastructureLayer.Components.StimulsoftReportComponents
{
    public class StimulSoftReportTools
    {
        public StimulSoftReportTools()
        {
            Fonts = new List<FontResource>();
            BusinessObjects = new Dictionary<string, object>();
        }

        public Stream? ResourceStream { get; set; }
        public List<FontResource> Fonts { get; set; }
        public Dictionary<string, object> BusinessObjects { get; set; }
        public string LicenseKey { get; set; }
    }
}
