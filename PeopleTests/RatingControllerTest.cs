using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleApi.Data;
using PeopleApi.Models;
using System.Threading.Tasks;

namespace PeopleTests
{
    [TestClass]
    public class RatingControllerTest
    {
        [TestMethod]
        public void GetPersonRatingTest()
        {
            var options = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase(databaseName: "PeopleTestDb")
                .Options;

            using var context = new PeopleDbContext(options);
            context.Database.EnsureDeleted();

            var rating1 = new Rating { PersonID = 1, UserID = "user1", Rate = 1 };
            var rating2 = new Rating { PersonID = 1, UserID = "user2", Rate = 10 };

            var controller = new PeopleApi.Controllers.RatingsController(context);            

            var action1 = controller.PostRating(rating1);
            Assert.AreEqual(TaskStatus.RanToCompletion, action1.Status);
            var action2 = controller.PostRating(rating2);
            Assert.AreEqual(TaskStatus.RanToCompletion, action2.Status);

            var result = controller.GetRating(1).Result;
                        
            var okResult = result as OkObjectResult;
            var rating = okResult.Value as double?;

            Assert.IsNotNull(result);
            Assert.AreEqual(5.5, rating);            
        }

        [TestMethod]
        public void GetPersonNonRatingTest()
        {
            var options = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase(databaseName: "PeopleTestDb")
                .Options;

            using var context = new PeopleDbContext(options);
            context.Database.EnsureDeleted();
            
            var controller = new PeopleApi.Controllers.RatingsController(context);
            
            var result = controller.GetRating(1).Result;

            var okResult = result as OkObjectResult;
            var rating = okResult.Value as double?;

            Assert.AreEqual(0.0, rating);
        }

        [TestMethod]
        public void PostPersonRatingTest()
        {
            var options = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase(databaseName: "PeopleTestDb")
                .Options;

            using var context = new PeopleDbContext(options);
            context.Database.EnsureDeleted();

            var controller = new PeopleApi.Controllers.RatingsController(context);
            var userID = "anonymous";
            var rating = new Rating(){ PersonID = 1, UserID = userID, Rate = 7 };

            var result = controller.PostRating(rating);

            Assert.AreEqual(TaskStatus.RanToCompletion, result.Status);

            //var okResult = result as OkObjectResult;
            //var rat = okResult.Value as bool?;

            //Assert.AreEqual(0.0, rating);
        }
    }
}
