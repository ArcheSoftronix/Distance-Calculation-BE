using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IDistance
    {
        /// <summary>
        /// Defines contract for calculating distance between 2 zip codes.
        /// </summary>
        /// <param name="getDistanceByZipCodesReqDTO"></param>
        /// <returns>CommonResponse model</returns>
        public Task<CommonResponse> GetDistanceByZipCodesAsync(GetDistanceByZipCodesReqDTO getDistanceByZipCodesReqDTO);
    }
}
