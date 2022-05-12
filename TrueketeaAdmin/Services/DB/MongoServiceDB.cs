using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using TrueketeaAdmin.Models;

namespace TrueketeaAdmin.Services.DB
{
    public class MongoServiceDB
    {

        readonly static string dbName = "Trueketea";
        B8Functions B8 = new B8Functions();
        static MongoClient client;
        private string conx_str;

        public MongoServiceDB()
        {
            try
            {
                    switch(System.Environment.MachineName)
                    {
                        case "ES5CD0179Z6R":
                            conx_str = "mongodb://localhost:27017";
                            break;

                    default:
                            conx_str = "mongodb://192.168.1.51:27017";
                            break;
                    }

                    var conx = conx_str;
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                       new MongoUrl(conx)
                   );
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
                    client = new MongoClient(settings);
                
                
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                B8.Log_Error("Constructor", "MongoServiceDB", msg);
            }
        }


        public List<ProductModel> DetailProduct(ObjectId ID)
        {
            try
            {
                IMongoCollection<ProductModel> ProductsCollection;
                var db = client.GetDatabase(dbName);

                ProductsCollection = db.GetCollection<ProductModel>("Products");

                var allItems = ProductsCollection.AsQueryable<ProductModel>()
                    .Where(tdi => tdi.Id.Equals(ID))
                    .ToList();

                return allItems;
            }

            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                B8.Log_Error("GetProductsCath", "MongoServiceDB", msg);
                return null;
            }
        }

        public List<ProductModel> GetProducts()
        {
            try
            {
                IMongoCollection<ProductModel> ProductsCollection;
                var db = client.GetDatabase(dbName);

                ProductsCollection = db.GetCollection<ProductModel>("Products");

                var allItems = ProductsCollection.AsQueryable<ProductModel>().ToList();

                return allItems;
            }

            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                B8.Log_Error("GetProducts", "MongoServiceDB", msg);
                return null;
            }
        }


        public List<ProductModel> GetProductsCath(String cat_name)
        {
            try
            {
                IMongoCollection<ProductModel> ProductsCollection;
                var db = client.GetDatabase(dbName);

                ProductsCollection = db.GetCollection<ProductModel>("Products");

                var allItems = ProductsCollection.AsQueryable<ProductModel>()
                    .Where(tdi => tdi.Categoria.Contains(cat_name))
                    .ToList();

                return allItems;
            }

            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                B8.Log_Error("GetProductsCath", "MongoServiceDB", msg);
                return null;
            }
        }


        public bool InsertNotify(NotiffyModel notify)
        {
            notify.Id = ObjectId.GenerateNewId();
            try
            {
                IMongoCollection<NotiffyModel> ProductsCollection;
                var db = client.GetDatabase(dbName);
                ProductsCollection = db.GetCollection<NotiffyModel>("Notifications");
                ProductsCollection.InsertOne(notify);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                B8.Log_Error("InsertNotify", "MongoServiceDB", msg);
                return false;
            }
            return true;
        }


        public bool InsertProduct(ProductModel prod)
        {
            prod.Id = ObjectId.GenerateNewId();
            try
            {
                IMongoCollection<ProductModel> ProductsCollection;
                var db = client.GetDatabase(dbName);

                ProductsCollection = db.GetCollection<ProductModel>("Products");

                ProductsCollection.InsertOne(prod);

                return true;

            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                B8.Log_Error("InsertProduct", "MongoServiceDB", msg);
                return false;
            }


        }


        public bool UpdateProduct(ProductModel prod)
        {

            try
            {
                IMongoCollection<ProductModel> ProductsCollection;
                var db = client.GetDatabase(dbName);

                ProductsCollection = db.GetCollection<ProductModel>("Products");

                if (ProductsCollection.ReplaceOne(tdi => tdi.Id == prod.Id, prod).ModifiedCount == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                B8.Log_Error("InsertProduct", "MongoServiceDB", msg);
                return false;
            }

        }







    }


}

