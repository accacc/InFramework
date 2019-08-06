using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace IF.Emarsys.Provider
{
	internal class EmarsysApiHeader
	{
		private static EmarsysApiHeader _instance;

		internal static EmarsysApiHeader GetInstance
		{
			get
			{
				EmarsysApiHeader emarsysApiHeader = EmarsysApiHeader._instance;
				if (emarsysApiHeader == null)
				{
					emarsysApiHeader = new EmarsysApiHeader();
					EmarsysApiHeader._instance = emarsysApiHeader;
				}
				return emarsysApiHeader;
			}
		}

		private EmarsysApiHeader()
		{
		}

		internal string CreateNewHeader(string key, string secret)
		{
			string randomString = this.GetRandomString(32);
			string str = DateTime.UtcNow.ToString("o");
			string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(this.Sha1(String.Concat(randomString, str, secret))));
			return String.Format("Username=\"{0}\", PasswordDigest=\"{1}\", Nonce=\"{2}\", Created=\"{3}\"  Content-type: \"application/json\", charset=\"utf-8\"", new Object[] { key, base64String, randomString, str });
		}

		private string GetRandomString(int length)
		{
			Random random = new Random();
			string[] strArray = new String[] { "0", "2", "3", "4", "5", "6", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(strArray[random.Next((int)strArray.Length)]);
			}
			return stringBuilder.ToString();
		}

		private string Sha1(string input)
		{
			byte[] numArray = (new SHA1CryptoServiceProvider()).ComputeHash(Encoding.UTF8.GetBytes(input));
			string str = String.Join(String.Empty, Array.ConvertAll<byte, string>(numArray, (byte b) => b.ToString("x2")));
			return str;
		}
	}
}