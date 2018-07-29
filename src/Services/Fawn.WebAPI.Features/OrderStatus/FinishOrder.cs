using System.Threading;
using System.Threading.Tasks;
using Fawn.DAL.Abstract;
using Fawn.Shared;
using JetBrains.Annotations;
using MediatR;

namespace Fawn.WebAPI.Features.OrderStatus
{
    public class FinishOrder
    {
        public class FinishOrderByIdCommand : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : AsyncRequestHandler<FinishOrderByIdCommand>
        {
            private readonly IOrderStatusRepository _statusRepository;

            public Handler(
                [NotNull] IOrderStatusRepository statusRepository)
            {
                Guard.NotNull(statusRepository, nameof(statusRepository));

                _statusRepository = statusRepository;
            }

            protected override async Task Handle(
                [NotNull] FinishOrderByIdCommand request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                await _statusRepository.FinishOrderByIdAsync(request.Id)
                    .ConfigureAwait(false);
            }
        }
    }
}