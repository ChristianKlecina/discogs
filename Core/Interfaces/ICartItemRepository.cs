using Core.Entities;

namespace Core.Interfaces;

public interface ICartItemRepository
{
    Task<IReadOnlyList<CartItem>> GetCartItems();
    Task<IReadOnlyList<CartItem>> GetCartItemsByCartId(int id);
    Task<CartItem> CreateCartItem(CartItem cartItem);

    public bool SaveChanges();
}