using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.DAO;

namespace SignalRChat.View
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string txtEmail, string txtPassword)
        {
            if (Request.Form["txtEmail"] != 0)
            {
                string email = Request.Form["txtEmail"];
                string password = Request.Form["txtPassword"];

                UserDAO userDAO = new UserDAO();
                UserDTO userDTO = userDAO.CheckLogin(email, password);

                if (userDTO != null)
                {

                    return RedirectToAction("Index");
                }

            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}