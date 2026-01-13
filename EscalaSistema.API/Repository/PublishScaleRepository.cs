using EscalaSistema.API.Data;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Middleware;
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

        public async Task PublishScaleAsync(Guid scaleId)
        {
            var scale = await _context.Scales.Include(s => s.ScaleAssignments).FirstOrDefaultAsync(s => s.Id == scaleId);

            if (scale is null)
            {
                throw new NotFoundException("Não encontrada");
            }
            
            if (scale.IsPublished)
            {
                throw new BadRequestException("Escala já publicada");
            }

            if (scale.IsClosed == true)
            {
                throw new BadRequestException("Escala encerrada não pode ser publicada");
            }

            if(scale.ScaleAssignments.Count == 0)
            {
                throw new BadRequestException("Escala sem atribuições não pode ser publicada");
            }

            scale.IsPublished = true;
            scale.PublishedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
