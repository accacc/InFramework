using System;
using System.Collections.Generic;
using System.Text;

namespace IF.CodeGeneration.Application.Generator
{
    public enum VSFileType
    {
        AddContractClass,
        AddRepositoryClass,
        ApiAddControllerMethod,
        AddControllerMethod,
        AddDataHandler,
        CommandHandler,
        AddFormView,
        AddMvcModels,

        UpdateContractClass,
        UpdateControllerMethod,
        ApiUpdateControllerMethod,
        UpdateDataHandler,
        //UpdateHandler,
        UpdateFormView,
        UpdateMvcModels,
        UpdateRepositoryClass,


        GetContractClass,
        GetControllerMethod,
        ApiGetControllerMethod,
        GetDataHandler,
        GetHandler,
        GetFormView,
        GetMvcModels,
        GetRepositorytMethods,


        ListGridview,
        ListContracts,
        ListDataHandler,
        ListMvcModel,
        ListHandler,
        ListMvcControllerMethods,
        ApiListControllerMethods,
        ListRepositorytMethods,
        ListIndexView




    }
}
