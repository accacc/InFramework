//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace IF.Core.RuleEngine
//{ 

//    public abstract class IFRule<T>

//    {

//        private IFRule<T> Next;



//        public IFRule()
//        {
//        }

//        public IFRule<T> SetNext(IFRule<T> next)
//        {
//            this.Next = next;
//            return next;
//        }

//        public virtual bool Run(T context)
//        {

//            if (this.IsSuccess(context))
//            {
//                if (this.Next != null)
//                {
//                    return this.Next.Run(context);
//                }

//                return true;
//            }


//            return false;
//        }

//        public abstract bool IsSuccess(T  context);
//    }
//}
