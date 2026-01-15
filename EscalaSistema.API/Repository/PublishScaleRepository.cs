using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalaSistema.API.Repository
{
    public class PublishScaleRepository : IPublishScaleRepository
    {
        private readonly EscalaSistemaDbContext _context;

        public PublishScaleRepository(EscalaSistemaDbContext context)
        {
            _context = context;
        }

        public async Task<Scale?> GetByIdAsync(Guid scaleId)
        {
            return await _context.Scales
                .Include(s => s.ScaleAssignments)
                .FirstOrDefaultAsync(s => s.Id == scaleId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
