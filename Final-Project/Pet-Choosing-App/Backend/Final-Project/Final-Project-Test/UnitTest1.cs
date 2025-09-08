using Final_Project.Models;
using Final_Project.AppDbContext;

using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Tests
{
    [TestFixture]
    public class AllModuleTests
    {
        private PetDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PetDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new PetDbContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }

        [Test]
        public void AddUser_Should_Add_User_To_Db()
        {
            var user = new User
            {
                Name = "Test",
                Email = "user@test.com",
                PasswordHash = "123",
                Role = "Customer"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            _context.Users.Any(u => u.Email == "user@test.com").Should().BeTrue();
        }

        [Test]
        public void GetUserByEmail_Should_Return_User()
        {
            var user = new User
            {
                Name = "LoginUser",
                Email = "login@test.com",
                PasswordHash = "hashed",
                Role = "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var fetched = _context.Users.SingleOrDefault(u => u.Email == "login@test.com");
            fetched.Should().NotBeNull();
            fetched.Email.Should().Be("login@test.com");
        }

        [Test]
        public void AddPet_Should_Add_Pet_To_Db()
        {
            var pet = new Pet
            {
                PetId = 1,
                Name = "Doggie",
                Type = "Dog",
                Breed = "Labrador",
                Price = 500m
            };

            _context.Pets.Add(pet);
            _context.SaveChanges();

            _context.Pets.Any(p => p.Name == "Doggie").Should().BeTrue();
        }

        [Test]
        public void GetPetById_Should_Return_Pet()
        {
            var pet = new Pet
            {
                PetId = 2,
                Name = "Catty",
                Type = "Cat",
                Breed = "Persian",
                Price = 300m
            };

            _context.Pets.Add(pet);
            _context.SaveChanges();

            var fetched = _context.Pets.Find(2);
            fetched.Should().NotBeNull();
            fetched.Name.Should().Be("Catty");
        }

        [Test]
        public void AddOrder_Should_Add_Order_To_Db()
        {
            var order = new Order
            {
                OrderId = 1,
                TotalAmount = 1000m,
                OrderDate = DateTime.UtcNow,
                Status = "Pending"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            _context.Orders.Any(o => o.TotalAmount == 1000m).Should().BeTrue();
        }

        [Test]
        public void AddCartItem_Should_Add_Item_To_Db()
        {
            var cartItem = new CartItem
            {
                CartItemId = 1,
                PetId = 1,
                Quantity = 2
            };

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            _context.CartItems.Any(c => c.PetId == 1 && c.Quantity == 2).Should().BeTrue();
        }
    }
}
