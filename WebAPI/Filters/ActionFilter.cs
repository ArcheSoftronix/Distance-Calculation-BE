using Helper;
using Helper.CommonHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace WebAPI.Filters
{
    public class ActionFilter : IActionFilter
	{

		private readonly IConfiguration _configuration;
		private readonly CommonHelper _commonHelper;
		public ActionFilter(IConfiguration configuration, CommonHelper commonHelper)
		{
			_configuration = configuration;
			_commonHelper = commonHelper;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			try
			{
				var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

				if (controllerActionDescriptor != null)
				{
					string JsonData = JsonConvert.SerializeObject(context.ActionArguments.Values);
					JsonData = JsonData.Substring(1, JsonData.Length - 2);
					context.HttpContext.Items["LogRequestBody"] = JsonData;
				}
			}
			catch (Exception ex) { _commonHelper.AddExceptionLog(ex.Message); }
		}

		public async void OnActionExecuted(ActionExecutedContext context)
		{
			try
			{
				bool isLogEnabled = Convert.ToBoolean(_configuration["CommonSwitches:APILogSwitch"].ToString());
				if (isLogEnabled)
				{
					var result = context.Result;
					if (result is ObjectResult json)
					{
						dynamic commonResponse;
						try
						{
							commonResponse = (CommonResponse)json.Value ?? new CommonResponse();
						}
						catch
						{
							commonResponse = (dynamic)json.Value;
						}
						dynamic jsonDataString1 = JsonConvert.SerializeObject(commonResponse, new JsonSerializerSettings
						{
							ContractResolver = new CamelCasePropertyNamesContractResolver()
						});

						if (isLogEnabled)
						{
							var requestBody = context.HttpContext.Items["LogRequestBody"] != null ? Convert.ToString(context.HttpContext.Items["LogRequestBody"]) : "";

							var requestData = context.HttpContext.Request;
							_commonHelper.AddActivityLog(requestData.Path, requestData.Method, jsonDataString1, requestBody);
						}
					}
				}
			}
			catch (Exception ex) { _commonHelper.AddExceptionLog(ex.Message); }
		}
	}
}
