using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Core.Interfaces;

public interface IUserRepository
{
    User LoginUser(string email, string password);

    public Task<User> Register(User user);

    public void DeleteUser(int id);
    
    public Task<User> UpdateUser (User user);

    public User GetUserById(int id);

    public bool SaveChanges();
}