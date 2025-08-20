using System;
using System.Collections.Generic;

namespace WebApp_DBFirst.Models;

public partial class Emp
{
    public int Id { get; set; }

    public string EmpName { get; set; } = null!;

    public string EmpEmail { get; set; } = null!;

    public int EmpPhone { get; set; }
}
