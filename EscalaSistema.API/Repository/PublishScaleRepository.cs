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
            var scale = await _context.Scales.FirstOrDefaultAsync(s => s.Id == scaleId);

            if (scale is null)
            {
                throw new NotFoundException("Não encontrada");
            }
            
            if (scale.IsClosed)
            {
                throw new BadRequestException("Escala encerrada não pode ser publicada");
            }

            if (scale.IsPublished)
            {
                throw new BadRequestException("Escala já publicada");
            }

            var hasAssignments = await _context.ScaleAssignments.AnyAsync(sa => sa.ScaleId == scaleId);

            if (hasAssignments is false)
                throw new BadRequestException("Escala sem músicos não pode ser publicada");

            scale.Publish();

            await _context.SaveChangesAsync();
        }
    }
}
