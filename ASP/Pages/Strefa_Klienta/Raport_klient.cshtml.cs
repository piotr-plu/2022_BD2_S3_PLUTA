using iText.StyledXmlParser.Jsoup.Select;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Narciarze_v_2.Pages.Strefa_Klienta.CennikModel;
using System.Text.Json;
using System.Text;


namespace Narciarze_v_2.Pages.Strefa_Klienta
{
    public class Raport_klientModel : PageModel
    {
        string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True";

        public List<Karnety> karnety = new List<Karnety>();
        public List<Bilety> bilety = new List<Bilety>();
        public List<Raport> raport_bilety = new List<Raport>();
        public List<Raport> raport_karnety = new List<Raport>();

        public void OnGet()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            //-> TO DO do ka¿dego z zasad zapytania query1-query4 zamiast na sztywno przypisywaæ id klienta pobraæ z sesji, w przypadku chêci odczytania dat zakupu (mniej wiêcej)
            //Mo¿na urzyæ zakresu dat z cennika do z³apania odopowiednich dat
            


            string query1 = @"SELECT k.ID as id_kar, k.Status as s_kar, k.Czas_trwania as poz_czas, s.Nazwa as n_stoku, k.Data_aktywacji as d_aktywacji, ck.Czas as czas_bilet, ck.Cena as Cena
                            FROM Karnety as k 
                            LEFT JOIN Cennik as c ON k.ID_Cennik = c.ID
                            LEFT JOIN Cena_karnety as ck ON ck.ID = c.ID_Cena_karnet 
                            LEFT JOIN Stoki as s ON s.ID = ck.ID_Stok
                            WHERE k.ID_Klient = "+ HttpContext.Session.GetInt32("_klient_id").ToString() + " ORDER BY poz_czas, Data_aktywacji";

            string query2 = @"SELECT b.ID as id_bilet, b.Ilosc_zjazdow as il_zjazdow, w.Nazwa as n_wyciagu, cb.Cena_przejazd as cena_przejazd
                            FROM Bilety as b
                            LEFT JOIN Cennik as c ON b.ID_Cennik = c.ID
                            LEFT JOIN Cena_bilety as cb ON c.ID_Cena_bilet = cb.ID
                            LEFT JOIN Wyciagi as w ON cb.ID_Wyciag = w.ID
                            WHERE b.ID_Klient= "+ HttpContext.Session.GetInt32("_klient_id").ToString() + " ORDER BY il_zjazdow DESC";

            string query_raport_karnety = @"with data as (
              select k.Data_sprzedazy as data_sprzedazy,
            sum(k.Czas_trwania) as laczny_czas
            from Karnety as k where k.ID_Klient="+ HttpContext.Session.GetInt32("_klient_id").ToString() + " GROUP BY Data_sprzedazy) select data_sprzedazy, sum(laczny_czas) over (order by data_sprzedazy asc rows between unbounded preceding and current row) as suma_kumulowana from data;";
            string query_raport_bilety = @"with data as (
              select  b.Data_sprzedazy as data_sprzedazy, sum(b.Ilosc_zjazdow*w.Dlugosc) as suma
            FROM Bilety as b, Wyciagi as w
            where b.ID_Wyciag = w.ID AND b.ID_Klient="+ HttpContext.Session.GetInt32("_klient_id").ToString() + 
            " GROUP BY data_sprzedazy) select data_sprzedazy, sum(suma) over (order by data_sprzedazy asc rows between unbounded preceding and current row) as suma_kumulowana from data;";
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
                        if (pobrane.dAktywacji == "")
                        {
                            pobrane.dAktywacji = "Brak";
                        }
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

            using (SqlCommand command = new SqlCommand(query_raport_bilety, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Raport pobrane = new Raport();
                        pobrane.data = reader["Data_sprzedazy"].ToString();
                        pobrane.suma = reader["suma_kumulowana"].ToString();

                        raport_bilety.Add(pobrane);

                    }
                }

            }

            using (SqlCommand command = new SqlCommand(query_raport_karnety, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Raport pobrane = new Raport();
                        pobrane.data = reader["Data_sprzedazy"].ToString();
                        pobrane.suma = reader["suma_kumulowana"].ToString();

                        raport_karnety.Add(pobrane);

                    }
                }

            }

        }
        public FileResult OnPostExport(string GridHtml)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(GridHtml)))
            {
                ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
                PdfWriter writer = new PdfWriter(byteArrayOutputStream);
                PdfDocument pdfDocument = new PdfDocument(writer);
                pdfDocument.SetDefaultPageSize(PageSize.A4);
                HtmlConverter.ConvertToPdf(stream, pdfDocument);
                pdfDocument.Close();
                return File(byteArrayOutputStream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }
        public class Stok
        {
            public string id, imie, nazw;
        }
        public class Raport
        {
            public string data, suma;
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
