namespace ServiceLayer.Services.DocumentsStorage.Contracts.Dtos
{
    public class GetDocumentDto
    {
        public byte[] Data { get; set; }
        public string Extension { get; set; }
    }
}
