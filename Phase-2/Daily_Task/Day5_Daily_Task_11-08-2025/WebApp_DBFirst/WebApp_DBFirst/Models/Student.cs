using System;
using System.Collections.Generic;

namespace WebApp_DBFirst.Models;

public partial class Student
{
    public short? Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }
}
