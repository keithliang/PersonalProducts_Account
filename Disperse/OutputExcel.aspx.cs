using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

// NPOI
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
// NPOI


public partial class OutputExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlConnection Conn_Month = new SqlConnection();
            Conn_Month.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDataReader dr_Month = null;
            SqlCommand cmd_Month = new SqlCommand("SELECT DISTINCT LEFT (Order_date, 7) AS Expr1 FROM Account_Order_M_View", Conn_Month);
            
            try //==== 以下程式，只放「執行期間」的指令！=====================
            {
                Conn_Month.Open();//---- 這時候才連結DB
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
        HSSFWorkbook workbook = new HSSFWorkbook();

        HSSFSheet u_sheet = (HSSFSheet)workbook.CreateSheet("My Sheet_121");

        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        SqlDataReader dr = null;
        //SqlCommand cmd = new SqlCommand("select Order_date, Assign_numbers, Account_numbers, Account_name, Account_abstract, Income, Spend from Account_Order_M_View where order_date like '%" + DropDownList1.Text + "%'", Conn);
        SqlCommand cmd = new SqlCommand("select Order_date, Assign_numbers, Account_numbers, Account_name, Account_abstract, Income, Spend from Account_Order_M_View where order_date like @order_date", Conn);
        cmd.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        IRow u_row = u_sheet.CreateRow(0);
        u_row.CreateCell(0).SetCellValue("Order_date");
        u_row.CreateCell(1).SetCellValue("Assign_numbers");
        u_row.CreateCell(2).SetCellValue("Account_numbers");
        u_row.CreateCell(3).SetCellValue("Account_name");
        u_row.CreateCell(4).SetCellValue("Account_abstract");
        u_row.CreateCell(5).SetCellValue("Income");
        u_row.CreateCell(6).SetCellValue("Spend");

        //此處存入數值
        try
        {
            Conn.Open();
            dr = cmd.ExecuteReader();
            int k = 1;
            //起跳不是0

            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {


                    if (i == 0)
                    {
                        u_sheet.CreateRow(k).CreateCell(i).SetCellValue(dr.GetValue(i).ToString());  //*** for Exporting to a Excel file
                    }
                    else
                    {

                        u_sheet.GetRow(k).CreateCell(i).SetCellValue(dr[i].ToString());
                    }
                }
                k++;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<b>Error Message----  </b>" + ex.ToString() + "<hr />");
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
        //此處存入數值

        //此處存檔
        MemoryStream ms = new MemoryStream();
        workbook.Write(ms);
        Response.AddHeader("Content-Disposition", "attachment; filename=EmptyWorkbook_2_DB.xls");
        Response.BinaryWrite(ms.ToArray());
        //此處存檔

        workbook = null;
        ms.Close();
        ms.Dispose();
    }
}