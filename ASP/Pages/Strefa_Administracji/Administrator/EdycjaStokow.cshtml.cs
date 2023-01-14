using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class EdycjaStokowModel : PageModel
    {
        public List<Stok> s = new List<Stok>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V3;Integrated Security=True");
            conn.Open();
            string query1 = "SELECT ID as id, Nazwa as nazwa FROM Stoki";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazwa"].ToString();
                        s.Add(s1);
                    }
                }
            }
        }

        public class Stok
        {
            public string id, nazwa;
        }
    }
}
