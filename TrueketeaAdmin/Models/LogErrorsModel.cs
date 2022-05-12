namespace TrueketeaAdmin.Models
{
    public class LogErrorsModel
    {
        public decimal ErrorId { get; set; }

        public string AppName { get; set; }

        public string Method { get; set; }

        public string ErrDesc { get; set; }

        public string Active { get; set; }

        public string Class { get; set; }

        public string SQL { get; set; }

    }
}
