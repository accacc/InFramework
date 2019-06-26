using System;
using System.Collections.Generic;
using System.Text;

namespace IF.Core.RuleEngine
{

    public interface IRuleEngine<Request>
    {
        void Execute(Request request);
    }

    public abstract class IFRule<Request, Response>

    {

        private IFRule<Request, Response> Next;



        public IFRule()
        {
        }

        public IFRule<Request, Response> SetNext(IFRule<Request, Response> next)
        {
            this.Next = next;
            return next;
        }

        public virtual bool Run(Request request, Response response)
        {

            if (this.IsSuccess(request, response))
            {
                if (this.Next != null)
                {
                    return this.Next.Run(request, response);
                }

                return true;
            }


            return false;
        }

        public abstract bool IsSuccess(Request request, Response response);
    }
}
