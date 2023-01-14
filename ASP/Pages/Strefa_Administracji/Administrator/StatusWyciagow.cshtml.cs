using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class StatusWyciagowModel : PageModel
    {
        public List<Trasa> trasy = new List<Trasa>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V3;Integrated Security=True");
            conn.Open();
            string query = "SELECT w.Nazwa as \"Wyciag\", h.Stan as \"Stan\" FROM Stoki as s, Wyciagi as w, Harmonogram as h WHERE s.ID = w.ID_Stok AND w.ID_Harmonogram = h.ID";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Trasa t1 = new Trasa();
                        t1.Wyciag = reader["Wyciag"].ToString();
                        t1.Stan = reader["Stan"].ToString();

                        trasy.Add(t1);
                    }
                }
            }
        }

        public void OnPostZatwierdz()
        {

        }

    }

    public class Trasa
    {
        public string Stok, Wyciag, Stan, Dlugosc;
    }


}


