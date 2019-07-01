using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Log
{

    public interface ILogService
    {


        void Error(System.Exception exception, string logger, string message, string UserId, Guid UniqueId,string IpAdress,string Channel);



        Task ErrorAsync(System.Exception exception, string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel);


        void Error(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel);

        Task ErrorAsync(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel);


        void Warn(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel);

        Task WarnAsync(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel);


        void Info(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel);

        Task InfoAsync(string logger, string message, string UserId, Guid UniqueId, string IpAdress, string Channel);

        Task<string> GetStackTraceAsync(Guid id);

        Task<PagedListResponse<IApplicationErrorLog>> GetPaginatedAsync(DateTime BeginDate, DateTime EndDate, string userId, string Message, string Source, string Channel, int skipNumber = 0, int takeNumber = 50);



    }
}