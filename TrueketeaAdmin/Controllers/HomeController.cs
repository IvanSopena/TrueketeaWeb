using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TrueketeaAdmin.Models;
using TrueketeaAdmin.Services.FirebaseServices;
using TrueketeaAdmin.ViewModels;

namespace TrueketeaAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static LoginViewModel lg = new LoginViewModel(Startup.dbContext);
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger; 
        }

        public IActionResult Index()
        {
            
            if(Startup.dbContext.IsOpen==false)
            {
                ViewBag.Message = "Error de conexión: " + Startup.dbContext.DbLastError;
                ViewBag.Type = "Error";
            }
            return View("Login");
        }

        public IActionResult Close()
        {
            ViewBag.Message = "Se ha cerrado sesión correctamente.";
            ViewBag.Type = "Info";
            //Startup.dbContext.DbClose();
            return View("Login");
        }

        public IActionResult Dashboard()
        {
            DashboardViewmodel db = new DashboardViewmodel();
            db.GetCountUsers();
            db.GetInfoProducts();
            db.GetOS();
            db.GetVersions();
            db.GetProvinceInfo();
            db.GetHistoryProduct();
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = lg.ErrorsCount;
            ViewBag.countNotify = lg.NotifyCount;
            ViewBag.UsersCount = db.UsersCount;
            ViewBag.TotalProducts = db.TotalProducts;
            ViewBag.TotalTransactions = db.TotalTransactions;
            ViewBag.FinishedTransactions = db.FinishedTransactions;
            ViewBag.ProInfo = db.ProvinceInfo;

            ViewBag.AndroidUsers = db.AndroidUsers;
            ViewBag.iOSUsers = db.iOSUsers;

            ViewBag.Subidos = db.PSubidos;
            ViewBag.Proceso = db.PProceso;
            ViewBag.Finalizado = db.PFinalizados;
            ViewBag.Bloqueados = db.PBloqueados;

            ViewBag.Message = db.DBError;
            return View("Home", db.VersionInfo);
        }

        public IActionResult Login(string Email, string pass)
        {
            if(lg.AppOpen(Email, pass) ==0)
            {
                

                DashboardViewmodel db = new DashboardViewmodel();
                db.GetCountUsers();
                db.GetInfoProducts();
                db.GetOS();
                db.GetVersions();
                db.GetProvinceInfo();
                db.GetHistoryProduct();
                ViewBag.name = lg.Usuarios.UserName;
                ViewBag.photo = lg.Usuarios.Photo;
                ViewBag.mail = lg.Usuarios.UserEmail;
                ViewBag.counterror = lg.ErrorsCount;
                ViewBag.countNotify = lg.NotifyCount;
                ViewBag.UsersCount = db.UsersCount;
                ViewBag.TotalProducts = db.TotalProducts;
                ViewBag.TotalTransactions = db.TotalTransactions;
                ViewBag.FinishedTransactions = db.FinishedTransactions;

                ViewBag.AndroidUsers = db.AndroidUsers;
                ViewBag.iOSUsers = db.iOSUsers;

                ViewBag.ProInfo = db.ProvinceInfo;

                ViewBag.Subidos = db.PSubidos;
                ViewBag.Proceso = db.PProceso;
                ViewBag.Finalizado = db.PFinalizados;
                ViewBag.Bloqueados = db.PBloqueados;

                return View("Home", db.VersionInfo);
            }

            ViewBag.Message = lg.LoginError;
            ViewBag.Type = "Error";

            return View("Login");
        }

        public IActionResult Restore()
        {
            return View();
        }

        public IActionResult Send_Restore (string Email)
        {
            RestorePassViewModel rst = new RestorePassViewModel(Startup.dbContext,Email);

            if(rst.ChangePass()== true)
            {
                ViewBag.Message = rst.RestoreError;
                ViewBag.Type = "Success";
            }
            else
            {
                ViewBag.Message = rst.RestoreError;
                ViewBag.Type = "Error";
            }

            return View("Restore");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

     
    }
}
