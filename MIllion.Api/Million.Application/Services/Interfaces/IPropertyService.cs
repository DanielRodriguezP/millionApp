using Million.Application.DTOs;
using Million.Data.Entities;
using Million.Data.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Application.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<ActionResponse<PropertyDto>> GetByIdAsync(Guid id);
        Task<ActionResponse<IEnumerable<PropertyDto>>> GetAllAsync();
        Task<ActionResponse<PropertyDto>> AddAsync(PropertyDto property);
        Task<ActionResponse<PropertyDto>> UpdateAsync(Guid id, PropertyDto property);
        Task<ActionResponse<bool>> DeleteAsync(Guid id);
    }
}
