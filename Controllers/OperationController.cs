using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Domains;
using MyFinances.WebApi.Models.Converters;
using MyFinances.WebApi.Models.Dtos;
using MyFinances.WebApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public OperationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DataResponse<IEnumerable<OperationDto>> Get()
        {
            var responce = new DataResponse<IEnumerable<OperationDto>>();

            try
            {
                responce.Data = _unitOfWork.Operation.Get().ToDtos();
            }
            catch (Exception ex)
            {
                // logowanie do pliku ...
                responce.Errors.Add( new Error(ex.Source,ex.Message));
            }
            
            return responce;
        }

        /// <summary>
        /// Get Operation by Id
        /// </summary>
        /// <param name="id"> Operation Id</param>
        /// <returns>Single operation</returns>
        [HttpGet("{id}")]
        public DataResponse<OperationDto> Get(int id)
        {
            var responce = new DataResponse<OperationDto>();

            try
            {
                responce.Data = _unitOfWork.Operation.Get(id)?.ToDto();
            }
            catch (Exception ex)
            {
                // logowanie do pliku ...
                responce.Errors.Add(new Error(ex.Source, ex.Message));
            }
            return responce;
        }


        [HttpPost]
        public DataResponse<int> Add(OperationDto operationDto  )
        {
            var responce = new DataResponse<int>();

            try
            {
                var operation = operationDto.ToDao();
                _unitOfWork.Operation.Add(operation);
                _unitOfWork.Complete();
                // tutaj jest już znane id dodanego rekordu
                responce.Data = operation.Id;
            }
            catch (Exception ex)
            {
                //todo do wykonania
                // logowanie do pliku ...
                responce.Errors.Add(new Error(ex.Source, ex.Message));
            }
            return responce;
        }


        [HttpPut]
        public Response Update(OperationDto operation)
        {
            var responce = new Response();

            try
            {
                _unitOfWork.Operation.Update(operation.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // logowanie do pliku ...
                responce.Errors.Add(new Error(ex.Source, ex.Message));
            }
            return responce;
        }

        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var responce = new Response();

            try
            {
                _unitOfWork.Operation.Delete(id);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                // logowanie do pliku ...
                responce.Errors.Add(new Error(ex.Source, ex.Message));
            }
            return responce;
        }

    }
}
