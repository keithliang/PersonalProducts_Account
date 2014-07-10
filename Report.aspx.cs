using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;   

public partial class Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlConnection Conn_Month = new SqlConnection();
            Conn_Month.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDataReader dr_Month = null;
            SqlCommand cmd_Month = new SqlCommand("SELECT DISTINCT LEFT (Order_date, 7) AS Expr1 FROM Account_Order_M_View ", Conn_Month);
            try 
            {
                
                Conn_Month.Open();
                dr_Month = cmd_Month.ExecuteReader();
                DropDownList1.DataValueField = "Expr1";
                DropDownList1.DataTextField = "Expr1";
                DropDownList1.DataSource = dr_Month;
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0,"-- Please select One --");
                
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
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        // Show資料用的
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlDataReader dr = null;
        SqlCommand cmd = new SqlCommand("select ID, Order_date, Assign_numbers, Account_numbers, Account_name, Account_abstract, Income, Spend from [Account_Order_M_View] where order_date like @order_date order by Order_date,ID desc", Conn);
        cmd.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        try  
        {
            Conn.Open();

            dr = cmd.ExecuteReader();

            myTable.Rows.Add(BuildNewRow("Order_date", "Assign_numbers", "Account_name", "Account_abstract", "Income", "Spend"));
            while (dr.Read())
            {
                myTable.Rows.Add(BuildNewRow(Convert.ToDateTime(dr["Order_date"]).ToShortDateString(), dr["Assign_numbers"].ToString(), dr["Account_name"].ToString(), dr["Account_abstract"].ToString(), dr["Income"].ToString(), dr["Spend"].ToString()));
            }

        }
        catch (Exception ex) 
        {
            Response.Write("<b>Error Message----  </b>" + ex.ToString());
        }
        finally
        {
            if (dr != null)
            {
                cmd.Cancel();
                dr.Close();
            }

            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                Conn.Dispose(); 
            }
        }
        // Show資料用的

        // 計算支出小計用的
        SqlConnection Conn2 = new SqlConnection();
        Conn2.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlDataReader dr2 = null;
        SqlCommand cmd2 = new SqlCommand("select sum(income) as income from [Account_Order_M_View] where order_date like @order_date", Conn2);
        cmd2.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        try   
        {
            Conn2.Open(); 
            dr2 = cmd2.ExecuteReader(); 
            dr2.Read();
            Label1.Text = "income totel:" + dr2["income"].ToString();

        }
        catch (Exception ex2)
        {
            Response.Write("<b>Error Message 2 ----  </b>" + ex2.ToString());
        }
        finally
        {
            if (dr2 != null)
            {
                cmd2.Cancel();
                dr2.Close();
            }

            if (Conn2.State == ConnectionState.Open)
            {
                Conn2.Close();
                Conn2.Dispose();
            }
        }
        // 計算支出小計用的

        // 計算收入小計用的
        SqlConnection Conn3 = new SqlConnection();
        Conn3.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlDataReader dr3 = null;
        SqlCommand cmd3 = new SqlCommand("select sum(Spend) as spend from [Account_Order_M_View] where order_date like @order_date", Conn3);
        cmd3.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        try  
        {
            Conn3.Open();   
            dr3 = cmd3.ExecuteReader();  
            dr3.Read();
            Label2.Text = "Spend totel:" + dr3["Spend"].ToString();

        }
        catch (Exception ex3) 
        {
            Response.Write("<b>Error Message 3 ----  </b>" + ex3.ToString());
        }
        finally
        {
            if (dr3 != null)
            {
                cmd3.Cancel();
                dr3.Close();
            }

            if (Conn3.State == ConnectionState.Open)
            {
                Conn3.Close();
                Conn3.Dispose();
            }
        }
        // 計算收入小計用的
    }

    HtmlTableRow BuildNewRow(string Cell1Text, string Cell2Text, string Cell3Text, string Cell4Text, string Cell5Text, string Cell6Text)
    {   
        HtmlTableRow row = new HtmlTableRow();  

        HtmlTableCell cell1 = new HtmlTableCell();  
        HtmlTableCell cell2 = new HtmlTableCell();
        HtmlTableCell cell3 = new HtmlTableCell();
        HtmlTableCell cell4 = new HtmlTableCell();
        HtmlTableCell cell5 = new HtmlTableCell();
        HtmlTableCell cell6 = new HtmlTableCell();

        cell1.Controls.Add(new LiteralControl(Cell1Text));
        //cell1.Width = "20px";
        cell1.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell1);

        cell2.Controls.Add(new LiteralControl(Cell2Text));
        //cell2.Width = "100px";
        cell2.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell2);

        cell3.Controls.Add(new LiteralControl(Cell3Text));
        //cell3.Width = "230px";
        cell3.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell3);

        cell4.Controls.Add(new LiteralControl(Cell4Text));
        //cell4.Width = "230px";
        cell4.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell4);

        cell5.Controls.Add(new LiteralControl(Cell5Text));
        //cell5.Width = "20px";
        cell5.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell5);

        cell6.Controls.Add(new LiteralControl(Cell6Text));
        //cell6.Width = "20px";
        cell6.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell6);

        return row;  //--回傳一個 HtmlTableRow
    }
}