using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fawn.DAL.Abstract;
using Fawn.DAL.Models;
using Fawn.Shared;
using JetBrains.Annotations;
using MediatR;
using AutoMapper;
using Fawn.WebAPI.Models.Goods;

namespace Fawn.WebAPI.Features.Goods
{
    public class List
    {
        public sealed class ListOfGoodsQuery : IRequest<IEnumerable<GoodsApiDTO>>
        {
            private ListOfGoodsQuery()
            {
            }

            public static readonly ListOfGoodsQuery Instance = new ListOfGoodsQuery();
        }

        public class Handler : IRequestHandler<ListOfGoodsQuery, IEnumerable<GoodsApiDTO>>
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

            public async Task<IEnumerable<GoodsApiDTO>> Handle(
                [NotNull] ListOfGoodsQuery request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                var goodsList = await _goodsRepository
                    .GetAllAsync(cancellationToken)
                    .ConfigureAwait(false);

                return goodsList.Select(_mapper.Map<GoodsApiDTO>);
            }
        }
    }
}