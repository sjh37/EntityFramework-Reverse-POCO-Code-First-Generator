// <auto-generated>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tester.Integration.EFCore8.Single_context_many_files
{
    // Colour
    public class Colour
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 255)

        // Reverse navigation

        /// <summary>
        /// Child Cars where [Car].[PrimaryColourId] point to this entity (CarPrimaryColourFK)
        /// </summary>
        public virtual ICollection<Car> Cars { get; set; } // Car.CarPrimaryColourFK

        /// <summary>
        /// Child CarToColours where [CarToColour].[ColourId] point to this entity (CarToColour_ColourId)
        /// </summary>
        public virtual ICollection<CarToColour> CarToColours { get; set; } // CarToColour.CarToColour_ColourId

        public Colour()
        {
            Cars = new List<Car>();
            CarToColours = new List<CarToColour>();
        }
    }

}
// </auto-generated>
