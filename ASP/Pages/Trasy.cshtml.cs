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
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            string query = @"SELECT s.Nazwa as Stok, w.Nazwa as Wyciag, h.Stan as Stan, w.Dlugosc as Dlugosc
                            FROM Wyciagi as w
                            LEFT JOIN Stoki as s ON w.ID_Stok = s.ID
                            LEFT JOIN Harmonogram as h ON w.ID = h.ID_Wyciagi
                            WHERE (Data_rozp <= (SELECT CAST(GETDATE() as Date))) AND ((Data_zak >= (SELECT CAST(GETDATE() as Date))) OR Data_zak IS NULL)";
            
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Trasa t1 = new Trasa();
                        t1.Stok = reader["Stok"].ToString();
                        t1.Wyciag = reader["Wyciag"].ToString();
                        t1.Stan = reader["Stan"].ToString();
                        t1.Dlugosc = reader["Dlugosc"].ToString();

                        trasy.Add(t1);
                    }
                }
            }
        }
    }

    public class Trasa
    {
        public string Stok, Wyciag, Stan, Dlugosc;
    }
}
