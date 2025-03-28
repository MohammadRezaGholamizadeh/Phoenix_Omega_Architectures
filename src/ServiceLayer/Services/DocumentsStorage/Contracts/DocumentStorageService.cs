using ServiceLayer.Services.DocumentsStorage.Contracts.Dtos;
using ServiceLayer.Setups.ServicecInterfaces;

namespace ServiceLayer.Services.DocumentsStorage.Contracts
{
    public interface DocumentStorageService : IService
    {
        Task<GetDocumentDto> GetDocumentById(Guid id);
        Task<Guid> Add(CreateDocumentDto dto);
        Task Delete(Guid id);
    }
}
