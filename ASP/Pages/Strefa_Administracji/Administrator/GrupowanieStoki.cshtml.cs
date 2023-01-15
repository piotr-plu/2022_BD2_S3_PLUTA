using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using static Narciarze_v_2.Pages.Strefa_Administracji.Administrator.EdycjaStokowModel;
using static Narciarze_v_2.Pages.Strefa_Administracji.Kasa.SprzedarzModel;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class GrupowanieStokiModel : PageModel
    {

        public List<NoweStoki> noweStoki = new List<NoweStoki>();
        public List<Stoki> stoki = new List<Stoki>();
        public List<Wyciagi> wyciagi = new List<Wyciagi>();
        public List<GrupWyciagi> grupWyciagi = new List<GrupWyciagi>();

        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
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

            string query2 = "SELECT w.ID as id, w.Nazwa as nazwa FROM Wyciagi as w";
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Wyciagi wyciag = new Wyciagi();
                        wyciag.id = reader["id"].ToString();
                        wyciag.nazwa = reader["nazwa"].ToString();
                        wyciagi.Add(wyciag);
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

        public void OnPostGrupujStoki()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V3;Integrated Security=True");
            conn.Open();
            GrupWyciagi var = new GrupWyciagi();

            var.wyciagi = Request.Form["nazwaWyciag"];
            var.stok = Request.Form["nazwaStok"];

            string[] wyciagi = var.wyciagi.Split(',');
            string query = "UPDATE Wyciagi SET ID_Stok = " + var.stok + " WHERE ID = " + wyciagi[0] + " ";

            for (int i = 1; i < wyciagi.Length; i++)
            {
                query = query + "OR ID = " + wyciagi[i] +" ";
            }

            
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
    public class Wyciagi
    {
        public string id, nazwa;
    }
    public class GrupWyciagi
    {
        public string wyciagi, stok;
    }
}
