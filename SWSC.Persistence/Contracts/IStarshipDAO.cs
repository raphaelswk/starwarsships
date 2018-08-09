using SWSC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SWSC.Persistence.Contracts
{
    public interface IStarshipDAO
    {
        Task<List<Starship>> GetAllStarshipsAsync(string pageNumber = "1");
    }
}
