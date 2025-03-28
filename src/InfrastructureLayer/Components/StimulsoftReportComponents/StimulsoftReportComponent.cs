using Microsoft.Extensions.Configuration;
using Stimulsoft.Base;
using Stimulsoft.Report;

namespace InfrastructureLayer.Components.StimulsoftReportComponents
{
    public static class StimulsoftReportComponent
    {
        public static StimulSoftReportTools CreateInstance()
        {
            return new StimulSoftReportTools();
        }

        public static StimulSoftReportTools AddBusinessObject(
            this StimulSoftReportTools tools,
            string name,
            object value)
        {
            tools.BusinessObjects.Add(name, value);
            return tools;
        }

        public static async Task<MemoryStream> RunExportPdfByBusinessObjectsData(
            this StimulSoftReportTools tools,
            StiExportFormat exportFormat = StiExportFormat.Pdf)
        {
            tools.Fonts
                 .ForEach(_ => StiFontCollection
                               .AddResourceFont(
                                   _.Name,
                                   _.ToByte,
                                   _.Extension,
                                   _.Alise));

            using var report = new StiReport();
            report.Load(tools.ResourceStream);
            var configuration = GetStimulSoftConfiguration();
            StiLicense.Key = configuration.LicenseKey;
            foreach (var _object in tools.BusinessObjects)
            {
                report.RegBusinessObject(_object.Key, _object.Value);
            }
            await report.RenderAsync();
            var memoryStream = new MemoryStream();
            report.ExportDocument(exportFormat, memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }

        public static StimulSoftReportTools SetResourceStream(
            this StimulSoftReportTools tools,
            string path)
        {
            var reportTemplateStream = typeof(StimulsoftReportComponent).Assembly.GetManifestResourceStream(path);
            tools.ResourceStream = reportTemplateStream;
            return tools;
        }

        public static StimulSoftReportTools SetFont(
            this StimulSoftReportTools tools,
            string name,
            string extension,
            string fontPath)
        {
            tools.Fonts
                 .Add(new FontResource
                 {
                     Name = name,
                     Extension = extension,
                     Alise = name,
                     ToByte = GetFontAsBinary(fontPath)
                 });

            return tools;
        }

        private static StimulSoftReportConfiguration GetStimulSoftConfiguration()
        {
            var configurationRoot =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory() + "/ConfigurationsJson")
                    .AddJsonFile("StimulsoftConfiguration.json")
                    .AddEnvironmentVariables()
                    .Build();

            var configuration = new StimulSoftReportConfiguration();

            configurationRoot
                .Bind(nameof(StimulSoftReportConfiguration),
                      configuration);

            return configuration;
        }

        private static byte[] GetFontAsBinary(string fontPath)
        {
            var fontStream =
                typeof(StimulSoftReportTools)
                      .Assembly
                      .GetManifestResourceStream(fontPath);

            var memoryStream = new MemoryStream();

            fontStream?.CopyTo(memoryStream);

            var fontByte = memoryStream.ToArray();

            return fontByte;
        }
    }
}
