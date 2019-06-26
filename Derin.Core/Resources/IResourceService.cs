using Derin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Resources
{
    public interface IResourceService: IBaseService
    {
        string GetResource(string key);
        string GetValidation(string key);
    }
}
