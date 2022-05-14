using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class OrderItem : BaseEntity
{

    public int TrackId { get; set; }
    
    
    
    //[ForeignKey("Id")]
    //public int TrackMediumId { get; set; }
    //public virtual Track Track { get; set; }
    
    
    
    public decimal Price { get; set; }
    
    
    //public decimal Price { get; set; }
    
    public int Quantity { get; set; }
}