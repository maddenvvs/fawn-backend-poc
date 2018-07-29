using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fawn.DAL.Abstract;
using Fawn.Shared;
using Fawn.WebAPI.Models.Goods;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fawn.WebAPI.Features.Goods
{
    public class Get
    {
        public class GetGoodsByIdQuery : IRequest<GoodsApiDTO>
        {
            [FromRoute]
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<GetGoodsByIdQuery, GoodsApiDTO>
        {
            private readonly IGoodsRepository _goodsRepository;
            private readonly IMapper _mapper;

            public Handler(
                [NotNull] IGoodsRepository goodsRepository,
                [NotNull] IMapper mapper)
            {
                Guard.NotNull(goodsRepository, nameof(goodsRepository));
                Guard.NotNull(mapper, nameof(mapper));

                _goodsRepository = goodsRepository;
                _mapper = mapper;
            }

            public async Task<GoodsApiDTO> Handle(
                [NotNull] GetGoodsByIdQuery request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                var goodsItem = await _goodsRepository.GetByIdAsync(
                    request.Id,
                    cancellationToken).ConfigureAwait(false);

                return _mapper.Map<GoodsApiDTO>(goodsItem);
            }
        }
    }
}