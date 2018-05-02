using System;
using System.Threading.Tasks;

namespace HaloSharp
{
    public interface IHaloSession : IDisposable
    {
        Task<TResult> Get<TResult>(string path);
        Task<Tuple<string, byte[]>> GetImage(string path);
    }
}