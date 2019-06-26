using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace IF.Core.Json
{
    public class JsonDataContext //: IJsonDataContext
    {

        public JsonDataContext()
        {

        }

        private readonly IJsonSerializer  jsonSerializer;
        public JsonDataContext(IJsonSerializer frameworkFactory)
        {
            this.jsonSerializer = frameworkFactory;
        }


        public List<T> GetList<T>(string path)
            where T : BaseJsonDto, new()

        {
            //Type type = typeof(T);

            //string name = type.Name;
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
                
            }

            List<T> @object = jsonSerializer.Deserialize<List<T>>(System.IO.File.ReadAllText(path));

            if(@object ==null)
            {
                return new List<T>();
            }

            return @object;
            

        }

        public T Get<T>(Guid Id, string path) where T : BaseJsonDto, new()

        {
            Type type = typeof(T);

            string name = type.Name;

            List<T> list = jsonSerializer.Deserialize<List<T>>(System.IO.File.ReadAllText(path));

            var item = list.SingleOrDefault(p => p.UniqueId == Id);

            return item;

        }

        public void WriteList<T>(List<T> list,string path) where T : BaseJsonDto, new()
        {
            Type type = typeof(T);

            string name = type.Name;

            System.IO.File.WriteAllText(path, jsonSerializer.Serialize(list));
        }

        public void Update<T>(T dto,string path) where T : BaseJsonDto, new()
        {
            var list = this.GetList<T>(path);

            var item = list.Where(l => l.UniqueId == dto.UniqueId).SingleOrDefault();


            if (item != null)
            {
                int index = list.IndexOf(item);
                list[index] = dto;
            }

            this.WriteList(list,path);
        }


        public void Add<T>(T dto,string path) where T : BaseJsonDto, new()
        {
            var list = this.GetList<T>(path);            
            
            list.Add(dto);
            this.WriteList(list,path);
        }

        public void Delete<T>(Guid Id,string path) where T : BaseJsonDto, new()
        {
            var list = this.GetList<T>(path);

            var item = list.SingleOrDefault(l => l.UniqueId == Id);

            if (item != null)
            {
                list.Remove(item);
            }

            this.WriteList(list,path);
        }
    }
}
