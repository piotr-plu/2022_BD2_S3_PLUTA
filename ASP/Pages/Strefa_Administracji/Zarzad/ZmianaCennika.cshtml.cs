using iText.StyledXmlParser.Jsoup.Select;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
using Narciarze_v_2.Pages.Strefa_Administracji.Administrator;
using System.Security.Cryptography.X509Certificates;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
//STAN PRAC -> Pracê nad tym elementem s¹ na etapie sprawdzenia czy podana przez u¿ytkownika cena znajduje siê w tabeli Cennik_x i w przypadku jej braku jest dodawana
//TO DO-> Napisaæ walidacjê dat w przypadku tworzenia nowej pozycji cennika data rozpoczencia nie mo¿e byæ wczeœniej od daty zakoñczenia poprzedniego
//W przypadku wartoœci null data zakoñczenia ma ustawiæ siê na dzieñ wczeœniej
namespace Narciarze_v_2.Pages.Strefa_Administracji.Zarzad
{
    public class ZmianaCennikaModel : PageModel
    {
        public List<Wyciag> w = new List<Wyciag>();
        public List<Cennik> c = new List<Cennik>();
        public List<Edycja> e = new List<Edycja>();
        public List<Edycja> ee = new List<Edycja>();
        public List<Licznik> l = new List<Licznik>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
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
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
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
            Wyciag w2 = new Wyciag();
            w2.id = Request.Form["wyciag"].ToString();
            string query2 = "SELECT c.ID as id, c.Data_rozp as rozp, c.Data_zak as zak FROM Cennik as c, Cena_bilety as cb, Wyciagi as w WHERE c.ID_Cena_bilet = cb.ID AND w.ID = cb.ID_Wyciag AND ((c.Data_rozp < '2023-01-10' AND c.Data_zak > '2023-01-10') OR c.Data_zak IS NULL) AND w.ID = '"+w2.id+"'";
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cennik c1= new Cennik();
                        c1.id = reader["id"].ToString();
                        c1.rozp = reader["rozp"].ToString();
                        string[] subs = c1.rozp.Split(" ");
                        c1.rozp = subs[0];
                        c1.zak = reader["zak"].ToString();
                        subs = c1.zak.Split(" ");
                        c1.zak = subs[0];
                        c.Add(c1);
                    }
                }
            }
        }

        public void OnPostCennik()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
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
            Cennik c2 = new Cennik();
            c2.id = Request.Form["cennik"].ToString();
            string query3 = "SELECT w.Nazwa as nazwa, cb.Cena_przejazd as cena, c.Data_rozp as rozp, c.Data_zak as zak FROM Cennik as c, Wyciagi as w, Cena_bilety as cb WHERE c.ID_Cena_bilet = cb.ID AND w.ID = cb.ID_Wyciag AND c.ID ='" + c2.id + "'";
            using (SqlCommand command = new SqlCommand(query3, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Edycja e1 = new Edycja();
                        e1.nazwa = reader["nazwa"].ToString();
                        e1.cena = reader["cena"].ToString();
                        e1.rozp = reader["rozp"].ToString();
                        e1.zak = reader["zak"].ToString();
                        string[] tab = e1.rozp.Split(" "); 
                        e1.rozp = tab[0];
                        tab = e1.zak.Split(" ");
                        e1.zak = tab[0];
                        e.Add(e1);
                    }
                }
            }
        }

        public void OnPostEdytuj()
        {
            Licznik l1 = new Licznik();
            Edycja e2 = new Edycja();
            Cennik c3 = new Cennik();
            e2.nazwa = Request.Form["nazwa"].ToString();
            e2.cena = Request.Form["cena"].ToString();
            e2.rozp = Request.Form["rozp"].ToString();
            e2.zak = Request.Form["zak"].ToString();
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            var date1 = DateTime.Parse(e2.rozp);
            e2.rozp = date1.Year.ToString() + "-" + date1.Month.ToString() + "-" + date1.Day.ToString();
            string query3 = "SELECT c.ID as id FROM Cennik as c, Cena_bilety as cb, Wyciagi as w " +
                "WHERE c.ID_Cena_bilet = cb.ID AND w.ID = cb.ID_Wyciag " +
                "AND w.Nazwa = '"+e2.nazwa+"' AND  c.Data_rozp = '"+e2.rozp+"'";
            using (SqlCommand command = new SqlCommand(query3, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        c3.id_c = reader["id"].ToString();
                    }
                }
            }
            string query4 = "SELECT COUNT(cb.ID) as liczba FROM Cena_bilety as cb, Wyciagi as w WHERE w.ID = cb.ID_Wyciag AND w.Nazwa = '" + e2.nazwa + "' AND cb.Cena_przejazd = '" + e2.cena + "'";
            using (SqlCommand command = new SqlCommand(query4, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        l1.liczba = reader["liczba"].ToString();
                    }
                }
            }
            int l = int.Parse(l1.liczba);
            if (l == 0)
            {
                string query5 = "INSERT INTO Cena_bilety (ID_Wyciag, Cena_przejazd) VALUES ((SELECT ID FROM Wyciagi WHERE Nazwa = '" + e2.nazwa + "'), '" + e2.cena + "')";
                using (SqlCommand command = new SqlCommand(query5, conn))
                {
                   command.ExecuteNonQuery();
                }
            }
            string query6 = "SELECT cb.ID as id FROM Cena_bilety as cb, Wyciagi as w WHERE w.Nazwa = '" + e2.nazwa + "' AND Cena_przejazd = '" + e2.cena + "' AND w.ID = cb.ID_Wyciag";
            using (SqlCommand command = new SqlCommand(query6, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        c3.id = reader["id"].ToString();
                    }
                }
            }
            string query7 = "UPDATE CENNIK set ID_Cena_bilet = '"+c3.id+"' WHERE ID = '"+c3.id_c+"'";
            using (SqlCommand command = new SqlCommand(query7, conn))
            {
                command.ExecuteNonQuery();
            }
            Response.Redirect("ZmianaCennika");
        }


        public void OnPostCennik_n()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            Edycja e3= new Edycja();
            e3.id = Request.Form["wyciag"].ToString();
            string query8 = "SELECT Nazwa as nazwa FROM Wyciagi WHERE ID = '"+e3.id+"'";
            using (SqlCommand command = new SqlCommand(query8, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Edycja ee1 = new Edycja();
                        ee1.nazwa = reader["nazwa"].ToString();
                        ee.Add(ee1);
                    }
                }
            }
        }

        public void OnPostDodaj()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            Licznik l1 = new Licznik();
            Edycja e4 = new Edycja();
            Cennik c3 = new Cennik();
            e4.nazwa = Request.Form["nazwa"].ToString();
            e4.rozp = Request.Form["rozp"].ToString();
            e4.cena = Request.Form["cena"].ToString();
            string query9 = "SELECT c.ID as id, c.Data_rozp as rozp FROM Cennik as c, Wyciagi as w, Cena_bilety as cb WHERE c.ID_Cena_bilet = cb.ID AND w.ID = cb.ID_Wyciag AND c.Data_zak IS NULL AND w.Nazwa = '" + e4.nazwa + "'";
            using (SqlCommand command = new SqlCommand(query9, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        c3.id = reader["id"].ToString();
                        c3.rozp = reader["rozp"].ToString();
                    }
                }
            }
            var date1 = DateTime.Parse(c3.rozp);
            var date2 = DateTime.Parse(e4.rozp);
            var diff = date2- date1;
            int idiff = diff.Days;
            if (idiff <= 0)
            {
                Console.WriteLine("B³¹d");
                //Data nowego cennika jest przed dat¹ starego, mo¿na wyrzuciæ jakiœ b³¹d na ekran
            }
            else
            {
                e4.zak = date2.Year.ToString() + "-" + date2.Month.ToString() + "-" + date2.Day.ToString();
                string query10 = "UPDATE Cennik SET Data_zak = '" + e4.zak + "' WHERE ID = '"+c3.id+"'";
                using (SqlCommand command = new SqlCommand(query10, conn))
                {
                    command.ExecuteNonQuery();
                }
                string querya = "SELECT COUNT(cb.ID) as liczba FROM Cena_bilety as cb, Wyciagi as w WHERE w.ID = cb.ID_Wyciag AND w.Nazwa = '" + e4.nazwa + "' AND cb.Cena_przejazd = '" + e4.cena + "'";
            using (SqlCommand command = new SqlCommand(querya, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            l1.liczba = reader["liczba"].ToString();
                        }
                    }
                }
                int l = int.Parse(l1.liczba);
                if (l == 0)
                {
                    string query5 = "INSERT INTO Cena_bilety (ID_Wyciag, Cena_przejazd) VALUES ((SELECT ID FROM Wyciagi WHERE Nazwa = '" + e4.nazwa + "'), '" + e4.cena + "')";
                    using (SqlCommand command = new SqlCommand(query5, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                string query11 = "SELECT cb.ID as id FROM Cena_bilety as cb, Wyciagi as w WHERE w.ID = cb.ID_Wyciag AND cb.Cena_przejazd = '"+e4.cena+"' AND w.Nazwa = '"+e4.nazwa+"'";
                using (SqlCommand command = new SqlCommand(query11, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            e4.id = reader["id"].ToString();
                        }
                    }
                }
                string query12 = "INSERT INTO Cennik (ID_Cena_bilet, Data_rozp) VALUES ('" + e4.id + "', '" + e4.zak + "')";
                using (SqlCommand command = new SqlCommand(query12, conn))
                {
                    command.ExecuteNonQuery();
                }
                Response.Redirect("ZmianaCennika");
            }
        }

        //public void OnPostAkt()
        //{
        //    Licznik l1 = new Licznik();
        //    Cennik c1 = new Cennik();
        //    Cennik c2 = new Cennik();
        //    SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
        //    conn.Open();
        //    Wyciag w3 = new Wyciag();
        //    w3.id = Request.Form["id"].ToString();
        //    w3.nazwa = Request.Form["z_nazwa"].ToString();
        //    w3.cena = Request.Form["z_cena"].ToString();
        //    w3.rozp = Request.Form["z_rozp"].ToString();
        //    w3.zak = Request.Form["z_zak"].ToString();
        //    Wyciag w4 = new Wyciag();
        //    string query3 = "SELECT COUNT(cb.ID) as liczba FROM Cena_bilety as cb, Wyciagi as w WHERE w.ID = cb.ID_Wyciag AND w.Nazwa = '" + w3.nazwa + "' AND cb.Cena_przejazd = '" + w3.cena + "'";
        //    using (SqlCommand command = new SqlCommand(query3, conn))
        //    {
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                l1.liczba = reader["liczba"].ToString();
        //            }
        //        }
        //    }
        //    int x = int.Parse(l1.liczba);
        //    if(x == 0)
        //    {
        //        string query4 = "INSERT INTO Cena_bilety (ID_Wyciag, Cena_przejazd) VALUES ((SELECT ID FROM Wyciagi WHERE Nazwa = '" + w3.nazwa + "'), '" + w3.cena + "')";
        //        using (SqlCommand command = new SqlCommand(query4, conn))
        //        {
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    string query5 = "SELECT cb.ID as id FROM Cena_bilety as cb, Wyciagi as w WHERE cb.ID_Wyciag = w.ID AND w.Nazwa = '"+w3.nazwa+"' AND cb.Cena_przejazd = '"+w3.cena+"'";
        //    using (SqlCommand command = new SqlCommand(query5, conn))
        //    {
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                c1.id_b = reader["id"].ToString();
        //            }
        //        }
        //    }
        //    string query6 = "SELECT c.Data_zak as zak, c.Data_rozp as rozp, c.ID as id  FROM Cennik as c, Cena_bilety as cb, Wyciagi as w WHERE c.ID_Cena_bilet = cb.ID AND cb.ID_Wyciag = w.ID AND c.Data_rozp < '2023-01-03' AND (c.Data_zak > '2023-01-03' OR c.Data_zak IS NULL) AND w.ID = '" + w3.id + "'";
        //    using (SqlCommand command = new SqlCommand(query6, conn))
        //    {
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                w4.zak = reader["zak"].ToString();
        //                w4.rozp = reader["rozp"].ToString();
        //                c2.id_b = reader["id"].ToString();
        //            }
        //        }
        //    }
        //    if (w4.zak == "")
        //    {
        //        var date1 =  DateTime.Parse(w3.rozp);
        //        w4.zak = date1.Year.ToString() + "-" + date1.Month.ToString() + "-" + date1.Day.ToString();
        //        string query7 = "UPDATE CENNIK SET Data_zak = '"+w4.zak+"' WHERE ID = '"+c2.id_b+ "' AND Data_rozp < '2023-01-03' AND (Data_zak > '2023-01-03' OR Data_zak IS NULL)";
        //        using (SqlCommand command = new SqlCommand(query7, conn))
        //        {
        //            command.ExecuteNonQuery();
        //        }
        //        Response.Redirect("ZmianaCennika");
        //    }
        //    else
        //    {
        //        var date1 = DateTime.Parse(w3.rozp);
        //        var date2 = DateTime.Parse(w4.zak);
        //        var diff = date1 - date2;
        //        int days = diff.Days;
        //        if (days < 0)
        //        {
        //            Error e1 = new Error();
        //            e1.tekst = "Data rozpoczêcia nowego cennika jest przed dat¹ zakoñczenia starego cennika -> '"+w4.zak+"'";
        //            e.Add(e1);
        //        }
        //        else if (days > 1)
        //        {
        //            Response.Redirect("ZmianaCennika");
        //        }
        //        else
        //        {
        //            Response.Redirect("ZmianaCennika");
        //        }
        //    }

        //}
        public class Wyciag
        {
            public string id, nazwa, cena, rozp, zak;
        }
        public class Cennik
        {
            public string id_c, rozp, zak, id;
        }
        public class Edycja
        {
            public string nazwa, rozp, zak, cena, id, gr;
        }

        public class Licznik
        {
            public string liczba;
        }
    }
}
