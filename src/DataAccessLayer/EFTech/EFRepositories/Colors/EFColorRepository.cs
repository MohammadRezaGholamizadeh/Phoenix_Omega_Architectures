using DataAccessLayer.EFTech.EFDataContexts;
using DomainLayer.Entities.Color;
using ServiceLayer.Services.ColorService.Contracts;

namespace DataAccessLayer.EFTech.EFRepositories.Colors
{
    public class EFColorRepository : IColorRepository
    {
        private readonly EFDataContext _context;

        public EFColorRepository(EFDataContext dataContext)
        {
            _context = dataContext;
        }

        public void Add(Color color)
        {
            _context.Add(color);
        }
    }
}
