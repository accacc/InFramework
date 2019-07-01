using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF.Core.Log
{
    public class NullLog : ILogService
    {
        public void Error(System.Exception exception, string logger, string message, string UserId, Guid UniqueId,string IpAddress, string Channel)
        {
        }

        public void Error(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            
        }

        public async Task ErrorAsync(System.Exception exception, string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            await Task.FromResult("OK");
        }

        public async Task ErrorAsync(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            await Task.FromResult("OK");
        }

        public void Info(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            
        }

        public async Task InfoAsync(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            await Task.FromResult("OK");
        }

        public void Warn(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            
        }

        public async Task WarnAsync(string logger, string message, string UserId, Guid UniqueId, string IpAddress, string Channel)
        {
            await Task.FromResult("OK");
        }
    }
}
