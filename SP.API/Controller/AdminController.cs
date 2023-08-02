//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using SP.Base.BaseResponse;
//using SP.Business.Abstract;
//using SP.Entity.Models;
//using SP.Schema.Request;
//using SP.Schema.Response;
//using System.Data;

//namespace SP.API.Controller
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    //[Authorize(Roles = "admin")]
//    public class AdminController : ControllerBase
//    {
//        private readonly IAdminService service;

//        public AdminController(IAdminService service)
//        {
//            this.service = service;
//        }
//        [HttpGet]
//        public async Task<ApiResponse<List<AdminResponse>>> GetAll()
//        {
//            var response = await service.GetAll();
//            return response;
//        }
//        [HttpGet("{id}")]
//        public Task<ApiResponse<AdminResponse>> GeyById(int id)  //initial yapabilirim belki dur şimdilik
//        {
//            var response = service.GetById(id, "Apartments");
//            return response;
//        }

//        [HttpPost]
//        public Task<ApiResponse> Post([FromBody] AdminRequest request)
//        {
//            var response = service.Insert(request);
//            return response;
//        }

//        [HttpPut("{id}")]
//        public Task<ApiResponse> Put(int id, [FromBody] AdminRequest request)
//        {

//            var response = service.Update(id, request);
//            return response;
//        }

//        [HttpDelete("{id}")]
//        public Task<ApiResponse> Delete(int id)
//        {
//            var response = service.Delete(id);
//            return response;
//        }
//    }
//}
