using System.Collections.Generic;
using System.Data;
using System.Linq;
using TrueketeaAdmin.Models;
using TrueketeaAdmin.Services.DB;
using TrueketeaAdmin.Services.Security;

namespace TrueketeaAdmin.ViewModels
{
    public class ProfileViewModel
    {

        private DBContext _dbContext;
        public UsersModel Usr = new UsersModel();
        private B8Functions B8 = new B8Functions();
        private readonly Encrypting Encrypt = new Encrypting();
        public List<ProvincesModel> provinces = new List<ProvincesModel>(); 
        public string ProfileError { get; set; }

        public ProfileViewModel(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool GetUsers(string email)
        {
            string sql = "";
            DataTable UsersDT = new DataTable();

            sql = $"select User_id,ISNULL(Name,''),ISNULL(Surname,''),Email,Password,PhotoPath,AppUser,ISNULL(Gender,'1'),ISNULL(Location,''),ISNULL(Birthday,CONVERT(varchar(30),SYSDATETIME())),ISNULL(Phone,'0') " +
                $" from {_dbContext.TableOwner}.Users where Email = '{email}'";

            if (0 != _dbContext.DbSelect(sql, ref UsersDT))
            {
                ProfileError = "Error: " + _dbContext.DbLastError;
                Usr.UserId = 0;
                Usr.Name = "";
                Usr.Surname = "";
                Usr.Email = "";
                Usr.Password = "";
                Usr.Photo = "";
                Usr.AppName = "";
                Usr.Gender = "0";
                Usr.Location = "";
                Usr.Birthday = "";
                Usr.Phone = 0;
                return false;
            }
            else
            {
                Usr.UserId = (decimal)UsersDT.Rows[0][0];
                Usr.Name = (string)UsersDT.Rows[0][1];
                Usr.Surname = (string)UsersDT.Rows[0][2];
                Usr.Email = (string)UsersDT.Rows[0][3];
                Usr.Password = Encrypt.Decrypt((string)UsersDT.Rows[0][4], (string)UsersDT.Rows[0][3]);
                Usr.Photo = (string)UsersDT.Rows[0][5];
                Usr.AppName = (string)UsersDT.Rows[0][6];
                Usr.Gender = (string)UsersDT.Rows[0][7];
                Usr.Location = (string)UsersDT.Rows[0][8];
                Usr.Birthday = B8.GetItemList(UsersDT.Rows[0][9].ToString(), 1, " "); 
                Usr.Phone = (decimal)UsersDT.Rows[0][10];


                
            }

            return true;    
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
        }


        public bool UpdateProfile(string name, string surname, string email, string AppName, string password, string phone, string time, string place, string gener)
        {
            int affectedRow = 0;
            string SQL;

            SQL = $"Update {_dbContext.TableOwner}.Users set Name = '{name}', Surname = '{surname}',Password = '{password}'," +
                $" AppUser = '{AppName}',Phone = '{phone}',Birthday = CONVERT(date,'{time}'),Gender = '{gener}',Location = '{place}' " +
                $" Where Email = '{email}'";

            if (0 != Startup.dbContext.DbExecute(SQL, ref affectedRow))
            {
                string err = Startup.dbContext.DbLastError;
                return false;
            }

            if (affectedRow == 0)
            {
                return false;
            }

            return true;

        }

    }
}
