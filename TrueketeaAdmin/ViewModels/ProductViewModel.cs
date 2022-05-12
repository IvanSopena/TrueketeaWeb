using Google.Cloud.Firestore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TrueketeaAdmin.Models;
using TrueketeaAdmin.Services.FirebaseServices;
using TrueketeaAdmin.key;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TrueketeaAdmin.ViewModels
{
    public class ProductViewModel
    {
        private IHostEnvironment _env;
        private IJavaScriptService _javascriptService;
        private readonly FirestoreDb datbase;
        private readonly B8Functions b8 = new B8Functions();
        public List<ProductModel> ListaProductos = new List<ProductModel>();
        public ProductModel model;

        public string ProductError { get; set; }

        public ProductViewModel(IHostEnvironment env, IJavaScriptService javaScriptService)
        {
            _javascriptService = javaScriptService;
            _env = env;
            string keyPath = Path.Combine(_env.ContentRootPath, "key\\trueketea-bd250-firebase-adminsdk-atg4m-d1234d9a36.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", keyPath);
            datbase = FirestoreDb.Create(FirebaseSettings.projectId);
        }


        public async Task<bool> AllProducts()
        {
            try
            {
                Query query = datbase.Collection("Productos");
                QuerySnapshot documentSnapshots = await query.GetSnapshotAsync();
               

                foreach (DocumentSnapshot item in documentSnapshots.Documents)
                {
                    if (item.Exists)
                    {
                        Dictionary<string, object> product = item.ToDictionary();
                        string jsonUser = JsonConvert.SerializeObject(product);
                        model = JsonConvert.DeserializeObject<ProductModel>(jsonUser);

                        ListaProductos.Add(model);
                    }
                }

                return true;
               

            }
            catch (Exception ex)
            {
                ProductError = ex.Message;
                b8.Log_Error("AllProducts", "ProductsViewModel", ex.Message);

                return false; 
            }

        }



    }
}

