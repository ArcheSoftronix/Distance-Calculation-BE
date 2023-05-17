using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Helper.CommonHelper
{
    public class CommonHelper
    {
        public const string DATE_FORMAT = "dd/MM/yyyy";
        private IConfiguration _configuration { get; }
        private IHostingEnvironment _hostingEnvironment { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommonHelper(IConfiguration configuration, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Generates .log file and writes logs like Exception or Activity logs
        /// </summary>
        /// <param name="text"></param>
        /// <param name="logType"></param>
        public void AddLog(string text, string? logType = null)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                logType = logType != null ? logType : "";
                string logFileName = GetCurrentDateTime().ToString("dd/MM/yyyy").Replace('/', '_').ToString() + ".log";
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Logs", logType);
                var exists = Directory.Exists(filePath);
                if (!exists)
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, logFileName);
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    text = GetCurrentDateTime().ToString() + " : " + text + "\n";
                    writer.WriteLine(text);
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Gets relative path from current hosted evironment.
        /// </summary>
        /// <returns>Relative path in form of string</returns>
        public string GetRelativePath()
        {
            return Convert.ToString(_hostingEnvironment.ContentRootPath);
        }

        /// <summary>
        /// Gets current datetime for whole project
        /// In case if need to use Centralized or standard time then change this function code only.
        /// </summary>
        /// <returns>DateTime object with current time</returns>
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Generates the log specific string message and write the log in .log file
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="methodType"></param>
        /// <param name="request"></param>
        /// <param name="requestResult"></param>
        /// <returns></returns>
        public async Task AddActivityLog(string apiUrl, string methodType, string request, string requestResult)
        {
            try
            {
                bool APILogSwitch = Convert.ToBoolean(_configuration["CommonSwitches:APILogSwitch"].ToString());
                if (APILogSwitch)
                {
                    string logText = apiUrl + " (" + methodType + ") - Request : ( " + requestResult + " ) - Response : ( " + request + " ).";
                    AddLog(logText, CommonConstant.ActivityLog);
                }
            }
            catch { throw; }
        }

        /// <summary>
        /// Adds log in Exception log file with log specific string
        /// </summary>
        /// <param name="exceptionText"></param>
        public void AddExceptionLog(string exceptionText)
        {
            try
            {
                bool ExceptionLogSwitch = Convert.ToBoolean(_configuration["CommonSwitches:ExceptionLogSwitch"].ToString());
                if (ExceptionLogSwitch)
                {
                    AddLog(exceptionText, CommonConstant.ExceptionLog);
                }
            }
            catch { throw; }
        }

    }
}
