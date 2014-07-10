using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class UpdateJournalUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //月份用
            SqlConnection Conn_Month = new SqlConnection();
            Conn_Month.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            SqlDataReader dr_Month = null;
            SqlCommand cmd_Month = new SqlCommand("SELECT * FROM [Account_Numbers_D]", Conn_Month);
            try
            {
                Conn_Month.Open();
                dr_Month = cmd_Month.ExecuteReader();
                DropDownList1.DataValueField = "Account_numbers";
                DropDownList1.DataTextField = "Account_name";
                DropDownList1.DataSource = dr_Month;
                DropDownList1.DataBind();
            }
            catch (Exception ex_Month)
            {
                Response.Write("<b>Error Message----  </b>" + ex_Month.ToString());
            }
            finally
            {
                if (dr_Month != null)
                {
                    cmd_Month.Cancel();
                    dr_Month.Close();
                }
                if (Conn_Month.State == ConnectionState.Open)
                {
                    Conn_Month.Close();
                    Conn_Month.Dispose();
                }
            }
            //月份用

            //資料用
            SqlConnection Conn_Data = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataReader Dr_Data = null;
            SqlCommand Cmd_Data = new SqlCommand("select * from [Account_Order_M_View] where id=" + Request["Id"], Conn_Data);

            try
            {
                Conn_Data.Open();
                Dr_Data = Cmd_Data.ExecuteReader();
                Dr_Data.Read();
                TextBox1.Text = Convert.ToDateTime(Dr_Data["Order_date"]).ToShortDateString();
                DropDownList1.Text = Dr_Data["Account_numbers"].ToString();
                TextBox2.Text = Dr_Data["Assign_numbers"].ToString();
                TextBox3.Text = Dr_Data["Account_abstract"].ToString();
                TextBox4.Text = Dr_Data["Income"].ToString();
                TextBox5.Text = Dr_Data["Spend"].ToString();
            }
            catch (Exception ex)
            {
                Response.Write("error" + ex.ToString());
            }
            finally
            {
                if (Dr_Data != null)
                {
                    Cmd_Data.Cancel();
                    Dr_Data.Close();
                }
                if (Conn_Data.State == ConnectionState.Open)
                {
                    Conn_Data.Close();
                    Conn_Data.Dispose();
                }
            }
            //資料用
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        TextBox1.Text = Calendar1.SelectedDate.ToShortDateString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

            SqlDataSource sqldatasource2 = new SqlDataSource();
            sqldatasource2.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            sqldatasource2.UpdateParameters.Add("Order_date", TextBox1.Text);
            sqldatasource2.UpdateParameters.Add("Assign_numbers", TextBox2.Text);
            sqldatasource2.UpdateParameters.Add("Account_numbers", DropDownList1.Text);
            sqldatasource2.UpdateParameters.Add("Account_abstract", TextBox3.Text);
            sqldatasource2.UpdateParameters.Add("Income", TextBox4.Text);
            sqldatasource2.UpdateParameters.Add("Spend", TextBox5.Text);
            sqldatasource2.UpdateCommand = "Update [Account_Order_M] set Order_date=@Order_date, Account_numbers=@Account_numbers, Assign_numbers=@Assign_numbers, Account_abstract=@Account_abstract, Income=@Income, Spend=@Spend where Id="+Request["Id"];

            int affraw_update = sqldatasource2.Update();

            if (affraw_update == 0)
            {
                Label1.Text = "error";
            }
            else
            {
                Label1.Text = "ok";
                Button1.Visible = false;
            }
            sqldatasource2.Dispose();


    }
}