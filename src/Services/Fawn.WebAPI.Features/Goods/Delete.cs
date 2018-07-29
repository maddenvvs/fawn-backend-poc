using System.Threading;
using System.Threading.Tasks;
using Fawn.DAL.Abstract;
using Fawn.Shared;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fawn.WebAPI.Features.Goods
{
    public class Delete
    {
        public class DeleteGoodsItemByIdCommand : IRequest
        {
            [FromRoute]
            public int Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<DeleteGoodsItemByIdCommand>
        {
            private readonly IGoodsRepository _goodsRepository;

            public Handler(
                [NotNull] IGoodsRepository goodsRepository)
            {
                Guard.NotNull(goodsRepository, nameof(goodsRepository));

                _goodsRepository = goodsRepository;
            }

            protected override async Task Handle(
                [NotNull] DeleteGoodsItemByIdCommand request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                var goodsItem = await _goodsRepository
                    .GetByIdAsync(request.Id, cancellationToken)
                    .ConfigureAwait(false);

                if (goodsItem != null)
                {
                    await _goodsRepository
                        .DeleteAsync(goodsItem, cancellationToken)
                        .ConfigureAwait(false);
                }
            }
        }
    }
}