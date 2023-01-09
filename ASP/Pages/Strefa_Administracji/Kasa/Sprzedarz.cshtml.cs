using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Narciarze_v_2.Pages.Strefa_Administracji.Kasa
{
    public class SprzedarzModel : PageModel
    {
        public List<Klient> k = new List<Klient>();
        public List<Wyciag> w = new List<Wyciag>();
        public List<Cennik> c = new List<Cennik>();

        public List<Rejestracja> reg = new List<Rejestracja>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V2;Integrated Security=True");
            conn.Open();
            string query1 = "SELECT Imie as imie, Nazwisko as nazw, ID as id FROM Klient ORDER BY Nazwisko ASC";
            string query2 = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
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
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Wyciag w1 = new Wyciag();
                        w1.id = reader["id"].ToString();
                        w1.nazwa = reader["nazw"].ToString();
                        w.Add(w1);
                    }
                }
            }
        }
        public void OnPostBilet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V2;Integrated Security=True");
            conn.Open();
            Klient k2 = new Klient();
            Wyciag w2 = new Wyciag();
            Cennik c1 = new Cennik();
            k2.id = Request.Form["klient"];
            w2.id = Request.Form["wyciag"];
            w2.ilosc_zjazdow = Request.Form["ilosc"];
            string query3 = "SELECT c.ID as id FROM Cennik as c, Cena_bilety as ck WHERE c.ID_Cena_bilet = ck.ID AND ck.ID_Wyciag = '" + w2.id + "' AND c.Data_rozp < '2023-01-03' AND (c.Data_zak > '2023-01-03' OR c.Data_zak IS NULL)";
            using (SqlCommand command = new SqlCommand(query3, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        c1.id = reader["id"].ToString();
                    }
                }
            }
            string query4 = "INSERT INTO Bilety (ID_Klient, ID_Wyciag, Ilosc_zjazdow, ID_cennik) VALUES ('"+k2.id+"', '"+w2.id+"', '"+w2.ilosc_zjazdow+"', '"+c1.id+"')";
            using (SqlCommand command = new SqlCommand(query4, conn))
            {
                //command.ExecuteNonQuery();
            }
            Response.Redirect("Sprzedarz");
        }

            public void OnPostRejestracja()
            {
                SqlConnection conn = new SqlConnection("DefaultConnection");
                conn.Open();
                Rejestracja r1 = new Rejestracja();
                r1.imie = Request.Form["imie"];
                r1.nazwisko = Request.Form["nazw"];
                r1.email = Request.Form["email"];
                r1.haslo = Request.Form["haslo"];
                string query = "INSERT INTO Klient (Imie, Nazwisko, EMail, Haslo) VALUES ('" + r1.imie + "','" + r1.nazwisko + "','" + r1.email + "','" + r1.haslo + "')";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.ExecuteNonQuery();
                }
            }


        public class Klient {
            public string id, imie, nazw;
        }
        public class Wyciag
        {
            public string id, nazwa, ilosc_zjazdow;
        }
        public class Cennik
        {
            public string id;
        }
        public class Rejestracja
        {
            public string imie, nazwisko, email, haslo;
        }

    }
}
