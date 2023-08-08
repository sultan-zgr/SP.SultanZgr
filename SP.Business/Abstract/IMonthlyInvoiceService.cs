using SP.Base.BaseResponse;
using SP.Base.Enums.Months;
using SP.Business.GenericService;
using SP.Entity;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Abstract
{
    public interface IMonthlyInvoiceService : IGenericService<MonthlyInvoice, MonthlyInvoiceRequest, MonthlyInvoiceResponse>
    {
        Task<ApiResponse<MonthlyInvoiceResponse>> AddMonthlyInvoiceByMonth(MonthlyInvoiceRequest monthlyInvoiceRequest, Months month);
        Task<ApiResponse<MonthlyInvoiceResponse>> UpdateMonthlyInvoiceByMonth(int invoiceId, MonthlyInvoiceRequest monthlyInvoiceRequest, Months month);

    }
}

