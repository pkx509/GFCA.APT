using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.NOTI.Interfaces
{
    public interface INotifyService
    {
        void SetContext();
        Task<bool> Send(string context);
    }
}
