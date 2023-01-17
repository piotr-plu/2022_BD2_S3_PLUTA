using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class PracownicycshtmlModel : PageModel
    {
        public List<Pracownik> p = new List<Pracownik>();

        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            string query3 = "SELECT Imie as imie, Nazwisko as nazw, ID as id FROM Pracownicy ORDER BY Nazwisko ASC";
                using (SqlCommand command = new SqlCommand(query3, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pracownik p1 = new Pracownik();
                        p1.id = reader["id"].ToString();
                        p1.imie = reader["imie"].ToString();
                        p1.nazw = reader["nazw"].ToString();
                        p.Add(p1);
                    }
                }
            }
        }

        public void OnPostRejestracja()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            Rejestracja r1 = new Rejestracja();
            r1.imie = Request.Form["imie"];
            r1.nazwisko = Request.Form["nazwisko"];
            r1.email = Request.Form["email"];
            r1.haslo = Request.Form["haslo"];
            r1.rola = Request.Form["rola"];
            string query = "INSERT INTO Pracownicy (Imie, Nazwisko, EMail, Haslo, Poz_uprawnien) VALUES ('" + r1.imie + "','" + r1.nazwisko + "','" + r1.email + "','" + r1.haslo + "','"+r1.rola+"')";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.ExecuteNonQuery();
            }
        }

        public void OnPostUsuwanie() 
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            Pracownik p2 = new Pracownik();
            p2.id = Request.Form["pracownik"];
            p2.id = p2.id.Split(':')[0];
            string query2 = "DELETE FROM Pracownicy WHERE ID='"+ p2.id +"';";
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}

public class Pracownik
{
    public string id, imie, nazw;
}
public class Rejestracja
{
    public string imie, nazwisko, email, haslo, rola;
}
