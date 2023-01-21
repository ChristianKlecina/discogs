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
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IGenericRepository<Track> _trackRepository;

    public CartController(ICartRepository cartRepository, IMapper mapper, IUserRepository usersRepository, ICartItemRepository cartItemRepository, 
        IGenericRepository<Track> trackRepository)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _usersRepository = usersRepository;
        _cartItemRepository = cartItemRepository;
        _trackRepository = trackRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cart>> GetCartById(int id)
    {
        var cart = await _cartRepository.GetCartById(id);
        //return _mapper.Map<Cart, CartDto>(cart);
        return cart;
    }
    
    
    [HttpGet()]
    public async Task<ActionResult<IReadOnlyList<Cart>>> GetCarts()
    {
        var cart = await _cartRepository.GetCarts();
        //return _mapper.Map<Cart, CartDto>(cart);
        return cart;
    }

    [HttpPost]
    public async Task<ActionResult<Cart>> CreateCart(CartDto cartDto)
    {
        decimal subtotal = 0;
        

        foreach (var item in cartDto.CartItems)
        {
            var track = _trackRepository.GetByIdAsync(item.TrackId);
            subtotal += (track.Result.Price*item.Quantity);
        }
        
        
        try
        {
            var cart = new Cart
            {
                OrderDate = cartDto.OrderDate,
                FirstName = cartDto.FirstName,
                LastName = cartDto.LastName,
                City = cartDto.City,
                Address = cartDto.Address,
                Comment = cartDto.Comment,
                Payment = cartDto.Payment,
                Subtotal = subtotal,
                PaymentMethod = cartDto.PaymentMethod
            };

            _cartRepository.CreateCart(cart);
            //Console.WriteLine(cartDto.CartItems);
            foreach (var item in cartDto.CartItems)
            {
                
                
                CartItemDto cartItemDto = new CartItemDto();
                cartItemDto.CartId = cart.Id;
                cartItemDto.Quantity = item.Quantity;
                cartItemDto.TrackId = item.TrackId;
                _cartItemRepository.CreateCartItem(_mapper.Map<CartItemDto,CartItem>(cartItemDto));
                
            }

            
            
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