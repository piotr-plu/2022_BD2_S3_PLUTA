using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static Narciarze_v_2.Pages.Strefa_Administracji.Kasa.SprzedarzModel;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class GrupowanieStokiModel : PageModel
    {

        public List<NoweStoki> noweStoki = new List<NoweStoki>();
        public List<Stoki> stoki = new List<Stoki>();

        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V3;Integrated Security=True");
            conn.Open();
            string query1 = "SELECT s.ID as id, s.Nazwa as nazwa FROM Stoki as s";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stoki stok = new Stoki();
                        stok.id = reader["id"].ToString();
                        stok.nazwa = reader["nazwa"].ToString();
                        stoki.Add(stok);
                    }
                }
            }

        }

        public void OnPostDodajStok()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V3;Integrated Security=True");
            conn.Open();
            NoweStoki stok = new NoweStoki();
            stok.nazwa = Request.Form["nazwaStokuDodaj"];
            string query = "INSERT INTO Stoki (Nazwa) VALUES ('"+stok.nazwa+"')";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.ExecuteNonQuery();
            }
            Response.Redirect("GrupowanieStoki");
        }

        public void OnPostUsunStok()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V3;Integrated Security=True");
            conn.Open();
            NoweStoki stok = new NoweStoki();

            stok.nazwa = Request.Form["nazwaStokuUsun"];
            string query = "DELETE FROM Stoki WHERE ID = " + stok.nazwa + "";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.ExecuteNonQuery();
            }
            Response.Redirect("GrupowanieStoki");
        }

    }

    public class NoweStoki
    {
        public string nazwa;
    }
    public class Stoki
    {
        public string id, nazwa;
    }
}
