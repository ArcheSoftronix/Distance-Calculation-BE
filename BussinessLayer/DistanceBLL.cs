using CsvHelper;
using DTO.ReqDTO;
using Helper;
using Helper.CommonHelper;
using Helper.CommonModels;
using System.Globalization;
using System.Net;

namespace BussinessLayer
{
    public class DistanceBLL
    {
        private readonly CommonHelper _commonHelper;
        public DistanceBLL(CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }

        /// <summary>
        ///	Gets distance between 2 different zip codes.
        /// </summary>
        /// <param name="getDistanceByZipCodesReqDTO"></param>
        /// <returns>200 If success</returns>
        /// <returns>404 If data not found</returns>
        /// <returns>400 If request is not valid</returns>
        /// <returns>500 If internal server error</returns>
        public async Task<CommonResponse> GetDistanceByZipCodesAsync(GetDistanceByZipCodesReqDTO getDistanceByZipCodesReqDTO)
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(getDistanceByZipCodesReqDTO.FromZipCode) && !string.IsNullOrWhiteSpace(getDistanceByZipCodesReqDTO.ToZipCode))
                {
                    List<CSVDataModel> CSVDataList;
                    var CSVDataListRes = await GetCSVDataListAsync(); // Reads CSV file data and gets into list.
                    double FromLat = 0;
                    double FromLng = 0;
                    double ToLat = 0;
                    double ToLng = 0;
                    CSVDataList = CSVDataListRes.Status ? CSVDataListRes.Data : new List<CSVDataModel>();
                    if (CSVDataList.Count > 0)
                    {
                        var FromZipCodeDetails = CSVDataList.FirstOrDefault(x => x.ZIP == getDistanceByZipCodesReqDTO.FromZipCode); // Gets "From"(Source) Zip code details.
                        var ToZipCodeDetails = CSVDataList.FirstOrDefault(x => x.ZIP == getDistanceByZipCodesReqDTO.ToZipCode); // Gets "To"(Destination) Zip code details.
                        if (FromZipCodeDetails != null && ToZipCodeDetails != null)
                        {
                            FromLat = FromZipCodeDetails.LAT;
                            FromLng = FromZipCodeDetails.LNG;
                            ToLat = ToZipCodeDetails.LAT;
                            ToLng = ToZipCodeDetails.LNG;

                            double distance = CalculateDistance(FromLat, ToLat, FromLng, ToLng);
                            string message = $"The distance between {FromZipCodeDetails.CITY} ({getDistanceByZipCodesReqDTO.FromZipCode}) to {ToZipCodeDetails.CITY} ({getDistanceByZipCodesReqDTO.ToZipCode}) is {distance} miles."; // Call to CalculateDistance function with paramters.
                            commonResponse.Data = distance;
                            commonResponse.Message = message;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Status = true;
                        }
                        else
                        {
                            commonResponse.StatusCode = HttpStatusCode.NotFound;
                            commonResponse.Message = CommonConstant.Zip_Code_Is_Not_Exist_In_CSV_File_Data;
                        }
                    }
                    else
                    {
                        commonResponse = CSVDataListRes;
                    }
                }
                else
                {
                    commonResponse.StatusCode = HttpStatusCode.BadRequest;
                    commonResponse.Message = CommonConstant.Please_Enter_Valid_Zip_Codes;
                }
            }
            catch { throw; }
            return commonResponse;
        }


        #region Private functions

        /// <summary>
        /// Calculates distance by passed source lat and long to destination lat long
        /// </summary>
        /// <param name="FromLat"></param>
        /// <param name="ToLat"></param>
        /// <param name="FromLng"></param>
        /// <param name="ToLng"></param>
        /// <returns>Distance in Miles</returns>
        private double CalculateDistance(double FromLat, double ToLat, double FromLng, double ToLng)
        {
            // The math module contains a function named toRadians which converts from degrees to radians.
            FromLng = ToRadians(FromLng);
            ToLng = ToRadians(ToLng);
            FromLat = ToRadians(FromLat);
            ToLat = ToRadians(ToLat);

            // Haversine formula
            double dlon = ToLng - FromLng;
            double dlat = ToLat - FromLat;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(FromLat) * Math.Cos(ToLat) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in kilometers. Use 3956 for miles
            //double r = 6371;
            double r = 3956;

            // calculate the result
            return Math.Round((c * r), 4);
        }

        /// <summary>
        /// Get the value to Radians by angle in 10th of a degree
        /// </summary>
        /// <param name="angleIn10thofaDegree"></param>
        /// <returns>value to Radians</returns>
        private double ToRadians(double angleIn10thofaDegree)
        {
            // Angle in 10th of a degree
            return angleIn10thofaDegree * Math.PI / 180;
        }

        /// <summary>
        /// Reads CSV file data from given static file path
        /// </summary>
        /// <returns>Data in list format from CSV file</returns>
        private async Task<CommonResponse> GetCSVDataListAsync()
        {
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                List<CSVDataModel> CSVDataList = new List<CSVDataModel>();

                string filePath = Path.Combine(_commonHelper.GetRelativePath(), "wwwroot", "Files", "ZipCodes", "ZipCodes.csv");
                using (var reader = new StreamReader(filePath))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        await Task.Run(() => CSVDataList = csv.GetRecords<CSVDataModel>().ToList());

                        if (CSVDataList.Count > 0)
                        {
                            commonResponse.Data = CSVDataList;
                            commonResponse.Message = CommonConstant.CSV_Data_Fetched_Successfully;
                            commonResponse.StatusCode = HttpStatusCode.OK;
                            commonResponse.Status = true;
                        }
                        else
                        {
                            commonResponse.StatusCode = HttpStatusCode.NotFound;
                        }
                    }
                }
            }
            catch { throw; }
            return commonResponse;
        }

        #endregion
    }
}
