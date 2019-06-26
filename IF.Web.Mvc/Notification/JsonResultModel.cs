using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IF.Web.Mvc.Notification
{
    public class JsonResultModel
    {
        public JsonResultModel()
        {
            this.Success = true;
            Messages = new string[0];
            FieldErrors = new FieldError[0];
            this.IsRedirecToLogout = false;
        }

        public JsonResultModel(ModelStateDictionary modelState)
            : this()
        {
            this.AddModelState(modelState);
        }

        public JsonResultModel SetFailed()
        {
            this.Success = false;
            return this;
        }

        public JsonResultModel SetSuccess()
        {
            this.Success = true;
            return this;
        }

        public bool IsRedirecToLogout { get; set; }

        public JsonResultModel AddModelState(ModelStateDictionary modelState)
        {
            foreach (var ms in modelState)
            {
                foreach (var err in ms.Value.Errors)
                {
                    this.AddMessage(err.ErrorMessage);
                }
            }

            return this;
        }

        public bool Success { get; set; }

        public string[] Messages { get; set; }

        public object Model { get; set; }
        public string TemplateViewName { get; set; }

        public string TemplateMasterViewName { get; set; }

        public FieldError[] FieldErrors { get; set; }

        public JsonResultModel AddFieldError(string fieldName, string message)
        {
            Success = false;
            FieldErrors = FieldErrors.Concat(new[] { new FieldError() { FieldName = fieldName, ErrorMessage = message } }).ToArray();
            return this;
        }

        public JsonResultModel AddMessage(string message)
        {
            Messages = Messages.Concat(new[] { message }).ToArray();
            return this;
        }

        public JsonResultModel AddException(System.Exception e)
        {
            this.Success = false;
            return AddMessage(e.Message);
        }
    }
}
