namespace DomainLayer.DocumentsStorage
{
    public class DocumentStorage
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public byte[] Data { get; set; }
        public DateTime CreationDate { get; set; }
        public DocumentStatus Status { get; set; }
    }

}
