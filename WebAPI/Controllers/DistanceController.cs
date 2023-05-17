using DTO.ReqDTO;
using Helper;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using WebAPI.ViewModels.ReqViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly IDistance _idistance;
        public DistanceController(IDistance idistance)
        {
            _idistance = idistance;
        }

        /// <summary>
        ///	Gets distance between 2 different zip codes.
        /// </summary>
        /// <param name="getDistanceByZipCodesReqViewModel"></param>
        /// <returns>200 If success</returns>
        /// <returns>404 If data not found</returns>
        /// <returns>400 If request is not valid</returns>
        /// <returns>500 If internal server error</returns>

        [HttpPost("GetDistanceByZipCodesAsync")]
        public async Task<CommonResponse> GetDistanceByZipCodesAsync(GetDistanceByZipCodesReqViewModel getDistanceByZipCodesReqViewModel)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                commonResponse = await _idistance.GetDistanceByZipCodesAsync(getDistanceByZipCodesReqViewModel.Adapt<GetDistanceByZipCodesReqDTO>());
            }
            catch { throw; }
            return commonResponse;
        }
    }
}
