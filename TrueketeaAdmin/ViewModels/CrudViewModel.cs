using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TrueketeaAdmin.Models;
using TrueketeaAdmin.Services.DB;
using TrueketeaAdmin.Services.FirebaseServices;
using TrueketeaAdmin.Services.Security;

namespace TrueketeaAdmin.ViewModels
{
    public class CrudViewModel
    {

        private DBContext _dbContext;
        private B8Functions b8 = new B8Functions();
        private readonly Encrypting Encrypt = new Encrypting();
        public List<ProvincesModel> provinces = new List<ProvincesModel>();
        public List<ViewUserModel> Usr = new List<ViewUserModel>();
        public List<RolesModel> rol = new List<RolesModel>();
        public List<LogErrorsModel> Errors = new List<LogErrorsModel>();
        public List<SubcategoriesModel> Subcategoria = new List<SubcategoriesModel>();
        public List<CategoriesModel> Categoria = new List<CategoriesModel>();
        public List<ProductModel> Pdrt = new List<ProductModel>();
        public List<NotificationModel> MSG = new List<NotificationModel>();
        public ProductModel MProd = new ProductModel();
        public NotiffyModel noti = new NotiffyModel();

        public string CRUDError { get; set; }
        public string CRUDtype { get; set; }


        public CrudViewModel(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void GetErr()
        {
            string sql;
            DataTable ErrDT = new DataTable();

            sql = $"select Id_Error,App_Name,Method,Error_Desc,IIF(Active = '1','Activo','Reconocido') Active,Class,SQL_Sentence from" +
                $" {_dbContext.TableOwner}.LogErrors ";

            if (0 == _dbContext.DbSelect(sql, ref ErrDT))
            {

                Errors = ErrDT.Rows.OfType<DataRow>().
                    Select(x => new LogErrorsModel()
                    {
                        ErrorId = (decimal)x["Id_Error"],
                        AppName = x["App_Name"].ToString(),
                        Method = x["Method"].ToString(),
                        ErrDesc = x["Error_Desc"].ToString(),
                        Active = x["Active"].ToString(),
                        Class = x["Class"].ToString(),
                        SQL = x["SQL_Sentence"].ToString()

                    }).ToList();

            }
            else
            {
                CRUDError = _dbContext.ClsLastError;
                CRUDtype = "Error";
            }
        }

        public void GetCategories()
        {
            string sql;
            DataTable CatDT = new DataTable();

            sql = $"select Id_Categorie,Categorie_Desc,Icon,IIF(Active = '1','Activo','No Activo') Active from" +
                $" {_dbContext.TableOwner}.Categories ";

            if (0 == _dbContext.DbSelect(sql, ref CatDT))
            {

                Categoria = CatDT.Rows.OfType<DataRow>().
                    Select(x => new CategoriesModel()
                    {
                        CatId = x["Id_Categorie"].ToString(),
                        CatDesc = x["Categorie_Desc"].ToString(),
                        CatIcon = x["Icon"].ToString(),
                        Estado = x["Active"].ToString()

                    }).ToList();

            }
            else
            {
                CRUDError = _dbContext.ClsLastError;
                CRUDtype = "Error";
            }
        }

        public void GetSubcategories()
        {
            string sql;
            DataTable SubCatDT = new DataTable();

            sql = $"select Id_SubCat,Subcat_Desc,IIF(Active = '1','Activo','No Activo') Active from" +
                $" {_dbContext.TableOwner}.Subcategories ";

            if (0 == _dbContext.DbSelect(sql, ref SubCatDT))
            {

                Subcategoria = SubCatDT.Rows.OfType<DataRow>().
                    Select(x => new SubcategoriesModel()
                    {
                        SubcatId = x["Id_SubCat"].ToString(),
                        SubcatDesc = x["Subcat_Desc"].ToString(),
                        Estado = x["Active"].ToString()

                    }).ToList();

            }
            else
            {
                CRUDError = _dbContext.ClsLastError;
                CRUDtype = "Error";
            }
        }

        public void GetRoles()
        {
            string sql;
            DataTable RolDT = new DataTable();

            sql = $"select Rol_Id,Rol_Desc from" +
                $" {_dbContext.TableOwner}.Users_Roles ";

            if (0 == _dbContext.DbSelect(sql, ref RolDT))
            {

                rol = RolDT.Rows.OfType<DataRow>().
                    Select(x => new RolesModel()
                    {
                        RolId = x["Rol_Id"].ToString(),
                        RolDesc = x["Rol_Desc"].ToString()
                      
                    }).ToList();

            }
            else
            {
                CRUDError = _dbContext.ClsLastError;
                CRUDtype = "Error";
            }
        }

        public void GetUsers(string rol)
        {
            string sql = "";
            DataTable UsersDT = new DataTable();

            sql = $"select User_id,Name,Email,Password,PhotoPath,AppUser,IIF(Gender = '1','H','M') AS Gen," +
                $"Province_Name,CONVERT(varchar(30),Birthday, 103) as Cumple ,Phone,Rol_Desc,IIF(Active = '1','Activo','No Activo') Active,Device,CONVERT(varchar(30),Last_Conection, 103) as LastConnect " +
                $" from {_dbContext.TableOwner}.view_users where Rol_Desc = '{rol}'";

            if (0 != _dbContext.DbSelect(sql, ref UsersDT))
            {
                CRUDError = _dbContext.ClsLastError;
                CRUDtype = "Error";
            }
            else
            {
                Usr = UsersDT.Rows.OfType<DataRow>().
                Select(x => new ViewUserModel()
                {
                        UserId = (decimal)x["User_id"],
                        Name = x["Name"].ToString(),
                        Email = x["Email"].ToString(),
                        Password = x["Password"].ToString(),
                        Photo = x["PhotoPath"].ToString(),
                        AppName = x["AppUser"].ToString(),
                        Gender = x["Gen"].ToString(),
                        Location = x["Province_Name"].ToString(),
                        Birthday = x["Cumple"].ToString(),
                        Phone = (decimal)x["Phone"],
                        Rol = x["Rol_Desc"].ToString(),
                        Activo = x["Active"].ToString(),
                        Device = x["Device"].ToString(),
                        LastConnection = x["LastConnect"].ToString()

                }).ToList();

            }

            
        }

        public void GetPronvinces()
        {
            string sql;
            DataTable ProvinceDT = new DataTable();

            sql = $"select Id_Province,Province_Name,Internal_Code from" +
                $" {_dbContext.TableOwner}.SpainLocation ";

            if (0 == _dbContext.DbSelect(sql, ref ProvinceDT))
            {

                provinces = ProvinceDT.Rows.OfType<DataRow>().
                    Select(x => new ProvincesModel()
                    {
                        Id = x["Id_Province"].ToString(),
                        Desc = x["Province_Name"].ToString(),
                        InternalCode = x["Internal_Code"].ToString()

                    }).ToList();

            }
            else
            {
                CRUDError = _dbContext.ClsLastError;
            }


        }

        public void GetProducts()
        {
            Pdrt = Startup.mongo.GetProducts();
        }

        public void DetailProduct(ObjectId ID)
        {
            Pdrt = Startup.mongo.DetailProduct(ID);

        }

        public void GetNotify ()
        {
            string sql = "";
            DataTable Notify = new DataTable();

            sql = $"Select  ie.Id_Notify as id, L.Name as name,ie.Message as msg,convert(varchar(30),ie.SendDate) as time_dif,Subject  " +
                $" From {_dbContext.TableOwner}.Internal_Emails AS ie  LEFT OUTER JOIN {_dbContext.TableOwner}.view_users  AS L ON L.User_id = ie.Id_Emisor " +
                $" Where ie.Status_id = 1";

            if (0 == _dbContext.DbSelect(sql, ref Notify))
            {

                MSG = Notify.Rows.OfType<DataRow>().
                    Select(x => new NotificationModel()
                    {
                        Id = Convert.ToInt32(x["id"].ToString()),
                        Emisor = x["name"].ToString(),
                        Message = x["msg"].ToString(),
                        subject = x["Subject"].ToString(),  //.Substring(1,14)
                        Date_Diff = x["time_dif"].ToString()

                    }).ToList();
            }
            else
            {
                CRUDError = _dbContext.ClsLastError;
                CRUDtype = "Error";
            }
        }

        public void GetUpdateInbox(string id)
        {
            b8.UpdateExpress("Internal_Emails", "Id_Notify", id, "Read_Email", "1");
            GetNotify();
        }

        public void SendEmail(string textarea, string to, string subject, decimal id)
        {
            string sql = "";
            int affectedRows = 0;
            string thisDay = DateTime.Today.ToString("dd/MM/yyyy");
            string Notyfy_id = b8.DBLookupEx("Internal_Emails", "Max(id_Notify) + 1", "1", "1");
            string receptor_id = b8.DBLookupEx("view_users", "User_id", "Email", to);

            sql = $"Insert into {_dbContext.TableOwner}.Internal_Emails (id_Notify,id_Emisor,id_Receptor,Message,status_id,Read_Email,SendDate,Subject) ";
            sql = sql + $" values('{Notyfy_id}','{id}','{receptor_id}','{textarea}'," +
                $"'1','0',convert(datetime,{thisDay},103),'{subject}' )";


            if (0 == _dbContext.DbExecute(sql, ref affectedRows))
            {
                CRUDError = "Mensaje Enviado";
                CRUDtype = "Success";
            }
            else
            {
                CRUDError = _dbContext.ClsLastError;
                CRUDtype = "Error";
            }
        }
   
    
        public bool InsertSC(string desc,string name)
        {
            string id =  b8.DBLookupEx("Subcategories", "Max(Id_SubCat) + 1 ");
            string sql = "";
            int afectedRow = 0;

            sql = $"Insert into {_dbContext.TableOwner}.Subcategories (Id_SubCat,Subcat_Desc,Subcat_Name,Active) ";
            sql = sql + $"Values ('{id}','{desc}','{name}','1')";

            if (0 == _dbContext.DbExecute(sql, ref afectedRow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatetSC(string id, string desc, string name)
        {
           
            string sql = "";
            int afectedRow = 0;

            sql = $"Update {_dbContext.TableOwner}.Subcategories set Subcat_Desc = '{desc}',Subcat_Name = '{name}' where Id_SubCat = '{id}' ";
           
            if (0 == _dbContext.DbExecute(sql, ref afectedRow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool InsertCat(string desc, string name)
        {
            string id = b8.DBLookupEx("Categories", "Max(Id_Categorie) + 1 ");
            string sql = "";
            int afectedRow = 0;

            sql = $"Insert into {_dbContext.TableOwner}.Categories (Id_Categorie,Icon,Categorie_Desc,Active) ";
            sql = sql + $"Values ('{id}','{name}','{desc}','1')";

            if (0 == _dbContext.DbExecute(sql, ref afectedRow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatetCat(string id, string desc, string name)
        {

            string sql = "";
            int afectedRow = 0;

            sql = $"Update {_dbContext.TableOwner}.Categories set Categorie_Desc = '{desc}',Icon = '{name}' where Id_Categorie = '{id}' ";

            if (0 == _dbContext.DbExecute(sql, ref afectedRow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool InsertProvince(string InternalCode, string ProvinceName)
        {
            string id = b8.DBLookupEx("SpainLocation", "Max(Id_Province) + 1 ");
            string sql = "";
            int afectedRow = 0;

            sql = $"Insert into {_dbContext.TableOwner}.SpainLocation (Id_Province,Province_Name,Internal_Code) ";
            sql = sql + $"Values ('{id}','{ProvinceName}','{InternalCode}')";

            if (0 == _dbContext.DbExecute(sql, ref afectedRow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatetProvince(string id, string InternalCode, string ProvinceName)
        {

            string sql = "";
            int afectedRow = 0;

            sql = $"Update {_dbContext.TableOwner}.SpainLocation set Province_Name = '{ProvinceName}',Internal_Code = '{InternalCode}' where Id_Province = '{id}' ";

            if (0 == _dbContext.DbExecute(sql, ref afectedRow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool DeleteRol (string id)
        {
            string sql = "";
            int afectedRow = 0;

            sql = $"Delete  from {_dbContext.TableOwner}.Users_Roles Where Rol_Id = '{id}'";

            if (0 == _dbContext.DbExecute(sql, ref afectedRow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool InsertRol(string RolName)
        {
            string id = b8.DBLookupEx("Users_Roles", "Max(Rol_Id) + 1 ");
            string sql = "";
            int afectedRow = 0;

            sql = $"Insert into {_dbContext.TableOwner}.Users_Roles (Rol_Id,Rol_Desc) ";
            sql = sql + $"Values ('{id}','{RolName}')";

            if (0 == _dbContext.DbExecute(sql, ref afectedRow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdatetRol(string id, string RolName)
        {

            string sql = "";
            int afectedRow = 0;

            sql = $"Update {_dbContext.TableOwner}.Users_Roles set Rol_Desc = '{RolName}' where Rol_Id = '{id}' ";

            if (0 == _dbContext.DbExecute(sql, ref afectedRow))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
