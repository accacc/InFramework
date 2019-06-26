using IF.Web.Mvc.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IF.Web.Mvc.Extensions
{
    public static class RazorPageExtensions
    {
        //public RedirectResult AjaxRedirect(string url)
        //{
        //    return new AjaxAwareRedirectResult(url);
        //}

        public static EmptyResult OperationResult(this PageModel page,OperationType operationType = OperationType.Success)
        {
            string message = "Unknown";

            switch (operationType)
            {
                case OperationType.Insert:
                    message = "Kayit basari ile eklendi.";
                    break;
                case OperationType.Update:
                    message = "Kayit basari ile guncellendi.";
                    break;
                case OperationType.Delete:
                    message = "Kayit basari ile silindi";
                    break;
                case OperationType.Success:
                    message = "Islem Basarili";
                    break;
                default:
                    message = "Islem Basarili";
                    break;
            }


            ShowMessage(page,MessageType.Success, message);

            return new EmptyResult();
        }


        public static bool IsAjaxRequest(this PageModel page)
        {
            if (page.Request == null)
                throw new ArgumentNullException("request");

            if (page.Request.Headers != null)
                return page.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }



        public static void ShowMessage(this PageModel page,MessageType messageType, string message, bool showAfterRedirect = false)
        {


            message = EncodeNonAsciiCharacters(message);

            var messageTypeKey = messageType.ToString();

            if (showAfterRedirect)
            {
                page.TempData[messageTypeKey] = message;
            }
            else
            {
                page.ViewData[messageTypeKey] = message;
            }
        }


        static string EncodeNonAsciiCharacters(string value)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in value)
            {
                if (c > 127)
                {
                    // This character is too big for ASCII
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
