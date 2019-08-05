using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Data
{
    public static class BaseResponseContractExtensions
    {
        public static JsonDataResult ToJsonResult(this BaseResponseContract baseResponseContract, object data)
        {
            JsonDataResult jsonDataResult = new JsonDataResult()
            {
                ErrorCode = baseResponseContract.ErrorCode,
                ErrorDetail = baseResponseContract.ErrorDetail,
                ErrorMessage = baseResponseContract.ErrorMessage,
                IsRecordEmpty = baseResponseContract.IsRecordEmpty,
                IsSuccess = baseResponseContract.IsSuccess,
                ExceptionType = baseResponseContract.ExceptionType,
                SuccessMessage = baseResponseContract.SuccessMessage,
                Messages = baseResponseContract.Messages,
                SystemTime = baseResponseContract.SystemTime,
                Data = data
            };
            
            return jsonDataResult;

        }
    }
}
