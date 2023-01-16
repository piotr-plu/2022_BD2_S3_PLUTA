using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Narciarze_v_2.Pages.Strefa_Administracji.Administrator
{
    public class HarmonogramModel : PageModel
    {
        //public List<WyciagID> wyciagiID = new List<WyciagID>();
        public List<WyciagiHarm> wyciagi = new List<WyciagiHarm>();
        public List<HarmDaty> harmDaty = new List<HarmDaty>();
        public List<NowyHarmonogram> infoHarmonogram = new List<NowyHarmonogram>();
        public List<NowyHarmonogram> NowyHarmonogram = new List<NowyHarmonogram>();

        public List<HarmDaty> errorDate = new List<HarmDaty>();
        public bool error = false;

        public void OnGet()
        {

            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();

            string query1 = "SELECT w.ID as id, w.Nazwa as nazwa FROM Wyciagi as w";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WyciagiHarm wyciag = new WyciagiHarm();
                        wyciag.id = reader["id"].ToString();
                        wyciag.nazwa = reader["nazwa"].ToString();
                        wyciagi.Add(wyciag);
                    }
                }
            }
        }
        public void OnPostDodajHarm()
        {
            NowyHarmonogram harmonogram = new NowyHarmonogram();


            harmonogram.id = Request.Form["nazwaWyciagDodajHarm"];
            harmonogram.stan = Request.Form["stanWyciagHarmDodaj"];
            harmonogram.dataRozp = Request.Form["dataRozpHarmDodaj"];
            harmonogram.dataZak = Request.Form["dataZakHarmDodaj"];

            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");

            string query1 = "SELECT h.Data_rozp as dataRozp, h.Data_zak as dataZak FROM Harmonogram as h WHERE h.ID_Wyciagi = 1";

            conn.Open();
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HarmDaty daty = new HarmDaty();
                        daty.dataRozp = reader["dataRozp"].ToString();
                        daty.dataZak = reader["dataZak"].ToString();
                        harmDaty.Add(daty);
                    }
                }
            }

            string dateFormat = "dd.MM.yyyy HH:mm:ss";

            // Walidacja dat 

            foreach (var data in harmDaty)
            {
                if ((DateTime.ParseExact(data.dataRozp, dateFormat, null)) <= (DateTime.Parse(harmonogram.dataZak, CultureInfo.InvariantCulture))
                    && (DateTime.ParseExact(data.dataZak, dateFormat, null)) >= (DateTime.Parse(harmonogram.dataRozp, CultureInfo.InvariantCulture)))
                {
                    HarmDaty errors = new HarmDaty();

                    errors.dataRozp = harmonogram.dataRozp;
                    errors.dataZak = harmonogram.dataZak;
                    errorDate.Add(errors);

                    error = true;
                }

                else if (((DateTime.ParseExact(data.dataRozp, dateFormat, null)) <= (DateTime.Parse(harmonogram.dataZak, CultureInfo.InvariantCulture)))
                        && ((DateTime.Parse(harmonogram.dataZak, CultureInfo.InvariantCulture)) <= (DateTime.ParseExact(data.dataZak, dateFormat, null)))
                        && !error)
                {
                    HarmDaty errors = new HarmDaty();

                    errors.dataRozp = data.dataRozp;
                    errors.dataZak = harmonogram.dataZak;
                    errorDate.Add(errors);

                    error = true;

                }

                else if (((DateTime.ParseExact(data.dataZak, dateFormat, null)) <= (DateTime.Parse(harmonogram.dataRozp, CultureInfo.InvariantCulture)))
                        && ((DateTime.Parse(harmonogram.dataRozp, CultureInfo.InvariantCulture)) <= (DateTime.ParseExact(data.dataRozp, dateFormat, null)))
                        && !error)
                {
                    HarmDaty errors = new HarmDaty();

                    errors.dataRozp = harmonogram.dataRozp;
                    errors.dataZak = data.dataZak;
                    errorDate.Add(errors);

                    error = true;

                }
            }

            if (!error)
            {
                string query2 = @"INSERT Harmonogram (ID_Wyciagi, Stan, Data_rozp, Data_zak)
                                  VALUES ('" + harmonogram.id + "', '" + harmonogram.stan + "', '" + harmonogram.dataRozp + "', '" + harmonogram.dataZak + "')";

                using (SqlCommand command = new SqlCommand(query2, conn))
                {
                    command.ExecuteNonQuery();
                }
            }


            Response.Redirect("Harmonogram");
        }

        public void OnPostWyciag()
        {
            WyciagID wyciag = new WyciagID();
            wyciag.id = Request.Form["nazwaWyciagHarm"];

            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            conn.Open();

            string query1 = "SELECT w.ID as id, w.Nazwa as nazwa FROM Wyciagi as w WHERE w.ID = "+wyciag.id+"";
            using (SqlCommand command = new SqlCommand(query1, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WyciagiHarm dane = new WyciagiHarm();
                        dane.id = reader["id"].ToString();
                        dane.nazwa = reader["nazwa"].ToString();
                        wyciagi.Add(dane);
                    }
                }
            }

            //SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Narty_V4;Integrated Security=True");
            //conn.Open();

            string query = "SELECT h.ID as idHarm, h.Stan as stan, h.Data_rozp as dataRozp, h.Data_zak as dataZak FROM Harmonogram as h WHERE h.ID_Wyciagi = " + wyciag.id + "";
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NowyHarmonogram info = new NowyHarmonogram();

                        info.id = reader["idHarm"].ToString();
                        info.stan = reader["stan"].ToString();
                        info.dataRozp = reader["dataRozp"].ToString();
                        info.dataZak = reader["dataZak"].ToString();

                        infoHarmonogram.Add(info);

                    }
                }
            }
        }

    }
}

public class WyciagID
{
    public string id;
}
public class WyciagiHarm
{
    public string id, nazwa;
}
public class HarmDaty
{
    public string dataRozp, dataZak;
}
public class NowyHarmonogram
{
    public string id, stan, dataRozp, dataZak;
}

