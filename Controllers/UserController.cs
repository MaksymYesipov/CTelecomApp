using CTelecomApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CTelecomApp.Controllers
{
    public class UserController : Controller
    {
        private telecom_dbContext DbContext = new telecom_dbContext();
        private static ISet<string> CheckedTokens = new HashSet<string>();

        [HttpPost]
        public ActionResult Login(String Phone, String Password, String CheckMeOut)
        {
            
            List<string> errors = new List<string>();
            Session["LoginErrors"] = "";

            Managers manager = null;
            try
            {
                manager = DbContext.Managers.Where(m => m.Login.Equals(Phone)).First();
            } catch (Exception e)
            {
                // that's not a manager
            }

            if (manager != null)
            {
                if (manager.Password.Equals(Password))
                {
                    Session["user"] = manager.Name;
                    Session["role"] = "manager";
                    return Redirect("/Admin/Orders");
                }
            }

            Users user = null;
            try
            {
                user = DbContext.Users.Where(u => u.SimCards.Where(s => s.PhoneNumber.Equals(Phone)).First() != null).First();
            } catch (Exception e)
            {
                errors.Add("There's no user with this phone number attached");
            }
            if (user != null)
            {
                if (user.Password.Equals(Password))
                {
                    Session["user"] = user.Name;
                    Session["userId"] = user.IdCard;
                    Session["balance"] = user.Balance;
                    if (CheckMeOut != null)
                    {
                        string AccessToken = CalculateMD5Hash(user.IdCard + user.Password);
                        CheckedTokens.Add(AccessToken);
                        Response.AppendCookie(new HttpCookie("AccessToken", AccessToken));
                        Response.AppendCookie(new HttpCookie("SignedAs", user.IdCard));
                    }
                } else
                {
                    errors.Add("Wrong password");
                }
            }

            if (errors.Any())
            {
                StringBuilder errorMsgs = new StringBuilder();
                errors.ForEach(err => errorMsgs.Append(err).Append('/'));
                errorMsgs = errorMsgs.Remove(errorMsgs.Length - 1, 1);
                Session["LoginErrors"] = errorMsgs.ToString();
                return Redirect("/Home/Login");
            }

            return RedirectPermanent("/Home/Index");
        }

        public ActionResult AutoLogin()
        {
            if (Request.Cookies.Get("AccessToken") != null && CheckedTokens.Contains(Request.Cookies.Get("AccessToken").Value))
            {
                if (Request.Cookies.Get("SignedAs") != null)
                {
                    Users user = DbContext.Users.Where(u => u.IdCard.Equals(Request.Cookies.Get("SignedAs"))).First();
                    if(user != null && Request.Cookies.Get("AccessToken").Equals(CalculateMD5Hash(user.IdCard + user.Password)))
                    {
                        Session["user"] = user.Name;
                        Session["userId"] = user.IdCard;
                        Session["balance"] = user.Balance;
                        return Redirect("/Home/Index");
                    }
                }
            }
            return Redirect("/Home/Login");
        }

        public ActionResult LogOut()
        {
            Session["user"] = null;
            Session["userId"] = null;
            Session["role"] = null;
            Session["balance"] = null;
            CheckedTokens.Remove(Request.Cookies.Get("AccessToken").Value);
            Response.SetCookie(new HttpCookie("AccessToken", ""));
           
            return Redirect("/Home/Index");
        }

        private string CalculateMD5Hash(string input)
        {

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}