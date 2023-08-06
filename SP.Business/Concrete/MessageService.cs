using AutoMapper;
using SP.Base.BaseResponse;
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

                // Create a new message object
                var message = new Messages
                {
                    SenderId = senderUser.UserId,
                    ReceiverId = receiverUser.UserId,  //Alıcı Admin id = 1 
                    Content = request.Content,
                    IsRead = false,
                    Date = DateTime.Now
                };

                // Add the message to the sender's sending messages list
                if (senderUser.MessagesSending == null)
                {
                    senderUser.MessagesSending = new List<Messages>();
                }
                senderUser.MessagesSending.Add(message);
                // Add the message to the receiver's receiving messages list


                // Save changes to the database
                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<MessagesResponse>("Your message has been delivered..");
            }

            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return new ApiResponse<MessagesResponse>("ERROR: " + ex.Message);
            }

        }
      

   

      
    }
}
