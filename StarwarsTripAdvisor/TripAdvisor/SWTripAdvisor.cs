using SharpTrooper.Entities;
using StarwarsTripAdvisor.TripAdvisor.Entity;
using StarwarsTripAdvisor.TripAdvisor.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsTripAdvisor

{
    public class SWTripAdvisor
    {

        /// <summary>
        /// Integer Enum referencing the number of hours of the specified time measure
        /// </summary>
        private enum ConsumableConversor
        {
            day = 24,
            week = 168,
            month = 730,
            year = 8760
        }

        /// <summary>
        /// Convert SWAPI Mgtl string (ie 2 years) to hours
        /// </summary>
        /// <param name="mglt">SWAPI formated mglt</param>
        /// <returns></returns>
        public int ConsumablesToHours(String mglt)
        {
            // removing plurals
            if (mglt[mglt.Length - 1] == 's')
            {
                mglt = mglt.Substring(0, mglt.Length - 1);
            }

            // grabbing the date type
            String[] split = mglt.Split(' ');
            try
            {
                ConsumableConversor conversor = (ConsumableConversor)Enum.Parse(typeof(ConsumableConversor), split[1]);
                return (int)conversor * Int32.Parse(split[0]);
            }
            catch (System.ArgumentException ex)
            {
                throw new MgtlFormatException("Consumable type " + split[1] + " not found.", ex);

            }
            catch (System.FormatException ex)
            {
                throw new MgtlFormatException("Could not read consumables number (" + split[0] + ") for string " + mglt, ex);

            }
        }

        /// <summary>
        /// Calculate how many stops will be needed to the parameter Ship travel the mega lights distance
        /// </summary>
        /// <param name="ship">The desire Starship</param>
        /// <param name="megaLightsDistance">How many MGLT for the trip</param>
        /// <returns>A CalculationResult containing reference for the ship and the number of stops</returns>
        public CalculationResult CalculateNumberOfStops(Starship ship, int megaLightsDistance)
        {
            // amount of hours the ship can run while full
            double hoursFullTank = ConsumablesToHours(ship.consumables);

            // how many hours the trip will take (rounded up)
            double tripHoursTotal = CalculateTripTime(ship, megaLightsDistance);

            // rounded up number of stops (half stops or 0.1 stops means the ship will need an extra stop anyway!!!)
            int numberOfStops = (int)Math.Ceiling(tripHoursTotal / hoursFullTank);

            // we presume the ship will leave full on its departure, so it wont need one of this stops
            numberOfStops -= 1; // if the starship is not full on its departure, please fire the commander

            if (numberOfStops < 0)
                numberOfStops = 0;

            return new CalculationResult(ship, numberOfStops);

        }

        /// <summary>
        /// Calculates in hours how long a trip will take
        /// </summary>
        /// <param name="ship">The tripping ship</param>
        /// <param name="megaLightsDistance">Distance the ship will travel</param>
        /// <returns>Returns a double referencing how many hours the travel will take</returns>
        public double CalculateTripTime(Starship ship, int megaLightsDistance)
        {
            double mgltPerHour = ship.MGLT;
            double hours = (double)megaLightsDistance / mgltPerHour;
            return hours;
        }

    }
}
