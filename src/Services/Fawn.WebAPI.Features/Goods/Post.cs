namespace Fawn.WebAPI.Features.Goods
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Fawn.DAL.Abstract;
    using Fawn.DAL.Models;
    using Fawn.Shared;
    using Fawn.WebAPI.Models.Goods;
    using FluentValidation;
    using JetBrains.Annotations;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public class Post
    {
        public class CreateNewGoodsCommand : IRequest<GoodsApiDTO>
        {
            [FromBody]
            [Required]
            public GoodsModel Goods { get; set; }
        }

        public class CreateNewGoodsCommandValidator
            : AbstractValidator<CreateNewGoodsCommand>
        {
            public CreateNewGoodsCommandValidator()
            {
                RuleFor(c => c.Goods)
                    .NotNull()
                    .WithMessage("New goods object cannot be empty.");
            }
        }

        public class Handler : IRequestHandler<CreateNewGoodsCommand, GoodsApiDTO>
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
                [NotNull] CreateNewGoodsCommand request,
                CancellationToken cancellationToken)
            {
                Guard.NotNull(request, nameof(request));

                var createdGoods = await _goodsRepository
                    .CreateAsync(_mapper.Map<GoodsDTO>(request.Goods))
                    .ConfigureAwait(false);

                return _mapper.Map<GoodsApiDTO>(createdGoods);
            }
        }
    }
}