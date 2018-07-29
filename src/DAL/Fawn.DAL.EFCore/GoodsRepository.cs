namespace Fawn.DAL.EFCore
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fawn.DAL.Abstract;
    using Fawn.DAL.EFCore.Contexts;
    using Fawn.DAL.Models;
    using Fawn.Shared;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;

    public class GoodsRepository : IGoodsRepository
    {
        private readonly FawnAppContext _context;

        public GoodsRepository(
            [NotNull] FawnAppContext context)
        {
            Guard.NotNull(context, nameof(context));

            _context = context;
        }

        [NotNull]
        public async Task<GoodsDTO> CreateAsync(
            [NotNull] GoodsDTO entity,
            CancellationToken token = default(CancellationToken))
        {
            Guard.NotNull(entity, nameof(entity));

            _context.Goods.Add(entity);

            await _context.SaveChangesAsync(token).ConfigureAwait(false);

            return entity;
        }

        public async Task DeleteAsync(
            [NotNull] GoodsDTO entity,
            CancellationToken token = default(CancellationToken))
        {
            Guard.NotNull(entity, nameof(entity));

            _context.Goods.Remove(entity);

            await _context.SaveChangesAsync(token).ConfigureAwait(false);
        }

        [NotNull]
        public async Task<IEnumerable<GoodsDTO>> GetAllAsync(
            CancellationToken token = default(CancellationToken))
        {
            return await _context.Goods
                .Include(g => g.Price)
                .OrderBy(g => g.Id)
                .ToListAsync(token)
                .ConfigureAwait(false);
        }

        [CanBeNull]
        public async Task<GoodsDTO> GetByIdAsync(
            int entityId,
            CancellationToken token = default(CancellationToken))
        {
            return await _context.Goods
                .Include(g => g.Price)
                .SingleOrDefaultAsync(g => g.Id == entityId, token)
                .ConfigureAwait(false);
        }

        public async Task<bool> ExistsByIdAsync(
            int entityId,
            CancellationToken token = default(CancellationToken))
        {
            return await _context.Goods.AnyAsync(g => g.Id == entityId, token);
        }

        public async Task<GoodsDTO> UpdateAsync(
            [NotNull] GoodsDTO entity,
            CancellationToken token = default(CancellationToken))
        {
            if (!await ExistsByIdAsync(entity.Id, token).ConfigureAwait(false))
            {
                return null;
            }

            _context.Goods.Update(entity);

            await _context.SaveChangesAsync(token).ConfigureAwait(false);

            return entity;
        }
    }
}