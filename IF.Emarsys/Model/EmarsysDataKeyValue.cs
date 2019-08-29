using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace IF.Emarsys.Model
{
	[Serializable]
	public class EmarsysDataKeyValue
	{
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public EmarsysDataKeyValue()
		{
		}
	}


    

   

    public class Data
    {

        [JsonProperty("global")]
        public Dictionary<string, string> Global
        {
            get;
            set;
        }
    }

   

    public class Contact
    {
        public string external_id { get; set; }
        public Data data { get; set; }
        //public Attachment attachment { get; set; }
    }

    public class RootObject
    {
        public int key_id { get; set; }
        public List<Contact> contacts { get; set; }
    }
}