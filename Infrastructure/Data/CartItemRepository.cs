using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CartItemRepository : ICartItemRepository
{
    private readonly StoreContext _context;

    public CartItemRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<CartItem>> GetCartItems()
    {
        return await _context.CartItem.ToListAsync();
    }

    public async Task<IReadOnlyList<CartItem>> GetCartItemsByCartId(int cartId)
    {
        return await _context.CartItem.Where(x => x.CartId == cartId).ToListAsync();
    }

    public async Task<CartItem> CreateCartItem(CartItem cartItem)
    {
        _context.CartItem.Add(cartItem);
        _context.SaveChanges();
        return cartItem;
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }
}