using BusinessLogicProject.BusinessLogicMethods;
using BusinessLogicProject.Contexts;
using BusinessLogicProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AirPanelFromVeryBegining.Controllers
{
   
    public class AccountController : Controller
    {

        AuthorizationMethods authorization = new AuthorizationMethods();
    

        public ActionResult Login()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {     
                User user = authorization.Login(loginModel);
                if (user !=null)
                {
                    FormsAuthentication.SetAuthCookie(loginModel.Login, true);
                    return RedirectToAction("UserPage", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильний логін або Пароль");
                }
            }
          
            return View(loginModel);
        }
       
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
       
        }


        // GET: Account
        [Authorize]
        public ActionResult Register()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel)
        {
           
            if (ModelState.IsValid)
            {
               
                if (authorization.RegisterMethod(registerModel))
                {
                    return RedirectToAction("UserPage", "Account");
                } 
                else
                {
                    ModelState.AddModelError("", "Користувач з таким ім'ям вже існує");
                }
            }
            return View(registerModel);
        }

        [Authorize]
        public ActionResult UserPage()
        {
            string userName;
            userName = User.Identity.Name.ToString();

            User name = authorization.UserInfo(userName);
            
            return View(name);
        }
    }
}