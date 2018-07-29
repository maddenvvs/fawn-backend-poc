using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fawn.DAL.Abstract;
using Fawn.DAL.Models;
using Fawn.Shared;
using Fawn.WebAPI.Models.Orders;
using Fawn.WebAPI.Models.OrderStatus;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fawn.WebAPI.Features.OrderStatus
{
    public class OrderStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderStatusController(
            [NotNull] IMediator mediator)
        {
            Guard.NotNull(mediator, nameof(mediator));

            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all orders with ordered goods.
        /// </summary>

        [HttpGet("api/v201807/Orders/{Id}/Status")]
        public async Task<ActionResult<OrderStatusApiDTO>> GetStatus(
            Get.GetOrderStatusByIdQuery query)
        {
            var orderStatus = await _mediator.Send(query);

            if (orderStatus == null)
            {
                return NotFound();
            }

            return Ok(orderStatus);
        }

        [HttpPut("api/v201807/Orders/{Id}/Cancel")]
        public async Task<ActionResult> CancelOrder(
            CancelOrder.CancelOrderByIdCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("api/v201807/Orders/{Id}/Done")]
        public async Task<ActionResult> FinishOrder(
            FinishOrder.FinishOrderByIdCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
