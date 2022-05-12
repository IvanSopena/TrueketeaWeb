using Firebase.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TrueketeaAdmin.Services.FirebaseServices
{
    public class FBStorage
    {
        public string PhotoLink { get; set; }
        public FirebaseStorage Fbs;
        public FBStorage()
        {
            Fbs = new FirebaseStorage("trueketea-bd250.appspot.com", new FirebaseStorageOptions
            {
                ThrowOnCancel = true
            });

        }


        public async Task<string> Addphoto(string photopath, string folderUser, string folderProduct, string filename)
        {
            try
            {
                var stream =File.Open(photopath, FileMode.Open);

                var task = Fbs.Child(folderUser).Child(folderProduct).Child(filename).PutAsync(stream);

                return await task;

            }
            catch(Exception ex)
            {
                string err = ex.Message;
                return null;
            }
            

        }
    }
}
