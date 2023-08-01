using SP.Business.GenericService;
using SP.Entity;
using SP.Entity.Models;
using SP.Schema.Request;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.Abstract
{
    public interface IApartmentService : IGenericService<Apartment, ApartmentRequest, ApartmentResponse>
    {
    }
}
