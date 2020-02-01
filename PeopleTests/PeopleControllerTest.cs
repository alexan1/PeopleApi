using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleApi.Data;
using PeopleApi.Models;
using System.Linq;
using System.Text.Json;
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
                var person1 = new Person { ID = 1, Name = "Putin", Rate = null };
                var person2 = new Person { ID = 2, Name = "Trump", Rate = null };                

                var controller = new PeopleApi.Controllers.PeopleController(context);

                var action1 = controller.PostPerson(person1);
                Assert.AreEqual(TaskStatus.RanToCompletion, action1.Status);
                var action2 = controller.PostPerson(person2);
                Assert.AreEqual(TaskStatus.RanToCompletion, action2.Status);

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
                context.Person.Add(person);
                context.SaveChanges();

                var controller = new PeopleApi.Controllers.PeopleController(context);
                var result = controller.GetPerson(1).Result;
                var resultPerson = JsonSerializer.Deserialize<Person>(result.ToString());
                Assert.AreEqual(person, resultPerson);
            }
        }
    }
}
