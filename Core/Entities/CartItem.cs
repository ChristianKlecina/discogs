using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class CartItem : BaseEntity
{
    
    
    
    
    public int CartId { get; set; }
    
    public virtual Cart Cart { get; set; }
    
    public int TrackId { get; set; }
    
    public virtual Track Track { get; set; }
    
    public int Quantity { get; set; }
}