using System.Threading;
using HaloSharp;

namespace HaloHistory.Web.Services
{
    public class HaloSharpTimer : IHaloSharpTimer
    {
        private Timer _timer;
        private CallbackEventHandler _eventHandler;

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public void Setup(CallbackEventHandler eventHandler, int dueTime, int period)
        {
            _eventHandler = eventHandler;
            _timer = new Timer(Callback, null, dueTime, period);
        }

        private void Callback(object state)
        {
            _eventHandler?.Invoke(state);
        }

        public void Change(int dueTime, int period)
        {
            _timer?.Change(dueTime, period);
        }
    }
}
