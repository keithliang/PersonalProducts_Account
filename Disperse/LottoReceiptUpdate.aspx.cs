using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;


public partial class LottoReceiptUpdate : System.Web.UI.Page
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
        SqlDataSource sqldatasource2 = new SqlDataSource();
        sqldatasource2.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
        sqldatasource2.UpdateParameters.Add("Lottery", DropDownList1.Text);
        sqldatasource2.UpdateCommand = "Update [Account_Order_M] set Lottery=@Lottery where Id=" + Request["Id"];
        
        int affraw_update = sqldatasource2.Update();

        if (affraw_update == 0)
        {
            Label8.Text = "error";
        }
        else
        {
            Label8.Text = "ok";
            Button1.Visible = false;
        }
        sqldatasource2.Dispose();
    }
}