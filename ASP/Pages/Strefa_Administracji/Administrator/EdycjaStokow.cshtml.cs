using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class EdycjaStokowModel : PageModel
    {
        public List<StokiWyc> stoki = new List<StokiWyc>();
        public List<Wyciag> edycjaWyciag = new List<Wyciag>();
        public List<Wyciag> wyciagi = new List<Wyciag>();
        public List<Wyciag> nowyWyciag = new List<Wyciag>();

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
                        Wyciag wyciag = new Wyciag();
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
                        StokiWyc stok = new StokiWyc();
                        stok.id = reader["id"].ToString();
                        stok.nazwa = reader["nazwa"].ToString();
                        stoki.Add(stok);
                    }
                }
            }
        }


        public void OnPostEdytujWyciag()
        {
            Wyciag wyciag = new Wyciag();

            wyciag.id = Request.Form["nazwaWyciagDodaj"];
            wyciag.nazwa = Request.Form["nazwaWyciaguEdytuj"];
            wyciag.dlugosc = Request.Form["dlugoscWyciagEdytuj"];
            wyciag.wysokosc = Request.Form["wysokoscWyciagEdytuj"];

            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();

            string query = "UPDATE Wyciagi SET  Nazwa = '"+wyciag.nazwa+"', Dlugosc = '"+wyciag.dlugosc+"', Wys_bezwzgl = '"+wyciag.wysokosc+"' WHERE ID = "+wyciag.id+"";

            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.ExecuteNonQuery();
            }
            Response.Redirect("EdycjaStokow");
        }

        public void OnPostDodajWyciag()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            Wyciag stok = new Wyciag();
            stok.nazwa = Request.Form["nazwaWyciagDodaj"];
            stok.stok = Request.Form["stokWyciagDodaj"];
            stok.dlugosc = Request.Form["dlugoscWyciagDodaj"];
            stok.wysokosc = Request.Form["wysokoscWyciagDodaj"];

            string query = @"INSERT INTO Wyciagi (Nazwa, ID_Stok, Dlugosc, Wys_bezwzgl ) 
                            VALUES ('"+stok.nazwa+"', '"+stok.stok+"', '"+stok.dlugosc+"', '"+stok.wysokosc+"')";
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
            string query = "DELETE FROM Wyciagi WHERE ID = "+stok.nazwa+"";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                command.ExecuteNonQuery();
            }
            Response.Redirect("EdycjaStokow");
        }

    }

    public class Wyciag
    {
        public string id, nazwa, stok, dlugosc, wysokosc;
    }
    public class StokiWyc
    {
        public string id, nazwa;
    }

}
