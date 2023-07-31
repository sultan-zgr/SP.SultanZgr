

using SP.Base.BaseResponse;
using SP.Schema.Request.Token;
using SP.Schema.Response.Token;

namespace SP.Business.Token;

public interface ITokenService
{
    ApiResponse<TokenResponse> Login(TokenRequest request);
}
