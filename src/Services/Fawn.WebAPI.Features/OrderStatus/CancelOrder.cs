using System.Threading;
using System.Threading.Tasks;
using Fawn.DAL.Abstract;
using Fawn.Shared;
using JetBrains.Annotations;
using MediatR;

namespace Fawn.WebAPI.Features.OrderStatus
{
    public class CancelOrder
    {
        public class CancelOrderByIdCommand : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<CancelOrderByIdCommand>
        {
            private readonly IOrderStatusRepository _statusRepository;

            public Handler(
                [NotNull] IOrderStatusRepository statusRepository)
            {
                Guard.NotNull(statusRepository, nameof(statusRepository));

                _statusRepository = statusRepository;
            }

            protected override async Task Handle(
                [NotNull] CancelOrderByIdCommand request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                await _statusRepository.CancelOrderByIdAsync(request.Id)
                    .ConfigureAwait(false);
            }
        }
    }
}