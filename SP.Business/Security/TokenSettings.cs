using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business
{
    public interface ITokenSettings
    {
        string SecurityKey { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        string ProviderKey { get; set; }
        int AccessTokenExpiration { get; set; }
        int RefreshTokenExpiration { get; set; }
    }

    public class TokenSettings : ITokenSettings
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ProviderKey { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}
