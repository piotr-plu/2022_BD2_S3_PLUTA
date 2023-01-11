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
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-L54I9S2\\NARCIARZE;Initial Catalog=narty;Integrated Security=True");
            conn.Open();
            string query1 = "SELECT ID as id, ID_Klient as id_k, ID_Stok as id_s, Czas_trwania as czas, Data_aktywacji as data, Status as stat, ID_Cennika as id_c FROM Karnety WHERE ID_Klient = 1 AND Czas_trwania > 0";
            string query2 = "SELECT ID as id, ID_Klient as id_k, ID_Stok as id_s, Czas_trwania as czas, Data_aktywacji as data, Status as stat, ID_Cennika as id_c FROM Karnety WHERE ID_Klient = 1 AND Czas_trwania = 0";
            string query3 = "SELECT ID as id, ID_Klient as id_k, ID_Wyciag as id_w, ID_Cennik as id_c, Ilosc_zjazdow as ilosc FROM Bilety WHERE ID_Klient = 1 AND Ilosc_zjazdow > 0";
            string query4 = "SELECT ID as id, ID_Klient as id_k, ID_Wyciag as id_w, ID_Cennik as id_c, Ilosc_zjazdow as ilosc FROM Bilety WHERE ID_Klient = 1 AND Ilosc_zjazdow = 0";
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
                        //conn.Close();
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
                        //conn.Open();
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
            public string id, id_k, czas, data_akt, status, id_c;
        }
        public class Bilety_A
        {
            public string id, id_k, id_w, ilosc, id_c;
        }

        public class Bilety_N
        {
            public string id, id_k, id_w, ilosc, id_c;
        }

    }
}
