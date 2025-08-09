using Million.Data.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Data.Interfaces
{
    public interface IRepository<T> where T: class
    {
        Task<ActionResponse<T>> GetByIdAsync(Guid id);
        Task<ActionResponse<IEnumerable<T>>> GetAllAsync();
        Task<ActionResponse<T>> AddAsync(T entity);
        Task<ActionResponse<T>> UpdateAsync(T entity);
        Task<ActionResponse<bool>> DeleteAsync(Guid id);  
    }
}
