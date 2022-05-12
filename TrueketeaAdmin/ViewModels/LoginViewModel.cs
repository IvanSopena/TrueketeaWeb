using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TrueketeaAdmin.Models;
using TrueketeaAdmin.Services.DB;
using TrueketeaAdmin.Services.Security;

namespace TrueketeaAdmin.ViewModels
{
    public class LoginViewModel
    {

        public Login_Users Usuarios = new Login_Users();
        public DataTable AppUsersDT = new DataTable();
        private DBContext _dbContext;
        private readonly Encrypting Encrypt = new Encrypting();
        public string LoginError { get; set; }
        public int ErrorsCount { get; set; }
        public string NotifyCount { get; set; }



        public LoginViewModel(DBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public int LoadUsers (string User)
        {  
            string Sql = "select User_id, Rol_id,User_Name ,User_Email,Password,ISNULL(Last_Conection, ''),Device,Active,Photo " +
                " from " + _dbContext.TableOwner + ".Login_Users where upper(User_Email) = '" + User.ToUpper() + "'";

            if (0 != _dbContext.DbSelect(Sql, ref AppUsersDT))
            {
                return 1;
            }

            return 0;
        }


        public int AppOpen(string AppUser, string AppPwd)
        {
            try
            {

                if (this.LoadUsers(AppUser) == 0)
                {
                    if(AppUsersDT.Rows.Count == 0)
                    {
                        LoginError = "Usuario de la aplicación no encontrado.";
                        return 1;
                    }
                    else
                    {
                        Usuarios.UserId = (decimal)AppUsersDT.Rows[0][0];
                        Usuarios.RolId = (decimal)AppUsersDT.Rows[0][1];
                        Usuarios.UserName = (string)AppUsersDT.Rows[0][2];
                        Usuarios.UserEmail = (string)AppUsersDT.Rows[0][3];
                        Usuarios.Password = (string)AppUsersDT.Rows[0][4];
                        //Usuarios.LastConnection = (string)AppUsersDT.Rows[0][5];
                        Usuarios.Device = (string)AppUsersDT.Rows[0][6];
                        Usuarios.Active = (decimal)AppUsersDT.Rows[0][7];
                        Usuarios.Photo =  (string)AppUsersDT.Rows[0][8];

                        if (Usuarios.Active == 0)
                        {
                            LoginError = "El Usuario " + Usuarios.UserEmail + ", no esta habilitado en la aplicación.";
                            return 1;
                        }

                        string pwd = Encrypt.Decrypt(Usuarios.Password, AppUser);

                        if (pwd.Equals(AppPwd) == false)
                        {
                            LoginError = "La contraseña no es correcta, vuelve a intentarlo.";

                            return 1;
                        }

                    }

                    this.SearchCounters();
                }
                else
                {
                    LoginError = "UPS!! Ha habido un error al búscar el usuario.";
                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                LoginError = "UPS!! " + ex.Message;
                return 1;
            }
        }

        public void SearchCounters()
        {
            DataTable Err = new DataTable();
            DataTable Notify = new DataTable();
            string sql = "select Count(Id_Error) " +
                " from " + _dbContext.TableOwner + ".LogErrors";

            if (0 != _dbContext.DbSelect(sql, ref Err))
            {
                this.ErrorsCount = 0;
            }
            else
            {
                this.ErrorsCount = (int)Err.Rows[0][0];
            }

             sql = "select Count(Id_Notify) " +
                " from " + _dbContext.TableOwner + ".Internal_Emails where Read_Email = '0'";

            if (0 != _dbContext.DbSelect(sql, ref Notify))
            {
                this.NotifyCount = "0";
            }
            else
            {
                this.NotifyCount = Notify.Rows[0][0].ToString();
            }


        }

        public int LoadAllUsers()
        {

            string Sql = "select User_id, Rol_id,User_Name ,User_Email,Password,First_Entry,Device,Active,Photo " +
                " from " + _dbContext.TableOwner + ".Login_Users";

            if (0 != _dbContext.DbSelect(Sql, ref AppUsersDT))
            {
                return 1;
            }

            return 0;
        }

    }
}
