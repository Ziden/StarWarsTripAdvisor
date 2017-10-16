using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarwarsTripAdvisor.TripAdvisor.Exception;
using SharpTrooper.Entities;
using StarwarsTripAdvisor.TripAdvisor.Entity;
using System;

namespace StarWarsTripAdvisor.Tests
{
    [TestClass()]
    public class SWTripAdvisorTests
    {

        private SWTripAdvisor tripAdvisor = new SWTripAdvisor();

        /// <summary>
        /// Tests if a invalid consumable type will raise an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(MgtlFormatException), "A invalid consumables format was accepted.")]
        public void ConsumablesToHoursTest_ConsumableFormat()
        {
            tripAdvisor.ConsumablesToHours("2 yoloyears");
        }

        /// <summary>
        /// Tests if a invalid consumable number will raise an exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(MgtlFormatException), "A invalid consumables number was accepted.")]
        public void ConsumablesToHoursTest_ConsumableNumber()
        {
            tripAdvisor.ConsumablesToHours("2a years");
        }

        /// <summary>
        /// Tests if the consumables string to hours conversion is correct
        /// </summary>
        [TestMethod()]
        public void ConsumablesToHoursTest_Conversion()
        {
            // testing pluralization
            Assert.AreEqual(tripAdvisor.ConsumablesToHours("2 year"), tripAdvisor.ConsumablesToHours("2 years"), "Pluralized name differencing consumables conversion");

            // testing conversions
            Assert.AreEqual(8760, tripAdvisor.ConsumablesToHours("1 year"), "Year to hours conversion is wrong");
            Assert.AreEqual(24 * 2, tripAdvisor.ConsumablesToHours("2 days"), "Days to hours conversion is wrong");
            Assert.AreEqual(730 * 5, tripAdvisor.ConsumablesToHours("5 months"), "Months to hours conversion is wrong");
            Assert.AreEqual(168 * 8, tripAdvisor.ConsumablesToHours("8 weeks"), "Weeks to hours conversion is wrong");

        }

        /// <summary>
        /// Tests simple math to check if the number of stops calculated are correct
        /// </summary>
        [TestMethod()]
        public void CalculateNumberOfStopsTest_Math()
        {
            Starship ship = new Starship();
            ship.consumables = "1 day"; // 24h
            ship.MGLT = 10; // per hour
            int distance = 100;

            CalculationResult result = tripAdvisor.CalculateNumberOfStops(ship, distance);
            Assert.AreEqual(0, result.numberOfStops, "The Ship should take a 10 hours travel while consumables are for 24h so no stops are needed");

            distance = 1000;
            result = tripAdvisor.CalculateNumberOfStops(ship, distance);
            Assert.AreEqual(4, result.numberOfStops, "The Ship should take a 100 hours travel while consumables are for 24h so 4 stops (96h) + starting supply are needed");

        }

        /// <summary>
        /// Tests for 100 random ships, if the number of stops and hours travelled are correct
        /// </summary>
        [TestMethod()]
        public void CalculateNumberOfStopsTest_RandomMath()
        {
            Random rnd = new Random();
            for (int x = 0; x < 100; x++)
            {
                Starship randomShip = new Starship();
                double consumablesDays = rnd.Next(10000) + 1;
                int randomSpeed = rnd.Next(100) + 1;
                int randomDistance = rnd.Next(1000) + 100;
                randomShip.consumables = consumablesDays + " days";
                randomShip.MGLT = randomSpeed;

                double hoursTravelled = tripAdvisor.CalculateTripTime(randomShip, randomDistance);
                int consumablesHours = tripAdvisor.ConsumablesToHours(randomShip.consumables);
                double myCalculatedHours = (double)randomDistance / (double)randomSpeed;
                double myCalculatedStops = myCalculatedHours / consumablesHours;
                CalculationResult result = tripAdvisor.CalculateNumberOfStops(randomShip, randomDistance);

                Assert.AreEqual(myCalculatedHours, hoursTravelled, "Random Ship had got a wrong amount of hours travelled. Speed=" + randomSpeed + " Distance=" + randomDistance);
                Assert.AreEqual(Math.Ceiling(myCalculatedStops) - 1, result.numberOfStops, "Random Ship had got a wrong number of stops. Speed=" + randomSpeed + " Distance=" + randomDistance + " Consumables: " + randomShip.consumables);
            }
        }

        /// <summary>
        /// Tests for decimal numbers if correct formatting is being made
        /// </summary>
        [TestMethod()]
        public void CalculateNumberOfStopsTest_DecimalMath()
        {
            Starship ship = new Starship();
            ship.consumables = "1 day"; // 24h
            ship.MGLT = 9; // per hour
            int distance = 1010; // 112.222222 hours for this travel
            // 112.222h / 24h = 4.675 (should become 5 stops - 1 = 4 )
            CalculationResult result = tripAdvisor.CalculateNumberOfStops(ship, distance);
            Assert.AreEqual(4, result.numberOfStops, "The Ship should take a 112.222222 hours travel while consumables are for 24h so 4.65 stops are needed, rounded to 5 - 1 = 4 stops");

            ship.MGLT = 3;
            distance = 200;     // 200 / 3 = 66,666666 hours for this travel
            // 66,6666 / 24 = 2,7777 stops, so it means should make 3 stop - 1 = 2 stops
            result = tripAdvisor.CalculateNumberOfStops(ship, distance);
            Assert.AreEqual(2, result.numberOfStops, "The Ship should take a 66,66666 hours travel while consumables are for 24h so 2,7777 stops are needed, rounded to 3 - 1 = 2 stops");
        }

        /// <summary>
        /// Tests if CalculateTripTime method is working as indeeded.
        /// </summary>
        [TestMethod()]
        public void CalculateTripTimeTest_Math()
        {
            Starship ship = new Starship();
            ship.MGLT = 10;
            Assert.AreEqual(10, tripAdvisor.CalculateTripTime(ship, 100));
            ship.MGLT = 15;
            Assert.AreEqual(7, Math.Round(tripAdvisor.CalculateTripTime(ship, 100)));
            ship.MGLT = 51;
            Assert.AreEqual(19.607, 84, tripAdvisor.CalculateTripTime(ship, 1000000));
        }
    }
}
