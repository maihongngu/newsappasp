﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using news.Models;
using model.DAO;
namespace news.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        NewsWebAppEntities db = new NewsWebAppEntities();
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginMD model)
        {
            var dao = new AccountDAO();

            var result = dao.log(model.username, model.password);

            if(result && ModelState.IsValid)
            {
                var user = dao.GetID(model.username);
                var session = new LoginMD();
                Session["UserID"] = user.Username.ToString();
                Session["UserName"] = user.PasswordHash.ToString();
                session.username = user.Username;
                Session.Add(Common.User_session, session);
                return RedirectToAction("index", "home");
            }
            return View(model);
        }
        
    }
}