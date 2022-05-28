using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace API.Dtos;

public class CartDto
{
    public int Id { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    public decimal Subtotal { get; set; }
    
    public string Comment { get; set; }
    
    public string PaymentMethod { get; set; }
    
    public bool Payment { get; set; }
    
    public string Address { get; set; }
    
    [ForeignKey("Id")]
    public int UserId { get; set; }

    public List<CartItemDto> CartItems = new List<CartItemDto>();
}