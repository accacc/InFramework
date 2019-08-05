using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Persistence
{
    public interface IJsonDataContext
    {
        List<T> GetList<T>() where T : class, new();

        void WriteList<T>(List<T> list) where T:class,new();

        T Get<T>(int Id) where T : BaseDto, new();

        void Update<T>(T dto) where T : BaseDto, new();

        void Add<T>(T dto) where T : BaseDto, new();


        void Delete<T>(int Id) where T : BaseDto, new();



    }
}
