using TrueketeaAdmin.Services.DB;
using TrueketeaAdmin.Services.SendEmail;

namespace TrueketeaAdmin.ViewModels
{
    public class RestorePassViewModel
    {
        private DBContext _dbContext;
        private SendEmail Email;
        private string Usurario;
        public string RestoreError { get; set; }

        public RestorePassViewModel(DBContext dbContext, string user)
        {
            _dbContext = dbContext;
            Usurario = user;
        }

        public bool ChangePass()
        {
            bool result;
            if (this.Verify_Email() == true)
            {
                Email = new SendEmail(Usurario);

               if(Email.Send_Email(0)== true)
                {
                    //Actualizar la contraseña en la tabla y retornar mensaje de verificacion
                    string sql;
                    int update = 0;

                    sql = $"Update  { _dbContext.TableOwner}.Users set Password = '{Email.Password}' where Email = '{Usurario}'";

                    if (0 != _dbContext.DbExecute(sql,ref update))
                    {
                        RestoreError = "Error al actualizar la contraseña: " + _dbContext.DbLastError;
                        result = false;
                    }

                    else
                    {
                        if(update > 0)
                        {
                            RestoreError = "Contraseña enviada. Revise su Email donde encontrara la nueva contraseña.";
                            result = true;
                        }
                        else
                        {
                            RestoreError = "Error al actualizar la contraseña, Email no encontrado.";
                            result = false;
                        }
                    }
                }
                else
                {
                    RestoreError =  Email.EmailError;
                    result = false;
                }

            }

            else
            {
                RestoreError = "La dirección de Email introducida no esta dada de alta en el sistema.";

                result = false;
            }

            return result;
        }

        private bool Verify_Email ()
        {
            bool result = true ;


            B8Functions fc = new B8Functions();


            string Email_Exist = fc.DBLookupEx(_dbContext.TableOwner + ".Login_Users", "COUNT(*)", "upper(User_Email)", Usurario.ToUpper());


            if(Email_Exist.Equals("0"))
            {
                result = false;
            }

            return result;
        }
    }
}
