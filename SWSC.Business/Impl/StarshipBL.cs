using SWSC.Business.Contracts;
using SWSC.Business.Infra;
using SWSC.Entities;
using SWSC.Persistence.Impl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWSC.Business.Impl
{
    public class StarshipBL : IStarshipBL
    {
        /// <summary>
        /// Calculate the amount of stops based on Hours Consumables, MGLT and Distance (user input)
        /// </summary>
        public async Task<int> CalculateAmountOfStopsAsync(Starship starship, decimal distance)
        {
            try
            {
                int hours = await starship.GetHoursConsumablesAsync();

                if (hours > 0 && Int32.TryParse(starship.MGLT.Trim(), out int mglt))
                {
                    // The (int) makes the number round down.
                    return await Task.FromResult((int)(distance / (hours * mglt)));
                }

                return await Task.FromResult(0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all the starship resources
        /// </summary>
        public async Task<List<Starship>> GetAllStarshipsAsync(decimal distance)
        {
            try
            {
                List<Starship> starships = await new StarshipDAO().GetAllStarshipsAsync();

                foreach (var starship in starships)
                {
                    starship.AmountStopsRequired = await CalculateAmountOfStopsAsync(starship, distance);
                }

                return starships;
            }
            catch (Exception)
            {
                throw;
            }            
        }
    }
}
