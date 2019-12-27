using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myArtShopCore.Data;
using myArtShopCore.Data.Entities;
using myArtShopCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myArtShopCore.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController:Controller
    {
        private readonly IArtShopRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(IArtShopRepository repository,ILogger<OrderItemsController> logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(orderId);
            if (order != null) return Ok(_mapper.Map<IEnumerable<OrderItem>,IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId,int orderItemId)
        {
            var order = _repository.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == orderItemId).FirstOrDefault();
                if(item!=null)
                {
                    return Ok(_mapper.Map<OrderItem,OrderItemViewModel>(item));
                }
            }
            
            return NotFound();
        }
    }
}
