using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fawn.DAL.Abstract;
using Fawn.Shared;
using Fawn.WebAPI.Models.Orders;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fawn.WebAPI.Features.Orders
{
    public class Get
    {
        public class GetOrderByIdQuery : IRequest<OrderApiDTO>
        {
            [FromRoute]
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<GetOrderByIdQuery, OrderApiDTO>
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

            public async Task<OrderApiDTO> Handle(
                [NotNull] GetOrderByIdQuery request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                var OrdersItem = await _ordersRepository.GetByIdAsync(
                    request.Id,
                    cancellationToken)
                    .ConfigureAwait(false);

                return _mapper.Map<OrderApiDTO>(OrdersItem);
            }
        }
    }
}