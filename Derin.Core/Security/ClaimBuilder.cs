using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Security
{
    public class ClaimBuilder<T> where T : IUserProfile
    {

        public ClaimsIdentity Claims { get; private set; }

        public ClaimBuilder()
        {
            this.Claims = Claims;
        }
        public void Add<TProperty>(Expression<Func<T, TProperty>> param, object value)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;

            Claim parameterBuilder = new Claim(propertyBody.Member.Name, value.ToString());
        }



    }
}
