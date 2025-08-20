using System;
using System.Collections.Generic;

namespace WebApp_DBFirst.Models;

public partial class Store
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public int NoOfEmployees { get; set; }
}
