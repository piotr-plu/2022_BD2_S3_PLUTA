using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//STAN PRAC -> Pracê nad tym elementem s¹ na etapie sprawdzenia czy podana przez u¿ytkownika cena znajduje siê w tabeli Cennik_x i w przypadku jej braku jest dodawana
//TO DO-> Napisaæ walidacjê dat w przypadku tworzenia nowej pozycji cennika data rozpoczencia nie mo¿e byæ wczeœniej od daty zakoñczenia poprzedniego
//W przypadku wartoœci null data zakoñczenia ma ustawiæ siê na dzieñ wczeœniej
namespace Narciarze_v_2.Pages.Strefa_Administracji.Zarzad
{
    public class ZmianaCennikaModel : PageModel
    {
        public List<Wyciag> w = new List<Wyciag>();
        public List<Wyciag> ww = new List<Wyciag>();
        public List<Licznik> l = new List<Licznik>();
        public List<Error> e = new List<Error>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-L54I9S2\\NARCIARZE;Initial Catalog=narty;Integrated Security=True");
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
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-L54I9S2\\NARCIARZE;Initial Catalog=narty;Integrated Security=True");
            conn.Open();
            Wyciag w2 = new Wyciag();
            w2.id = Request.Form["wyciag"].ToString();
            string query2 = "SELECT w.ID as id, w.nazwa as 'nazw', cb.Cena_przejazd as 'cena', c.Data_rozp as 'rozp', c.Data_zak as 'zak' FROM Wyciagi as w, Cena_bilety as cb, Cennik as c WHERE c.ID_Cena_bilet = cb.ID AND cb.ID_Wyciag = w.ID AND c.Data_rozp < '2023-01-03' AND (c.Data_zak > '2023-01-03' OR c.Data_zak IS NULL) AND w.ID = '" + w2.id + "'";
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Wyciag ww1 = new Wyciag();
                        ww1.id = reader["id"].ToString();
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

        public void OnPostAkt()
        {
            Licznik l1 = new Licznik();
            Cennik c1 = new Cennik();
            Cennik c2 = new Cennik();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-L54I9S2\\NARCIARZE;Initial Catalog=narty;Integrated Security=True");
            conn.Open();
            Wyciag w3 = new Wyciag();
            w3.id = Request.Form["id"].ToString();
            w3.nazwa = Request.Form["z_nazwa"].ToString();
            w3.cena = Request.Form["z_cena"].ToString();
            w3.rozp = Request.Form["z_rozp"].ToString();
            w3.zak = Request.Form["z_zak"].ToString();
            Wyciag w4 = new Wyciag();
            string query3 = "SELECT COUNT(cb.ID) as liczba FROM Cena_bilety as cb, Wyciagi as w WHERE w.ID = cb.ID_Wyciag AND w.Nazwa = '" + w3.nazwa + "' AND cb.Cena_przejazd = '" + w3.cena + "'";
            using (SqlCommand command = new SqlCommand(query3, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        l1.liczba = reader["liczba"].ToString();
                    }
                }
            }
            int x = int.Parse(l1.liczba);
            if(x == 0)
            {
                string query4 = "INSERT INTO Cena_bilety (ID_Wyciag, Cena_przejazd) VALUES ((SELECT ID FROM Wyciagi WHERE Nazwa = '" + w3.nazwa + "'), '" + w3.cena + "')";
                using (SqlCommand command = new SqlCommand(query4, conn))
                {
                    command.ExecuteNonQuery();
                }
            }
            string query5 = "SELECT cb.ID as id FROM Cena_bilety as cb, Wyciagi as w WHERE cb.ID_Wyciag = w.ID AND w.Nazwa = '"+w3.nazwa+"' AND cb.Cena_przejazd = '"+w3.cena+"'";
            using (SqlCommand command = new SqlCommand(query5, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        c1.id_b = reader["id"].ToString();
                    }
                }
            }
            string query6 = "SELECT c.Data_zak as zak, c.Data_rozp as rozp, c.ID as id  FROM Cennik as c, Cena_bilety as cb, Wyciagi as w WHERE c.ID_Cena_bilet = cb.ID AND cb.ID_Wyciag = w.ID AND c.Data_rozp < '2023-01-03' AND (c.Data_zak > '2023-01-03' OR c.Data_zak IS NULL) AND w.ID = '" + w3.id + "'";
            using (SqlCommand command = new SqlCommand(query6, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        w4.zak = reader["zak"].ToString();
                        w4.rozp = reader["rozp"].ToString();
                        c2.id_b = reader["id"].ToString();
                    }
                }
            }
            if (w4.zak == "")
            {
                var date1 =  DateTime.Parse(w3.rozp);
                w4.zak = date1.Year.ToString() + "-" + date1.Month.ToString() + "-" + date1.Day.ToString();
                string query7 = "UPDATE CENNIK SET Data_zak = '"+w4.zak+"' WHERE ID = '"+c2.id_b+ "' AND Data_rozp < '2023-01-03' AND (Data_zak > '2023-01-03' OR Data_zak IS NULL)";
                using (SqlCommand command = new SqlCommand(query7, conn))
                {
                    command.ExecuteNonQuery();
                }
                Response.Redirect("ZmianaCennika");
            }
            else
            {
                var date1 = DateTime.Parse(w3.rozp);
                var date2 = DateTime.Parse(w4.zak);
                var diff = date1 - date2;
                int days = diff.Days;
                if (days < 0)
                {
                    Error e1 = new Error();
                    e1.tekst = "Data rozpoczêcia nowego cennika jest przed dat¹ zakoñczenia starego cennika -> '"+w4.zak+"'";
                    e.Add(e1);
                }
                else if (days > 1)
                {
                    Response.Redirect("ZmianaCennika");
                }
                else
                {
                    Response.Redirect("ZmianaCennika");
                }
            }

        }
        public class Wyciag
        {
            public string id, nazwa, cena, rozp, zak;
        }
        public class Licznik
        {
            public string liczba;
        }
        public class Cennik
        {
            public string id_b, data_rozp, data_zak;
        }
        public class Error
        {
            public string tekst;
        }
    }
}
