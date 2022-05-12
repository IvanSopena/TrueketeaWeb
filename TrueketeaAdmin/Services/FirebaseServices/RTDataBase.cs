using Firebase.Database;
using System.Threading.Tasks;

namespace TrueketeaAdmin.Services.FirebaseServices
{
    public class RTDataBase
    {

        FirebaseClient firebaseClient = new FirebaseClient(
          "https://trueketea-bd250-default-rtdb.europe-west1.firebasedatabase.app/",
          new FirebaseOptions
          {
              AuthTokenAsyncFactory = () => Task.FromResult("7Vha89tEcSjTpUO324p8WyqeHmXXml377C3qXVZI")
          });





    }
}
