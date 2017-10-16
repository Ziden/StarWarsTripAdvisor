using SharpTrooper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarwarsTripAdvisor.TripAdvisor.Entity
{
    public class CalculationResult
    {

        public Starship ship { get; set; }
        public int numberOfStops { get; set; }

        public CalculationResult(Starship ship, int numberOfStops)
        {
            this.ship = ship;
            this.numberOfStops = numberOfStops;
        }

    }
}
