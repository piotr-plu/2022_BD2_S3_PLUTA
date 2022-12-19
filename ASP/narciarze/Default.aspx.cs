using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;

namespace narciarze
{
    public partial class Test1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\narciarze;Initial Catalog=narciarze;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open){
                con.Close();
            }
            con.Open();


        }

        protected void B1_Click(object sender, EventArgs e)
        {
            //SqlCommand cmd = con.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * FROM Klient WHERE Nazwisko = '"+T1.Text+"'";
            //cmd.ExecuteNonQuery();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from Klient WHERE Nazwisko = '"+T1.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Klient (Imię, Nazwisko, Email, Hasło) values ('" + Name.Text + "','" + Last_name.Text + "','" + Email.Text + "','" + Password.Text + "')";
            cmd.ExecuteNonQuery();


        }

        //public void disp_data()
        //{
        //    SqlCommand cmd = con.CreateCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "SELECT * from Klient";
        //    cmd.ExecuteNonQuery();
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(dt);
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();

        //}
    }
}