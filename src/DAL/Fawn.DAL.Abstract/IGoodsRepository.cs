
namespace Fawn.DAL.Abstract
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Fawn.DAL.Models;
    using JetBrains.Annotations;

    public interface IGoodsRepository
    {
        Task<bool> ExistsByIdAsync(
            int entityId,
           CancellationToken token = default(CancellationToken)
        );

        [CanBeNull]
        Task<GoodsDTO> GetByIdAsync(
           int entityId,
           CancellationToken token = default(CancellationToken));

        [NotNull]
        Task<IEnumerable<GoodsDTO>> GetAllAsync(
            CancellationToken token = default(CancellationToken));

        Task DeleteAsync(
            [NotNull] GoodsDTO entity,
            CancellationToken token = default(CancellationToken));

        [NotNull]
        Task<GoodsDTO> CreateAsync(
            [NotNull] GoodsDTO entity,
            CancellationToken token = default(CancellationToken));

        [NotNull]
        Task<GoodsDTO> UpdateAsync(
            [NotNull] GoodsDTO entity,
            CancellationToken token = default(CancellationToken));
    }
}