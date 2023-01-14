using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Kasa

{
    public class BlokowanieModel : PageModel
    {
        public List<Klient> k = new List<Klient>();
        public List<Wartownik> w = new List<Wartownik>();

        string connectionString = "Data Source=DESKTOP-L54I9S2\\NARCIARZE;Initial Catalog=narty;Integrated Security=True";

        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-L54I9S2\\NARCIARZE;Initial Catalog=narty;Integrated Security=True");
            conn.Open();
            string query1 = "SELECT Imie as imie, Nazwisko as nazw, ID as id FROM Klient ORDER BY Nazwisko ASC";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Klient k1 = new Klient();
                        k1.id = reader["id"].ToString();
                        k1.imie = reader["imie"].ToString();
                        k1.nazw = reader["nazw"].ToString();
                        k.Add(k1);
                    }
                }
            }
        }

        public void OnPostBlok()
        {
            Klient k2 = new Klient();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-L54I9S2\\NARCIARZE;Initial Catalog=narty;Integrated Security=True");
            conn.Open();
            string query1 = "SELECT Imie as imie, Nazwisko as nazw, ID as id FROM Klient ORDER BY Nazwisko ASC";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Klient k1 = new Klient();
                        k1.id = reader["id"].ToString();
                        k1.imie = reader["imie"].ToString();
                        k1.nazw = reader["nazw"].ToString();
                        k.Add(k1);
                    }
                }
            }
            k2.id = Request.Form["klient"];
            string query2 = "SELECT k.ID as id, kl.Imie as imie, kl.ID as id_k , kl.Nazwisko as nazwisko, s.Nazwa as nazw, k.Czas_trwania as czas, k.Status as status  FROM Karnety as k, Stoki as s, Klient as kl WHERE kl.ID = k.ID_Klient AND s.ID = k.ID_Stok AND k.ID_Klient = '" + k2.id+"' ";
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Wartownik w1 = new Wartownik();
                        w1.imie = reader["imie"].ToString();
                        w1.nazwisko = reader["nazwisko"].ToString();
                        w1.id = reader["id"].ToString();
                        w1.ilosc = reader["czas"].ToString();
                        w1.nazw = reader["nazw"].ToString();
                        w1.status = reader["status"].ToString();
                        w1.id_k = reader["id_k"].ToString();
                        w.Add(w1);

                    }
                }
            }
        }
        public class Klient
        {
            public string id, imie, nazw;
        }

        public class Wartownik
        {
            public string id, imie, nazwisko, nazw, ilosc, status, id_k;
        }
    }
}
