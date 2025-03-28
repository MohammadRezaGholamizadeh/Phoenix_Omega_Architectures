using ServiceLayer.Services.ColorService.Contracts.Dtos;
using ServiceLayer.Setups.ServicecInterfaces;

namespace ServiceLayer.Services.ColorService.Contracts
{
    public interface IColorService : IService
    {
        Task<int> Add(AddColorDto addColorDto);
    }
}
