using SP.Base.BaseResponse;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Token
{
    public interface ITokenService
    {
        public ApiResponse<TokenResponse> CreateToken(TokenRequest tokenRequest);
    }
}
