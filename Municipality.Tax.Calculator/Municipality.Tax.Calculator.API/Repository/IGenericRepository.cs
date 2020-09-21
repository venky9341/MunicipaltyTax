using System.Collections.Generic;
using System.Threading.Tasks;

namespace Municipality.Tax.Calculator.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Insert(T obj);
        Task<List<T>> GetById(int municipaltyId, int taxTypeId);
    }
}
