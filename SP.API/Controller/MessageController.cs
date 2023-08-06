﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP.API.RabbitMq.Producer;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Data;
using SP.Entity;
using SP.Schema.Request;
using SP.Schema.Response;

namespace SP.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IRabbitMqProducer _rabbitMqProducer;
        private readonly IMessageService _messageService;


        public MessageController(IMessageService messageService, IRabbitMqProducer rabbitMqProducer)
        {
            _messageService = messageService;
            _rabbitMqProducer = rabbitMqProducer;

        }
        [HttpPost]
        public async Task<ApiResponse<MessagesResponse>> SendMessage([FromBody] MessagesRequest request)
        {
            _rabbitMqProducer.SendMessage(request);

            // Insert the message into the database using the IMessageService.
            var response = await _messageService.UserSendMessageAsync(request);

            return response;
        }
        [HttpGet]
        public async Task<ApiResponse<List<MessagesResponse>>> GetAllMessages()
        {
            var response = await _messageService.GetAll();
            return response;
        }

        }


    }
