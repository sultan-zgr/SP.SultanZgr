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

namespace SP.Business.Concrete
{
    public class AdminService : GenericService<Admin, AdminRequest, AdminResponse>, IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse> AddApartment(ApartmentRequest request)
        {
            try
            {
                // Yeni bir User nesnesi oluşturun ve ApartmentRequest'ten gelen verilerle doldurun
                var newUser = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    TCNo = request.TCNo,
                    VehiclePlateNumber = request.VehiclePlateNumber
                    // Diğer kullanıcı özelliklerini buraya ekleyin...
                };

                // Oluşturulan yeni User nesnesini veritabanına ekleyin
                await _unitOfWork.DynamicRepo<User>().InsertAsync(newUser);
                await _unitOfWork.SaveChangesAsync();

                // Yeni bir Apartment nesnesi oluşturun ve ApartmentRequest'ten gelen verilerle doldurun
                var newApartment = new Apartment
                {
                    User = newUser, // Oluşturulan User nesnesini Apartment nesnesine atayın
                    IsOccupied = request.IsOccupied,
                    IsOwner = request.IsOwner,
                    Type = request.Type,
                    BlockName = request.BlockName,
                    FloorNumber = request.FloorNumber,
                    ApartmentNumber = request.ApartmentNumber
                    // Diğer daire özelliklerini buraya ekleyin...
                };

                // Oluşturulan yeni Apartment nesnesini veritabanına ekleyin
                await _unitOfWork.DynamicRepo<Apartment>().InsertAsync(newApartment);
                await _unitOfWork.SaveChangesAsync();

                var response = new ApiResponse
                {
                    Success = true,
                    Message = "Daire başarıyla eklendi.",
                };

                return response;
            }
            catch (Exception ex)
            {
                var response = new ApiResponse
                {
                    Success = false,
                    Message = "Daire eklenirken bir hata oluştu: " + ex.Message
                };

                return response;
            }
        }

    }

}





