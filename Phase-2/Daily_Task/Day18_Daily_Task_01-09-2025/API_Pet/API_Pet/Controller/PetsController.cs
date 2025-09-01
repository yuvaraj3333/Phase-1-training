using System;
using API_Pet.Context;
using API_Pet.Model;
using Microsoft.AspNetCore.Mvc;

namespace API_Pet.Controller;

public class PetsController : ControllerBase
{
  private readonly AppDBContext _context;
  public PetsController(AppDBContext dbContext)
  {
    _context = dbContext;
  }
    
    [HttpGet("/GetPets")]
    public IActionResult GetPets()
    {
        var result = _context.PetsData.ToList();
        return Ok(result);
    }

}
