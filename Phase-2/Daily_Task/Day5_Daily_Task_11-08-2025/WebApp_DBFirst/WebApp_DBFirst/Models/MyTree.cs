using System;
using System.Collections.Generic;

namespace WebApp_DBFirst.Models;

public partial class MyTree
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int TreeCount { get; set; }
}
