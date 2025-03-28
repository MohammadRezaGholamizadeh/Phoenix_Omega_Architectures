using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Services.DocumentsStorage.Contracts.Dtos
{
    public class CreateDocumentDto
    {
        [Required]
        public byte[] Data { get; set; }
        [Required]
        public string Extension { get; set; }
    }
}
