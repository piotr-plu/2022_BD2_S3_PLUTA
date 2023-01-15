using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class EdycjaStokowModel : PageModel
    {
        public List<Stoki_wyc> stoki = new List<Stoki_wyc>();
        public List<Wyciagi_edycja> wyciagi = new List<Wyciagi_edycja>();
        public List<NoweWyciagi> noweWyciagi = new List<NoweWyciagi>();
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
                        Wyciagi_edycja wyciag = new Wyciagi_edycja();
                        wyciag.id = reader["id"].ToString();
                        wyciag.nazwa = reader["nazwa"].ToString();
                        wyciagi.Add(wyciag);
                    }
                }
            }

            string query2 = "SELECT s.ID as id, s.Nazwa as nazwa FROM Stoki as s";
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stoki_wyc stok = new Stoki_wyc();
                        stok.id = reader["id"].ToString();
                        stok.nazwa = reader["nazwa"].ToString();
                        stoki.Add(stok);
                    }
                }
            }

            string query3 = "SELECT s.ID as id, s.Nazwa as nazwa FROM Stoki as s";
            using (SqlCommand command = new SqlCommand(query3, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stoki_wyc stok = new Stoki_wyc();
                        stok.id = reader["id"].ToString();
                        stok.nazwa = reader["nazwa"].ToString();
                        stoki.Add(stok);
                    }
                }
            }
        }


        public void OnPostDodajWyciag()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            NoweWyciagi stok = new NoweWyciagi();
            stok.nazwa = Request.Form["nazwaWyciagDodaj"];
            stok.harmonogram = Request.Form["harmonogramWyciagDodaj"];
            stok.stok = Request.Form["stokWyciagDodaj"];
            stok.dlugosc = Request.Form["dlugoscWyciagDodaj"];
            stok.wysokosc = Request.Form["wysokoscWyciagDodaj"];

            string query = "";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.ExecuteNonQuery();
            }
            Response.Redirect("EdycjaStokow");
        }

        public void OnPostUsunWyciag()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            NoweStoki stok = new NoweStoki();

            stok.nazwa = Request.Form["nazwaWyciagUsun"];
            string query = "";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.ExecuteNonQuery();
            }
            Response.Redirect("EdycjaStokow");
        }

    }
    public class Wyciagi_edycja
    {
        public string id, nazwa;
    }
    public class NoweWyciagi
    {
        public string nazwa, harmonogram, stok, dlugosc, wysokosc;
    }
    public class Stoki_wyc
    {
        public string id, nazwa;
    }

}
