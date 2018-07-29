using System.Threading;
using System.Threading.Tasks;
using Fawn.DAL.Abstract;
using Fawn.Shared;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fawn.WebAPI.Features.Orders
{
    public class Delete
    {
        public class DeleteOrderByIdCommand : IRequest
        {
            [FromRoute]
            public int Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<DeleteOrderByIdCommand>
        {
            private readonly IOrdersRepository _ordersRepository;

            public Handler(
                [NotNull] IOrdersRepository ordersRepository)
            {
                Guard.NotNull(ordersRepository, nameof(ordersRepository));

                _ordersRepository = ordersRepository;
            }

            protected override async Task Handle(
                [NotNull] DeleteOrderByIdCommand request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                var order = await _ordersRepository
                    .GetByIdAsync(request.Id, cancellationToken)
                    .ConfigureAwait(false);

                if (order != null)
                {
                    await _ordersRepository
                        .DeleteAsync(order, cancellationToken)
                        .ConfigureAwait(false);
                }
            }
        }
    }
}