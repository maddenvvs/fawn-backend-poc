using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fawn.DAL.Abstract;
using Fawn.DAL.Models;
using Fawn.Shared;
using Fawn.WebAPI.Models.Orders;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fawn.WebAPI.Features.Orders
{
    [Route("api/v201807/Orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(
            [NotNull] IMediator mediator)
        {
            Guard.NotNull(mediator, nameof(mediator));

            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all orders with ordered goods.
        /// </summary>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderApiDTO>>> Get()
        {
            var ordersList = await _mediator.Send(List.ListOfOrdersQuery.Instance);

            return Ok(ordersList);
        }

        /// <summary>
        /// Creates new order.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OrderApiDTO>> Post(
            [NotNull] Post.CreateNewOrderCommand command)
        {
            var createdOrder = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { Id = createdOrder.Id },
                createdOrder);
        }

        /// <summary>
        /// Retrieves a specific order.
        /// </summary>
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<OrderApiDTO>> GetById(
            [NotNull] Get.GetOrderByIdQuery query)
        {
            var order = await _mediator.Send(query);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // /// <summary>
        // /// Updates a specific goods item.
        // /// </summary>
        // [HttpPut("{Id:int}")]
        // public async Task<IActionResult> Put(
        //     [NotNull] Put.UpdateGoodsItemByIdCommand command)
        // {
        //     var goods = await _mediator.Send(command);

        //     if (goods == null)
        //     {
        //         return NotFound();
        //     }

        //     return NoContent();
        // }

        /// <summary>
        /// Deletes a specific goods item.
        /// </summary>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(
            [NotNull] Delete.DeleteOrderByIdCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
