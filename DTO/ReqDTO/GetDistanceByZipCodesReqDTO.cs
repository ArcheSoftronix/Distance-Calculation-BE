namespace DTO.ReqDTO
{
    /// <summary>
    /// Used for travel request parameter from controller to BLL
    /// </summary>
    public class GetDistanceByZipCodesReqDTO
    {
        //Gets and Sets property of FromZipCode
        public string FromZipCode { get; set; }

        // Gets and Sets property of ToZipCode
        public string ToZipCode { get; set; }
    }
}
