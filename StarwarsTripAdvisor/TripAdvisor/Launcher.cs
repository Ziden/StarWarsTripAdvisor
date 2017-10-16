using SharpTrooper.Core;
using SharpTrooper.Entities;
using StarwarsTripAdvisor.TripAdvisor;
using StarwarsTripAdvisor.TripAdvisor.Entity;
using System;
using StarwarsTripAdvisor.TripAdvisor.Exception;
using System.Collections.Generic;

namespace StarWarsTripAdvisor
{
    class Launcher
    {
        /// <summary>
        /// Main Program
        /// </summary>
        /// <param name="args">Not Used</param>
        static void Main(string[] args)
        {
            IUserInterface userInterface = new ConsoleInterface();
            userInterface.onEnable();

            int megaLightsDistance = userInterface.GetUserMegalightsInput();

            SharpTrooperCore sharpTrooper = new SharpTrooperCore();

            SWTripAdvisor tripAdvisor = new SWTripAdvisor();

            List<CalculationResult> results = new List<CalculationResult>();

            foreach (Starship ship in sharpTrooper.GetAllStarships().results)
            {
                try
                {
                    results.Add(tripAdvisor.CalculateNumberOfStops(ship, megaLightsDistance));
                }
                catch (MgtlFormatException e)
                {
                    userInterface.DisplayError(ship, e);
                }
            }

            userInterface.DisplayResults(results);

            Console.ReadLine();
        }

    }
}
