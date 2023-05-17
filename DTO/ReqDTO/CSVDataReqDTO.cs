using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ReqDTO
{
	public class CSVDataReqDTO
	{
		[Index(0)]
		public string ZIP { get; set; }
		[Index(1)]
		public double LAT { get; set; }
		[Index(2)]
		public double LNG { get; set; }
		[Index(3)]
		public string CITY { get; set; }
	}
}
