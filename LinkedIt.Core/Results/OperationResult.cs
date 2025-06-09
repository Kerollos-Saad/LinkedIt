using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedIt.Core.Results
{
	public class OperationResult<T>
	{
		public bool IsSuccess { get; set; }
		public T? Data { get; set; }
		public string? ErrorMessage { get; set; }

		public static OperationResult<T> Success(T data) =>
			new OperationResult<T> { IsSuccess = true, Data = data };

		public static OperationResult<T> Failure(string errorMessage) =>
			new OperationResult<T> { IsSuccess = false, ErrorMessage = errorMessage };
	}
}
