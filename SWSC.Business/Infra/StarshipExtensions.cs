using SWSC.Entities;
using System;
using System.Threading.Tasks;

namespace SWSC.Business.Infra
{
    public static class StarshipExtensions
    {
        /// <summary>
        /// Extension method responsible for calculate the amount of hours consumables based on string from API
        /// </summary>
        public async static Task<int> GetHoursConsumablesAsync(this Starship starship)
        {
            try
            {
                string hoursTxt;
                int consumable;

                if (string.IsNullOrEmpty(starship.Consumables) || starship.Consumables.Contains("unknown"))
                {
                    return await Task.FromResult(0);
                }
                else if (starship.Consumables.Contains(" hours"))
                {
                    hoursTxt = starship.Consumables.Replace(" hours", "");

                    if (Int32.TryParse(hoursTxt, out consumable)) { return await Task.FromResult(consumable); }
                    return await Task.FromResult(0);
                }
                else if (starship.Consumables.Contains(" hour"))
                {
                    return await Task.FromResult(1); // It is just a hour
                }
                else if (starship.Consumables.Contains(" days"))
                {
                    hoursTxt = starship.Consumables.Replace(" days", "");

                    if (Int32.TryParse(hoursTxt, out consumable)) { return await Task.FromResult(consumable * 24); }
                    return await Task.FromResult(0);
                }
                else if (starship.Consumables.Contains(" day"))
                {
                    return await Task.FromResult(24);
                }
                else if (starship.Consumables.Contains(" weeks"))
                {
                    hoursTxt = starship.Consumables.Replace(" weeks", "");

                    if (Int32.TryParse(hoursTxt, out consumable)) { return await Task.FromResult(consumable * 168); } // 7 * 24 = 168 hours/week
                    return await Task.FromResult(0);
                }
                else if (starship.Consumables.Contains(" week"))
                {
                    return await Task.FromResult(168);
                }
                else if (starship.Consumables.Contains(" months"))
                {
                    hoursTxt = starship.Consumables.Replace(" months", "");

                    if (Int32.TryParse(hoursTxt, out consumable)) { return await Task.FromResult(consumable * 720); } // 30 * 24 = 720 hours/month 
                    return await Task.FromResult(0);
                }
                else if (starship.Consumables.Contains(" month"))
                {
                    return await Task.FromResult(720);
                }
                else if (starship.Consumables.Contains(" years"))
                {
                    hoursTxt = starship.Consumables.Replace(" years", "");

                    if (Int32.TryParse(hoursTxt, out consumable)) { return await Task.FromResult(consumable * 8760); } // 365 * 24 = 8,760 hours/year 
                    return await Task.FromResult(0);
                }
                else if (starship.Consumables.Contains(" year"))
                {
                    return await Task.FromResult(8760);
                }
                else
                {
                    return await Task.FromResult(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
