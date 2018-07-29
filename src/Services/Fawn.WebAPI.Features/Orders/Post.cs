using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fawn.DAL.Abstract;
using Fawn.DAL.Models;
using Fawn.Shared;
using Fawn.WebAPI.Models.Orders;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fawn.WebAPI.Features.Orders
{
    public class Post
    {
        public class CreateNewOrderCommand : IRequest<OrderApiDTO>
        {
            [FromBody]
            public NewOrderApiDTO NewOrder { get; set; }
        }

        public class Validator : AbstractValidator<CreateNewOrderCommand>
        {
            public Validator()
            {
                RuleFor(c => c.NewOrder)
                    .NotNull()
                    .WithMessage("You must provide new order information");
            }
        }

        public class Handler : IRequestHandler<CreateNewOrderCommand, OrderApiDTO>
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
                [NotNull] CreateNewOrderCommand request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                var newOrder = _mapper.Map<OrderDTO>(request.NewOrder);

                await _ordersRepository.CreateAsync(newOrder, cancellationToken);

                var newOrderDto = await _ordersRepository.GetByIdAsync(
                    newOrder.Id,
                    cancellationToken);

                return _mapper.Map<OrderApiDTO>(newOrderDto);
            }
        }
    }
}