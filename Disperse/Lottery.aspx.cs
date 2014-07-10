using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.UI.WebControls.WebParts; //textbox顯示資料用
using System.Data.SqlClient;
using System.Web.Configuration;

using System.Web.UI.HtmlControls;   //-- HtmlTableRow 會用到這個命名空間！

public partial class Lottery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["q"] != null)
        //q來自於前頁QueryString
        //q設在jquery.autocomplete內的function(q)
        {

            string DataSelect = "Select Assign_numbers, count(Assign_numbers) as N From [Account_Order_M_View] Group By Assign_numbers Having Assign_numbers  Like '%" + Request.QueryString["q"] + "%' ";
            //string sQry = "Select V00302, Count(V00302) As N From TestClass Group By V00302 Having V00302 Like '%" + Request.QueryString["q"] + "%' ";
            //count合計完成後傳入QueryString,決定要顯示的東西
            //having 為函數產生的值設定要撈的方式用,與where類似
            // http://www.1keydata.com/tw/sql/sqlhaving.html
            string DataConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            using (SqlConnection conn = new SqlConnection(DataConnectionString))
            {
                conn.Open();
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(DataSelect, conn);

                SqlDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    Response.Write(dr.GetString(0) + Environment.NewLine);
                }

                dr.Close();
                dr.Dispose();
                comm.Dispose();
            }

            Response.End();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        // Show資料用的
        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings("testConnectionString").ConnectionString.ToString());

        SqlDataReader dr = null;

        SqlCommand cmd = new SqlCommand("SELECT Id, Order_date, Account_numbers, Assign_numbers FROM [Account_Order_M_View] where Assign_numbers like '%" + TextBox1.Text + "%'", Conn);

        try     //==== 以下程式，只放「執行期間」的指令！=====================
        {
            Conn.Open();   //---- 這時候才連結DB

            dr = cmd.ExecuteReader();   //---- 這時候執行SQL指令，取出資料


            //string myTitle, mySummary;
            myTable.Rows.Add(BuildNewRow("Id", "Order_date", "Account_numbers", "Assign_numbers"));
            while (dr.Read())
            {
                //myTitle = "<Strong><B><A href=Disp.aspx?id=" + dr["id"] + ">" + dr["title"] + "</A></B></Strong>";
                //mySummary = "<small><font color=#969696>" + dr["summary"] + "</font></small>......<A href=Disp.aspx?id=" + dr["id"] + ">詳見內文</A>";


                myTable.Rows.Add(BuildNewRow(dr["Id"].ToString(), dr["Order_date"].ToString(), dr["Account_numbers"].ToString(), dr["Assign_numbers"].ToString()));
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

    HtmlTableRow BuildNewRow(string Cell1Text, string Cell2Text, string Cell3Text, string Cell4Text)
    {   //必須用到 using System.Web.UI.HtmlControls;。
        HtmlTableRow row = new HtmlTableRow();  //-- 新的一列

        HtmlTableCell cell1 = new HtmlTableCell();  //-- 新的儲存格
        HtmlTableCell cell2 = new HtmlTableCell();
        HtmlTableCell cell3 = new HtmlTableCell();
        HtmlTableCell cell4 = new HtmlTableCell();

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
        cell4.Width = "100px";
        cell4.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell4);
        return row;  //--回傳一個 HtmlTableRow
    }
}