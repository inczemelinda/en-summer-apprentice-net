using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMS.Exceptions;
using TMS.Models;
using TMS.Models.Dto;
using TMS.Repositories;

namespace TMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ITicketCategoryRepository ticketCategoryRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _ticketCategoryRepository = ticketCategoryRepository;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            var orderDto = orders.Select(o => new OrderDto()
            {
                OrderId = o.OrderId,
                NumberOfTickets = o.NumberOfTickets ?? 0,
                TicketCategory = o.TicketCategory?.Description ?? string.Empty,
                TotalPrice = (double)(o.TotalPrice ?? 0),
            });
            return Ok(orderDto);
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var @order = await _orderRepository.GetById(id);

            var orderDto = _mapper.Map<OrderDto>(@order);

            return Ok(orderDto);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatchDto)
        {
            var orderEntity = await _orderRepository.GetById(orderPatchDto.OrderId);


            if (orderEntity == null)
            {
                throw new EntityNotFoundException(orderPatchDto.OrderId, nameof(Order));
            }

            if (orderPatchDto.NumberOfTickets != 0) orderEntity.NumberOfTickets = orderPatchDto.NumberOfTickets;

            if (orderPatchDto.TicketCategoryId != 0) orderEntity.TicketCategoryId = orderPatchDto.TicketCategoryId;

            var ticketCategory = await _ticketCategoryRepository.GetById((int)orderEntity.TicketCategoryId);

            orderEntity.TotalPrice = orderPatchDto.NumberOfTickets * ticketCategory.Price;

            _orderRepository.Update(orderEntity);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);

            if (orderEntity == null)
            {
                return NotFound();
            }

            _orderRepository.Delete(orderEntity);
            return NoContent();
        }

    }
}