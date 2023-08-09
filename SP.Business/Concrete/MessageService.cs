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
        public MessageService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
                    Date = DateTime.Now,
                    MessageStatus = MessageStatus.Unread
                };


                await _unitOfWork.DynamicRepo<Messages>().InsertAsync(message);
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
            try
            {
                var message = await _unitOfWork.DynamicRepo<Messages>().GetByIdAsync(id);

                if (message == null)
                {
                    return new ApiResponse<MessagesResponse>("Message not found");
                }

                if (message.MessageStatus == MessageStatus.Read)  // Check if the message is already read
                {
                    return new ApiResponse<MessagesResponse>("Message has already been read");
                }

                message.MessageStatus = MessageStatus.Read;  // Set the message status to "Read"
                await _unitOfWork.DynamicRepo<Messages>().UpdateAsync(message);
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<MessagesResponse>("Message marked as read");
            }
            catch (Exception ex)
            {
                return new ApiResponse<MessagesResponse>("ERROR: " + ex.Message);
            }
        }


        public async Task<ApiResponse<List<MessagesResponse>>> GetAllMessages()
        {
            var response = await GetAll();
            return response;
        }

    }
}
