using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TrueketeaAdmin.Models;
using TrueketeaAdmin.Services.DB;
using TrueketeaAdmin.Services.FirebaseServices;

namespace TrueketeaAdmin.ViewModels
{




    public class DashboardViewmodel
    {

        
        public DataTable AppUsersDT = new DataTable();
        private DBContext _dbContext;
        private B8Functions B8 = new B8Functions();
        public static List<ProductModel> Pdrt = new List<ProductModel>();
        public string DBError { get; set; }

        public int UsersCount { get; set; }

        public int TotalProducts { get; set; }

        public int TotalTransactions { get; set; }

        public int BlockProducts { get; set; }

        public int FinishedTransactions { get; set; }

        public string AndroidUsers { get; set; }
        public string iOSUsers { get; set; }

        public List<VersionModel> VersionInfo { get; set; }

        public List<ProvinceInfoModel> ProvinceInfo { get; set; }

        private string[] _bloqueados = new string[13];
        public string[] PBloqueados
        {
            get
            {
                return _bloqueados;
            }
        }

        private  string[] _subidos = new string[13];
        public  string[] PSubidos
        {
            get
            {
                return _subidos;
            }
        }

        private string[] _Finalizados = new string[13];
        public string[] PFinalizados
        {
            get
            {
                return _Finalizados;
            }
        }
        private string[] _Proceso = new string[13];
        public string[] PProceso
        {
            get
            {
                return _Proceso;
            }
        }



        public DashboardViewmodel()
        {
            _dbContext = Startup.dbContext;

        }

        public void GetCountUsers()
        {
            string sql = "";
            DataTable UsersDT = new DataTable();

            sql = $"select User_id" +
                $" from {_dbContext.TableOwner}.view_users where Rol_Desc = 'Usuario'";

            if (0 != _dbContext.DbSelect(sql, ref UsersDT))
            {
                DBError = _dbContext.ClsLastError;
            }
            else
            {
                UsersCount = UsersDT.Rows.Count;
            }
        }

        private void GetProducts()
        {
            Pdrt = Startup.mongo.GetProducts();
        }

        public void GetInfoProducts()
        {
            this.GetProducts();

            TotalProducts = Pdrt.Count;

            var allItemsFinised = Pdrt
                    .Where(tdi => tdi.Estado.Equals("Finalizado"))
                    .ToList();

            FinishedTransactions = allItemsFinised.Count;


            var allTransactions = Pdrt
                    .Where(tdi => tdi.Estado.Equals("EnProceso"))
                    .ToList();

            TotalTransactions = allTransactions.Count;


            var ProductsBlocks = Pdrt
                   .Where(tdi => tdi.Estado.Equals("Bloqueado"))
                   .ToList();

            BlockProducts = ProductsBlocks.Count;

        }

        public void GetOS()
        {
            AndroidUsers = B8.DBLookupEx("view_users", "count(Device)", "Device", "Android");

            iOSUsers = B8.DBLookupEx("view_users", "count(Device)", "Device", "iOS");
        }

        public void GetVersions()
        {
            string sql;
            DataTable dt_version = new DataTable();

            sql = "SELECT  T1.Version_number VN, count(T2.AppVersion) VT " +
                    $"From {_dbContext.TableOwner}.AppVersion AS T1 LEFT OUTER JOIN " +
                         $"{_dbContext.TableOwner}.Login_Users AS T2 ON T2.AppVersion = T1.Version_number" +
                    " Group by T1.Version_number";

            if (0 != _dbContext.DbSelect(sql, ref dt_version))
            {
                DBError = _dbContext.ClsLastError;
            }
            else
            {
                int totales = dt_version.Rows.Count;
                VersionInfo = dt_version.Rows.OfType<DataRow>().
                    Select(x => new VersionModel()
                    {
                        Version = x["VN"].ToString(),
                        VersionCount = x["VT"].ToString(),
                        TotalPercent = Math.Round(Convert.ToDecimal(totales * Convert.ToInt32(x["VT"].ToString())), 2)

                    }).ToList();
            }
        }
    
        public void GetProvinceInfo()
        {
            string sql;
            DataTable dt_info = new DataTable();

            sql = "SELECT  T1.Internal_Code IDP,T1.Province_Name VN, count(T2.Location) VT " +
                    $"From {_dbContext.TableOwner}.SpainLocation T1, " +
                         $"{_dbContext.TableOwner}.Users T2 where T1.Id_Province = T2.Location " +
                    " Group by T1.Internal_Code,T1.Province_Name ";

            if (0 != _dbContext.DbSelect(sql, ref dt_info))
            {
                DBError = _dbContext.ClsLastError;
            }
            else
            {

                ProvinceInfo = dt_info.Rows.OfType<DataRow>().
                    Select(x => new ProvinceInfoModel()
                    {
                        InternalCode = x["IDP"].ToString(),
                        Name = x["VN"].ToString(),
                        TotalUsers = x["VT"].ToString()

                    }).ToList();
            }
        }
    
    
       public void GetHistoryProduct()
        {
            this.charge(_subidos,"1");
            this.charge(_Proceso, "2");
            this.charge(_Finalizados, "3");
            this.charge(_bloqueados, "4");

        }


        private void charge (string[]data,string status) //DataTable snap,
        {
            int i ;
            for (i =0; i <= data.Length-1; i++)
            {
                int mes = i + 1;
                string valor = "" + B8.DBLookupEx("HistoricalActivities", "Count(StatusId)", "StatusId", status, "MONTH (TransDate)", mes.ToString());

                if(valor != "")
                {
                    if (i == 12)
                    {
                        data[i] = valor;
                    }
                    else
                    {
                        data[i] = valor + ",";
                    }
                }
           
            }


          
        }

    }

}
