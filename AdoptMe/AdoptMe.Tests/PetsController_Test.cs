using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdoptMe.Controllers;
using AdoptMe.Data;
using AdoptMe.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Drawing;
using MockQueryable;
using MockQueryable.Moq;

namespace AdoptMe.Tests
{
    [TestFixture]
    public class PetsControllerTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private PetsController _controller;
        private Mock<DbSet<Pet>> _mockSet;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PetsDb") // In-memory DB
                .Options;

            var _context = new ApplicationDbContext(options);
            var pets = new List<Pet>
            {
                new Pet { Name = "Buddy", Type = "Dog", Age = 1, Breed = "test", Color = "black", Details = "",  ImageURL = "", Location = "1", Price = 5, Sex = "f", Weight = 10},
                new Pet { Name = "Mittens", Type = "Cat", Age = 1, Breed = "test", Color = "black", Details = "",  ImageURL = "", Location = "1", Price = 5, Sex = "f", Weight = 10 }
            };
            _context.Pets.AddRange(pets);
            _context.SaveChanges(); // Seed database

            var mock = pets.AsQueryable().BuildMockDbSet();
           
            _mockContext = new Mock<ApplicationDbContext>(options);
            _mockContext.Setup(c => c.Pets).Returns(mock.Object);
            _mockContext.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1); // Mock SaveChangesAsync

            _controller = new PetsController(_mockContext.Object);
        }



        [Test]
        public async Task Index_ReturnsViewResult_WithListOfPets()
        {
            var result = await _controller.Index();

            var viewResult = result as ViewResult;
            Assert.NotNull(viewResult);

            var model = viewResult.Model as IEnumerable<Pet>;
            Assert.NotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [Test]
        public async Task Details_ReturnsNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        
        [Test]
        public async Task Edit_ReturnsNotFound_WhenIdDoesNotMatch()
        {
            var pet = new Pet { Id = 2, Name = "Buddy" };
            var result = await _controller.Edit(1, pet);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }
    }
}
