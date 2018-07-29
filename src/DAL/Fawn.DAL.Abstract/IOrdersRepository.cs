namespace Fawn.DAL.Abstract
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Fawn.DAL.Models;
    using JetBrains.Annotations;

    public interface IOrdersRepository
    {
        Task<bool> ExistsByIdAsync(
            int entityId,
           CancellationToken token = default(CancellationToken)
        );

        [CanBeNull]
        Task<OrderDTO> GetByIdAsync(
           int entityId,
           CancellationToken token = default(CancellationToken));

        [NotNull]
        Task<IEnumerable<OrderDTO>> GetAllAsync(
            CancellationToken token = default(CancellationToken));

        Task DeleteAsync(
            [NotNull] OrderDTO entity,
            CancellationToken token = default(CancellationToken));

        [NotNull]
        Task<OrderDTO> CreateAsync(
            [NotNull] OrderDTO entity,
            CancellationToken token = default(CancellationToken));
    }
}
