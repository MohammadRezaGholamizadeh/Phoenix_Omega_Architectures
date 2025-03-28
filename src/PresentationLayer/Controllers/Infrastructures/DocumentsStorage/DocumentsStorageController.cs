using InfrastructureLayer.Services.Images;
using Microsoft.AspNetCore.Mvc;
using MimeMapping;
using ServiceLayer.Services.DocumentsStorage.Contracts;
using ServiceLayer.Services.DocumentsStorage.Contracts.Dtos;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Controllers.Infrastructures.DocumentsStorage
{

    [ApiController, Route("api/v{version:apiVersion}/documents-storage")]

    public class DocumentsStorageController : ControllerBase
    {
        private readonly DocumentStorageService _service;
        private readonly ImagingService _imagingService;

        public DocumentsStorageController(
            DocumentStorageService service,
            ImagingService imagingService)
        {
            _service = service;
            _imagingService = imagingService;
        }

        [HttpPost]
        public async Task<Guid> Add([FromForm, Required] IFormFile file)
        {
            var documentDto = new CreateDocumentDto
            {
                Extension = Path.GetExtension(file.FileName),
                Data = await FormFileToByteArrayAsync(file)
            };
            return await _service.Add(documentDto);
        }

        [HttpGet("{id}")]
        public async Task<FileResult> Download([FromRoute, Required] Guid id,
                                                        [FromQuery] int? size)
        {
            var file = await _service.GetDocumentById(id);
            var data = file.Data;

            if (size.HasValue)
            {
                data = _imagingService.GetThumbnail(file.Data, size.Value);
            }
            return File(data, MimeUtility.GetMimeMapping(file.Extension));
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _service.Delete(id);
        }

        private async Task<byte[]> FormFileToByteArrayAsync(IFormFile file)
        {
            byte[] fileStream;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileStream = memoryStream.ToArray();
                await memoryStream.FlushAsync();
            }

            return fileStream;
        }
    }
}
