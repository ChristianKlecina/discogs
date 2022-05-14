using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UsersRepository : IUserRepository
{
    
    private readonly StoreContext _context;

    public UsersRepository(StoreContext context)
    {
        _context = context;
    }
    
    public User LoginUser(string email, string password)
    {
        
        return _context.User
            .FirstOrDefault(o => o.Email.ToLower() == email && o.Password == password); 
    }

    public async Task<User> Register(User user)
    {
        var createdUser = _context.User.AddAsync(user);
        _context.SaveChanges();

        return await _context.User.FindAsync(createdUser);
    }

    public void DeleteUser(int id)
    {
        
        
        _context.User.Remove(GetUserById(id));
        _context.SaveChanges();
    }

    
    public async Task<User> UpdateUser(User user)
    {
        
        
        var createdUser = _context.User.Update(user);
        _context.SaveChanges();

        return await _context.User.FindAsync(createdUser);
    }
    
    public User GetUserById(int id)
    {
        
        var user = _context.User.FirstOrDefault(x => x.Id == id);
        return user;
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() > 0;
    }
}