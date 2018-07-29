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
using Fawn.WebAPI.Models.Orders;

namespace Fawn.WebAPI.Features.Orders
{
    public class List
    {
        public sealed class ListOfOrdersQuery : IRequest<IEnumerable<OrderApiDTO>>
        {
            private ListOfOrdersQuery()
            {
            }

            public static readonly ListOfOrdersQuery Instance = new ListOfOrdersQuery();
        }

        public class Handler : IRequestHandler<ListOfOrdersQuery, IEnumerable<OrderApiDTO>>
        {
            private readonly IOrdersRepository _ordersRepository;
            private readonly IMapper _mapper;

            public Handler(
                [NotNull] IOrdersRepository ordersRepository,
                [NotNull] IMapper mapper)
            {
                Guard.NotNull(ordersRepository, nameof(ordersRepository));
                Guard.NotNull(mapper, nameof(mapper));

                _ordersRepository = ordersRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<OrderApiDTO>> Handle(
                [NotNull] ListOfOrdersQuery request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                var OrdersList = await _ordersRepository
                    .GetAllAsync(cancellationToken)
                    .ConfigureAwait(false);

                return OrdersList.Select(_mapper.Map<OrderApiDTO>);
            }
        }
    }
}