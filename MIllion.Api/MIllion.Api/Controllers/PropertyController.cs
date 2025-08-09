using Microsoft.AspNetCore.Mvc;
using Million.Application.DTOs;
using Million.Application.Services.Interfaces;

namespace MIllion.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController: ControllerBase
    {
        private readonly IPropertyService _propertyService;
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpPost("AddProperty")]
        public async Task<IActionResult> AddProperty(PropertyDto propertyDTO)
        {
            var result = await _propertyService.AddAsync(propertyDTO);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("{propertyId}")]
        public async Task<IActionResult> GetPropertyByIdAsync(Guid propertyId)
        {
            var response = await _propertyService.GetByIdAsync(propertyId);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
