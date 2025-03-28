using DomainLayer.DocumentsStorage;
using ServiceLayer.Services.DocumentsStorage.Contracts;
using ServiceLayer.Services.DocumentsStorage.Contracts.Dtos;
using ServiceLayer.Services.DocumentsStorage.Exceptions;
using ServiceLayer.Setups.DateTimeServices;
using ServiceLayer.Setups.RepositoryInterfaces;

namespace ServiceLayer.Services.DocumentsStorage
{
    public class DocumentStorageAppService : DocumentStorageService
    {
        private readonly DocumentStorageRepository _repository;
        private readonly DateTimeService _dateTime;
        private readonly UnitOfWork _unitOfWork;
        public DocumentStorageAppService(UnitOfWork unitOfWork,
            DocumentStorageRepository repository, DateTimeService dateTime)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _dateTime = dateTime;
        }

        public async Task<GetDocumentDto> GetDocumentById(Guid id)
        {
            var document = await _repository.GetDocumentById(id);
            if (document == null)
                throw new DocumentNotFoundException();

            return document;
        }

        public async Task<Guid> Add(CreateDocumentDto dto)
        {
            var id = Guid.NewGuid();

            var document = new DocumentStorage
            {
                Id = id,
                CreationDate = _dateTime.Now,
                Data = dto.Data,
                FileName = id.ToString("N"),
                Status = DocumentStatus.Reserve,
                Extension = dto.Extension.TrimStart('.')
            };

            _repository.AddDocument(document);

            await _unitOfWork.SaveAllChangesAsync();

            return document.Id;
        }

        public async Task Delete(Guid id)
        {
            var document = await _repository.FindById(id);
            if (document == null)
                throw new DocumentNotFoundException();

            _repository.DeleteDocument(document);

            await _unitOfWork.SaveAllChangesAsync();
        }
    }
}
