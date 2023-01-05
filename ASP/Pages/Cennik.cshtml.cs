using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static Narciarze_v_2.Pages.Strefa_Klienta.TrasyModel;

namespace Narciarze_v_2.Pages.Strefa_Klienta
{
    public class CennikModel : PageModel
    {
        public List<Cennik_k> ceny_k = new List<Cennik_k>();
        public List<Cennik_b> ceny_b = new List<Cennik_b>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V2;Integrated Security=True");
            conn.Open();
            string query = "SELECT s.nazwa as 'Nazwa', ck.Cena as 'Cena', ck.czas 'Wymiar godzinowy' FROM Stoki as s, Cennik as c, Cena_karnety as ck WHERE s.ID = ck.ID_Stok AND c.ID_Cena_karnet = ck.ID AND c.Data_rozp < '2023-01-03' AND (c.Data_zak > '2023-01-03' OR c.Data_zak IS NULL)";
            string query_2 = "SELECT w.nazwa as 'Nazwa', cb.Cena_przejazd as 'Cena' FROM Wyciagi as w, Cena_bilety as cb, Cennik as c WHERE c.ID_Cena_bilet = cb.ID AND cb.ID_Wyciag = w.ID AND c.Data_rozp < '2023-01-03' AND (c.Data_zak > '2023-01-03' OR c.Data_zak IS NULL)";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cennik_k t1 = new Cennik_k();
                        t1.Nazwa = reader["Nazwa"].ToString();
                        t1.Cena = reader["Cena"].ToString();
                        t1.Wymiar_godz = reader["Wymiar godzinowy"].ToString();
                        ceny_k.Add(t1);
                    }
                }
            }
            using (SqlCommand command_2 = new SqlCommand(query_2, conn))
            {
                using (SqlDataReader reader_2 = command_2.ExecuteReader())
                {
                    while (reader_2.Read()) {
                        Cennik_b t2 = new Cennik_b();
                        t2.Nazwa = reader_2["Nazwa"].ToString();
                        t2.Cena_p = reader_2["Cena"].ToString();
                        ceny_b.Add(t2);
                    }
                }
            }
        }
        public class Cennik_k
        {
            public string Nazwa, Cena, Wymiar_godz;
        }
        public class Cennik_b
        {
            public string Nazwa, Cena_p;
        }
    }
}
       
    
