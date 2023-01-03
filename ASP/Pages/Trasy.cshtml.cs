using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;

namespace Narciarze_v_2.Pages.Strefa_Klienta
{
    public class TrasyModel : PageModel
    {
        public List<Trasa> trasy = new List<Trasa>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V2.0;Integrated Security=True");
            conn.Open();
            string query = "SELECT s.nazwa as 'Nazwa', ck.Cena as 'Cena', ck.czas 'Wymiar godzinowy' FROM Stoki as s, Cennik as c, Cena_karnety as ck WHERE s.ID = ck.ID_Stok AND c.ID_Cena_karnet = ck.ID";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Trasa t1 = new Trasa();
                        t1.Nazwa = reader["Nazwa"].ToString();
                        t1.Cena = reader["Cena"].ToString();
                        t1.Wymiar_godz = reader["Wymiar godzinowy"].ToString();
                        trasy.Add(t1);
                    }
                }
            }
        }
    }

    public class Trasa
    {
        public string Nazwa, Cena, Wymiar_godz;
    }
}
