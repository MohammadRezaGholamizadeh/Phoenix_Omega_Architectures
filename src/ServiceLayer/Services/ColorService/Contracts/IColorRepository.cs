using DomainLayer.Entities.Color;
using ServiceLayer.Setups.RepositoryInterfaces;

namespace ServiceLayer.Services.ColorService.Contracts
{
    public interface IColorRepository : IRepository
    {
        void Add(Color color);
    }
}
