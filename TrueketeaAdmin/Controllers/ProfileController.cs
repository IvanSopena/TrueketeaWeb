using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrueketeaAdmin.Services.FirebaseServices;
using TrueketeaAdmin.Services.Security;
using TrueketeaAdmin.ViewModels;

namespace TrueketeaAdmin.Controllers
{
    public class ProfileController : Controller
    {
        LoginViewModel lg = HomeController.lg;
        private ProfileViewModel pf = new ProfileViewModel(Startup.dbContext);
        private B8Functions B8 = new B8Functions();
        private FBStorage stge = new FBStorage();

        public IActionResult Profile()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = lg.ErrorsCount;
            ViewBag.countNotify = lg.NotifyCount;

            pf.GetPronvinces();

            if (pf.GetUsers(lg.Usuarios.UserEmail) == false)
            {


                ViewBag.Message = pf.ProfileError;
                ViewBag.Type = "Error";

            }

            ViewBag.Provinces = pf.provinces;
            return View("Profile", pf.Usr);

        }


        public IActionResult Change_Profile(string name, string email, string AppName, string password, string phone, string time, string place, string gener)
        {
            Encrypting Encrypt = new Encrypting();
            
            string nombre = B8.GetItemList(name, 1, " ");
            string apellidos = B8.GetItemList(name, 2, " ");

            password = Encrypt.Encrypt(password,email);
            place = B8.DBLookupEx("SpainLocation", "Id_Province", "Internal_Code", place);
            
            if(gener.Equals("M"))
            {
                gener = "1";
            }else
            {
                gener = "0";
            }

            bool result = pf.UpdateProfile(nombre, apellidos, email, AppName, password, phone, time, place, gener);

            if (result == true)
            {
                pf.GetPronvinces();
                if (pf.GetUsers(lg.Usuarios.UserEmail) == false)
                {
                    ViewBag.Message = pf.ProfileError;
                    ViewBag.Type = "Error";
                }
                else
                {
                    lg.Usuarios.UserName = AppName;
                    ViewBag.Message = "Perfil Actualizado";
                    ViewBag.Type = "Success";
                    ViewBag.name = lg.Usuarios.UserName;
                    ViewBag.photo = lg.Usuarios.Photo;
                    ViewBag.mail = lg.Usuarios.UserEmail;
                    ViewBag.counterror = lg.ErrorsCount;
                    ViewBag.countNotify = lg.NotifyCount;
                }
            }
            else
            {
                pf.GetPronvinces();
                if (pf.GetUsers(lg.Usuarios.UserEmail) == false)
                {
                    ViewBag.Message = pf.ProfileError;
                    ViewBag.Type = "Error";
                }
                ViewBag.Message = "Error al actualizar el perfil";
                ViewBag.Type = "Error";
                ViewBag.name = lg.Usuarios.UserName;
                ViewBag.photo = lg.Usuarios.Photo;
                ViewBag.mail = lg.Usuarios.UserEmail;
                ViewBag.counterror = lg.ErrorsCount;
                ViewBag.countNotify = lg.NotifyCount;
            }

            ViewBag.Provinces = pf.provinces;
            return View("Profile", pf.Usr);

        }


        public async Task<IActionResult> Change_Image_Profile(string thefile1)
        {
          

            if (string.IsNullOrEmpty(thefile1) == true)
            {
                pf.GetPronvinces();
                if (pf.GetUsers(lg.Usuarios.UserEmail) == false)
                {
                    ViewBag.Message = pf.ProfileError;
                    ViewBag.Type = "Error";
                }
                else
                {
                    
                    ViewBag.Message = "Selecciona una foto";
                    ViewBag.Type = "Warning";
                    ViewBag.name = lg.Usuarios.UserName;
                    ViewBag.photo = lg.Usuarios.Photo;
                    ViewBag.mail = lg.Usuarios.UserEmail;
                    ViewBag.counterror = lg.ErrorsCount;
                    ViewBag.countNotify = lg.NotifyCount;
                }
            }
            else
            {
                string paht = @"E:\" + thefile1;
                string Foto = await stge.Addphoto(paht, lg.Usuarios.UserId.ToString(), "Profile", "Pofile.png");

                if (string.IsNullOrEmpty(Foto) == false)
                {
                    B8.UpdateExpress("Users", "Email", lg.Usuarios.UserEmail, "PhotoPath", Foto);
                    lg.Usuarios.Photo = Foto;

                    pf.GetPronvinces();
                    if (pf.GetUsers(lg.Usuarios.UserEmail) == false)
                    {
                        ViewBag.Message = pf.ProfileError;
                        ViewBag.Type = "Error";
                    }
                    else
                    {
                        lg.Usuarios.Photo = Foto;
                        ViewBag.Message = "Foto Cambiada con exito.";
                        ViewBag.Type = "Success";
                        ViewBag.name = lg.Usuarios.UserName;
                        ViewBag.photo = lg.Usuarios.Photo;
                        ViewBag.mail = lg.Usuarios.UserEmail;
                        ViewBag.counterror = lg.ErrorsCount;
                        ViewBag.countNotify = lg.NotifyCount;
                    }
                }
                else
                {
                    pf.GetPronvinces();
                    if (pf.GetUsers(lg.Usuarios.UserEmail) == false)
                    {
                        ViewBag.Message = pf.ProfileError;
                        ViewBag.Type = "Error";
                    }
                    else
                    {
                        
                        ViewBag.Message = "Error en la modificacion de la foto.";
                        ViewBag.Type = "Error";
                        ViewBag.name = lg.Usuarios.UserName;
                        ViewBag.photo = lg.Usuarios.Photo;
                        ViewBag.mail = lg.Usuarios.UserEmail;
                        ViewBag.counterror = lg.ErrorsCount;
                        ViewBag.countNotify = lg.NotifyCount;
                    }
                }

                
            }

            ViewBag.Provinces = pf.provinces;
            return View("Profile", pf.Usr);
        }


    }
}
