

namespace TrueketeaAdmin.Models
{
    public  class Login_Users
    {
       
        public decimal UserId { get; set; }
       
        public decimal RolId { get; set; }
        
        public string UserName { get; set; }
        
        public string UserEmail { get; set; }
       
        public string Password { get; set; }
        
        public string LastConnection { get; set; }
        
        public string Device { get; set; }

        public decimal Active { get; set; }

        public string Photo { get; set; }
    }
}
