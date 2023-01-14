using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class StatusWyciagowModel : PageModel
    {
        public List<Wyciag> w = new List<Wyciag>();
        public List<Wyciag> ww = new List<Wyciag>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V3;Integrated Security=True");
            conn.Open();
            string query = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Wyciag w1 = new Wyciag();
                        w1.id = reader["id"].ToString();
                        w1.nazwa = reader["nazw"].ToString();
                        w.Add(w1);
                    }
                }
            }
        }

        public void OnPostWyciag()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V3;Integrated Security=True");
            conn.Open();
            Wyciag w2 = new Wyciag();
            w2.id = Request.Form["wyciag"].ToString();
            string query2 = "SELECT w.nazwa as 'nazw', cb.Cena_przejazd as 'cena', c.Data_rozp as 'rozp', c.Data_zak as 'zak' FROM Wyciagi as w, Cena_bilety as cb, Cennik as c WHERE c.ID_Cena_bilet = cb.ID AND cb.ID_Wyciag = w.ID AND c.Data_rozp < '2023-01-03' AND (c.Data_zak > '2023-01-03' OR c.Data_zak IS NULL) AND w.ID = '" + w2.id + "'";
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Wyciag ww1 = new Wyciag();
                        ww1.nazwa = reader["nazw"].ToString();
                        ww1.cena = reader["cena"].ToString();
                        ww1.rozp = reader["rozp"].ToString();
                        string[] subs = ww1.rozp.Split(" ");
                        ww1.rozp = subs[0];
                        ww1.zak = reader["zak"].ToString();
                        subs = ww1.zak.Split(" ");
                        ww1.zak = subs[0];
                        ww.Add(ww1);
                    }
                }
            }
        }

        public void OnPostZatwierdz()
        {

        }

    }

    public class Wyciag
    {
        public string id, nazwa, cena, rozp, zak;
    }


}


