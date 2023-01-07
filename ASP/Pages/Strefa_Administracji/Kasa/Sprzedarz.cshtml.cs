using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Configuration;


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
        }
            public void OnPostBilet()
            {

                SqlConnection conn = new SqlConnection("DefaultConnection");
                conn.Open();
                Klient k1 = new Klient();
                k1.imie = Request.Form["imie"];
                k1.nazw = Request.Form["nazwisko"];
                k1.email = Request.Form["email"];
                string query1 = "SELECT ID as id from Klient where Imie =  '" + k1.imie + "' AND Nazwisko = '" + k1.nazw + "' AND EMail ='" + k1.email + "'";
                using (SqlCommand command = new SqlCommand(query1, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            k1.id = reader["id"].ToString();
                            k.Add(k1);
                        }
                    }
                }
                Wyciag w1 = new Wyciag();
                w1.nazwa = Request.Form["wyciag"];
                w1.ilosc_zjazdow = Request.Form["zjazdy"];
                string query2 = "SELECT ID as id from Wyciagi WHERE Nazwa = '" + w1.nazwa + "'";
                using (SqlCommand command = new SqlCommand(query2, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            w1.id = reader["id"].ToString();
                            w.Add(w1);
                        }
                    }
                }
                Cennik c1 = new Cennik();
                string query3 = "SELECT c.ID as id from Cennik as c, Cena_bilety as cb WHERE cb.ID_Wyciag = 2 AND c.ID_Cena_bilet = cb.ID AND c.Data_rozp < '2023-01-03' AND (c.Data_zak > '2023-01-03' OR c.Data_zak IS NULL)";
                using (SqlCommand command = new SqlCommand(query3, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            c1.id = reader["id"].ToString();
                            c.Add(c1);
                        }
                    }
                }
                string query4 = "INSERT INTO Bilety (ID_Klient, ID_Wyciagi, Ilosc_zjazdow, ID_cennika) VALUES ('" + k1.id + "', '" + w1.id + "','" + w1.ilosc_zjazdow + "','" + c1.id + "')";
                using (SqlCommand command = new SqlCommand(query3, conn))
                {
                   command.ExecuteNonQuery();
                }
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
            public string id, imie, nazw, email;
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
