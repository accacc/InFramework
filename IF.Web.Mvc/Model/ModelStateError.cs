//using System.Linq;
//using System.Web.Mvc;

//namespace IF.Web.Mvc.Model
//{
//    public static class ModelStateError
//    {
//        public static string ModelStateErrors(this ModelStateDictionary modelState)
//        {
//            int i = 0;
//            string errorMessage = string.Empty;
//            var errors = modelState.Where(a => a.Value.Errors.Count > 0).Select(b => new { b.Key, b.Value.Errors }).ToArray();
//            foreach (var modelStateErrors in errors)
//            {
//                foreach (var modelStateError in modelStateErrors.Errors)
//                {
//                    if (i == 0)
//                    {
//                        if (!string.IsNullOrEmpty(modelStateError.ErrorMessage))
//                            errorMessage += modelStateError.ErrorMessage;
//                        else
//                            errorMessage = modelStateError.Exception.Message;
//                    }
//                    else
//                    {
//                        if (!string.IsNullOrEmpty(modelStateError.ErrorMessage))
//                            errorMessage += string.Format("{0} {1}", "<br />", modelStateError.ErrorMessage);
//                        else
//                            errorMessage += string.Format("{0} {1}", "<br />", modelStateError.Exception.Message);
//                    }
//                    i++;
//                }
//            }
//            return errorMessage;
//        }
//    }
//}
