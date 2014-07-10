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

//iTextShar
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
//iTextShar

public partial class Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SqlConnection Conn_Month = new SqlConnection();
            Conn_Month.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDataReader dr_Month = null;
            SqlCommand cmd_Month = new SqlCommand("SELECT DISTINCT LEFT (Order_date, 7) AS Expr1 FROM Account_Order_M_View", Conn_Month);

            try 
            {
                Conn_Month.Open();
                dr_Month = cmd_Month.ExecuteReader();
                DropDownList1.DataValueField = "Expr1"; 
                DropDownList1.DataTextField = "Expr1"; 
                DropDownList1.DataSource = dr_Month;
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "-- Please select One --");
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
        HSSFWorkbook workbook = new HSSFWorkbook();
        HSSFSheet u_sheet = (HSSFSheet)workbook.CreateSheet("My Sheet_121");

        SqlConnection Conn = new SqlConnection();
        Conn.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        SqlDataReader dr = null;
        SqlCommand cmd = new SqlCommand("select Order_date, Assign_numbers, Account_name, Account_abstract, Income, Spend from Account_Order_M_View where order_date like @order_date", Conn);
        cmd.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        IRow u_row = u_sheet.CreateRow(0);
        u_row.CreateCell(0).SetCellValue("Order_date");
        u_row.CreateCell(1).SetCellValue("Assign_numbers");
        u_row.CreateCell(3).SetCellValue("Account_name");
        u_row.CreateCell(4).SetCellValue("Account_abstract");
        u_row.CreateCell(5).SetCellValue("Income");
        u_row.CreateCell(6).SetCellValue("Spend");

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

        MemoryStream ms = new MemoryStream();
        workbook.Write(ms);
        Response.AddHeader("Content-Disposition", "attachment; filename=EmptyWorkbook_2_DB.xls");
        Response.BinaryWrite(ms.ToArray());
        workbook = null;
        ms.Close();
        ms.Dispose();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        SqlDataReader dr = null;
        SqlCommand cmd = new SqlCommand("select Order_date, Assign_numbers, Account_name, Account_abstract, Income, Spend from Account_Order_M_View where order_date like @order_date ", conn);
        cmd.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        try
        {

            conn.Open();
            dr = cmd.ExecuteReader();

            var NewDoc = new Document(PageSize.A3);
            MemoryStream DocMonery = new MemoryStream();
            PdfWriter DocPdfWriter = PdfWriter.GetInstance(NewDoc, DocMonery);
            BaseFont bfChinese = BaseFont.CreateFont(@"C:\Windows\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font DocFont = new Font(bfChinese, 10);

            NewDoc.Open();
            Chunk account_title = new Chunk("account list", DocFont);
            Phrase account_title_u = new Phrase(account_title);
            NewDoc.Add(account_title);

            PdfPTable DocTable = new PdfPTable(new float[] { 1, 1, 2, 2, 1, 1 });
            DocTable.TotalWidth = 600F;
            DocTable.LockedWidth = true;
            PdfPCell cell_1 = new PdfPCell(new Phrase("OrderDate", DocFont));
            PdfPCell cell_2 = new PdfPCell(new Phrase("AssignNumbers", DocFont));
            PdfPCell cell_3 = new PdfPCell(new Phrase("AccountName", DocFont));
            PdfPCell cell_4 = new PdfPCell(new Phrase("AccountAbstract", DocFont));
            PdfPCell cell_5 = new PdfPCell(new Phrase("Income", DocFont));
            PdfPCell cell_6 = new PdfPCell(new Phrase("Spend", DocFont));
            DocTable.AddCell(cell_1);
            DocTable.AddCell(cell_2);
            DocTable.AddCell(cell_3);
            DocTable.AddCell(cell_4);
            DocTable.AddCell(cell_5);
            DocTable.AddCell(cell_6);
            string cell_Text_1, cell_Text_2, cell_Text_3, cell_Text_4, cell_Text_5, cell_Text_6;

            while (dr.Read())
            {
                cell_Text_1 = Convert.ToDateTime(dr["Order_date"]).ToShortDateString();
                cell_Text_2 = dr["Assign_numbers"].ToString();
                cell_Text_3 = dr["Account_name"].ToString();
                cell_Text_4 = dr["Account_abstract"].ToString();
                cell_Text_5 = dr["Income"].ToString();
                cell_Text_6 = dr["Spend"].ToString();

                PdfPCell karabs1 = new PdfPCell(new Phrase(cell_Text_1, DocFont));
                PdfPCell karabs2 = new PdfPCell(new Phrase(cell_Text_2, DocFont));
                PdfPCell karabs3 = new PdfPCell(new Phrase(cell_Text_3, DocFont));
                PdfPCell karabs4 = new PdfPCell(new Phrase(cell_Text_4, DocFont));
                PdfPCell karabs5 = new PdfPCell(new Phrase(cell_Text_5, DocFont));
                PdfPCell karabs6 = new PdfPCell(new Phrase(cell_Text_6, DocFont));
                DocTable.AddCell(karabs1);
                DocTable.AddCell(karabs2);
                DocTable.AddCell(karabs3);
                DocTable.AddCell(karabs4);
                DocTable.AddCell(karabs5);
                DocTable.AddCell(karabs6);
            }

            NewDoc.Add(DocTable);

            NewDoc.Close();
            Response.Clear();
            Response.AddHeader("Content-disposition", "attachment;filename=test.pdf");
            Response.ContentType = "application/octet-stream";
            Response.OutputStream.Write(DocMonery.GetBuffer(), 0, DocMonery.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
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
    }
}