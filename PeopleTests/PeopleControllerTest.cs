using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleApi.Data;
using PeopleApi.Models;
using System.Linq;
using System.Text.Json;

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
                context.Person.Add(new Person { ID = 1, Name = "Putin", Rate = null });
                context.SaveChanges();

                var controller = new PeopleApi.Controllers.PeopleController(context);
                var result = controller.GetPeople();
                var count = result.Count();

                Assert.IsNotNull(result);
                Assert.AreEqual(1, count);

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
