using AutoMapper;
using Serilog;
using SP.Base.BaseResponse;
using SP.Data;
using SP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.GenericService
{
    public class GenericService<T, TRequest, TResponse> : IGenericService<T, TRequest, TResponse> where T : class
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GenericService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<TResponse>> GetById(int id, params string[] includes)
        {
            try
            {
                var entity = await unitOfWork.DynamicRepo<T>().GetByIdWithIncludeAsync(id, includes);
                var mapped = mapper.Map<T, TResponse>(entity);
                return new ApiResponse<TResponse>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GenericService.GetById");
                return new ApiResponse<TResponse>(ex.Message);
            }
        }
        public async Task<ApiResponse<List<TResponse>>> GetAll(params string[] includes)
        {
            try
            {
                var entity = await unitOfWork.DynamicRepo<T>().GetAllWithIncludeAsync(includes);
                var mapped = mapper.Map<List<T>, List<TResponse>>(entity);
                return new ApiResponse<List<TResponse>>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GenericService.GetAll");
                return new ApiResponse<List<TResponse>>(ex.Message);
            }
        }

        public async Task<ApiResponse> Insert(TRequest request)
        {
            try
            {
                var entity = mapper.Map<TRequest, T>(request);

                if (entity is User UserEntity)
                {
                    UserEntity.Role = "User";
                }

                await unitOfWork.DynamicRepo<T>().InsertAsync(entity);
                await unitOfWork.SaveChangesAsync();

                return new ApiResponse();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GenericService.Insert");
                return new ApiResponse(ex.Message);
            }
        }




        public async Task<ApiResponse> Update(int Id, TRequest request)
        {
            try
            {
                var exist = await unitOfWork.DynamicRepo<T>().GetByIdAsync(Id);
                if (exist == null)
                {
                    return new ApiResponse("Record not found!.");
                }

                var entity = mapper.Map<TRequest, T>(request);
                await unitOfWork.DynamicRepo<T>().UpdateAsync(entity);
                await unitOfWork.SaveChangesAsync();                             
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                // Log.Error(ex, "GenericService.GetAll");
                return new ApiResponse(ex.Message);

            }
        }
        public async Task<ApiResponse> Delete(int Id)
        {
            try
            {
                var entity = await unitOfWork.DynamicRepo<T>().GetByIdAsync(Id);
                if (entity == null)
                {
                    return new ApiResponse("Record not found!");
                }
                await unitOfWork.DynamicRepo<T>().DeleteAsync(Id);
                await unitOfWork.SaveChangesAsync();                           
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "GenericService.Delete");
                return new ApiResponse(ex.Message);
            }
        }

        
    }
}
