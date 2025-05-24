using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.Response
{
	public class APIResponse
	{
		public HttpStatusCode StatusCode { get; set; }
		public bool IsSuccess { get; set; } = true;
		public List<string> ErrorMessages { get; set; }
		public object Result { get; set; }

		public void SetResponseInfo(HttpStatusCode statusCode, List<string> errorMessages, object result,
			bool isSuccess = true)
		{
			StatusCode = statusCode;
			IsSuccess = isSuccess;
			ErrorMessages = errorMessages ?? new List<string>();
			Result = result;
		}

		public static APIResponse Success(object? result = null, HttpStatusCode code = HttpStatusCode.OK) =>
			new APIResponse { StatusCode = code, IsSuccess = true, Result = result };

		public static APIResponse Fail(List<string> errors, HttpStatusCode code = HttpStatusCode.BadRequest) =>
			new APIResponse { StatusCode = code, IsSuccess = false, ErrorMessages = errors };

	}
}
