using DTO.ReqDTO;
using Helper;

namespace ServiceLayer.Interface
{
    public interface IDistance
    {
        public Task<CommonResponse> GetDistanceByZipCodesAsync(GetDistanceByZipCodesReqDTO getDistanceByZipCodesReqDTO);
    }
}
