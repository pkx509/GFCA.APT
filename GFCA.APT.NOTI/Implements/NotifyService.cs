using GFCA.APT.NOTI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.NOTI.Implements
{
    public class NotifyService : INotifyService
    {
        private readonly NotifyType notifyType;
        private EmailService emailService;
        private LineService lineService;

        public NotifyService(NotifyType notifyType)
        {
            emailService = new EmailService();
            lineService = new LineService();
            this.notifyType = notifyType;
        }

        public async Task<bool> Send(string context)
        {
            if (notifyType == NotifyType.Email)
            {
                return await emailService.SendAsync();
            }
            else if (notifyType == NotifyType.Line)
            {
                string response = await lineService.Send("");
                bool tsk = response.Length > 0;
                return tsk;
            }

            return false;
        }

        public void SetContext()
        {
            throw new NotImplementedException();
        }
    }

    public enum NotifyType
    {
        Email,
        Line
    }

}
