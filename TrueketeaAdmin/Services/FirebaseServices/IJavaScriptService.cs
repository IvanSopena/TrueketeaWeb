using System.Threading.Tasks;

namespace TrueketeaAdmin.Services.FirebaseServices
{
    public interface IJavaScriptService
    {
        Task<dynamic> FileList();
        Task<int> AddNumbers(int x, int y);
        Task<string> Hello(string name);
        Task<string> Goodbye(string name);
      
    }
}
