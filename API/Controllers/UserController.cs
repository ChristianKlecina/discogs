using System.Security.Claims;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;

using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserRepository _userRepo;
    private readonly IMapper _mapper;


    public UserController(IUserRepository userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Endpoint()
    {
        var currentUser = GetCurrentUser();
        return Ok(currentUser);
    }

    [HttpPost("admin")]
    public IActionResult AdminRegister([FromBody]AdminRegisterDto adminDto)
    {
        if (adminDto != null)
        {
            var user = new User
            {
                Name = adminDto.Name,
                Lastname = adminDto.Lastname,
                Role = adminDto.Role,
                Address = adminDto.Address,
                City = adminDto.City,
                Country = adminDto.Country,
                Telephone = adminDto.Telephone,
                Email = adminDto.Email,
                Password = adminDto.Password
            };
            _userRepo.Register(user);
            return Ok(user);
        }

        return BadRequest();
    }
    
    [HttpPost("user")]
    public IActionResult UserRegister([FromBody]UserRegisterDto userDto)
    {
        if (userDto != null)
        {
            var user = new User
            {
                Name = userDto.Name,
                Lastname = userDto.Lastname,
                Role = userDto.Role,
                Address = userDto.Address,
                City = userDto.City,
                Country = userDto.Country,
                Telephone = userDto.Telephone,
                Email = userDto.Email,
                Password = userDto.Password
            };

            //return Ok(_userRepo.Register(user));
            _userRepo.Register(user);
            return Ok(user);
        }
        
            return BadRequest();
    }

    [HttpDelete("{Id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            if (id != null)
            {
                _userRepo.DeleteUser(id);
            
                return Ok();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
            throw;
        }

        return null;

    }

    [HttpPut]
    public IActionResult UpdateUser([FromBody]User user)
    {
        var oldUser = _userRepo.GetUserById(user.Id);
        if (oldUser != null)
        {
            _mapper.Map(user, oldUser);
            _userRepo.SaveChanges();
            return Ok(user);
        }

        return NotFound();
    }

    [HttpGet("{Id}")]
    public IActionResult GetUser(int id)
    {
        if (id != null)
        {
            var user = _userRepo.GetUserById(id);
            return Ok(user);
        }

        return NoContent();
    }
    
    
    private User GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new User
            {
                Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                Lastname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                
            };
        }

        return null;
    }
    
}