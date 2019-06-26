//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace IF.Core.Exception
//{
//    public class ErrorCode
//    {
//        public string Message { get; protected set; }
//        public string Code { get; set; }
//        public HttpStatusCode HttpStatusCode { get; }

//        public ErrorCode(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
//        {
//            Message = message;
//            HttpStatusCode = httpStatusCode;
//        }

//        public static ErrorCode EmptyCommand => new ErrorCode(nameof(EmptyCommand), HttpStatusCode.InternalServerError);
//        public static ErrorCode InvalidCommand => new ErrorCode(nameof(InvalidCommand), HttpStatusCode.InternalServerError);
//        public static ErrorCode FaultWhileSavingToDatabase => new ErrorCode(nameof(FaultWhileSavingToDatabase), HttpStatusCode.InternalServerError);
//        public static ErrorCode InvalidPassword => new ErrorCode(nameof(InvalidPassword));
//        public static ErrorCode InvalidUserClaimName => new ErrorCode(nameof(InvalidUserClaimName));

//        public static ErrorCode GenericNotExist<T>()
//            => new ErrorCode($"{nameof(T)}NotExist");
//    }
//}
