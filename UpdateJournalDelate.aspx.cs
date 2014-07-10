using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class UpdateJournalDelate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select * from [Account_Order_M_View] where id=" + Request["Id"], conn);

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                Label1.Text = Convert.ToDateTime(dr["Order_date"]).ToShortDateString();
                Label2.Text = dr["Assign_numbers"].ToString();
                Label3.Text = dr["Account_numbers"].ToString();
                Label4.Text = dr["Account_name"].ToString();
                Label5.Text = dr["Account_abstract"].ToString();
                Label6.Text = dr["Income"].ToString();
                Label7.Text = dr["Spend"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
            }
            finally
            {
                if (dr != null)
                {
                    cmd.Cancel();
                    dr.Close();
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlDataSource SqlDataSource2 = new SqlDataSource();
        SqlDataSource2.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        try
        {
            SqlDataSource2.DeleteCommand = "delete [Account_Order_M] where id=" + Request["Id"];

            int affraw = SqlDataSource2.Delete();
            if (affraw == 0)
            {
                Label8.Text = "error";
                Button1.Visible = false;
            }
            else
            {
                Label8.Text = "ok";
                Button1.Visible = false;
            }
        }
        catch (Exception ex2)
        {
            Response.Write(ex2.ToString());
        }
        finally
        {
            SqlDataSource2.Dispose();
        }


    }

}