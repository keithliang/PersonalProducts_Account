using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;   //-- HtmlTableRow 會用到這個命名空間！


public partial class View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlConnection Conn_Month = new SqlConnection();
            Conn_Month.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDataReader dr_Month = null;
            SqlCommand cmd_Month = new SqlCommand("SELECT DISTINCT LEFT (Order_date, 7) AS Expr1 FROM Account_Order_M_View ", Conn_Month);
            try //==== 以下程式，只放「執行期間」的指令！=====================
            {
                Conn_Month.Open();
                dr_Month = cmd_Month.ExecuteReader();
                DropDownList1.DataValueField = "Expr1"; //沒有指定名稱直接沿用expr1
                DropDownList1.DataTextField = "Expr1"; //沒有指定名稱直接沿用expr1
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
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        // Show資料用的
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings("testConnectionString").ConnectionString.ToString());

        SqlDataReader dr = null;
        //SqlCommand cmd = new SqlCommand("select ID, Order_date, Assign_numbers, Account_numbers, Account_name, Account_abstract, Income, Spend from [Account_Order_M_View] where order_date like '%" + DropDownList1.Text + "%' order by Order_date,ID desc", Conn);
        SqlCommand cmd = new SqlCommand("select ID, Order_date, Assign_numbers, Account_numbers, Account_name, Account_abstract, Income, Spend from [Account_Order_M_View] where order_date like @order_date order by Order_date,ID desc", Conn);
        cmd.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        try     //==== 以下程式，只放「執行期間」的指令！=====================
        {
            Conn.Open();   //---- 這時候才連結DB

            dr = cmd.ExecuteReader();   //---- 這時候執行SQL指令，取出資料

            //string myTitle, mySummary;
            myTable.Rows.Add(BuildNewRow("Order_date", "Assign_numbers", "Account_name", "Account_numbers", "Account_abstract", "Income", "Spend"));
            while (dr.Read())
            {
                //myTitle = "<Strong><B><A href=Disp.aspx?id=" + dr["id"] + ">" + dr["title"] + "</A></B></Strong>";
                //mySummary = "<small><font color=#969696>" + dr["summary"] + "</font></small>......<A href=Disp.aspx?id=" + dr["id"] + ">詳見內文</A>";


                myTable.Rows.Add(BuildNewRow(Convert.ToDateTime(dr["Order_date"]).ToShortDateString(), dr["Assign_numbers"].ToString(), dr["Account_numbers"].ToString(), dr["Account_name"].ToString(), dr["Account_abstract"].ToString(), dr["Income"].ToString(), dr["Spend"].ToString()));
                //Table1.Rows.Add(BuildNewRow("", mySummary));
            }

        }
        catch (Exception ex)   //---- 如果程式有錯誤或是例外狀況，將執行這一段
        {
            Response.Write("<b>Error Message----  </b>" + ex.ToString());
        }
        finally
        {
            //---- Always call Close when done reading.
            if (dr != null)
            {
                cmd.Cancel();
                //----關閉DataReader之前，一定要先「取消」SqlCommand
                //參考資料： http://blog.darkthread.net/blogs/darkthreadtw/archive/2007/04/23/737.aspx
                dr.Close();
            }

            //---- Close the connection when done with it.
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                Conn.Dispose();  //---- 一開始宣告有用到 New的,最後必須以 .Dispose()結束
            }
        }
        // Show資料用的

        // 計算支出小計用的
        SqlConnection Conn2 = new SqlConnection();
        Conn2.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlDataReader dr2 = null;
        //SqlCommand cmd2 = new SqlCommand("select sum(income) as income from [Account_Order_M_View] where order_date like '%" + DropDownList1.Text + "%'", Conn2);
        SqlCommand cmd2 = new SqlCommand("select sum(income) as income from [Account_Order_M_View] where order_date like @order_date", Conn2);
        cmd2.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        try     //==== 以下程式，只放「執行期間」的指令！=====================
        {
            Conn2.Open();   //---- 這時候才連結DB
            dr2 = cmd2.ExecuteReader();   //---- 這時候執行SQL指令，取出資料
            dr2.Read();
            Label1.Text = "income totel:" + dr2["income"].ToString();

        }
        catch (Exception ex2)   //---- 如果程式有錯誤或是例外狀況，將執行這一段
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
        //SqlCommand cmd3 = new SqlCommand("select sum(Spend) as spend from [Account_Order_M_View] where order_date like '%" + DropDownList1.Text + "%'", Conn3);
        SqlCommand cmd3 = new SqlCommand("select sum(Spend) as spend from [Account_Order_M_View] where order_date like @order_date", Conn3);
        cmd3.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        try     //==== 以下程式，只放「執行期間」的指令！=====================
        {
            Conn3.Open();   //---- 這時候才連結DB
            dr3 = cmd3.ExecuteReader();   //---- 這時候執行SQL指令，取出資料
            dr3.Read();
            Label2.Text = "Spend totel:" + dr3["Spend"].ToString();

        }
        catch (Exception ex3)   //---- 如果程式有錯誤或是例外狀況，將執行這一段
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

    HtmlTableRow BuildNewRow(string Cell1Text, string Cell2Text, string Cell3Text, string Cell4Text, string Cell5Text, string Cell6Text, string Cell7Text)
    {   //必須用到 using System.Web.UI.HtmlControls;。
        HtmlTableRow row = new HtmlTableRow();  //-- 新的一列

        HtmlTableCell cell1 = new HtmlTableCell();  //-- 新的儲存格
        HtmlTableCell cell2 = new HtmlTableCell();
        HtmlTableCell cell3 = new HtmlTableCell();
        HtmlTableCell cell4 = new HtmlTableCell();
        HtmlTableCell cell5 = new HtmlTableCell();
        HtmlTableCell cell6 = new HtmlTableCell();
        HtmlTableCell cell7 = new HtmlTableCell();

        cell1.Controls.Add(new LiteralControl(Cell1Text));
        cell1.Width = "20px";
        cell1.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell1);

        cell2.Controls.Add(new LiteralControl(Cell2Text));
        cell2.Width = "100px";
        cell2.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell2);

        cell3.Controls.Add(new LiteralControl(Cell3Text));
        cell3.Width = "100px";
        cell3.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell3);

        cell4.Controls.Add(new LiteralControl(Cell4Text));
        cell4.Width = "230px";
        cell4.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell4);

        cell5.Controls.Add(new LiteralControl(Cell5Text));
        cell5.Width = "230px";
        cell5.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell5);

        cell6.Controls.Add(new LiteralControl(Cell6Text));
        cell6.Width = "20px";
        cell6.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell6);

        cell7.Controls.Add(new LiteralControl(Cell7Text));
        cell7.Width = "20px";
        cell7.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell7);

        return row;  //--回傳一個 HtmlTableRow
    }
}