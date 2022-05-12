using System;
using System.Text.Json.Serialization;

namespace TrueketeaAdmin.key
{
    public class FirebaseSettings
    {

        public static string ApiKey = "AIzaSyCu4Lb4YLJdF4rcxm_cnkSzZLc5vWl30b0";
        public static string Bucket = "trueketea-bd250-default-rtdb.europe-west1.firebasedatabase.app";
        public static string BucketFile = "trueketea-bd250.appspot.com";
        public static string projectId = "trueketea-bd250";

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }



        //[JsonPropertyName("project_id")]
        //public string ProjectId => "trueketea-bd250";

        //[JsonPropertyName("private_key_id")]
        //public string PrivateKeyId => "AIzaSyDUUq6-RrEZ5asHs6xtErnsvE9wgVvoz5w";

    }


//    const firebaseConfig = {
//  apiKey: "AIzaSyCu4Lb4YLJdF4rcxm_cnkSzZLc5vWl30b0",
//  authDomain: "trueketea-bd250.firebaseapp.com",
//  databaseURL: "https://trueketea-bd250-default-rtdb.europe-west1.firebasedatabase.app",
//  projectId: "trueketea-bd250",
//  storageBucket: "trueketea-bd250.appspot.com",
//  messagingSenderId: "365832877438",
//  appId: "1:365832877438:web:b4ad984e4594224375af2f"
//};

}
