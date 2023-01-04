using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations;
using static Narciarze_v_2.Pages.Strefa_Klienta.CennikModel;

namespace Narciarze_v_2.Pages.Strefa_Klienta
{
    public class RejestracjaModel : PageModel
    {
        public List<Rejestracja> reg = new List<Rejestracja>();
        public void OnGet()
        {
        }
        public void OnPost()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-L54I9S2\\NARCIARZE;Initial Catalog=narty;Integrated Security=True");
            conn.Open();
            Rejestracja r1 = new Rejestracja();
            r1.imie = Request.Form["imie"];
            r1.nazwisko = Request.Form["nazw"];
            r1.email = Request.Form["email"];
            r1.haslo = Request.Form["haslo"];
            string query = "INSERT INTO Klient (Imie, Nazwisko, EMail, Haslo) VALUES ('"+r1.imie+"','"+r1.nazwisko+"','"+r1.email+"','"+r1.haslo+"')";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.ExecuteNonQuery();
            }
        }
        public class Rejestracja
        {
            public string imie, nazwisko, email, haslo;
        }
    }
}
