using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class UpdateEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Show資料用
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select * from [Account_Order_M] where id=" + Request["Id"], conn);

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                dr.Read();

                TextBox1.Text = Convert.ToDateTime(dr["Order_date"]).ToShortDateString();
                TextBox2.Text = dr["Assign_numbers"].ToString();
                DropDownList1.SelectedValue = dr["Account_numbers"].ToString();
                TextBox3.Text = dr["Account_abstract"].ToString();
                TextBox4.Text = dr["Income"].ToString();
                TextBox5.Text = dr["Spend"].ToString();
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
            //Show資料用

            //月份用
            SqlConnection Conn_Month = new SqlConnection();
            Conn_Month.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDataReader dr_Month = null;
            SqlCommand cmd_Month = new SqlCommand("SELECT * FROM [Account_Numbers_D]", Conn_Month);
            try //==== 以下程式，只放「執行期間」的指令！=====================
            {
                Conn_Month.Open();
                dr_Month = cmd_Month.ExecuteReader();
                DropDownList1.DataValueField = "Account_numbers";
                DropDownList1.DataTextField = "Account_name";
                DropDownList1.DataSource = dr_Month;
                DropDownList1.DataBind();
            }
            catch (Exception ex_Month) //---- 如果程式有錯誤或是例外狀況，將執行這一段
            {
                Response.Write("<b>Error Message----  </b>" + ex_Month.ToString());
            }
            finally //---- 關掉資料連結
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
        }
        
    }

    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        TextBox1.Text = Calendar1.SelectedDate.ToShortDateString();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        SqlDataSource SqlDataSource2 = new SqlDataSource();
        SqlDataSource2.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlDataSource2.UpdateParameters.Add("Order_date", TextBox1.Text);
        SqlDataSource2.UpdateParameters.Add("Assign_numbers", TextBox2.Text);
        SqlDataSource2.UpdateParameters.Add("Account_numbers", DropDownList1.Text);
        SqlDataSource2.UpdateParameters.Add("Account_abstract", TextBox3.Text);
        SqlDataSource2.UpdateParameters.Add("Income", TextBox4.Text);
        SqlDataSource2.UpdateParameters.Add("Spend", TextBox5.Text);

        SqlDataSource2.UpdateCommand = "Update [Account_Order_M] set Order_date=@Order_date, Assign_numbers=@Assign_numbers, Account_numbers=@Account_numbers, Account_abstract=@Account_abstract, Income=@Income, Spend=@Spend where Id=" + Request["Id"];

        int affraw = SqlDataSource2.Update();
        if (affraw == 0)
        {
            Label1.Text = "Error";
        }
        else
        {
            Label1.Text = "OK";
            Button1.Visible = false;
        }
        SqlDataSource2.Dispose();
    }
}