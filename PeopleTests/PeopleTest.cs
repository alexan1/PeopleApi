using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleApi;
using PeopleApi.Data;
using PeopleApi.Models;

namespace PeopleTests
{
    [TestClass]
    public class PeopleTest
    {
        [TestMethod]
        public void GetPeopleTest()
        {
            var context = new Mock<PeopleDbContext>();

            //context.Setup(x => x.Person.Add(It.IsAny<Person>())).Returns((Person u) => u);


            //context.Setup(x => x.Person)
            //    .Returns(new Person
            //    {
            //         DbSet<Person> { ID = 1, Name = "Fake Person" }
            //    });

            var controller = new PeopleApi.Controllers.PeopleController(context.Object);

            var result = controller.GetPeople();

            Assert.IsNotNull(result);
        }
    }
}
