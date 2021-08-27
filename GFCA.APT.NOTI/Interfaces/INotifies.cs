using System.Threading.Tasks;

namespace GFCA.APT.NOTI.Interfaces
{
    public interface INotifies<T>
    {
        void SetContext();
        Task<string> Send(T context);
    }
}
