using SWSC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWSC.Business.Contracts
{
    public interface IStarshipBL
    {
        Task<int> CalculateAmountOfStopsAsync(Starship starship, decimal distance);
        Task<List<Starship>> GetAllStarshipsAsync(decimal distance);
    }
}
