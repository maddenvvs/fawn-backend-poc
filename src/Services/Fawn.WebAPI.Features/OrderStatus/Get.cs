using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fawn.DAL.Abstract;
using Fawn.Shared;
using Fawn.WebAPI.Models.OrderStatus;
using JetBrains.Annotations;
using MediatR;

namespace Fawn.WebAPI.Features.OrderStatus
{
    public class Get
    {
        public class GetOrderStatusByIdQuery : IRequest<OrderStatusApiDTO>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<GetOrderStatusByIdQuery, OrderStatusApiDTO>
        {
            private readonly IOrderStatusRepository _orderStatusRepository;
            private readonly IMapper _mapper;

            public Handler(
                [NotNull] IOrderStatusRepository orderStatusRepository,
                [NotNull] IMapper mapper)
            {
                Guard.NotNull(orderStatusRepository, nameof(orderStatusRepository));
                Guard.NotNull(mapper, nameof(mapper));

                _orderStatusRepository = orderStatusRepository;
                _mapper = mapper;
            }

            public async Task<OrderStatusApiDTO> Handle(
                [NotNull] GetOrderStatusByIdQuery request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                var status = await _orderStatusRepository
                    .GetStatusByIdAsync(request.Id, cancellationToken)
                    .ConfigureAwait(false);

                return _mapper.Map<OrderStatusApiDTO>(status);
            }
        }
    }
}