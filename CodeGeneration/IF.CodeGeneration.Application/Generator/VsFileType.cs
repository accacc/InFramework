using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator
{
    public enum VSFileType
    {
        AddContractClass,
        ApiAddControllerMethod,
        AddControllerMethod,
        AddDataHandler,
        AddHandler,
        AddFormView,
        AddMvcModels,

        UpdateContractClass,
        UpdateControllerMethod,
        ApiUpdateControllerMethod,
        UpdateDataHandler,
        UpdateHandler,
        UpdateFormView,
        UpdateMvcModels,

       

        GetContractClass,
        GetControllerMethod,
        ApiGetControllerMethod,
        GetDataHandler,
        GetHandler,
        GetFormView,
        GetMvcModels,

        ListGridview,
        ListContracts,
        ListDataHandler,
        ListMvcModel,
        ListHandler,
        ListMvcControllerMethods,
        ApiListControllerMethods,
        ListIndexView




    }
}
