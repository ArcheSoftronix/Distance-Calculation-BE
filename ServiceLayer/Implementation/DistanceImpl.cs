using BussinessLayer;
using DTO.ReqDTO;
using Helper;
using ServiceLayer.Interface;

namespace ServiceLayer.Implementation
{
    public class DistanceImpl : IDistance
    {
        private readonly DistanceBLL _distanceBLL;

        public DistanceImpl(DistanceBLL distanceBLL)
        {
            _distanceBLL = distanceBLL;
        }

        public async Task<CommonResponse> GetDistanceByZipCodesAsync(GetDistanceByZipCodesReqDTO getDistanceByZipCodesReqDTO) => await _distanceBLL.GetDistanceByZipCodesAsync(getDistanceByZipCodesReqDTO);
    }
}
