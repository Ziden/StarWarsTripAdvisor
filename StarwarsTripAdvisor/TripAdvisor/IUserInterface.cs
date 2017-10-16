using SharpTrooper.Entities;
using StarwarsTripAdvisor.TripAdvisor.Entity;
using StarwarsTripAdvisor.TripAdvisor.Exception;
using System.Collections.Generic;

namespace StarwarsTripAdvisor.TripAdvisor
{
    interface IUserInterface
    {
        /// <summary>
        /// When application UI starts
        /// </summary>
        void onEnable();

        /// <summary>
        /// Reads user numeric input
        /// </summary>
        /// <returns>Number typed by the user</returns>
        int GetUserMegalightsInput();

        /// <summary>
        /// Display for the user the resulting number of stops for each starship
        /// </summary>
        /// <param name="ships">List of ships and theyr respective number of stops</param>
        void DisplayResults(List<CalculationResult> results);

        /// <summary>
        /// Displays an error while parsing SWAPI data
        /// </summary>
        /// <param name="ship">The reffering starship</param>
        /// <param name="e">The exception thrown</param>
        void DisplayError(Starship ship, MgtlFormatException e);

    }
}
