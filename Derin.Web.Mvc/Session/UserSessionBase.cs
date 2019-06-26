using Derin.Core.Mvc.Filters;
using IF.Core.Security;
using System.Collections.Generic;

namespace Derin.Core.Mvc.Session
{
    public abstract class UserSessionContext
    {

        public UserSessionContext()
        {
            
        }
        public UserProfile User
        {
            get;
            set;
        }

        public T GetUser<T>() where T : UserProfile
        {

            return this.User as T;
        }

        public int[] ActionsIds
        {
            get;
            protected set;
        }

        public List<PermissionDto> Permissions
        {
            get;
            protected set;
        }


        public void AddPermission(PermissionDto dto)
        {
            this.Permissions.Add(dto);
        }


        public abstract bool IsAuthenticated();

        public abstract bool IsAuthorized(MvcAuthorization AuthorizationContext);
    }
}
