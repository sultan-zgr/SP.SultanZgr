using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _service;

        public ApartmentController(IApartmentService service)
        {
            _service = service;
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse<List<ApartmentResponse>>> GetAllApartments()
        {
            var response = await _service.GetAll("Apartments"); 
            return response;
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse<ApartmentResponse>> GetApartmentById(int id)
        {
            var response = await _service.GetById(id, "Apartments"); 
            return response;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse> AddApartment([FromBody] ApartmentRequest request)
        {
            var response = await _service.Insert(request);
             return response;
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse> UpdateApartment(int id, [FromBody] ApartmentRequest request)
        {
            var response = await _service.Update(id, request);
            return response;
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ApiResponse> DeleteApartment(int id)
        {
            var response = await _service.Delete(id);
            return response;
        }
    }
}

