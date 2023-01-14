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
        string connectionString = "Data Source=DESKTOP-QIV9GDD\\SQLEXPRESS;Initial Catalog=Narty_V3;Integrated Security=True";

        public List<Karnety> karnety = new List<Karnety>();
        public List<Bilety> bilety = new List<Bilety>();

        public void OnGet()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //-> TO DO do ka¿dego z zasad zapytania query1-query4 zamiast na sztywno przypisywaæ id klienta pobraæ z sesji, w przypadku chêci odczytania dat zakupu (mniej wiêcej)
            //Mo¿na urzyæ zakresu dat z cennika do z³apania odopowiednich dat

            string query1 = @"SELECT k.ID as id_kar, k.Status as s_kar, k.Czas_trwania as poz_czas, s.Nazwa as n_stoku, k.Data_aktywacji as d_aktywacji, ck.Czas as czas_bilet, ck.Cena as Cena
                            FROM Karnety as k 
                            LEFT JOIN Cennik as c ON k.ID_Cennika = c.ID
                            LEFT JOIN Cena_karnety as ck ON ck.ID = c.ID_Cena_karnet 
                            LEFT JOIN Stoki as s ON s.ID = ck.ID_Stok
                            WHERE k.ID_Klient = 1
                            ORDER BY poz_czas, Data_aktywacji";

            string query2 = @"SELECT b.ID as id_bilet, b.Ilosc_zjazdow as il_zjazdow, w.Nazwa as n_wyciagu, cb.Cena_przejazd as cena_przejazd
                            FROM Bilety as b
                            LEFT JOIN Cennik as c ON b.ID_Cennik = c.ID
                            LEFT JOIN Cena_bilety as cb ON c.ID_Cena_bilet = cb.ID
                            LEFT JOIN Wyciagi as w ON cb.ID_Wyciag = w.ID
                            WHERE b.ID_Klient = 1
                            ORDER BY il_zjazdow DESC";


            // Wykonanie Zapytania SQL o Karnety 
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Karnety pobrane = new Karnety();
                        pobrane.idKar = reader["id_kar"].ToString();
                        pobrane.statusKar = reader["s_kar"].ToString();
                        pobrane.nazwaStoku = reader["n_stoku"].ToString();
                        pobrane.pozCzas = reader["poz_czas"].ToString();
                        pobrane.dAktywacji = reader["d_aktywacji"].ToString();
                        pobrane.czasBilet = reader["czas_bilet"].ToString();
                        pobrane.cena = reader["cena"].ToString();

                        karnety.Add(pobrane);

                    }
                }

            }

            using (SqlCommand command = new SqlCommand(query2, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bilety pobrane = new Bilety();
                        pobrane.idBil = reader["id_bilet"].ToString();
                        pobrane.ilZjazdow = reader["il_zjazdow"].ToString();
                        pobrane.nWyciagu = reader["n_wyciagu"].ToString();
                        pobrane.cena = reader["cena_przejazd"].ToString();

                        bilety.Add(pobrane);

                    }
                }

            }



        }


        public class Karnety
        {
            public string idKar, statusKar, nazwaStoku, pozCzas, dAktywacji, czasBilet, cena;
        }

        public class Bilety
        {
            public string idBil, ilZjazdow, nWyciagu, cena;
        }

    }
}
