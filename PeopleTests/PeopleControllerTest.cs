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

            using (var context = new PeopleDbContext(options))
            {

                var rates = new List<Rating>()
                { new Rating { PersonID = 1, UserID = "user1", Rate = 1 },
                new Rating { PersonID = 1, UserID = "user2", Rate = 10 } };

                var person1 = new Person { ID = 1, Name = "Putin", Rate = rates };                 

                  
                var person2 = new Person { ID = 2, Name = "Trump", Rate = null };                

                var controller = new PeopleApi.Controllers.PeopleController(context);

                if (controller.PersonExists(1))
                    _ = controller.DeletePerson(1);

                if (controller.PersonExists(2))
                    _ = controller.DeletePerson(2);

                var action1 = controller.PostPerson(person1);
                //Assert.AreEqual(TaskStatus.RanToCompletion, action1.Status);
                var action2 = controller.PostPerson(person2);
                //Assert.AreEqual(TaskStatus.RanToCompletion, action2.Status);

                var result = controller.GetPeople();
                var count = result.Count();

                Assert.IsNotNull(result);
                Assert.AreEqual(2, count);

                var action3 = controller.DeletePerson(1);
                Assert.AreEqual(TaskStatus.RanToCompletion, action3.Status);
                var action4 = controller.DeletePerson(2);
                Assert.AreEqual(TaskStatus.RanToCompletion, action4.Status);


            }
        }
        [TestMethod]
        public void GetPersonTest()
        {
            var options = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseInMemoryDatabase(databaseName: "PeopleTestDb")
                .Options;

            using (var context = new PeopleDbContext(options))
            {

                var person = new Person { ID = 1, Name = "Putin", Rate = null };                

                var controller = new PeopleApi.Controllers.PeopleController(context);
                if (controller.PersonExists(1))
                    _ = controller.DeletePerson(1);

                var action = controller.PostPerson(person);
                Assert.AreEqual(TaskStatus.RanToCompletion, action.Status);

                var result = controller.GetPerson(1);

                var okResult = result as OkObjectResult;
                var personR = okResult.Value as Person;
                
                Assert.AreEqual(person, personR);
            }
        }
    }
}
