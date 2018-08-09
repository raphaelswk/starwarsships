﻿namespace SWSC.Entities
{
    public class Starship
    {
        /// <summary>
        /// The name of this starship. The common name, such as "Death Star".
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Maximum number of Megalights this starship can travel in a standard hour. 
        /// A "Megalight" is a standard unit of distance and has never been defined before within the Star Wars universe. 
        /// This figure is only really useful for measuring the difference in speed of starships. 
        /// We can assume it is similar to AU, the distance between our Sun (Sol) and Earth.
        /// </summary>
        public string MGLT { get; set; }

        /// <summary>
        /// The maximum length of time that this starship can provide consumables for its entire crew without having to resupply.
        /// </summary>
        public string Consumables { get; set; }

        /// <summary>
        /// The total amount of stops required to make the distance between the planets.
        /// </summary>
        public int AmountStopsRequired { get; set; }
    }
}
