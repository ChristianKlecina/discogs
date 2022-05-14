using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;

namespace API.Dtos;

public class OrderCreationDto
{

    
    
    public string Comment { get; set; }
    
    public string PaymentMethod { get; set; }
    
    public bool Payment { get; set; }
    
    
    
    public int UserId { get; set; }
    
    
    
    
    
    public string CustomerBasketId { get; set; }
}