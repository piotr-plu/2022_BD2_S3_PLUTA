using iText.StyledXmlParser.Jsoup.Select;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Narciarze_v_2.Pages.Strefa_Klienta.CennikModel;

namespace Narciarze_v_2.Pages.Strefa_Klienta
{
    public class Raport_klientModel : PageModel
    {
        public List<Karnety_A> ka = new List<Karnety_A>();
        public List<Karnety_N> kn = new List<Karnety_N>();
        public List<Bilety_A> ba = new List<Bilety_A>();
        public List<Bilety_N> bn = new List<Bilety_N>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-L54I9S2\\NARCIARZE;Initial Catalog=narty;Integrated Security=True");
            conn.Open();
            //-> TO DO do ka¿dego z zasad zapytania query1-query4 zamiast na sztywno przypisywaæ id klienta pobraæ z sesji, w przypadku chêci odczytania dat zakupu (mniej wiêcej)
            //Mo¿na urzyæ zakresu dat z cennika do z³apania odopowiednich dat
            string query1 = "SELECT ID as id, ID_Klient as id_k, ID_Stok as id_s, Czas_trwania as czas, Data_aktywacji as data, Status as stat, ID_Cennika as id_c FROM Karnety WHERE ID_Klient = 1 AND Czas_trwania > 0";
            string query2 = "SELECT ID as id, ID_Klient as id_k, ID_Stok as id_s, Czas_trwania as czas, Data_aktywacji as data, Status as stat, ID_Cennika as id_c FROM Karnety WHERE ID_Klient = 1 AND Czas_trwania = 0";
            string query3 = "SELECT ID as id, ID_Klient as id_k, ID_Wyciag as id_w, ID_Cennik as id_c, Ilosc_zjazdow as ilosc FROM Bilety WHERE ID_Klient = 1 AND Ilosc_zjazdow > 0";
            string query4 = "SELECT ID as id, ID_Klient as id_k, ID_Wyciag as id_w, ID_Cennik as id_c, Ilosc_zjazdow as ilosc FROM Bilety WHERE ID_Klient = 1 AND Ilosc_zjazdow = 0";
            //Wyci¹gniêcie danych do aktualne karnety 
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Karnety_A ka1 = new Karnety_A();
                        ka1.id = reader["id"].ToString();
                        ka1.status = reader["stat"].ToString();
                        ka1.czas = reader["czas"].ToString();
                        ka1.data_akt = reader["data"].ToString();
                        ka1.id_s = reader["id_s"].ToString();
                        ka1.id_c = reader["id_c"].ToString();
                        string qquery1 = "SELECT ck.Cena as cena FROM Cena_karnety as ck, Cennik as c WHERE ck.ID = c.ID_Cena_karnet AND c.ID = '" + ka1.id_c + "'";
                        string qquery2 = "SELECT Nazwa as nazwa FROM Stoki WHERE ID = '"+ka1.id_s+"'";
                        SqlConnection conn1 = new SqlConnection("data source=desktop-l54i9s2\\narciarze;initial catalog=narty;integrated security=true");
                        conn1.Open();
                        using (SqlCommand command1 = new SqlCommand(qquery1, conn1))
                        {
                            using (SqlDataReader reader1 = command1.ExecuteReader())
                            {
                                reader1.Read();
                                ka1.cena = reader1["cena"].ToString();
                                conn1.Close();
                            }
                        }
                        SqlConnection conn2 = new SqlConnection("data source=desktop-l54i9s2\\narciarze;initial catalog=narty;integrated security=true");
                        conn2.Open();
                        using (SqlCommand command2 = new SqlCommand(qquery2, conn2))
                        {
                            using (SqlDataReader reader2 = command2.ExecuteReader())
                            {
                                reader2.Read();
                                ka1.nazwa_s = reader2["nazwa"].ToString();
                                conn2.Close();
                            }
                        }

