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

public partial class LottoReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["q"] != null)
        {

            string DataSelect = "Select Assign_numbers, count(Assign_numbers) as N [From Account_Order_M_View] Group By Assign_numbers Having Assign_numbers Like '%" + Request.QueryString["q"] + "%' ";
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
        SqlDataReader dr = null;
        SqlCommand cmd = new SqlCommand("select ID, Order_date, Assign_numbers, Account_numbers, Account_name, Account_abstract from [Account_Order_M_View] where Assign_numbers like @Assign_numbers order by Order_date,ID desc", Conn);
        cmd.Parameters.AddWithValue("@Assign_numbers", TextBox1.Text.Trim() + "%");
        try
        {
            Conn.Open();

            dr = cmd.ExecuteReader();

            myTable.Rows.Add(BuildNewRow("Order_date", "Assign_numbers", "Account_name", "Account_abstract",""));
            string Lotto;
            while (dr.Read())
            {
                Lotto = "<b><a href=LottoReceiptUpdate.aspx?id="+ dr["Id"]+">選擇</a></b>";
                myTable.Rows.Add(BuildNewRow(Convert.ToDateTime(dr["Order_date"]).ToShortDateString(), dr["Assign_numbers"].ToString(), dr["Account_name"].ToString(), dr["Account_abstract"].ToString(),Lotto));
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
    }

    HtmlTableRow BuildNewRow(string Cell1Text, string Cell2Text, string Cell3Text, string Cell4Text, string Cell5Text)
    {
        HtmlTableRow row = new HtmlTableRow();

        HtmlTableCell cell1 = new HtmlTableCell();
        HtmlTableCell cell2 = new HtmlTableCell();
        HtmlTableCell cell3 = new HtmlTableCell();
        HtmlTableCell cell4 = new HtmlTableCell();
        HtmlTableCell cell5 = new HtmlTableCell();

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
        //cell4.Width = "230px";
        cell5.Style.Add("border-bottom", "1px dotted black");
        row.Cells.Add(cell5);

        return row;  //--回傳一個 HtmlTableRow
    }
}