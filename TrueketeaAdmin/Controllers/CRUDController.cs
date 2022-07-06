using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;
using TrueketeaAdmin.Services.FirebaseServices;
using TrueketeaAdmin.ViewModels;

namespace TrueketeaAdmin.Controllers
{
    public class CRUDController : Controller
    {
        private LoginViewModel lg = HomeController.lg;
        private ProfileViewModel pf = new ProfileViewModel(Startup.dbContext);
        private B8Functions b8 = new B8Functions();
        private static CrudViewModel crud = new CrudViewModel(Startup.dbContext);
        private IHostEnvironment _env;
        private IJavaScriptService _javascriptService;

        public CRUDController(IHostEnvironment env, IJavaScriptService javaScriptService)
        {
            _javascriptService = javaScriptService;
            _env = env;
        }
       
        public IActionResult Users()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.GetUsers("Usuario");
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Usuarios";

            return View("DataListUsers", crud.Usr);
        }

        public IActionResult Admin()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.GetUsers("Administrador");
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Administradores";

            return View("DataListUsers", crud.Usr);
        }

        public IActionResult Provincias()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.GetPronvinces();
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Provincias";

            return View("DataListProvince", crud.provinces);
        }

        public IActionResult Roles()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.GetRoles();
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Roles";

            return View("DataListRol", crud.rol);
        }

        public IActionResult Categorias()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.GetCategories();
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Categorias";

            return View("DataListCat", crud.Categoria);
        }

        public IActionResult Subcategorias()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.GetSubcategories();
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Subcategorias";

            return View("DataListSubCat", crud.Subcategoria);
        }

        public IActionResult Errores()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.GetErr();
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Errores";

            return View("DataListError", crud.Errors);
        }

        public IActionResult Products()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.GetProducts();
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Productos";

            return View("DataListProduct", crud.Pdrt);
        }

        public IActionResult ProductDetail(string ID)
        {
            
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            crud.DetailProduct(ObjectId.Parse(ID));
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Detalles del PRoducto";
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            return View("ProductDetail", crud.Pdrt);
        }

        public IActionResult BlockProduct(string ID)
        {
            

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            crud.DetailProduct(ObjectId.Parse(ID));
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Detalles del Producto";
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;

            foreach(var item in crud.Pdrt)
            {
               crud.MProd.Id = item.Id; 
               crud.MProd.Categoria = item.Categoria;   
               crud.MProd.Descripcion = item.Descripcion;
               crud.MProd.Direccion = item.Direccion;
               item.Estado = "Bloqueado";
               crud.MProd.Estado = item.Estado;
               crud.MProd.Foto1 = item.Foto1;
               crud.MProd.Foto2 = item.Foto2;
               crud.MProd.Foto3 = item.Foto3;
               crud.MProd.Foto4 = item.Foto4;
               crud.MProd.IdProducto = item.IdProducto;
               crud.MProd.Likes = item.Likes;
               crud.MProd.NombreProducto = item.NombreProducto;
               crud.MProd.Precio= item.Precio;
               crud.MProd.User_id = item.User_id;
               crud.MProd.User_Name = item.User_Name;
               crud.MProd.ShortDesc = item.ShortDesc;
               crud.MProd.Longitud = item.Longitud;
               crud.MProd.Latitud = item.Latitud;
               crud.MProd.USerPic = item.USerPic;
               crud.MProd.EstadoActual = item.EstadoActual; 
               crud.MProd.Fav_User_Id = item.Fav_User_Id;
            }

            if (Startup.mongo.UpdateProduct(crud.MProd) == true)
            {
                string thisDay = DateTime.Today.ToString("dd/MM/yyyy");
                if (b8.UpdateExpress("HistoricalActivities", "IdProduct", crud.MProd.Id.ToString(), "StatusId", "4", "TransDate = CONVERT(date,'" + thisDay + "'),UserID", "1") == true)
                {

                    crud.noti.Chat = false;
                    crud.noti.Receptor = "all";
                    crud.noti.Tipo = 0;
                    crud.noti.Estado = 0;
                    crud.noti.IdProducto = crud.MProd.Id.ToString();
                    crud.noti.Texto = $"El articulo: {crud.MProd.NombreProducto}, ha sido bloquedo por el administrador.";

                    Startup.mongo.InsertNotify(crud.noti);
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }   
            }
            else
            {
                ViewBag.Type = "Error";
                ViewBag.Message = "No se ha podido actualizar";
            }

            return View("ProductDetail", crud.Pdrt);
        }



        public IActionResult UnblockProduct(string ID)
        {


            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            crud.DetailProduct(ObjectId.Parse(ID));
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Detalles del Producto";
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;

            foreach (var item in crud.Pdrt)
            {
                crud.MProd.Id = item.Id;
                crud.MProd.Categoria = item.Categoria;
                crud.MProd.Descripcion = item.Descripcion;
                crud.MProd.Direccion = item.Direccion;
                item.Estado = "Subido";
                crud.MProd.Estado = item.Estado;
                crud.MProd.Foto1 = item.Foto1;
                crud.MProd.Foto2 = item.Foto2;
                crud.MProd.Foto3 = item.Foto3;
                crud.MProd.Foto4 = item.Foto4;
                crud.MProd.IdProducto = item.IdProducto;
                crud.MProd.Likes = item.Likes;
                crud.MProd.NombreProducto = item.NombreProducto;
                crud.MProd.Precio = item.Precio;
                crud.MProd.User_id = item.User_id;
                crud.MProd.User_Name = item.User_Name;
                crud.MProd.ShortDesc = item.ShortDesc;
                crud.MProd.Longitud = item.Longitud;
                crud.MProd.Latitud = item.Latitud;
                crud.MProd.USerPic = item.USerPic;
                crud.MProd.EstadoActual = item.EstadoActual;
                crud.MProd.Fav_User_Id = item.Fav_User_Id;
            }

            if (Startup.mongo.UpdateProduct(crud.MProd) == true)
            {
                string thisDay = DateTime.Today.ToString("dd/MM/yyyy");
                if (b8.UpdateExpress("HistoricalActivities", "IdProduct", crud.MProd.Id.ToString(), "StatusId", "1", "TransDate = CONVERT(date,'" + thisDay + "'),UserID", "1") == true)
                {
                    crud.noti.Chat = false;
                    crud.noti.Receptor = "all";
                    crud.noti.Tipo = 0;
                    crud.noti.Estado = 0;
                    crud.noti.IdProducto = crud.MProd.Id.ToString();
                    crud.noti.Texto = $"El articulo: {crud.MProd.NombreProducto}, ha sido desbloquedo por el administrador.";

                    Startup.mongo.InsertNotify(crud.noti);
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
            }
            else
            {
                ViewBag.Type = "Error";
                ViewBag.Message = "No se ha podido actualizar";
            }

            return View("ProductDetail", crud.Pdrt);
        }

        public IActionResult ChangePrice(string ID)
        {

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            crud.DetailProduct(ObjectId.Parse(ID));
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Detalles del Producto";
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            foreach (var item in crud.Pdrt)
            {
                crud.MProd.Id = item.Id;
                crud.MProd.Categoria = item.Categoria;
                crud.MProd.Descripcion = item.Descripcion;
                crud.MProd.Direccion = item.Direccion;
                item.Estado = item.Estado;
                crud.MProd.Estado = item.Estado;
                crud.MProd.Foto1 = item.Foto1;
                crud.MProd.Foto2 = item.Foto2;
                crud.MProd.Foto3 = item.Foto3;
                crud.MProd.Foto4 = item.Foto4;
                crud.MProd.IdProducto = item.IdProducto;
                crud.MProd.Likes = item.Likes;
                crud.MProd.NombreProducto = item.NombreProducto;
                crud.MProd.Precio = item.Precio;
                crud.MProd.User_id = item.User_id;
                crud.MProd.User_Name = item.User_Name;
                crud.MProd.ShortDesc = item.ShortDesc;
                crud.MProd.Longitud = item.Longitud;
                crud.MProd.Latitud = item.Latitud;
                crud.MProd.USerPic = item.USerPic;
                crud.MProd.EstadoActual = item.EstadoActual;
                crud.MProd.Fav_User_Id = item.Fav_User_Id;
            }

            string cat = b8.DBLookupEx("Categories", "Id_Categorie", "Categorie_Desc", crud.MProd.Categoria);
            string price = b8.DBLookupEx("ProductPrice", "Max(Price)", "Categorie_id", cat, $"Product_Name like '%{crud.MProd.NombreProducto}%' and '1'", "1");

            if(price.Equals("") || price.Equals("0"))
            {
                price = "0";
            }

            crud.MProd.Precio = Convert.ToDecimal(price);

            if (Startup.mongo.UpdateProduct(crud.MProd) == true)
            {
                ViewBag.Type = "Info";
                ViewBag.Message = $"Se ha modificado el precio del producto {crud.MProd.NombreProducto} a {price}€";
            }
            else
            {
                ViewBag.Type = "Error";
                ViewBag.Message = "No se ha podido actualizar";
            }

            return View("ProductDetail", crud.Pdrt);

        }

        public IActionResult Notify()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.GetNotify();
            ViewBag.Message = crud.Errors;
            ViewBag.Type = crud.CRUDtype;
            ViewBag.Titulo = "Inbox";

            return View("Inbox",crud.MSG);
        }


        public IActionResult ReadNotify(string id)
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            ViewBag.ActualId = id;
            crud.GetUpdateInbox(id);
            ViewBag.Titulo = "Leer Notificacion";
            return View("ReadEmail", crud.MSG);
        }

        public IActionResult WriteNotify()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;

            ViewBag.Titulo = "Escribir Notificacion";

            return View("WriteEmail");
        }

        public IActionResult SendNotify(string textarea, string to, string subject)
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");//lg.NotifyCount;
            crud.SendEmail(textarea, to, subject,lg.Usuarios.UserId);
            ViewBag.Message = crud.Errors;
            ViewBag.Type = crud.CRUDtype;
            ViewBag.Titulo = "Escribir Notificacion";

            return View("WriteEmail");
        }


        public IActionResult ShowError(string ID)
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.AppName = b8.DBLookupEx("LogErrors", "App_Name", "Id_Error", ID);
            ViewBag.Method = b8.DBLookupEx("LogErrors", "Method", "Id_Error", ID);
            ViewBag.Class = b8.DBLookupEx("LogErrors", "Class", "Id_Error", ID);
            ViewBag.Error_Desc = b8.DBLookupEx("LogErrors", "Error_Desc", "Id_Error", ID);
            ViewBag.Query = b8.DBLookupEx("LogErrors", "SQL_Sentence", "Id_Error", ID);
            ViewBag.Err_Id = ID;


            return View("Detail");
        }


        public IActionResult ReviewError(string ID)
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
           
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.AppName = b8.DBLookupEx("LogErrors", "App_Name", "Id_Error", ID);
            ViewBag.Method = b8.DBLookupEx("LogErrors", "Method", "Id_Error", ID);
            ViewBag.Class = b8.DBLookupEx("LogErrors", "Class", "Id_Error", ID);
            ViewBag.Error_Desc = b8.DBLookupEx("LogErrors", "Error_Desc", "Id_Error", ID);
            ViewBag.Query = b8.DBLookupEx("LogErrors", "SQL_Sentence", "Id_Error", ID);
            ViewBag.Err_Id = ID;

           if(b8.UpdateExpress("LogErrors", "Id_Error", ID,"Active","0") == true)
            {
                ViewBag.Type = "Success";
                ViewBag.Message = "Error reconocido";
            }
            else
            {
                ViewBag.Type = "Error";
                ViewBag.Message = "No se ha podido reconocer el error";
            }


            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));

            return View("Detail");
        }

        
        /// CRUD SUBCATEGORIRES //////////////////////////////////////
        
        public IActionResult DeleteSubCategories(string Id)
        {
            string status = b8.DBLookupEx("Subcategories", "Active", "Id_SubCat", Id);

            if (status.Equals("0"))
            {
                if (b8.UpdateExpress("Subcategories", "Id_SubCat", Id, "Active", "1") == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "No se ha podido actualizar";
                }
            }
            else
            {
                if (b8.UpdateExpress("Subcategories", "Id_SubCat", Id, "Active", "0") == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "No se ha podido actualizar";
                }
            }

            

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");
            crud.GetSubcategories();
            
            ViewBag.Titulo = "Listado de Subcategorias";

            return View("DataListSubCat", crud.Subcategoria);
        }

        public IActionResult NewSubcategories()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.Desc = "";
            ViewBag.NameSub = "";

            ViewBag.Err_Id = "0";


            return View("DetailSubCategories");
        }

        public IActionResult ShowSubcategories(string ID)
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.Desc = b8.DBLookupEx("Subcategories", "Subcat_Desc", "Id_SubCat", ID);
            ViewBag.NameSub = b8.DBLookupEx("Subcategories", "Subcat_Name", "Id_SubCat", ID);
           
            ViewBag.Err_Id = ID;


            return View("DetailSubCategories");
        }


        public IActionResult EditSubcategoria(string desc,string id,string name)
        {

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            if (id == "0")
            {
                if (crud.InsertSC(desc, name)==true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Insertado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "Error al Insertar";
                }
            }
            else
            {
                if (crud.UpdatetSC(id,desc, name) == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "Error al actualizar";
                }
            }

            return View("DetailSubCategories");
        }


        /// CRUD CATEGORIRES //////////////////////////////////////

        public IActionResult NewCategories()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.Desc = "";
            ViewBag.icon = "";
            ViewBag.Err_Id = "0";


            return View("DetailCategories");
        }

        public IActionResult DeleteCategories(string Id)
        {

            string status = b8.DBLookupEx("Categories", "Active", "Id_Categorie", Id);

            if(status.Equals("0"))
            {
                if (b8.UpdateExpress("Categories", "Id_Categorie", Id, "Active", "1") == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "No se ha podido actualizar";
                }
            }
            else
            {
                if (b8.UpdateExpress("Categories", "Id_Categorie", Id, "Active", "0") == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "No se ha podido actualizar";
                }
            }


            

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");
            crud.GetCategories();
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Categorias";

            return View("DataListCat", crud.Categoria);
        }


        public IActionResult EditCategoria(string desc, string id, string name)
        {

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            if (id == "0")
            {
                if (crud.InsertCat(desc, name) == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Insertado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "Error al Insertar";
                }
            }
            else
            {
                if (crud.UpdatetCat(id, desc, name) == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "Error al actualizar";
                }
            }

            return View("DetailCategories");
        }

        public IActionResult ShowCategories(string ID)
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.Desc = b8.DBLookupEx("Categories", "Categorie_Desc", "Id_Categorie", ID);
            ViewBag.icon = b8.DBLookupEx("Categories", "Icon", "Id_Categorie", ID);

            ViewBag.Err_Id = ID;


            return View("DetailCategories");
        }

        /// CRUD PROVINCES //////////////////////////////////////
        public IActionResult NewProvinces()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.ProvinceName = "";
            ViewBag.InternalCode = "";
            ViewBag.Province_Id = "0";


            return View("DetailProvinces");
        }

        public IActionResult ShowProvinces(string ID)
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.ProvinceName = b8.DBLookupEx("SpainLocation", "Province_Name", "Id_Province", ID);
            ViewBag.InternalCode = b8.DBLookupEx("SpainLocation", "Internal_Code", "Id_Province", ID);

            ViewBag.Province_Id = ID;


            return View("DetailProvinces");
        }

        public IActionResult EditProvinces(string InternalCode, string id, string ProvinceName)
        {

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            if (id == "0")
            {
                if (crud.InsertProvince(InternalCode, ProvinceName) == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Insertado";
                    ViewBag.ProvinceName = b8.DBLookupEx("SpainLocation", "Province_Name", "Id_Province", id);
                    ViewBag.InternalCode = b8.DBLookupEx("SpainLocation", "Internal_Code", "Id_Province", id);

                    ViewBag.Province_Id = id;
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "Error al Insertar";
                    ViewBag.ProvinceName = b8.DBLookupEx("SpainLocation", "Province_Name", "Id_Province", id);
                    ViewBag.InternalCode = b8.DBLookupEx("SpainLocation", "Internal_Code", "Id_Province", id);

                    ViewBag.Province_Id = id;
                }
            }
            else
            {
                if (crud.UpdatetProvince(id, InternalCode, ProvinceName) == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";

                    ViewBag.ProvinceName = b8.DBLookupEx("SpainLocation", "Province_Name", "Id_Province", id);
                    ViewBag.InternalCode = b8.DBLookupEx("SpainLocation", "Internal_Code", "Id_Province", id);

                    ViewBag.Province_Id = id;
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "Error al actualizar";

                    ViewBag.ProvinceName = b8.DBLookupEx("SpainLocation", "Province_Name", "Id_Province", id);
                    ViewBag.InternalCode = b8.DBLookupEx("SpainLocation", "Internal_Code", "Id_Province", id);

                    ViewBag.Province_Id = id;
                }
            }

            return View("DetailProvinces");
        }

        /// CRUD Roles //////////////////////////////////////

        public IActionResult NewRol()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.RolName = "";
            ViewBag.Rol_Id = "0";


            return View("DetailRoles");
        }

        public IActionResult ShowRoles(string ID)
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.RolName = b8.DBLookupEx("Users_Roles", "Rol_Desc", "Rol_Id", ID);
            ViewBag.Rol_Id = ID;


            return View("DetailRoles");
        }

        public IActionResult DeleteRoles(string Id)
        {

            if (crud.DeleteRol(Id) == true)
            {
                ViewBag.Type = "Success";
                ViewBag.Message = "Elemento Actualizado";
            }
            else
            {
                ViewBag.Type = "Error";
                ViewBag.Message = "No se ha podido actualizar";
            }

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");
            crud.GetRoles();
            ViewBag.Message = crud.CRUDError;
            ViewBag.Titulo = "Listado de Roles";

            return View("DataListCat", crud.Categoria);
        }

        public IActionResult EditRoles(string id, string RolName)
        {

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            if (id == "0")
            {
                if (crud.InsertRol(RolName) == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Insertado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "Error al Insertar";
                }
            }
            else
            {
                if (crud.UpdatetRol(id, RolName) == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "Error al actualizar";
                }
            }

            return View("DetailCategories");
        }


        /// CRUD Users //////////////////////////////////////

        public IActionResult NewUser()
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            pf.GetPronvinces();

            ViewBag.UserName = "";
            ViewBag.email = "";
            ViewBag.pass = "";
            ViewBag.telf = "";
            ViewBag.Id = "0";
            ViewBag.fecha = "";
            ViewBag.appuser = "";
            ViewBag.province = "";
            ViewBag.genero = "";
            ViewBag.activo = "";
            ViewBag.foto = "/img/no_photo.jpg";
            ViewBag.Provinces = pf.provinces;

            return View("DetailUsers");
        }

        public IActionResult ShowUsers(string ID)
        {
            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

           
            ViewBag.Id = ID;

            ViewBag.UserName = b8.DBLookupEx("view_users", "Name", "User_id", ID);
            ViewBag.email = b8.DBLookupEx("view_users", "Email", "User_id", ID);
            ViewBag.pass = b8.DBLookupEx("view_users", "Password", "User_id", ID);
            ViewBag.telf = b8.DBLookupEx("view_users", "ISNULL(Phone,'0')", "User_id", ID);
            ViewBag.fecha = b8.DBLookupEx("view_users", "ISNULL(Birthday,CONVERT(varchar(30),SYSDATETIME()))", "User_id", ID);
            ViewBag.appuser = b8.DBLookupEx("view_users", "AppUser", "User_id", ID);
            ViewBag.province = b8.DBLookupEx("Users", "Location", "User_id", ID);
            ViewBag.genero = b8.DBLookupEx("Users", "ISNULL(Gender,'1')", "User_id", ID);
            ViewBag.activo = b8.DBLookupEx("Login_Users", "Active", "User_id", ID);
            ViewBag.foto = b8.DBLookupEx("view_users", "PhotoPath", "User_id", ID);

            pf.GetPronvinces();
            ViewBag.Provinces = pf.provinces;


            return View("DetailUsers");
        }

        public IActionResult DeleteUsers(string Id)
        {

            string id = b8.GetItemList(Id,1,",");
            string title = b8.GetItemList(Id, 2, ",");


            string status = b8.DBLookupEx("Login_Users", "Active", "User_Id", id);

            if (status.Equals("0"))
            {
                if (b8.UpdateExpress("Login_Users", "User_Id", id, "Active", "1") == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "No se ha podido actualizar";
                }
            }
            else
            {
                if (b8.UpdateExpress("Login_Users", "User_Id", id, "Active", "0") == true)
                {
                    ViewBag.Type = "Success";
                    ViewBag.Message = "Elemento Actualizado";
                }
                else
                {
                    ViewBag.Type = "Error";
                    ViewBag.Message = "No se ha podido actualizar";
                }
            }

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            if(title == "Listado de Administradores")
            {
                crud.GetUsers("Administrador");
                ViewBag.Titulo = "Listado de Administradores";
            }
            else
            {
                crud.GetUsers("Usuario");
                ViewBag.Titulo = "Listado de Usuarios";
            }

            ViewBag.Message = crud.CRUDError;
            

            return View("DataListUsers", crud.Usr);
        }



        public IActionResult Save(string ID)
        {

            if (b8.UpdateExpress("Login_Users", "User_Id", ID, "Active", "0") == true)
            {
                ViewBag.Type = "Success";
                ViewBag.Message = "Elemento Actualizado";
            }
            else
            {
                ViewBag.Type = "Error";
                ViewBag.Message = "No se ha podido actualizar";
            }

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.name = lg.Usuarios.UserName;
            ViewBag.photo = lg.Usuarios.Photo;
            ViewBag.mail = lg.Usuarios.UserEmail;
            ViewBag.counterror = Convert.ToInt32(b8.DBLookupEx("LogErrors", "Count(Id_Error)", "Active", "1"));
            ViewBag.countNotify = b8.DBLookupEx("Internal_Emails", "Count(Id_Notify)", "Read_Email", "0");

            ViewBag.Id = ID;

            ViewBag.UserName = b8.DBLookupEx("view_users", "Name", "User_id", ID);
            ViewBag.email = b8.DBLookupEx("view_users", "Email", "User_id", ID);
            ViewBag.pass = b8.DBLookupEx("view_users", "Password", "User_id", ID);
            ViewBag.telf = b8.DBLookupEx("view_users", "ISNULL(Phone,'0')", "User_id", ID);
            ViewBag.fecha = b8.DBLookupEx("view_users", "ISNULL(Birthday,CONVERT(varchar(30),SYSDATETIME()))", "User_id", ID);
            ViewBag.appuser = b8.DBLookupEx("view_users", "AppUser", "User_id", ID);
            ViewBag.province = b8.DBLookupEx("Users", "Location", "User_id", ID);
            ViewBag.genero = b8.DBLookupEx("Users", "ISNULL(Gender,'1')", "User_id", ID);
            ViewBag.activo = b8.DBLookupEx("Login_Users", "Active", "User_id", ID);
            ViewBag.foto = b8.DBLookupEx("view_users", "PhotoPath", "User_id", ID);

            pf.GetPronvinces();
            ViewBag.Provinces = pf.provinces;


            return View("DetailUsers");
        }

    }
}