                        ka.Add(ka1);
                    }
                }

            }
            //Wyci¹gniêcie danych o wykorzystanych karnetach
            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Karnety_N kn1 = new Karnety_N();
                        kn1.id = reader["id"].ToString();
                        kn1.status = reader["stat"].ToString();
                        kn1.czas = reader["czas"].ToString();
                        kn1.data_akt = reader["data"].ToString();
                        kn1.id_s = reader["id_s"].ToString();
                        kn1.id_c = reader["id_c"].ToString();
                        string qquery1 = "SELECT ck.Cena as cena FROM Cena_karnety as ck, Cennik as c WHERE ck.ID = c.ID_Cena_karnet AND c.ID = '" + kn1.id_c + "'";
                        string qquery2 = "SELECT Nazwa as nazwa FROM Stoki WHERE ID = '" + kn1.id_s + "'";
                        SqlConnection conn1 = new SqlConnection("data source=desktop-l54i9s2\\narciarze;initial catalog=narty;integrated security=true");
                        conn1.Open();
                        using (SqlCommand command1 = new SqlCommand(qquery1, conn1))
                        {
                            using (SqlDataReader reader1 = command1.ExecuteReader())
                            {
                                reader1.Read();
                                kn1.cena = reader1["cena"].ToString();
                                conn1.Close();
                            }
                        }
                        SqlConnection conn2 = new SqlConnection("data source=desktop-l54i9s2\\narciarze;initial catalog=narty;integrated security=true");
                        conn2.Open();
                        using (SqlCommand command2 = new SqlCommand(qquery2, conn2))
                        {
                            using (SqlDataReader reader2 = command2.ExecuteReader())
                            {
                                reader2.Read();
                                kn1.nazwa_s = reader2["nazwa"].ToString();
                                conn2.Close();
                            }
                        }

                        kn.Add(kn1);
                    }
                }

            }
            //Dane o aktualnych biletach
            using (SqlCommand command = new SqlCommand(query3, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bilety_A ba1 = new Bilety_A();
                        ba1.id = reader["id"].ToString();
                        ba1.id_c = reader["id_c"].ToString();
                        ba1.ilosc = reader["ilosc"].ToString();
                        ba1.id_w = reader["id_w"].ToString();
                        string qquery1 = "SELECT cb.Cena_przejazd as cena FROM Cena_bilety as cb, Cennik as c WHERE cb.ID = c.ID_Cena_bilet AND c.ID = '" + ba1.id_c + "'";
                        string qquery2 = "SELECT Nazwa as nazwa FROM Wyciagi WHERE ID = '" + ba1.id_w + "'";
                        SqlConnection conn1 = new SqlConnection("data source=desktop-l54i9s2\\narciarze;initial catalog=narty;integrated security=true");
                        conn1.Open();
                        using (SqlCommand command1 = new SqlCommand(qquery1, conn1))
                        {
                            using (SqlDataReader reader1 = command1.ExecuteReader())
                            {
                                reader1.Read();
                                ba1.cena = reader1["cena"].ToString();
                                conn1.Close();
                            }
                        }
                        SqlConnection conn2 = new SqlConnection("data source=desktop-l54i9s2\\narciarze;initial catalog=narty;integrated security=true");
                        conn2.Open();
                        using (SqlCommand command2 = new SqlCommand(qquery2, conn2))
                        {
                            using (SqlDataReader reader2 = command2.ExecuteReader())
                            {
                                reader2.Read();
                                ba1.nazwa_w = reader2["nazwa"].ToString();
                                conn2.Close();
                            }
                        }

                        ba.Add(ba1);
                    }
                }

            }
            //Dane o nieaktualnych biletach
            using (SqlCommand command = new SqlCommand(query4, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bilety_N bn1 = new Bilety_N();
                        bn1.id = reader["id"].ToString();
                        bn1.id_c = reader["id_c"].ToString();
                        bn1.ilosc = reader["ilosc"].ToString();
                        bn1.id_w = reader["id_w"].ToString();
                        string qquery1 = "SELECT cb.Cena_przejazd as cena FROM Cena_bilety as cb, Cennik as c WHERE cb.ID = c.ID_Cena_bilet AND c.ID = '" + bn1.id_c + "'";
                        string qquery2 = "SELECT Nazwa as nazwa FROM Wyciagi WHERE ID = '" + bn1.id_w + "'";
                        SqlConnection conn1 = new SqlConnection("data source=desktop-l54i9s2\\narciarze;initial catalog=narty;integrated security=true");
                        conn1.Open();
                        using (SqlCommand command1 = new SqlCommand(qquery1, conn1))
                        {
                            using (SqlDataReader reader1 = command1.ExecuteReader())
                            {
                                reader1.Read();
                                bn1.cena = reader1["cena"].ToString();
                                conn1.Close();
                            }
                        }
                        SqlConnection conn2 = new SqlConnection("data source=desktop-l54i9s2\\narciarze;initial catalog=narty;integrated security=true");
                        conn2.Open();
                        using (SqlCommand command2 = new SqlCommand(qquery2, conn2))
                        {
                            using (SqlDataReader reader2 = command2.ExecuteReader())
                            {
                                reader2.Read();
                                bn1.nazwa_w = reader2["nazwa"].ToString();
                                conn2.Close();
                            }
                        }

                        bn.Add(bn1);
                    }
                }

            }

        }

        public class Karnety_A
        {
            public string id, id_k, czas, data_akt, status, id_c, id_s, cena, nazwa_s;
        }
        public class Karnety_N
        {
            public string id, id_k, czas, data_akt, status, id_c, id_s, cena, nazwa_s;
        }
        public class Bilety_A
        {
            public string id, id_k, id_w, ilosc, id_c, cena, nazwa_w;
        }

        public class Bilety_N
        {
            public string id, id_k, id_w, ilosc, id_c, cena, nazwa_w;
        }

    }
}
