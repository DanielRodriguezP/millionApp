using AutoMapper;
using Million.Application.DTOs;
using Million.Application.Services.Interfaces;
using Million.Data.Entities;
using Million.Data.Interfaces;
using Million.Data.Response;

namespace Million.Application.Services.Implementations
{
    public class PropertyService : IPropertyService
    {
        private readonly IRepository<Property> _repository;
        private readonly IMapper _mapper;

        public PropertyService(IRepository<Property> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ActionResponse<PropertyDto>> GetByIdAsync(Guid id)
        {
            var property = await _repository.GetByIdAsync(id);
            return _mapper.Map<ActionResponse<PropertyDto>>(property);
        }
        public async Task<ActionResponse<IEnumerable<PropertyDto>>> GetAllAsync()
        {
            var properties = await _repository.GetAllAsync();
            return _mapper.Map<ActionResponse<IEnumerable<PropertyDto>>>(properties);
        }
        public async Task<ActionResponse<PropertyDto>> AddAsync(PropertyDto request)
        {
            var property = _mapper.Map<Property>(request);
            property.IdProperty = Guid.NewGuid();
            await _repository.AddAsync(property);
            return _mapper.Map<ActionResponse<PropertyDto>>(property);
        }
        public async Task<ActionResponse<PropertyDto>> UpdateAsync(Guid id, PropertyDto request)
        {
            var getProperty = await _repository.GetByIdAsync(id);
            var mapper = _mapper.Map<Property>(getProperty);
            await _repository.UpdateAsync(mapper);
            return _mapper.Map<ActionResponse<PropertyDto>>(mapper);
        }
        public async Task<ActionResponse<bool>> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);

    }
}
