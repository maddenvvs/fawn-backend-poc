namespace Fawn.DAL.Abstract
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Fawn.DAL.Models;
    using JetBrains.Annotations;

    public interface IOrderStatusRepository
    {
        [CanBeNull]
        Task<OrderStatus?> GetStatusByIdAsync(
           int entityId,
           CancellationToken token = default(CancellationToken));

        Task CancelOrderByIdAsync(
            int entityId,
            CancellationToken token = default(CancellationToken));

        Task FinishOrderByIdAsync(
            int entityId,
            CancellationToken token = default(CancellationToken));
    }
}
