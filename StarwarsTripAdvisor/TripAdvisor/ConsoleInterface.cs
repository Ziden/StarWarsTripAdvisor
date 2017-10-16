using SharpTrooper.Entities;
using StarwarsTripAdvisor.TripAdvisor.Entity;
using StarwarsTripAdvisor.TripAdvisor.Exception;
using System;
using System.Collections.Generic;

namespace StarwarsTripAdvisor.TripAdvisor
{
    /// <summary>
    /// Implements a simple Console UX for SWTripAdvisor APP
    /// </summary>
    class ConsoleInterface : IUserInterface
    {
        public void onEnable()
        {
            Console.WriteLine("To the star wars trip advisor welcome.  Yeesssssss. !");
        }

        public void DisplayError(Starship ship, MgtlFormatException ex)
        {
            Console.WriteLine("");
            Console.WriteLine("Error while parsing data from SWAPI.");
            Console.WriteLine("SHIP: " + ship.name);
            Console.WriteLine("ERROR: " + ex.message);
        }

        public int GetUserMegalightsInput()
        {
            int megaLightsDistance = -1;

            while (megaLightsDistance == -1)
            {
                Console.WriteLine("How many mega lights travelling are you ?");
                String line = Console.ReadLine();
                try
                {
                    megaLightsDistance = Int32.Parse(line);
                    if (megaLightsDistance < 0)
                    {
                        Console.WriteLine("Like my words, backwards going you are then. Allow that i can't.");
                        Console.WriteLine("Again you shall try.");
                        megaLightsDistance = -1;
                        continue;
                    }
                    else if (megaLightsDistance == 0)
                    {
                        Console.WriteLine("Travelling, you are not. My help, you won't need.");
                        Console.WriteLine("Again you shall try.");
                        megaLightsDistance = -1;
                        continue;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(line + " a number is not, no. ");
                    Console.WriteLine("Again you shall try.");
                }
            }
            Console.WriteLine("For " + megaLightsDistance + " that you shall travel, the knowledge i shall have.");
            Console.WriteLine("RESTful HTTP Requests i shall make, so prepared you can be.");
            return megaLightsDistance;
        }

        public void DisplayResults(List<CalculationResult> results)
        {
            foreach (CalculationResult result in results)
            {
                Console.WriteLine(result.ship.name + " - " + result.numberOfStops);
            }
        }

    }
}
