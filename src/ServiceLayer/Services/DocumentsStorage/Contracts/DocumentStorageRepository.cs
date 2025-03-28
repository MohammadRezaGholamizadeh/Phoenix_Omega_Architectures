using DomainLayer.DocumentsStorage;
using ServiceLayer.Services.DocumentsStorage.Contracts.Dtos;
using ServiceLayer.Setups.RepositoryInterfaces;

namespace ServiceLayer.Services.DocumentsStorage.Contracts
{
    public interface DocumentStorageRepository : IRepository
    {
        void AddDocument(DocumentStorage document);
        Task<DocumentStorage> FindById(Guid id);
        Task<GetDocumentDto?> GetDocumentById(Guid id);
        void DeleteDocument(DocumentStorage document);
    }
}
