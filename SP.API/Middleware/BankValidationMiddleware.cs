using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Middleware
{
    public class BankValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public BankValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/InvoicePayment/pay"))
            {
                // Gelen istek URL'si /api/pay ile başlıyorsa, burada verilerin kontrolünü yapabiliriz.
                if (!IsBankDataValid(context.Request))
                {
                    // Hatalı veri varsa uygun hata mesajını döndürüyoruz.
                    context.Response.StatusCode = 400; // BadRequest
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync("Banka verileri hatalı. Lütfen girdiğiniz verileri kontrol edin.");
                    return;
                }
            }

            // Sonraki middleware veya controller'a devam etmek için _next nesnesini kullanıyoruz.
            await _next(context);
        }

        private bool IsBankDataValid(HttpRequest request)
        {
            // Burada gelen request içerisindeki banka verilerini kontrol edebilirsiniz.
            // Örneğin, gelen isteğin query parametrelerini kontrol ederek doğrulama yapabilirsiniz.

            var cardNumber = request.Query["cardNumber"].ToString();
            var expiryDate = request.Query["expiryDate"].ToString();
            var cvv = request.Query["cvv"].ToString();


            // Örnek bir kontrol:
            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(expiryDate) || string.IsNullOrEmpty(cvv))
            {
                return false;
            }

            // Diğer kontrolleri buraya ekleyebilirsiniz.

            return true;
        }


        // Bu metod, IApplicationBuilder tarafından kullanılmak üzere bir extension metodudur.
      
    }
}


