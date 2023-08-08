using SP.Base.BaseResponse;
using SP.Business.GenericService;
using SP.Entity;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Abstract
{
    public interface IMessageService : IGenericService<Messages, MessagesRequest, MessagesResponse>
    {
        Task<ApiResponse<MessagesResponse>> UserSendMessageAsync(MessagesRequest request);
        Task<ApiResponse<List<MessagesResponse>>> GetAllMessages();
        Task UpdateMessageStatusAsync(Messages message);
    }

}
