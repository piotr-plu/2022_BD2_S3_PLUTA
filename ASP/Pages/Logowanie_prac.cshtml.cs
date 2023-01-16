using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Narciarze_v_2.Pages
{
    public class LogowaniePracModel : PageModel
    {
//        public const string SessionKeyAge = "_Age";

        private readonly ILogger<LogowanieModel> _logger;

        public LogowaniePracModel(ILogger<LogowanieModel> logger)
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
            Pracownik cur_prac = new Pracownik();
            login.email = Request.Form["email"];
            login.haslo = Request.Form["haslo"];
            string query = "select * from Pracownicy where Email='"+login.email+"' and Haslo='"+login.haslo+"' ";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        cur_prac.email = reader["EMail"].ToString();
                        cur_prac.imie = reader["Imie"].ToString();
                        cur_prac.nazwisko = reader["Nazwisko"].ToString();
                        cur_prac.id = Int32.Parse(reader["ID"].ToString());
                        cur_prac.uid = Int32.Parse(reader["Poz_uprawnien"].ToString());

                        }
                    }
                    HttpContext.Session.SetInt32("_prac_id", cur_prac.id);
                    HttpContext.Session.SetInt32("_prac_uid", cur_prac.uid);
                    HttpContext.Session.SetString("_prac_imie", cur_prac.imie);
                    HttpContext.Session.SetString("_prac_nazwisko", cur_prac.nazwisko);
                    HttpContext.Session.SetString("_prac_email", cur_prac.email);
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
        public class Pracownik
        {
            public string imie, nazwisko, email;
            public int id, uid;
        }

    }
}
