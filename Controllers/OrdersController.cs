using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myArtShopCore.Data;
using myArtShopCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myArtShopCore.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController:Controller
    {
        private readonly IArtShopRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(IArtShopRepository repository, ILogger<OrdersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get orders:{ex}");
                return BadRequest("Bad request");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if (order != null)
                {
                    return Ok(order);
                }
                else return NotFound();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders:{ex}");
                return BadRequest("Bad request");
            }
        }

    }
}
