using Helper;
using Helper.CommonHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly CommonHelper _commonHelper;


        public ExceptionFilter(CommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }

        public void OnException(ExceptionContext context)
        {
            try
            {
                CommonResponse commonReponse = new CommonResponse();
                context.Result = new JsonResult(commonReponse);

                var item = context.Exception;
                _commonHelper.AddExceptionLog(item.ToString());
            }
            catch (Exception ex)
            {
                _commonHelper.AddExceptionLog(ex.ToString());
            }
        }
    }
}
