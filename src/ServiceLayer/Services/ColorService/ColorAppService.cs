using DomainLayer.Entities.Color;
using ServiceLayer.Services.ColorService.Contracts;
using ServiceLayer.Services.ColorService.Contracts.Dtos;
using ServiceLayer.Setups.RepositoryInterfaces;

namespace ServiceLayer.Services.ColorService
{
    public class ColorAppService : Contracts.IColorService
    {   
        private readonly IColorRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public ColorAppService(
            IColorRepository repository,
            UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(AddColorDto addColorDto)
        {
            var color = new Color()
            {
                Title = addColorDto.Title,
                ColorHex = addColorDto.ColorHex
            };

            _repository.Add(color);
            await _unitOfWork.SaveAllChangesAsync();

            return color.Id;
        }
    }
}
