using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SWSC.Entities;
using SWSC.Persistence.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWSC.Tests
{
    [TestClass]
    public class UnitTestStarshipDAO
    {
        private IStarshipDAO _starshipDAO;

        private List<Starship> _ships = new List<Starship>
        {
            new Starship{ Name = "Lorem Ipsum", MGLT = "100", Consumables = "5 days" },
            new Starship{ Name = "Adipisci Velit", MGLT = "unknown", Consumables = "7 days" },
            new Starship{ Name = "Dolor Sit", MGLT = "60", Consumables = "1 year" }
        };

        [TestInitialize]
        public void Init()
        {
            var mock = new Mock<IStarshipDAO>();

            // GetAll
            mock.Setup(p => p.GetAllStarshipsAsync("1"))
                .Returns(Task.FromResult(_ships));

            // Set Object
            _starshipDAO = mock.Object;
        }

        [TestCleanup]
        public void Finish()
        {
            _starshipDAO = null;
        }

        [TestMethod]
        public async Task GetAllTestValidAsync()
        {
            var list = await _starshipDAO.GetAllStarshipsAsync();

            Assert.IsNotNull(list);

            Assert.AreNotEqual(list.Count, 0);
        }
    }
}
