using GFCA.APT.Domain.Common;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using log4net;
//using Microsoft.Owin.Security;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Web.Security;

namespace GFCA.APT.BAL.Implements
{
    using GFCA.APT.BAL.Interfaces;
    using GFCA.APT.DAL.Implements;
    using GFCA.APT.DAL.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private const string Salt = "Asiatic";
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static AuthenticationService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new AuthenticationService(uow);

            return svc;
        }

        public AuthenticationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public AuthenticationResponse ValidateUser(string userName, string password)
        {
            AuthenticationResponse response = AuthenticationResponse.FAILED(userName);
            string key = userName.ToMD5Hash();
            string passwd = password.ToMD5Hash(key);
            var emp = _uow.EmployeeRepository.GetEmployee(userName, passwd);
            bool isAuthen = emp != null;

            if (!isAuthen)
            {
                return response;
            }
            UserInfoDto user = new UserInfoDto();
            user.FirstName = emp.FIRSTNAME;
            user.LastName = emp.LASTNAME;
            user.UserId = emp.EMP_ID;
            user.Title = emp.PREFIX;
            user.Email = emp.EMAIL;
            user.EmpCode = emp.EMP_CODE;
            user.UserName = emp.EMAIL;

            IList<EmployeeRoleDto> roles = _uow.EmployeeRepository.GetRoles(emp.EMP_ID.Value).ToList();

            user.Roles = roles.Select(x => x.ROLE_NAME).ToArray<string>();
            return AuthenticationResponse.SUCCESS(user);
            //return userName.ToLower().Equals("admin") && passwd.Equals(passwd_in_db) ? AuthenticationResponse.SUCCESS() : response;
        }

    }

    internal static class Encryptor
    {
        public static string ToMD5Hash(this string text, string sult = "")
        {

            //System.Security.Cryptography.MD5 md5Hash = new System.Security.Cryptography.MD5CryptoServiceProvider();
            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                string concat = text + sult;
                var bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(concat);
                //md5Hash.ComputeHash(bytes);
                byte[] hash = md5Hash.ComputeHash(bytes);
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public static string ToSHA256Hash(this string text, string sult = "")
        {
            // Create a SHA256   
            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                string concat = text + sult;
                var bytes = System.Text.Encoding.UTF8.GetBytes(concat);
                byte[] hash = sha256Hash.ComputeHash(bytes);
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }

        }
    }
}
