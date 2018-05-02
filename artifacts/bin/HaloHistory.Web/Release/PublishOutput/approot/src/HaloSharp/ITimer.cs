using System;

namespace HaloSharp
{
    public delegate void CallbackEventHandler(object state);
    public interface IHaloSharpTimer : IDisposable
    {
        void Setup(CallbackEventHandler eventHandler, int dueTime, int period);
        void Change(int dueTime, int period);

    }
}
