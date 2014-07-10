using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class DeleteEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString());
            SqlDataReader dr = null;
            //SqlCommand cmd = new SqlCommand("select * from [Joural_Order_M] where id=" + Request["Id"], conn);
            SqlCommand cmd = new SqlCommand("select * from [Account_Order_M_View] where id=" + Request["Id"], conn);

            try //==== 以下程式，只放「執行期間」的指令！=====================
            {
                conn.Open();//---- 這時候才連結DB
                dr = cmd.ExecuteReader();  //---- 這時候執行SQL指令，取出資料
                dr.Read();

                //view
                //TextBox1.Text = Convert.ToDateTime(dr["Order_date"]).ToShortDateString();
                //TextBox2.Text = dr["Assign_numbers"].ToString();
                //DropDownList1.SelectedValue = dr["Account_numbers"].ToString();
                //TextBox3.Text = dr["Account_abstract"].ToString();
                //TextBox4.Text = dr["Income"].ToString();
                //TextBox5.Text = dr["Spend"].ToString();
                //view

                Label2.Text = Convert.ToDateTime(dr["Order_date"]).ToShortDateString();
                Label3.Text = dr["Assign_numbers"].ToString();
                Label4.Text = dr["Account_name"].ToString();
                Label5.Text = dr["Account_abstract"].ToString();
                Label6.Text = dr["Income"].ToString();
                Label7.Text = dr["Spend"].ToString();
            }
            catch (Exception ex) //---- 如果程式有錯誤或是例外狀況，將執行這一段
            {
                Response.Write("error" + ex.ToString());
            }
            finally  //---- 關掉資料連結
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
        SqlDataSource2.ConnectionString=WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        try
        {
            //update
            //SqlDataSource2.UpdateParameters.Add("Order_date", TextBox1.Text);
            //SqlDataSource2.UpdateParameters.Add("Assign_numbers", TextBox2.Text);
            //SqlDataSource2.UpdateParameters.Add("Account_numbers", DropDownList1.Text);      
            //SqlDataSource2.UpdateParameters.Add("Account_abstract", TextBox3.Text);
            //SqlDataSource2.UpdateParameters.Add("Income", TextBox4.Text);
            //SqlDataSource2.UpdateParameters.Add("Spend", TextBox5.Text);
            //SqlDataSource2.UpdateCommand = "Update [Joural_Order_M] set Order_date=@Order_date, Assign_numbers=@Assign_numbers, Account_numbers=@Account_numbers, Account_abstract=@Account_abstract, Income=@Income, Spend=@Spend where Id=" + Request["Id"];
            //update

            SqlDataSource2.DeleteCommand = "delete [Account_Order_M]  where Id=" + Request["Id"];

            //delete table Joural_Order_M content
            //SqlDataSource2.DeleteParameters.Add("Order_date", Label1.Text);
            //SqlDataSource2.DeleteParameters.Add("Assign_numbers", Label2.Text);
            //SqlDataSource2.DeleteParameters.Add("Account_numbers", Label3.Text);
            //SqlDataSource2.DeleteParameters.Add("Account_abstract", TextBox3.Text);
            //SqlDataSource2.DeleteParameters.Add("Income", TextBox4.Text);
            //SqlDataSource2.DeleteParameters.Add("Spend", TextBox5.Text);
            //delete table Joural_Order_M content

            int affraw = SqlDataSource2.Delete();

            if (affraw == 0)
            {
                Label1.Text = "Error";
            }
            else
            {
                Label1.Text = "Delete OK";
                Button1.Visible = false;
            }
        }
        catch (Exception ex2)
        {
            Response.Write("error" + ex2.ToString());
        }
        finally
        {
            SqlDataSource2.Dispose();
        }
    }

}