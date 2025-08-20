using System;
using System.Collections.Generic;

namespace WebApp_DBFirst.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public float Price { get; set; }

    public int Stock { get; set; }
}
