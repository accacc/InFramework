using IF.Core.Handler;
using System.Collections.Generic;

namespace IF.Core.Security
{
    public class PermissionListRequest : BaseRequest
    {
    }

    public class PermissionListResponse : BaseResponse
    {
        public IList<PermissionDto> Permissions { get; set; }
    }

    public class PermissionMapListRequest : BaseRequest
    {
    }

    public class PermissionMapListResponse : BaseResponse
    {
        public IList<PermissionMapDto> PermissionMaps { get; set; }
    }

    public class PermissionAddCommand : BaseCommand
    {
        public PermissionDto Data { get; set; }
    }

    public class PermissionMapDeleteCommand : BaseCommand
    {



    }



    public class PermissionMapAddCommand : BaseCommand
    {


        public PermissionMapDto PermissionMapDto { get; set; }


    }

    public class PermissionMapGetRequest : BaseRequest
    {

    }

    public class PermissionMapGetResponse : BaseResponse
    {


        public PermissionMapDto PermissionMap { get; set; }


    }

    public class PermissionGetRequest : BaseRequest
    {
    }

    public class PermissionGetResponse : BaseResponse
    {


        public PermissionDto Permission { get; set; }


    }

    public class PermissionMapUpdateCommand : BaseCommand
    {
        public PermissionMapDto PermissionMapDto { get; set; }

    }

    public class PermissionUpdateCommand : BaseCommand
    {
        public PermissionDto PermissionDto { get; set; }

    }
}
