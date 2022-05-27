using API.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrderController : BaseApiController
{
    private readonly IGenericRepository<Order> _orderRepo;
    private readonly IBasketRepository _basketRepo;
    private readonly IUserRepository _userRepo;

    public OrderController(IGenericRepository<Order> orderRepo, IBasketRepository basketRepo, IUserRepository userRepo)
    {
        _orderRepo = orderRepo;
        _basketRepo = basketRepo;
        _userRepo = userRepo;
    }

    //[Authorize(Roles = "User")]
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderCreationDto orderDto)
    {
        try
        {
            if (orderDto != null)
            {
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    Comment = orderDto.Comment,
                    PaymentMethod = orderDto.PaymentMethod,
                    Payment = orderDto.Payment,
                    CustomerBasket = _basketRepo.GetBasketAsync(orderDto.CustomerBasketId).Result,
                    Subtotal = SubtotalCount(_basketRepo.GetBasketAsync(orderDto.CustomerBasketId).Result),
                    User = _userRepo.GetUserById(orderDto.UserId)
                    
                };

                _orderRepo.CreateAsync(order);
                return Ok(order);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return NoContent();
    }

    private decimal SubtotalCount(CustomerBasket basket)
    {
        decimal subtotal = 0;
        var items = basket.Items;

        foreach (var item in items)
        {
            return subtotal = subtotal + item.Price * item.Quantity;
        }

        return subtotal;
    }
    
    
}