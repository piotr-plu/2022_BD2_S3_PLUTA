using iText.Html2pdf;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Zarzad
{
    public class raporty_zarzad_Model : PageModel
    {
        public List<Rap> rap_k = new List<Rap>();
        public List<Rap> rap_b = new List<Rap>();
        public void OnGet()
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();
            string query_karn = "SELECT  MONTH([Data_sprzedazy]) as mies, year([Data_sprzedazy]) as rok, sum(ck.Cena) as suma, count(k.ID) as suma_karnety FROM Karnety k, Stoki as s, Cennik as c, Cena_karnety as ck WHERE s.ID = ck.ID_Stok AND c.ID_Cena_karnet = ck.ID and k.ID_Cennik=c.ID GROUP BY MONTH([Data_sprzedazy]), YEAR([Data_sprzedazy])";
            string query_bil = "SELECT  MONTH([Data_sprzedazy]) as mies, year([Data_sprzedazy]) as rok, sum(cb.Cena_przejazd * b.Ilosc_zjazdow) as suma , count(b.ID) as suma_bilety FROM Bilety as b, Cena_bilety as cb, Cennik as c, Wyciagi as w  where c.ID_Cena_bilet = cb.ID AND cb.ID_Wyciag = w.ID and b.ID_Cennik=c.ID GROUP BY MONTH([Data_sprzedazy]), YEAR([Data_sprzedazy])";
            using (SqlCommand command = new SqlCommand(query_karn, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rap t1 = new Rap();
                        t1.mies = reader["mies"].ToString();
                        t1.rok = reader["rok"].ToString();
                        t1.suma = reader["suma"].ToString();
                        t1.il = reader["suma_karnety"].ToString();
                        rap_k.Add(t1);
                    }
                }
            }
            using (SqlCommand command = new SqlCommand(query_bil, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Rap t2 = new Rap();
                        t2.mies = reader["mies"].ToString();
                        t2.rok = reader["rok"].ToString();
                        t2.suma = reader["suma"].ToString();
                        t2.il = reader["suma_bilety"].ToString();
                        rap_b.Add(t2);
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
        public class Rap
        {
            public string mies, rok, suma, il;
        }
    }
}
