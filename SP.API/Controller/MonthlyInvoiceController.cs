﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.Concrete;
using SP.Schema.Request;
using SP.Schema.Response;
using System.Data;
using System.Security.Claims;

namespace SP.API.Controller
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyInvoiceController : ControllerBase
    {
        private readonly IMonthlyInvoiceService _monthlyInvoiceService;

        public MonthlyInvoiceController(IMonthlyInvoiceService monthlyInvoiceService)
        {
            _monthlyInvoiceService = monthlyInvoiceService;
        }

        [HttpGet]
    
        public async Task<ApiResponse<List<MonthlyInvoiceResponse>>>GetAllMonthlyInvoices()
        {
            var response = await _monthlyInvoiceService.GetAll();
            return response;
        }

        [HttpGet("{id}")]
    
        public async Task<ApiResponse<MonthlyInvoiceResponse>> GetMonthlyInvoiceById(int id)
        {
            var response = await _monthlyInvoiceService.GetById(id);
            return response;
        }

        [HttpPost]
     
        public async Task<ApiResponse> AddMonthlyInvoice([FromBody] MonthlyInvoiceRequest request)
        {
            var response = await _monthlyInvoiceService.Insert(request);
            return response;
        }

        [HttpPut("{id}")]

        public async Task<ApiResponse> UpdateMonthlyInvoice(int id, [FromBody] MonthlyInvoiceRequest request)
        {
            var response = await _monthlyInvoiceService.Update(id, request);
            return response;
        }

        [HttpDelete("{id}")]

        public async Task<ApiResponse> DeleteMonthlyInvoice(int id)
        {
            var response = await _monthlyInvoiceService.Delete(id);
            return response;
        }
    
    }
}

