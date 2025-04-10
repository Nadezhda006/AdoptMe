using AdoptMe.Data;
using AdoptMe.Data.Entities;
using AdoptMe.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoptMe.Tests
{
    [TestFixture]
    public class CrudRepositoryTests
    {
        private ApplicationDbContext _context;
        private CrudRepository<Pet> _petRepository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb") // Use an in-memory database
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted(); // Reset database before each test
            _context.Database.EnsureCreated();

            _context.Pets.AddRange(new List<Pet>
            {
                new Pet {
                    Id = 1,
                    Name = "Mochi",
                    Type = "Cat",
                    Breed = "British Shorthair",
                    Sex = "Male",
                    Age = 4,
                    Color = "Gray",
                    Weight = 5.8,
                    ImageURL = "https://example.com/images/mochi.jpg",
                    Price = 150.00,
                    Location = "Portland, OR",
                    Details = "Calm and cuddly. Loves to nap in sunbeams.",
                    Visits = new List<Visit>()
    },
                new Pet {
                    Id = 2,
                    Name = "Oliver",
                    Type = "Dog",
                    Breed = "Beagle",
                    Sex = "Male",
                    Age = 5,
                    Color = "Tri-color",
                    Weight = 10.3,
                    ImageURL = "https://example.com/images/oliver.jpg",
                    Price = 180.00,
                    Location = "Austin, TX",
                    Details = "Friendly and energetic. Great with families.",
                    Visits = new List<Visit>() // Optional: can also be null or left out }
            } });
            _context.SaveChanges();

            _petRepository = new CrudRepository<Pet>(_context);
        }

        [Test]
        public async Task CreateAsync_ShouldAddEntity()
        {
            var newCat = new Pet { Id = 3,
                Name = "Cleo",
                Type = "Cat",
                Breed = "Maine Coon",
                Sex = "Female",
                Age = 3,
                Color = "Gray and White",
                Weight = 6.1,
                ImageURL = "https://example.com/images/cleo.jpg",
                Price = 140.00,
                Location = "Denver, CO",
                Details = "Gentle giant. Loves high places and belly rubs.",
                Visits = new List<Visit>
    {
        new Visit
        {
            PetId = 3,
            VetId = 1,
            VisitDate = DateTime.Now.AddDays(-10),
            Description = "Spay surgery and recovery check"
        }
    }
            };
            await _petRepository.CreateAsync(newCat);

            var result = await _context.Pets.FindAsync(3);
            Assert.IsNotNull(result);
            Assert.AreEqual("Cleo", result.Name);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            var pets = await _petRepository.GetAllAsync();
            Assert.AreEqual(2, pets.Count);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCorrectEntity()
        {
            var pet = await _petRepository.GetByIdAsync(1);
            Assert.IsNotNull(pet);
            Assert.AreEqual("Mochi", pet.Name);
        }

        [Test]
        public async Task DeleteByIdAsync_ShouldRemoveEntity_WhenEntityExists()
        {
            await _petRepository.DeleteByIdAsync(1);

            var pet = await _context.Pets.FindAsync(1);
            Assert.IsNull(pet);
        }

        [Test]
        public void DeleteByIdAsync_ShouldThrowException_WhenEntityDoesNotExist()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _petRepository.DeleteByIdAsync(99));
        }

        [Test]
        public async Task UpdateAsync_ShouldModifyEntity()
        {
            var existingPet = await _petRepository.GetByIdAsync(1);
            existingPet.Name = "UpdatedName";
            await _petRepository.UpdateAsync(existingPet);

            var updatedPet = await _context.Pets.FindAsync(1);
            Assert.AreEqual("UpdatedName", updatedPet.Name);
        }

        [Test]
        public void GetByFilter_ShouldReturnFilteredEntities()
        {
            var filteredPets = _petRepository.GetByFilter(c => c.Name == "Mochi");
            Assert.AreEqual(1, filteredPets.Count);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

    }
}

        

