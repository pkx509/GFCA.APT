using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GFCA.APT.WEB
{
    public class FormsAuthenticationExtensions
    {
        public static HttpCookie GetAuthCookie(string userName, object userData, bool createPersistentCookie = true, int timeout = 30)
        {
            int OneYear = 525600; // 525600 min = 1 year
            int timeouter = createPersistentCookie ? OneYear : timeout; // 525600 min = 1 year

            HttpCookie ck = FormsAuthentication.GetAuthCookie(userName, createPersistentCookie);
            var tk = FormsAuthentication.Decrypt(ck.Value);

            var js = JsonConvert.SerializeObject(userData);
            var tkUser = new FormsAuthenticationTicket(tk.Version, tk.Name, DateTime.Now, DateTime.Now.AddMinutes(timeouter), createPersistentCookie, js, tk.CookiePath);
            var enTk = FormsAuthentication.Encrypt(tkUser);

            ck.Value = enTk;
            ck.Expires = DateTime.Now.AddMinutes(timeouter);
            ck.HttpOnly = true;
            return ck;

        }

        public static T ExtractUserData<T>(System.Security.Principal.IIdentity identity)
        {
            var formsIdentity = identity as FormsIdentity;
            if (formsIdentity == null)
                throw new ArgumentException("identity is not a FormsIdentity");

            return ExtractUserData<T>(formsIdentity);
        }

        public static T ExtractUserData<T>(FormsIdentity identity)
        {
            var tk = identity.Ticket;
            var js = tk.UserData;
            var data = JsonConvert.DeserializeObject<T>(js);
            if (data == null)
                throw new ArgumentException("Could not Deserialize ticket user data. Userdata: " + tk.UserData);

            return data;
        }
    }
}