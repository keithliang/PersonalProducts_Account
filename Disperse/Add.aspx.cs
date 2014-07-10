using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

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
        SqlDataSource SqlDataSource1 = new SqlDataSource();
        SqlDataSource1.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlDataSource1.InsertParameters.Add("Order_date", TextBox1.Text);
        SqlDataSource1.InsertParameters.Add("Assign_numbers", TextBox2.Text);
        SqlDataSource1.InsertParameters.Add("Account_numbers", DropDownList1.Text);
        SqlDataSource1.InsertParameters.Add("Account_abstract", TextBox3.Text);
        SqlDataSource1.InsertParameters.Add("Income", TextBox4.Text);
        SqlDataSource1.InsertParameters.Add("Spend", TextBox5.Text);
        SqlDataSource1.InsertCommand = "Insert into Account_Order_M(Order_date,Assign_numbers,Account_numbers,Account_abstract,Income,Spend) values(@Order_date,@Assign_numbers,@Account_numbers,@Account_abstract,@Income,@Spend)";


        int affraw = SqlDataSource1.Insert();
        if (affraw == 0)
        {
            Label1.Text = "Error";
        }
        else
        {
            Label1.Text = "OK";
            Button1.Visible = false;
        }
        SqlDataSource1.Dispose();
    }
}