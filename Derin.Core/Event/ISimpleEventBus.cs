using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Event
{
    public interface ISimpleEventBus
    {
        void Attach<T>(IDerinEventHandler<T> @event) where T : DerinEvent;
        void Detach<T>(IDerinEventHandler<T> @event) where T : DerinEvent;
        void Notify<T>(T @event) where T : DerinEvent;
    }
}
