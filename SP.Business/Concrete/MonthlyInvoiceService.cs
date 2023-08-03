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
    public class MonthlyInvoiceService : GenericService<MonthlyInvoice, MonthlyInvoiceRequest, MonthlyInvoiceResponse>, IMonthlyInvoiceService
    {
        public MonthlyInvoiceService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

    
    }

   
    }





