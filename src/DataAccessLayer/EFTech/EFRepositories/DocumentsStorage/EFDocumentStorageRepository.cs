using DataAccessLayer.EFTech.EFDataContexts;
using DomainLayer.DocumentsStorage;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Services.DocumentsStorage.Contracts;
using ServiceLayer.Services.DocumentsStorage.Contracts.Dtos;

namespace DataAccessLayer.EFTech.EFRepositories.DocumentsStorage
{
    public class EFDocumentStorageRepository : DocumentStorageRepository
    {
        private readonly DbSet<DocumentStorage> _documents;

        public EFDocumentStorageRepository(EFDataContext context)
        {
            _documents = context.Set<DocumentStorage>();
        }

        public void AddDocument(DocumentStorage document)
        {
            _documents.Add(document);
        }

        public void DeleteDocument(DocumentStorage document)
        {
            _documents.Remove(document);
        }

        public async Task<DocumentStorage> FindById(Guid id)
        {
            return await _documents.FindAsync(id);
        }

        public async Task<GetDocumentDto?> GetDocumentById(Guid id)
        {
            return await _documents.Where(_ => _.Id == id)
                .Select(document => new GetDocumentDto
                {
                    Extension = document.Extension,
                    Data = document.Data
                })
                .SingleOrDefaultAsync();
        }
    }
}
