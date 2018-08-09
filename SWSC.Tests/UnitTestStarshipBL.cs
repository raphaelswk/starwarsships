using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SWSC.Business.Contracts;
using SWSC.Business.Impl;
using SWSC.Entities;
using System.Threading.Tasks;

namespace SWSC.Tests
{
    [TestClass]
    public class UnitTestStarshipBL
    {
        private IStarshipBL _starshipBL;

        [TestInitialize]
        public void Init()
        {
            var mock = new Mock<IStarshipBL>();

            // Set Object
            _starshipBL = new StarshipBL();
        }

        [TestCleanup]
        public void Finish()
        {
            _starshipBL = null;
        }

        /// <summary>
        /// If MGLT = "105" and Consumables = "5 days", the result must be 79 stops
        /// </summary>
        [TestMethod]
        public async Task CalculateAmountOfStopsAsync_105MGLTAnd5daysConsumables_79Stops()
        {
            decimal distance = 1000000;

            var ship = new Starship { Name = "Lorem Ipsum", MGLT = "105", Consumables = "5 days" };

            Assert.AreEqual(await _starshipBL.CalculateAmountOfStopsAsync(ship, distance), 79);
        }

        /// <summary>
        /// If MGLT = "70" and Consumables = "1 month" the calc is 19.841 but the result must be 19
        /// It must always Round Down
        /// </summary>
        [TestMethod]
        public async Task CalculateAmountOfStopsAsync_DecimalResult_RoundDown()
        {
            decimal distance = 1000000;

            var ship = new Starship { Name = "Lorem Ipsum", MGLT = "70", Consumables = "1 month" };

            Assert.AreEqual(await _starshipBL.CalculateAmountOfStopsAsync(ship, distance), 19);
            Assert.AreNotEqual(await _starshipBL.CalculateAmountOfStopsAsync(ship, distance), 20);
        }

        /// <summary>
        /// There is no stop for MGLT = "unknown"
        /// </summary>
        [TestMethod]
        public async Task CalculateAmountOfStopsAsync_UnknownMGLT_NoneStop()
        {
            decimal distance = 1000000;

            var ship = new Starship { Name = "Adipisci Velit", MGLT = "unknown", Consumables = "7 days" };

            Assert.AreEqual(await _starshipBL.CalculateAmountOfStopsAsync(ship, distance), 0);
        }

        /// <summary>
        /// There is no stop for Consumables = "unknown"
        /// </summary>
        [TestMethod]
        public async Task CalculateAmountOfStopsAsync_UnknownConsumables_NoneStop()
        {
            decimal distance = 1000000;

            var ship = new Starship { Name = "Adipisci Velit", MGLT = "105", Consumables = "unknown" };

            Assert.AreEqual(await _starshipBL.CalculateAmountOfStopsAsync(ship, distance), 0);
        }

        /// <summary>
        /// There is no stop for distance = 0
        /// </summary>
        [TestMethod]
        public async Task CalculateAmountOfStopsAsync_NoneDistance_NoneStop()
        {
            decimal distance = 0;

            var ship = new Starship { Name = "Adipisci Velit", MGLT = "105", Consumables = "5 days" };

            Assert.AreEqual(await _starshipBL.CalculateAmountOfStopsAsync(ship, distance), 0);
        }
    }
}
