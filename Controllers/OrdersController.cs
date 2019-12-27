using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController:Controller
    {
        private readonly IArtShopRepository _repository;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IArtShopRepository repository, ILogger<OrdersController> logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel> >(_repository.GetAllOrders()));
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
                    //using IMapper to map Order OrderViewModel,because we always want to return a view model
                    return Ok(_mapper.Map<Order,OrderViewModel>(order));
                }
                else return NotFound();
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get orders:{ex}");
                return BadRequest("Bad request");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            //add it to the db
            try
            {
                if (ModelState.IsValid)
                {
                    //Dont NEED to do a conversion mannualy because we use IMapper instead
                    //var newOrder = new Order()
                    //{
                    //    OrderDate = model.OrderDate,
                    //    OrderNumber = model.OrderNumber,
                    //    Id = model.OrderID
                    //};
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);


                    //if they didn't specify the date
                    if (newOrder.OrderDate==DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    //adding to repository
                    _repository.AddEntity(newOrder);

                    //mapping  back from OrderModel to OrderViewModel
                    //Dont NEED to do a conversion mannualy because we use IMapper instead

                    //var vm = new OrderViewModel()
                    //{
                    //    OrderID = newOrder.Id,
                    //    OrderDate = newOrder.OrderDate,
                    //    OrderNumber = newOrder.OrderNumber
                    //};

                    var vm = _mapper.Map<Order, OrderViewModel>(newOrder);

                    if (_repository.SaveAll())
                    {
                        return Created($"/api/orders/{vm.OrderID}", vm); //"Created" matching 201 code
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to save a new order:{ex}");
            }

            return BadRequest("Failed to save new order");
            
        }

    }
}
