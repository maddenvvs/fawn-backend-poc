using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fawn.DAL.Abstract;
using Fawn.DAL.Models;
using Fawn.Shared;
using Fawn.WebAPI.Models.Goods;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fawn.WebAPI.Features.Goods
{
    [Route("api/v201807/Goods")]
    public class GoodsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GoodsController(
            [NotNull] IMediator mediator)
        {
            Guard.NotNull(mediator, nameof(mediator));

            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all goods item.
        /// </summary>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoodsApiDTO>>> Get()
        {
            var goodsList = await _mediator.Send(List.ListOfGoodsQuery.Instance);

            return Ok(goodsList);
        }

        /// <summary>
        /// Creates new goods item.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<GoodsApiDTO>> Post(
            [NotNull] Post.CreateNewGoodsCommand command)
        {
            var createdGoods = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { Id = createdGoods.Id },
                createdGoods);
        }

        /// <summary>
        /// Retrieves a specific goods item.
        /// </summary>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<GoodsApiDTO>> GetById(
            [NotNull] Get.GetGoodsByIdQuery query)
        {
            var goods = await _mediator.Send(query);

            if (goods == null)
            {
                return NotFound();
            }

            return Ok(goods);
        }

        /// <summary>
        /// Updates a specific goods item.
        /// </summary>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Put(
            [NotNull] Put.UpdateGoodsItemByIdCommand command)
        {
            var goods = await _mediator.Send(command);

            if (goods == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific goods item.
        /// </summary>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(
            [NotNull] Delete.DeleteGoodsItemByIdCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
