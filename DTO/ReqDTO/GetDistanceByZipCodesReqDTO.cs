using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class GetDistanceByZipCodesReqDTO
	{
        public string FromZipCode { get; set; }
        public string ToZipCode { get; set; }
    }
}
