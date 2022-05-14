﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class CustomerBasket
{
    public CustomerBasket(string id)
    {
        Id = id;
    }

    
    public string Id { get; set; }

    public virtual List<OrderItem> Items { get; set; } = new List<OrderItem>();
}