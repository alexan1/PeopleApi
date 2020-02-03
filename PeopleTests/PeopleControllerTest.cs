using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleApi.Data;
using PeopleApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleTests
{
    [TestClass]
    public class PeopleControllerTest
    {
        [TestMethod]
        public void GetPeopleTest()
        {
            var options = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase(databaseName: "PeopleTestDb")
                .Options;

            using var context = new PeopleDbContext(options);
            context.Database.EnsureDeleted();

            var rates = new List<Rating>()
                { new Rating { PersonID = 1, UserID = "user1", Rate = 1 },
                new Rating { PersonID = 1, UserID = "user2", Rate = 10 } };

            var person1 = new Person { ID = 1, Name = "Putin", Rate = rates };
            var person2 = new Person { ID = 2, Name = "Trump", Rate = null };

            var controller = new PeopleApi.Controllers.PeopleController(context);

            var action1 = controller.PostPerson(person1);
            Assert.AreEqual(TaskStatus.RanToCompletion, action1.Status);
            var action2 = controller.PostPerson(person2);
            Assert.AreEqual(TaskStatus.RanToCompletion, action2.Status);

            var result = controller.GetPeople();
            var count = result.Result.Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, count);
        }
        [TestMethod]
        public void GetPersonTest()
        {
            var options = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase(databaseName: "PeopleTestDb")
                .Options;

            using var context = new PeopleDbContext(options);
            context.Database.EnsureDeleted();
            var person = new Person { ID = 1, Name = "Putin", Rate = null };

            var controller = new PeopleApi.Controllers.PeopleController(context);

            var action = controller.PostPerson(person);
            Assert.AreEqual(TaskStatus.RanToCompletion, action.Status);

            var result = controller.GetPerson(1).Result;

            var okResult = result as OkObjectResult;
            var personR = okResult.Value as Person;

            Assert.AreEqual(person, personR);
        }
    }
}
