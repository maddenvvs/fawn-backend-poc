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

    public class OrdersRepository : IOrdersRepository, IOrderStatusRepository
    {
        private readonly FawnAppContext _context;

        public OrdersRepository([NotNull] FawnAppContext context)
        {
            Guard.NotNull(context, nameof(context));

            _context = context;
        }

        public async Task CancelOrderByIdAsync(
            int entityId,
            CancellationToken token = default(CancellationToken))
        {
            if (!await ExistsByIdAsync(entityId, token)) return;

            _context.Orders.Update(new OrderDTO
            {
                Id = entityId,
                Status = OrderStatus.Cancelled
            });

            await _context.SaveChangesAsync(token);
        }

        public async Task FinishOrderByIdAsync(
            int entityId,
            CancellationToken token = default(CancellationToken))
        {
            if (!await ExistsByIdAsync(entityId, token)) return;

            _context.Orders.Update(new OrderDTO
            {
                Id = entityId,
                Status = OrderStatus.Done
            });

            await _context.SaveChangesAsync(token);
        }

        [NotNull]
        public async Task<OrderDTO> CreateAsync(
            [NotNull] OrderDTO entity,
            CancellationToken token = default(CancellationToken))
        {
            Guard.NotNull(entity, nameof(entity));

            _context.Orders.Add(entity);

            await _context.SaveChangesAsync(token);

            return entity;
        }

        public async Task DeleteAsync(
            [NotNull] OrderDTO entity,
            CancellationToken token = default(CancellationToken))
        {
            _context.Orders.Remove(entity);

            await _context.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public async Task<bool> ExistsByIdAsync(
            int entityId,
            CancellationToken token = default(CancellationToken))
        {
            return await _context.Orders
                .AnyAsync(o => o.Id == entityId, token)
                .ConfigureAwait(false);
        }

        [NotNull]
        public async Task<IEnumerable<OrderDTO>> GetAllAsync(
            CancellationToken token = default(CancellationToken))
        {
            return await _context.Orders
                .Include(o => o.OrderGoods)
                    .ThenInclude(og => og.GoodsItem)
                .OrderBy(o => o.Id)
                .ToListAsync(token)
                .ConfigureAwait(false);
        }

        [CanBeNull]
        public async Task<OrderDTO> GetByIdAsync(
            int entityId,
            CancellationToken token = default(CancellationToken))
        {
            return await _context.Orders
                .Include(o => o.OrderGoods)
                    .ThenInclude(og => og.GoodsItem)
                .SingleOrDefaultAsync(o => o.Id == entityId, token)
                .ConfigureAwait(false);
        }

        public async Task<OrderStatus?> GetStatusByIdAsync(
            int entityId,
            CancellationToken token = default(CancellationToken))
        {
            if (!await ExistsByIdAsync(entityId, token)) return null;

            return await _context.Orders
                .Where(o => o.Id == entityId)
                .Select(o => o.Status)
                .SingleOrDefaultAsync(token);
        }
    }
}