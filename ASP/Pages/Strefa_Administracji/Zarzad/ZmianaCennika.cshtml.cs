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
        public List<Nazwa> n = new List<Nazwa>();
        public List<Nazwa> nk = new List<Nazwa>();
        public List<Stok> s = new List<Stok>();
        public List<Cennik> ck = new List<Cennik>();
        public List<Edycja> ek = new List<Edycja>();
        public List<Edycja> eek = new List<Edycja>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
        }
        //---EDYCJA CENNIKA KARNET
        public void OnPostWyciag()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            //Ponowe za³adowanie wyci¹gów aby mo¿liwe by³o ich ponowne wybranie
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            //---------------------------------------------------------------
            //---------|Generowanie listy rozwijanej z cennikami|------------
            Wyciag w2 = new Wyciag();
            w2.id = Request.Form["wyciag"].ToString();
            string query1 = "SELECT Nazwa as nazwa from Wyciagi where ID = '" + w2.id + "'";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Nazwa n1 = new Nazwa();
                        n1.nazwa = reader["nazwa"].ToString();
                        n.Add(n1);
                    }
                }
            }
            string query2 = "SELECT c.ID as id, c.Data_rozp as rozp, c.Data_zak as zak FROM Cennik as c, Cena_bilety as cb, Wyciagi as w WHERE c.ID_Cena_bilet = cb.ID AND w.ID = cb.ID_Wyciag AND ((c.Data_rozp < (SELECT CAST(GETDATE() as Date)) AND c.Data_zak > (SELECT CAST(GETDATE() as Date))) OR (c.Data_rozp > (SELECT CAST(GETDATE() as Date)) AND c.Data_zak > (SELECT CAST(GETDATE() as Date))) OR c.Data_zak IS NULL) AND w.ID = '" + w2.id + "'";
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cennik c1 = new Cennik();
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
            //Ponowe za³adowanie wyci¹gów aby mo¿liwe by³o ich ponowne wybranie
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            //---------------------------------------------------------
            //-----|Zebranie danych do edycji istniej¹cego cennika|----
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
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            //Ponowe za³adowanie wyci¹gów aby mo¿liwe by³o ich ponowne wybranie
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            Licznik l1 = new Licznik();
            Edycja e2 = new Edycja();
            Cennik c3 = new Cennik();
            Nazwa n2 = new Nazwa();
            //-Zebranie danych z formsa-
            e2.nazwa = Request.Form["nazwa"].ToString();
            e2.cena = Request.Form["cena"].ToString();
            float icena = float.Parse(e2.cena);
            if (icena <= 0)
            {
                n2.error = "Wprowadzona cena musi spe³niaæ warunek: cena > 0";
                n.Add(n2);
                
            }
            else
            {
                e2.rozp = Request.Form["rozp"].ToString();
                e2.zak = Request.Form["zak"].ToString();
                var date1 = DateTime.Parse(e2.rozp);
                e2.rozp = date1.Year.ToString() + "-" + date1.Month.ToString() + "-" + date1.Day.ToString();
                string query3 = "SELECT c.ID as id FROM Cennik as c, Cena_bilety as cb, Wyciagi as w " +
                    "WHERE c.ID_Cena_bilet = cb.ID AND w.ID = cb.ID_Wyciag " +
                    "AND w.Nazwa = '" + e2.nazwa + "' AND  c.Data_rozp = '" + e2.rozp + "'";
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
                //--Sprawdzenie czy istnieje cennik bilety dla danego wyci¹gu o podanej cenie
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
                //W przypadku braku istnienia cennika dodanie go do tabeli
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
                string query7 = "UPDATE CENNIK set ID_Cena_bilet = '" + c3.id + "' WHERE ID = '" + c3.id_c + "'";
                using (SqlCommand command = new SqlCommand(query7, conn))
                {
                    command.ExecuteNonQuery();
                }
                Response.Redirect("ZmianaCennika");
            }
        }


        public void OnPostCennik_n()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            //Ponowe za³adowanie wyci¹gów aby mo¿liwe by³o ich ponowne wybranie
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            //-----------------------------------------------------
            //Wype³nienie danymi edycyjnymi i informacyjnymi formsa
            Edycja e3 = new Edycja();
            e3.id = Request.Form["wyciag"].ToString();
            string query8 = "SELECT Nazwa as nazwa, CAST(GETDATE() as Date) as Date FROM Wyciagi WHERE ID = '" + e3.id + "'";
            using (SqlCommand command = new SqlCommand(query8, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Edycja ee1 = new Edycja();
                        ee1.nazwa = reader["nazwa"].ToString();
                        ee1.rozp = reader["Date"].ToString();
                        var date = DateTime.Parse(ee1.rozp);
                        ee1.rozp = date.Day.ToString() + "." + date.Month.ToString() + "." + date.Year.ToString();
                        ee.Add(ee1);
                    }
                }
            }
        }

        public void OnPostDodaj()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            //Ponowe za³adowanie wyci¹gów aby mo¿liwe by³o ich ponowne wybranie
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            //-----------------------------------------------------
            Licznik l1 = new Licznik();
            Edycja e4 = new Edycja();
            Cennik c3 = new Cennik();
            Nazwa n2 = new Nazwa();
            e4.nazwa = Request.Form["nazwa"].ToString();
            e4.rozp = Request.Form["rozp"].ToString();
            e4.cena = Request.Form["cena"].ToString();
            float icena = float.Parse(e4.cena);
            if (icena <= 0)
            {
                n2.error = "Wprowadzona cena musi spe³niaæ warunek: cena > 0";
                n.Add(n2);
            }
            else
            {
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
                string query_time = "SELECT CAST(GETDATE() as Date) as Date";
                using (SqlCommand command = new SqlCommand(query_time, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            l1.dzis = reader["Date"].ToString();
                        }
                    }
                }
                //Mechanizm sprawdzania dat, czy nowa data cennika po dzisiejszej dacie oraz po dacie ostatniego cennika
                var date1 = DateTime.Parse(c3.rozp);//Data ostatniego cennika z bazy
                var date2 = DateTime.Parse(e4.rozp);//Data z formsa
                var date3 = DateTime.Parse(l1.dzis);//Data dzisiejsza
                var diff = date2 - date1;
                var diff1 = date2 - date3;
                int idiff = diff.Days;
                int idiff1 = diff1.Days;
                if (idiff <= 0 || idiff1 < 0)
                {
                    n2.error = "Podana data jest nieprawid³owa, nie mo¿na podaæ daty poprzedzaj¹cej dzisiejsz¹, lub daty najnowszego istniej¹cego cennika.";
                    n.Add(n2);
                }
                else
                {
                    e4.zak = date2.Year.ToString() + "-" + date2.Month.ToString() + "-" + date2.Day.ToString();
                    string query10 = "UPDATE Cennik SET Data_zak = '" + e4.zak + "' WHERE ID = '" + c3.id + "'";
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
                    string query11 = "SELECT cb.ID as id FROM Cena_bilety as cb, Wyciagi as w WHERE w.ID = cb.ID_Wyciag AND cb.Cena_przejazd = '" + e4.cena + "' AND w.Nazwa = '" + e4.nazwa + "'";
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
        }
        //---------------------------EDYCJA CENNIKA KARNET-----------------------\\
        public void OnPostStok()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            Stok s2 = new Stok();
            s2.id = Request.Form["stok"].ToString();
            s2.godz = Request.Form["godz"].ToString(); string query0 = "SELECT Nazwa as nazwa from Stoki where ID = '" + s2.id + "'";
            using (SqlCommand command = new SqlCommand(query0, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Nazwa n1 = new Nazwa();
                        n1.nazwa = reader["nazwa"].ToString();
                        nk.Add(n1);
                    }
                }
            }
            string query1 = "SELECT c.ID as id,c.Data_rozp as rozp, c.Data_zak as zak FROM Cennik as c, Stoki as s, Cena_karnety as ck WHERE c.ID_Cena_karnet = ck.ID AND ck.ID_Stok = s.ID AND ck.Czas = '"+s2.godz+"' AND s.ID ='"+s2.id+ "' AND (c.Data_rozp < (SELECT CAST(GETDATE() as Date)) AND c.Data_zak > (SELECT CAST(GETDATE() as Date)) OR c.Data_rozp > (SELECT CAST(GETDATE() as Date)) OR c.Data_zak > (SELECT CAST(GETDATE() as Date)) OR c.Data_rozp IS NULL)";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Cennik c1 = new Cennik();
                        c1.id = reader["id"].ToString();
                        c1.rozp = reader["rozp"].ToString();
                        string[] subs = c1.rozp.Split(" ");
                        c1.rozp = subs[0];
                        c1.zak = reader["zak"].ToString();
                        subs = c1.zak.Split(" ");
                        c1.zak = subs[0];
                        ck.Add(c1);
                    }
                }
            }
        }
        public void OnPostCennik_k()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            Cennik c2 = new Cennik();
            c2.id = Request.Form["cennik_k"].ToString();
            string query3 = "SELECT s.Nazwa as nazwa, ck.Cena as cena,ck.Czas as czas, c.Data_rozp as rozp, c.Data_zak as zak FROM Cennik as c, Stoki as s, Cena_karnety as ck WHERE c.ID_Cena_karnet = ck.ID AND s.ID = ck.ID_Stok AND c.ID ='" + c2.id + "'";
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
                        e1.gr = reader["czas"].ToString();
                        string[] tab = e1.rozp.Split(" ");
                        e1.rozp = tab[0];
                        tab = e1.zak.Split(" ");
                        e1.zak = tab[0];
                        ek.Add(e1);
                    }
                }
            }
        }

        public void OnPostEdytuj_k()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            Licznik l1 = new Licznik();
            Edycja e2 = new Edycja();
            Cennik c3 = new Cennik();
            Nazwa n2 = new Nazwa();
            //-Zebranie danych z formsa-
            e2.nazwa = Request.Form["nazwa"].ToString();
            e2.cena = Request.Form["cena"].ToString();
            float icena = float.Parse(e2.cena);
            if (icena <= 0)
            {
                n2.error = "Wprowadzona cena musi spe³niaæ warunek: cena > 0";
                n.Add(n2);

            }
            else
            {
                e2.rozp = Request.Form["rozp"].ToString();
                e2.zak = Request.Form["zak"].ToString();
                e2.gr = Request.Form["czas"].ToString();
                var date1 = DateTime.Parse(e2.rozp);
                e2.rozp = date1.Year.ToString() + "-" + date1.Month.ToString() + "-" + date1.Day.ToString();
                string query3 = "SELECT c.ID as id FROM Cennik as c, Cena_karnety as ck, Stoki as s " +
                    "WHERE c.ID_Cena_karnet = ck.ID AND ck.ID_Stok = s.ID "+
                    "AND s.Nazwa = '"+e2.nazwa+"' AND ck.Czas = '"+e2.gr+"' AND c.Data_rozp ='"+e2.rozp+"'";
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
                string query4 = "SELECT COUNT(ck.ID) as liczba FROM Cena_karnety as ck, Stoki as s WHERE s.ID = ck.ID_Stok AND s.Nazwa = '" + e2.nazwa + "' AND ck.Cena = '" + icena + "' AND ck.Czas = '"+e2.gr+"'";
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
                    string query5 = "INSERT INTO Cena_karnety (ID_Stok, Cena, Czas) VALUES ((SELECT ID FROM Stoki WHERE Nazwa = '" + e2.nazwa + "'), '" + icena + "', '"+e2.gr+"')";
                    using (SqlCommand command = new SqlCommand(query5, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                string query6 = "SELECT ck.ID as id FROM Cena_karnety as ck, Stoki as s WHERE s.Nazwa = '" + e2.nazwa + "' AND ck.Cena = '" + icena + "' AND s.ID = ck.ID_Stok AND ck.Czas = '"+e2.gr+"'";
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
                string query7 = "UPDATE CENNIK set ID_Cena_karnet = '" + c3.id + "' WHERE ID = '" + c3.id_c + "'";
                using (SqlCommand command = new SqlCommand(query7, conn))
                {
                    command.ExecuteNonQuery();
                }
                Response.Redirect("ZmianaCennika");
            }
        }
        public void OnPostCennik_n_b()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            Edycja ee1 = new Edycja();
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            Stok s2 = new Stok();
            s2.id = Request.Form["stok"].ToString();
            ee1.gr = Request.Form["godz"].ToString();
            string query8 = "SELECT Nazwa as nazwa, CAST(GETDATE() as Date) as Date FROM Stoki WHERE ID = '" + s2.id + "'";
            using (SqlCommand command = new SqlCommand(query8, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ee1.nazwa = reader["nazwa"].ToString();
                        ee1.rozp = reader["Date"].ToString();
                        var date = DateTime.Parse(ee1.rozp);
                        ee1.rozp = date.Day.ToString() + "." + date.Month.ToString() + "." + date.Year.ToString();
                        eek.Add(ee1);
                    }
                }
            }
        }

        public void OnPostDodaj_k()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            //Ponowe za³adowanie wyci¹gów aby mo¿liwe by³o ich ponowne wybranie
            string queryw = "SELECT ID as id, Nazwa as nazw FROM Wyciagi";
            string querys = "SELECT ID as id, Nazwa as nazw FROM Stoki";
            using (SqlCommand command = new SqlCommand(queryw, conn))
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
            using (SqlCommand command = new SqlCommand(querys, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Stok s1 = new Stok();
                        s1.id = reader["id"].ToString();
                        s1.nazwa = reader["nazw"].ToString();
                        s.Add(s1);
                    }
                }
            }
            //-----------------------------------------------------
            Licznik l1 = new Licznik();
            Edycja e4 = new Edycja();
            Cennik c3 = new Cennik();
            Nazwa n2 = new Nazwa();
            e4.nazwa = Request.Form["nazwa"].ToString();
            e4.rozp = Request.Form["rozp"].ToString();
            e4.cena = Request.Form["cena"].ToString();
            e4.gr = Request.Form["czas"].ToString();
            float icena = float.Parse(e4.cena);
            if (icena <= 0)
            {
                n2.error = "Wprowadzona cena musi spe³niaæ warunek: cena > 0";
                n.Add(n2);
            }
            else
            {
                string query9 = "SELECT c.ID as id, c.Data_rozp as rozp FROM Cennik as c, Stoki as s, Cena_karnety as ck WHERE c.ID_Cena_karnet = ck.ID AND s.ID = ck.ID_Stok AND c.Data_zak IS NULL AND s.Nazwa = '" + e4.nazwa + "' AND ck.Czas = '" + e4.gr + "'";
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
                string query_time = "SELECT CAST(GETDATE() as Date) as Date";
                using (SqlCommand command = new SqlCommand(query_time, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            l1.dzis = reader["Date"].ToString();
                        }
                    }
                }
                //Mechanizm sprawdzania dat, czy nowa data cennika po dzisiejszej dacie oraz po dacie ostatniego cennika
                var date1 = DateTime.Parse(c3.rozp);//Data ostatniego cennika z bazy
                var date2 = DateTime.Parse(e4.rozp);//Data z formsa
                var date3 = DateTime.Parse(l1.dzis);//Data dzisiejsza
                var diff = date2 - date1;
                var diff1 = date2 - date3;
                int idiff = diff.Days;
                int idiff1 = diff1.Days;
                if (idiff <= 0 || idiff1 < 0)
                {
                    n2.error = "Podana data jest nieprawid³owa, nie mo¿na podaæ daty poprzedzaj¹cej dzisiejsz¹, lub daty najnowszego istniej¹cego cennika.";
                    n.Add(n2);
                }
                else
                {
                    e4.zak = date2.Year.ToString() + "-" + date2.Month.ToString() + "-" + date2.Day.ToString();
                    string query10 = "UPDATE Cennik SET Data_zak = '" + e4.zak + "' WHERE ID = '" + c3.id + "'";
                    using (SqlCommand command = new SqlCommand(query10, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                    string querya = "SELECT COUNT(ck.ID) as liczba FROM Cena_karnety as ck, Stoki as s WHERE s.ID = ck.ID_Stok AND s.Nazwa = '" + e4.nazwa + "' AND ck.Cena= '" + e4.cena + "' AND ck.Czas = '" + e4.gr + "'";
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
                        string query5 = "INSERT INTO Cena_karnety (ID_Stok, Cena, Czas) VALUES ((SELECT ID FROM Stoki WHERE Nazwa = '" + e4.nazwa + "'), '" + e4.cena + "', '" + e4.gr + "')";
                        using (SqlCommand command = new SqlCommand(query5, conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    string query11 = "SELECT ck.ID as id FROM Cena_karnety as ck, Stoki as s WHERE s.ID = ck.ID_Stok AND ck.Cena = '" + e4.cena + "' AND s.Nazwa = '" + e4.nazwa + "'AND ck.Czas = '" + e4.gr + "'";
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
                    string query12 = "INSERT INTO Cennik (ID_Cena_karnet, Data_rozp) VALUES ('" + e4.id + "', '" + e4.zak + "')";
                    using (SqlCommand command = new SqlCommand(query12, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                    Response.Redirect("ZmianaCennika");
                }
            }
        }


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
            public string liczba, dzis;
        }
        public class Nazwa
        {
            public string nazwa, error;
        }
        public class Stok
        {
            public string id, nazwa, godz;
        }
    }
}
