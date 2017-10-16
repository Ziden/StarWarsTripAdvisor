using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarwarsTripAdvisor.TripAdvisor.Exception
{
    /// <summary>
    /// Exception used when a parameter sent by SWAPI has a invalid format
    /// </summary>
    public class MgtlFormatException : FormatException
    {
        /// <summary>
        /// The generated message for this exception
        /// </summary>
        public String message { get; set; }

        /// <summary>
        /// The original exception thrown.
        /// </summary>
        public System.Exception exception { get; set; }

        public MgtlFormatException(String message, ArgumentException ex)
        {
            this.message = message;
            this.exception = ex;
        }

        public MgtlFormatException(String message, FormatException ex)
        {
            this.message = message;
            this.exception = ex;
        }
    }
}
