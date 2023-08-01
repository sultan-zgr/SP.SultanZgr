using AutoMapper;
using Serilog;
using SP.Base.BaseResponse;
using SP.Business.Abstract;
using SP.Business.GenericService;
using SP.Data.UnitOfWork;
using SP.Data;
using SP.Entity.Models;
using SP.Entity;
using SP.Schema.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Schema.Response;

namespace SP.Business.Concrete
{
    public class ApartmentService : GenericService<Apartment, ApartmentRequest, ApartmentResponse>, IApartmentService
    {
        public ApartmentService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }
     
    }

}

