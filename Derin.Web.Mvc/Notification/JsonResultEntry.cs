﻿using System.Linq;
using System.Web.Mvc;

namespace Derin.Core.Mvc
{
    public class FieldError
    {
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class JsonResultEntry
    {
        public JsonResultEntry()
        {
            this.Success = true;
            Messages = new string[0];
            FieldErrors = new FieldError[0];
            this.IsRedirecToLogout = false;
        }

        public JsonResultEntry(ModelStateDictionary modelState)
            : this()
        {
            this.AddModelState(modelState);
        }

        public JsonResultEntry SetFailed()
        {
            this.Success = false;
            return this;
        }

        public JsonResultEntry SetSuccess()
        {
            this.Success = true;
            return this;
        }

        public bool IsRedirecToLogout { get; set; }

        public JsonResultEntry AddModelState(ModelStateDictionary modelState)
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

        public JsonResultEntry AddFieldError(string fieldName, string message)
        {
            Success = false;
            FieldErrors = FieldErrors.Concat(new[] { new FieldError() { FieldName = fieldName, ErrorMessage = message } }).ToArray();
            return this;
        }

        public JsonResultEntry AddMessage(string message)
        {
            Messages = Messages.Concat(new[] { message }).ToArray();
            return this;
        }

        public JsonResultEntry AddException(System.Exception e)
        {
            this.Success = false;
            return AddMessage(e.Message);
        }
    }
}
