using IF.Emarsys.Helper;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IF.Emarsys.Provider
{
    internal class EmarsysApiProvider
	{
		private readonly string _key;

		private readonly string _secret;

		private readonly string _url;

		private readonly int? _timeout;


		internal EmarsysApiProvider(string key, string secret, string url)
		{
			this._key = key;
			this._secret = secret;
			this._url = url;
		}

		internal EmarsysApiProvider(string key, string secret, string url, int timeout) : this(key, secret, url)
		{
			this._timeout = new int?(timeout);
		}

		private static string GetStreamReadText(WebResponse response)
		{
			string str;
			string end;
			using (Stream responseStream = ((HttpWebResponse)response).GetResponseStream())
			{
				if (responseStream != null)
				{
					end = (new StreamReader(responseStream)).ReadToEnd();
				}
				else
				{
					end = null;
				}
				str = end;
			}
			return str;
		}

		private HttpWebRequest HttpWebRequest<T>(RequestType method, string uri, object parameters, Encoding encoding)
		{
			HttpWebRequest value = (HttpWebRequest)WebRequest.Create(String.Concat(this._url, uri));
			if (this._timeout.HasValue)
			{
				value.Timeout = this._timeout.Value;
			}
			value.Method = method.ToString();
			string str = EmarsysApiHeader.GetInstance.CreateNewHeader(this._key, this._secret);
			value.Headers.Add(String.Format("X-WSSE: UsernameToken {0}", str));
			if ((object)parameters != (object)null)
			{
				string str1 = JsonConvert.SerializeObject(parameters);
				byte[] bytes = Encoding.UTF8.GetBytes(str1);
				if (encoding != null)
				{
					bytes = encoding.GetBytes(str1);
				}
				value.ContentType = "application/json";
				value.ContentLength = (long)((int)bytes.Length);
				using (Stream requestStream = value.GetRequestStream())
				{
					requestStream.Write(bytes, 0, (int)bytes.Length);
				}
			}
			return value;
		}

		internal T Send<T>(RequestType method, string uri)
		{
			return this.Send<T>(method, uri, null);
		}

		internal T Send<T>(RequestType method, string uri, object parameters)
		{
			return this.Send<T>(method, uri, parameters, null);
		}

		internal T Send<T>(RequestType method, string uri, object parameters, Encoding encoding)
		{
			T t;
			try
			{
				HttpWebRequest httpWebRequest = this.HttpWebRequest<T>(method, uri, parameters, encoding);
				WebResponse response = httpWebRequest.GetResponse();
				t = JsonConvert.DeserializeObject<T>(EmarsysApiProvider.GetStreamReadText(response));
			}
			catch (WebException webException1)
			{
				WebException webException = webException1;
				//LoggerExtensions.LogError(this._logger, webException, "Emarsys Api Provider Send WebException", Array.Empty<object>());
				if (webException.Response == null)
				{
					throw;
				}
				string streamReadText = EmarsysApiProvider.GetStreamReadText(webException.Response);
				t = JsonConvert.DeserializeObject<T>(streamReadText);
			}
			return t;
		}

		internal async Task<T> SendAsync<T>(RequestType method, string uri)
		{
			return await this.SendAsync<T>(method, uri, null, null);
		}

		internal async Task<T> SendAsync<T>(RequestType method, string uri, object parameters)
		{
			return await this.SendAsync<T>(method, uri, null, null);
		}

		internal async Task<T> SendAsync<T>(RequestType method, string uri, object parameters, Encoding encoding)
		{
			T t;
			try
			{
				HttpWebRequest httpWebRequest = this.HttpWebRequest<T>(method, uri, parameters, encoding);
				WebResponse responseAsync = await httpWebRequest.GetResponseAsync();
				WebResponse webResponse = responseAsync;
				responseAsync = null;
				t = JsonConvert.DeserializeObject<T>(EmarsysApiProvider.GetStreamReadText(webResponse));
			}
			catch (WebException webException1)
			{
				WebException webException = webException1;
				//LoggerExtensions.LogError(this._logger, webException, "Emarsys Api Provider Send WebException", Array.Empty<object>());
				if (webException.Response == null)
				{
					throw;
				}
				string streamReadText = EmarsysApiProvider.GetStreamReadText(webException.Response);
				t = JsonConvert.DeserializeObject<T>(streamReadText);
			}
			return t;
		}
	}
}