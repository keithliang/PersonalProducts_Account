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

public partial class Search_Temp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        // Show資料用的
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings("testConnectionString").ConnectionString.ToString());

        SqlDataReader dr = null;

        SqlCommand cmd = new SqlCommand("SELECT DISTINCT LEFT(Order_date, 7) AS Expr1, SUM(Income) AS Expr2 FROM Account_Order_M_View where Order_date like '%" + TextBox1.Text + "%'GROUP BY LEFT(Order_date, 7)", Conn);

        try     //==== 以下程式，只放「執行期間」的指令！=====================
        {
            Conn.Open();   //---- 這時候才連結DB

            dr = cmd.ExecuteReader();   //---- 這時候執行SQL指令，取出資料


            //string myTitle, mySummary;
            myTable.Rows.Add(BuildNewRow("momth", "totel"));
            while (dr.Read())
            {
                //myTitle = "<Strong><B><A href=Disp.aspx?id=" + dr["id"] + ">" + dr["title"] + "</A></B></Strong>";
                //mySummary = "<small><font color=#969696>" + dr["summary"] + "</font></small>......<A href=Disp.aspx?id=" + dr["id"] + ">詳見內文</A>";


                myTable.Rows.Add(BuildNewRow(dr["expr1"].ToString(), dr["expr2"].ToString()));
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
                dr.Close();
            }

            //---- Close the connection when done with it.
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                Conn.Dispose();  //---- 一開始宣告有用到 New的,最後必須以 .Dispose()結束
            }
        }
    }

    HtmlTableRow BuildNewRow(string Cell1Text, string Cell2Text)
    {   //必須用到 using System.Web.UI.HtmlControls;。
        HtmlTableRow row = new HtmlTableRow();  //-- 新的一列

        HtmlTableCell cell1 = new HtmlTableCell();  //-- 新的儲存格
        HtmlTableCell cell2 = new HtmlTableCell();

        cell1.Controls.Add(new LiteralControl(Cell1Text));
        cell1.Width = "20px";
        cell1.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell1);

        cell2.Controls.Add(new LiteralControl(Cell2Text));
        cell2.Width = "100px";
        cell2.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell2);


        return row;  //--回傳一個 HtmlTableRow
    }
}