using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Cart : BaseEntity
{
    public DateTime OrderDate { get; set; }
    
    public decimal Subtotal { get; set; }
    
    public string Comment { get; set; }
    
    public string Address { get; set; }
    
    public string PaymentMethod { get; set; }
    
    public bool Payment { get; set; }
    
    
    [ForeignKey("Id")]
    public int UserId { get; set; }
    
    public virtual User User { get; set; }
    

    public List<CartItem> CartItems = new List<CartItem>();
}