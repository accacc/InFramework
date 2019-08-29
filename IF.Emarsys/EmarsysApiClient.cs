using IF.Emarsys.Helper;
using IF.Emarsys.Model;
using IF.Emarsys.Model.Parameter;
using IF.Emarsys.Provider;
using System;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IF.Emarsys
{
    public class EmarsysApiClient
    {
        private readonly EmarsysApiProvider _manager;

        public EmarsysApiClient(string key, string secret, string url)
        {
            this._manager = new EmarsysApiProvider(key, secret, url);
        }

        public EmarsysApiClient(string key, string secret, string url, int timeout)
        {
            this._manager = new EmarsysApiProvider(key, secret, url, timeout);
        }

        public ApiResponse CreateOrUpdateContact(ContactParameter contact, Encoding encoding = null)
        {
            if (String.IsNullOrEmpty(contact.Email))
            {
                throw new Exception("Contact email is null!");
            }
            Dictionary<string, string> strs = new Dictionary<string, string>()
            {
                { "3", contact.Email }
            };
            if (!String.IsNullOrEmpty(contact.Name))
            {
                strs.Add("1", contact.Name);
            }
            if (!String.IsNullOrEmpty(contact.Surname))
            {
                strs.Add("2", contact.Surname);
            }
            if ((!String.IsNullOrEmpty(contact.Name) || !String.IsNullOrEmpty(contact.Surname) || String.IsNullOrEmpty(contact.Fullname) ? false : contact.Fullname.Trim().Contains(" ")))
            {
                int num = contact.Fullname.Trim().LastIndexOf(' ');
                strs.Add("1", contact.Fullname.Substring(0, num));
                strs.Add("2", contact.Fullname.Substring(num));
            }
            return this._manager.Send<ApiResponse>(RequestType.PUT, "contact/?create_if_not_exists=1", strs, encoding);
        }

        public int GetEventIdFromName(string eventName)
        {
            int num;
            int? nullable;
            int? nullable1;
            if (eventName != null)
            {
                ApiResponseModel apiResponseModel = this._manager.Send<ApiResponseModel>(RequestType.GET, "event");
                if ((apiResponseModel != null ? apiResponseModel.ReplyCode == 0 : false))
                {
                    List<EmarsysDataKeyValue> data = apiResponseModel.Data;
                    if (data != null)
                    {
                        EmarsysDataKeyValue emarsysDataKeyValue = data.Find((EmarsysDataKeyValue c) => c.Name.ToLower().Trim().Equals(eventName.ToLower().Trim()));
                        if (emarsysDataKeyValue != null)
                        {
                            nullable1 = new int?(emarsysDataKeyValue.Id);
                        }
                        else
                        {
                            nullable = null;
                            nullable1 = nullable;
                        }
                    }
                    else
                    {
                        nullable = null;
                        nullable1 = nullable;
                    }
                    int? nullable2 = nullable1;
                    num = (nullable2.HasValue ? nullable2.GetValueOrDefault() : -1);
                }
                else
                {
                    num = -1;
                }
            }
            else
            {
                num = -1;
            }
            return num;
        }

        public ApiResponseModel GetExternalEvents()
        {
            return this._manager.Send<ApiResponseModel>(RequestType.GET, "event");
        }

        public ApiResponse SendEmail(string email, List<string> contacs, string eventName, Dictionary<string, string> dynamicParameterList = null, Encoding encoding = null, List<AttachmentParameter> attachment = null)
        {
            int eventIdFromName = this.GetEventIdFromName(eventName);
            if (eventIdFromName == -1)
            {
                throw new Exception("Event doesn't exists");
            }
            return this.SendEmail(email,contacs, eventIdFromName, dynamicParameterList, encoding, attachment);
        }

        public ApiResponse SendEmail(string email, List<string> contacs, int eventId, Dictionary<string, string> dynamicParameterList = null, Encoding encoding = null, List<AttachmentParameter> attachment = null)
        {
            return this.SendEmail(new ContactParameter()
            {
                
                Email = email
            },contacs ,eventId, dynamicParameterList, encoding, attachment);
        }

        public ApiResponse SendEmail(ContactParameter contact, List<string> contacs, string eventName, Dictionary<string, string> dynamicParameterList = null, Encoding encoding = null, List<AttachmentParameter> attachment = null)
        {
            int eventIdFromName = this.GetEventIdFromName(eventName);
            if (eventIdFromName == -1)
            {
                throw new Exception("Event doesn't exists");
            }
            return this.SendEmail(contact,contacs, eventIdFromName, dynamicParameterList, encoding, attachment);
        }


        public ApiResponse CreateEvent(string Name, Encoding encoding = null)
        {

            Dictionary<string, string> strs = new Dictionary<string, string>();
            strs.Add("name", Name);
            ApiResponse apiResponse2 = this._manager.Send<ApiResponse>(RequestType.POST, "event", Name, encoding);
            return apiResponse2;

        }

        public ApiResponse SendEmail(ContactParameter contact,List<string> contacs ,int eventId, Dictionary<string, string> dynamicParameterList = null, Encoding encoding = null, List<AttachmentParameter> attachment = null)
        {
            ApiResponse apiResponse;
            string email;
            ApiResponse apiResponse1 = this.CreateOrUpdateContact(contact, null);
            if ((apiResponse1 != null ? apiResponse1.ReplyCode != 0 : true))
            {
                apiResponse = apiResponse1;
            }
            else if (eventId > 0)
            {
                EmailDynamicParameter emailDynamicParameter = null;
                if (dynamicParameterList != null)
                {
                    emailDynamicParameter = new EmailDynamicParameter()
                    {
                        Global = dynamicParameterList
                    };
                }
                List<Attachment> attachments = null;
                if (attachment != null)
                {
                    attachments = new List<Attachment>();
                    foreach (AttachmentParameter attachmentParameter in attachment)
                    {
                        attachments.Add(new Attachment()
                        {
                            FileName = String.Format("{0}.{1}", (object)attachmentParameter.FileName, attachmentParameter.FileExtension),
                            Data = attachmentParameter.Data
                        });
                    }
                }
                EmailParameter emailParameter = new EmailParameter()
                {
                    KeyId = "3"
                };
                if (contact != null)
                {
                    email = contact.Email;
                }
                else
                {
                    email = null;
                }
                emailParameter.Contacts = contacs;
                emailParameter.ExternalId = email;
                emailParameter.Data = emailDynamicParameter;
                emailParameter.Contacts = null;
                emailParameter.Attachments = attachments;
                EmailParameter emailParameter1 = emailParameter;
                ApiResponse apiResponse2 = this._manager.Send<ApiResponse>(RequestType.POST, String.Format("event/{0}/trigger", eventId), emailParameter1, encoding);
                apiResponse = apiResponse2;
            }
            else
            {
                apiResponse = null;
            }
            return apiResponse;
        }
    }
}
