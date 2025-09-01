using System;
using API_Pet.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Pet.Context;

public class AppDBContext : DbContext
{
  public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

  public DbSet<PetModel> PetsData { get; set; }
}
