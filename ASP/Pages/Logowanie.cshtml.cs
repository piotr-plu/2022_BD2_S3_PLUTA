using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Narciarze_v_2.Pages
{
    public class LogowanieModel : PageModel
    {
//        public const string SessionKeyAge = "_Age";

        private readonly ILogger<LogowanieModel> _logger;

        public LogowanieModel(ILogger<LogowanieModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {

        }
        public void OnPost()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            Login login = new Login();
            Klient cur_klient = new Klient();
            login.email = Request.Form["email"];
            login.haslo = Request.Form["haslo"];
            string query = "select * from Klient where Email='"+login.email+"' and haslo='"+login.haslo+"' ";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        cur_klient.email = reader["EMail"].ToString();
                        cur_klient.imie = reader["Imie"].ToString();
                        cur_klient.nazwisko = reader["Nazwisko"].ToString();
                        cur_klient.id = Int32.Parse(reader["ID"].ToString());

                        }
                    }
                    HttpContext.Session.SetInt32("_klient_id", cur_klient.id);
                    HttpContext.Session.SetString("_klient_imie", cur_klient.imie);
                    HttpContext.Session.SetString("_klient_nazwisko", cur_klient.nazwisko);
                    HttpContext.Session.SetString("_klient_email", cur_klient.email);
                }
                catch
                {
                    
                }
            }
        }
        public class Login
        {
            public string email, haslo;
        }
        public class Klient
        {
            public string imie, nazwisko, email;
            public int id;
        }

    }
}
