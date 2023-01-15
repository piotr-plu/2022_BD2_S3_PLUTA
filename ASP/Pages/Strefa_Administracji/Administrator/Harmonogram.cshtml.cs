using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class HarmonogramModel : PageModel
    {
        public List<Wyciagi_harm> wyciagi = new List<Wyciagi_harm>();

        public void OnGet()
        {


            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();

            string query1 = "SELECT w.ID as id, w.Nazwa as nazwa FROM Wyciagi as w";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Wyciagi_harm wyciag = new Wyciagi_harm();
                        wyciag.id = reader["id"].ToString();
                        wyciag.nazwa = reader["nazwa"].ToString();
                        wyciagi.Add(wyciag);
                    }
                }
            }
        }
    }
}
public class Wyciagi_harm
{
    public string id, nazwa;
}
