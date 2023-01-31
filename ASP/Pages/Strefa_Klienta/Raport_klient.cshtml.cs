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
        public List<Json> raport_bilety = new List<Json>();
        public List<Json> raport_karnety = new List<Json>();

        public void OnGet()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            


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
              select cast(k.Data_sprzedazy as date) as data_sprzedazy,
            sum(k.Czas_trwania) as laczny_czas
            from Karnety as k where k.ID_Klient="+ HttpContext.Session.GetInt32("_klient_id").ToString() + " GROUP BY Data_sprzedazy) select(select data_sprzedazy, sum(laczny_czas) over (order by data_sprzedazy asc rows between unbounded preceding and current row) as suma_kumulowana from data for json auto)as jsonek;";
            
            string query_raport_bilety = @"with data as (
              select  cast(b.Data_sprzedazy as date) as data_sprzedazy, sum(b.Ilosc_zjazdow*w.Dlugosc) as suma
            FROM Bilety as b, Wyciagi as w
            where b.ID_Wyciag = w.ID AND b.ID_Klient="+ HttpContext.Session.GetInt32("_klient_id").ToString() + 
            " GROUP BY data_sprzedazy) select(select data_sprzedazy, sum(suma) over (order by data_sprzedazy asc rows between unbounded preceding and current row) as suma_kumulowana from data for json auto)as jsonek;";
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
                        Json pobrane = new Json();
                        pobrane.json = reader["jsonek"].ToString();
                        raport_bilety.Add(pobrane);

                    }
                }

            }

            using (SqlCommand command = new SqlCommand(query_raport_karnety, conn))
            {https://localhost:44301/
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Json pobrane = new Json();
                        pobrane.json = reader["jsonek"].ToString();

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
                return File(byteArrayOutputStream.ToArray(), "application/pdf", "Raport_Klient.pdf");
            }
        }
        public class Stok
        {
            public string id, imie, nazw;
        }
        public class Json
        {
            public string json;
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
