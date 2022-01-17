//using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GFCA.APT.NOTI.Implements
{
    public class EmailService : IDisposable
    {
        private readonly string _server;
        private readonly int _port = 25;
        private SmtpClient _smtp;
        private MailMessage _msg;

        public EmailService(string server = "smtp.gmail.com", int port = 587, string userName = "admin@user.abc", string password = "passwd")
        {
            string systemEmail = "admin@user.abc";
            string systemPasswd = "passwd";

            this._server = server;
            this._port = port;
            this._smtp = new SmtpClient()
            {
                Host = server,
                Port = port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(userName, password),
                UseDefaultCredentials = false,
                EnableSsl = true
            };
            this._smtp.SendCompleted += SmtpSendCompletedHanler;
        }

        public void SetParticipant(string from, IList<string> to)
        {
            IList<string> bcc = new List<string>();
            IList<string> cc = new List<string>();
            this.SetParticipant(from, to, cc, bcc);

        }
        public void SetParticipant(string from, IList<string> to, IList<string> cc)
        {
            IList<string> bcc = new List<string>();
            this.SetParticipant(from, to, cc, bcc);
        }
        public void SetParticipant(string from, IList<string> to, IList<string> cc, IList<string> bcc)
        {
            _msg.From = new MailAddress(from);
            foreach (string t in to)
                _msg.To.Add(new MailAddress(t));

            foreach (string c in cc)
                _msg.CC.Add(new MailAddress(c));

            foreach (string b in bcc)
                _msg.Bcc.Add(new MailAddress(b));
        }
        public void SetParticipant(MailAddress from, IList<MailAddress> to)
        {
            IList<MailAddress> bcc = new List<MailAddress>();
            IList<MailAddress> cc = new List<MailAddress>();
            this.SetParticipant(from, to, cc, bcc);

        }
        public void SetParticipant(MailAddress from, IList<MailAddress> to, IList<MailAddress> cc)
        {
            IList<MailAddress> bcc = new List<MailAddress>();
            this.SetParticipant(from, to, cc, bcc);
        }
        public void SetParticipant(MailAddress from, IList<MailAddress> to, IList<MailAddress> cc, IList<MailAddress> bcc)
        {
            _msg.From = from;
            foreach (MailAddress t in to)
                _msg.To.Add(new MailAddress(t.Address, t.DisplayName));

            foreach (MailAddress c in cc)
                _msg.CC.Add(new MailAddress(c.Address, c.DisplayName));

            foreach (MailAddress b in bcc)
                _msg.Bcc.Add(new MailAddress(b.Address, b.DisplayName));

        }
        public void SetAttachment(IList<Attachment> attachments)
        {
            foreach (Attachment a in attachments)
                _msg.Attachments.Add(a);
        }

        public void SetContentMessage(string subject, string body, string footer)
        {
            _msg.SubjectEncoding = Encoding.UTF8;
            _msg.BodyEncoding = Encoding.UTF8;
            _msg.IsBodyHtml = true;

            _msg.Subject = subject;
            _msg.Body = body + footer;
        }

        public bool Send()
        {
            bool isSuccess = false;

            try
            {
                _smtp.Send(_msg);
                isSuccess = true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //_logger.Error("SmtpFailedRecipientsException : ", ex);
                throw ex;
            }
            catch (SmtpException ex)
            {
                //_logger.Error("SmtpException : ", ex);
                throw ex;
            }
            catch (Exception ex)
            {
                //_logger.Error("Exception : ", ex);
                throw ex;
            }

            return isSuccess;
        }
        public Task<bool> SendAsync()
        {
            bool isSuccess = false;

            try
            {
                _smtp.Send(_msg);
                isSuccess = true;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //_logger.Error("SmtpFailedRecipientsException : ", ex);
                throw ex;
            }
            catch (SmtpException ex)
            {
                //_logger.Error("SmtpException : ", ex);
                throw ex;
            }
            catch (Exception ex)
            {
                //_logger.Error("Exception : ", ex);
                throw ex;
            }

            return Task.FromResult(isSuccess);
        }

        public void Dispose()
        {
            _smtp.Dispose();
            _msg.Dispose();
            _smtp = null;
            _msg = null;
        }
        private void SmtpSendCompletedHanler(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                    throw e.Error;

                object userState = e.UserState;
                bool isCancelled = e.Cancelled;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
