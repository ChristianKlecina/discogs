using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;



public class CartController : BaseApiController
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly IUserRepository _usersRepository;

    public CartController(ICartRepository cartRepository, IMapper mapper, IUserRepository usersRepository)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _usersRepository = usersRepository;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CartDto>> GetBasketById(int Id)
    {
        var cart = await _cartRepository.GetCartByUserId(Id);
        var mappedCart = _mapper.Map<Cart, CartDto>(cart);

        return Ok(mappedCart);
    }
    
    [HttpPost]
    public async Task<ActionResult<Cart>> CreateCart(CartDto cartDto)
    {
        try
        {
            var cart = new Cart
            {
                Id = cartDto.Id,
                OrderDate = cartDto.OrderDate,
                UserId = cartDto.UserId,
                User = _usersRepository.GetUserById(cartDto.UserId),
                Address = cartDto.Address,
                Comment = cartDto.Comment,
                Payment = cartDto.Payment,
                Subtotal = cartDto.Subtotal,
                PaymentMethod = cartDto.PaymentMethod
            
            };

            _cartRepository.CreateCart(cart);

            return Ok(cart);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCart(int id)
    {
        try
        {
            if (id != null)
            {
                _cartRepository.RemoveCart(id);
                _cartRepository.SaveChanges();
                return Ok();
            }

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}