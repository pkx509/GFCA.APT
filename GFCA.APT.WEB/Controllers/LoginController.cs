using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using GFCA.APT.WEB.CustomAttributes;
using GFCA.APT.WEB.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GFCA.APT.WEB.Controllers
{
    [AllowAnonymous]
    public class LoginController : ControllerWebBase
    {
        private readonly IBusinessProvider _biz;
        public LoginController(IBusinessProvider biz)
        {
            this._biz = biz;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel user, string returnUrl)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    AuthenticationResponse authenticationResult = _biz.AuthenticationService.ValidateUser(user.Username, user.Password);
                    if (!authenticationResult.IsSuccess)
                    {
                        //this.Flash(FLASH_MESSAGE_TYPE.Error, authenticationResult.ErrorMessage);
                        ModelState.Remove("Password");
                        return View(user);
                    }

                    UserInfoDto userData = new UserInfoDto();
                    userData = authenticationResult.User;

                    HttpCookie ck = FormsAuthenticationExtensions.GetAuthCookie(user.Username, userData, user.RememberMe);
                    Response.Cookies.Add(ck);

                    //this.Flash(FLASH_MESSAGE_TYPE.Success, "Log In successful");
                    return RedirectToLocal(returnUrl);
            
                }
            }
            catch (Exception ex)
            {
                //this.Flash(FLASH_MESSAGE_TYPE.Error, ex.Message);
                return View(user);
            }

            return View(user);
        }

        [HttpPost]
        [Authorizer]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            try
            {
                FormsAuthentication.SignOut();
                //this.Flash(FLASH_MESSAGE_TYPE.Success, "Log Off successful");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //this.Flash(FLASH_MESSAGE_TYPE.Error, ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }
}