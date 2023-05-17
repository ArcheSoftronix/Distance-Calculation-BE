using CsvHelper.Configuration.Attributes;

namespace Helper.CommonModels
{
    /// <summary>
    /// Used to read the CSV file data as per indexes
    /// </summary>
    public class CSVDataModel
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
