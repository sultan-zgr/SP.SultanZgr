using AutoMapper;
using SP.Base.BaseResponse;
using SP.Base.Enums.MessagesType;
using SP.Business.Abstract;
using SP.Business.GenericService;
using SP.Data;
using SP.Data.UnitOfWork;
using SP.Entity;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Concrete
{
    public class MessageService : GenericService<Messages, MessagesRequest, MessagesResponse>, IMessageService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageService _messageService;
        public MessageService(IMapper mapper, IUnitOfWork unitOfWork, IMessageService messageService) : base(mapper, unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _messageService = messageService;
        }


        public async Task UpdateMessageStatusAsync(Messages message)
        {
            if (message.MessageStatus == MessageStatus.New)
            {
                TimeSpan timeSinceSent = DateTime.Now - message.Date;
                if (timeSinceSent.TotalMinutes >= 5) // 5 dakika geçtiyse
                {
                    message.MessageStatus = MessageStatus.Unread;
                }
            }
        }
            public async Task<ApiResponse<MessagesResponse>> UserSendMessageAsync(MessagesRequest request)
        {
            try
            {
                var senderUser = await _unitOfWork.DynamicRepo<User>().GetByIdAsync(request.SenderId);
                var receiverUser = await _unitOfWork.DynamicRepo<User>().GetByIdAsync(request.ReceiverId);


                if (senderUser == null || receiverUser == null)
                {
                    return new ApiResponse<MessagesResponse>("Cannot be left blank");
                }

                var message = new Messages
                {
                    SenderId = senderUser.UserId,
                    ReceiverId = receiverUser.UserId,  //Alıcı Admin id = 1 
                    Content = request.Content,
                    IsRead = false,
                    Date = DateTime.Now,
                    MessageStatus = MessageStatus.New
                };
                await UpdateMessageStatusAsync(message);   //MESAJ GÖNDERELİ 5 DAKİKAYI GEÇTİYSE UNREAD OLARAK DEĞİŞECEK

                var response = await _messageService.UserSendMessageAsync(request);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<MessagesResponse>("Your message has been delivered..");
            }

            catch (Exception ex)
            {
                return new ApiResponse<MessagesResponse>("ERROR: " + ex.Message);
            }

        }
        public async Task<ApiResponse<MessagesResponse>> GetMessage(int id)
        {
            var response = await _messageService.GetById(id);
            if (response.Success)
            {
                var message = response.Response;

                    if (message.MessageStatus == MessageStatus.Unread) 
                    {
                        message.MessageStatus = MessageStatus.Read; 
                        await _unitOfWork.SaveChangesAsync(); 
                    }
                }
            
            return response;
        }
        public async Task<ApiResponse<List<MessagesResponse>>> GetAllMessages()
        {
            var response = await _messageService.GetAll();
            return response;
        }

    }
}
