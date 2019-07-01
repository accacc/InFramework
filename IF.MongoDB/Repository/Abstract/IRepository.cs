using IF.Core.Log;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.MongoDB.Repository.Abstract
{
    public interface IRepository
    {
        IMongoCollection<T> GetQuery<T>();

        Task<IEnumerable<T>> GetAllLogsAsync<T>();

        Task<T> GetLogAsync<T>(Guid id) where T : IIFSystemTable;


        Task AddLogAsync<T>(T item);

        void AddLog<T>(T item);


    }
}
