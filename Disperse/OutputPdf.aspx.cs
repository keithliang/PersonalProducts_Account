using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class OutputPdf : System.Web.UI.Page
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
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        SqlDataReader dr = null;
        //SqlCommand cmd = new SqlCommand("select Order_date, Assign_numbers, Account_numbers, Account_name, Account_abstract, Income, Spend from Account_Order_M_View where order_date like '%'+@order_date+'%' ", conn);
        SqlCommand cmd = new SqlCommand("select Order_date, Assign_numbers, Account_numbers, Account_name, Account_abstract, Income, Spend from Account_Order_M_View where order_date like @order_date ", conn);
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
            //NewDoc.Add(new Paragraph(10f, "Accounting", DocFont));
            Chunk account_title = new Chunk("account list", DocFont);
            Phrase account_title_u = new Phrase(account_title);
            NewDoc.Add(account_title);

            PdfPTable DocTable = new PdfPTable(new float[] { 1, 1, 1, 2, 2, 1, 1});
            //PdfPTable DocTable = new PdfPTable(new float[] { 4 });
            DocTable.TotalWidth = 700F;
            DocTable.LockedWidth = true;
            //Order_date, Assign_numbers, Account_numbers, Account_name, Account_abstract, Income, Spend
            PdfPCell cell_1 = new PdfPCell(new Phrase("OrderDate", DocFont));
            PdfPCell cell_2 = new PdfPCell(new Phrase("AssignNumbers", DocFont));
            PdfPCell cell_3 = new PdfPCell(new Phrase("AccountNumbers", DocFont));
            PdfPCell cell_4 = new PdfPCell(new Phrase("AccountName", DocFont));
            PdfPCell cell_5 = new PdfPCell(new Phrase("AccountAbstract", DocFont));
            PdfPCell cell_6 = new PdfPCell(new Phrase("Income", DocFont));
            PdfPCell cell_7 = new PdfPCell(new Phrase("Spend", DocFont));
            DocTable.AddCell(cell_1);
            DocTable.AddCell(cell_2);
            DocTable.AddCell(cell_3);
            DocTable.AddCell(cell_4);
            DocTable.AddCell(cell_5);
            DocTable.AddCell(cell_6);
            DocTable.AddCell(cell_7);
            string cell_Text_1, cell_Text_2, cell_Text_3, cell_Text_4, cell_Text_5, cell_Text_6, cell_Text_7;

            while (dr.Read())
            {
                cell_Text_1 = Convert.ToDateTime(dr["Order_date"]).ToShortDateString();
                cell_Text_2 = dr["Assign_numbers"].ToString();
                cell_Text_3 = dr["Account_numbers"].ToString();
                cell_Text_4 = dr["Account_name"].ToString();
                cell_Text_5 = dr["Account_abstract"].ToString();
                cell_Text_6 = dr["Income"].ToString();
                cell_Text_7 = dr["Spend"].ToString();

                PdfPCell karabs1 = new PdfPCell(new Phrase(cell_Text_1, DocFont));
                PdfPCell karabs2 = new PdfPCell(new Phrase(cell_Text_2, DocFont));
                PdfPCell karabs3 = new PdfPCell(new Phrase(cell_Text_3, DocFont));
                PdfPCell karabs4 = new PdfPCell(new Phrase(cell_Text_4, DocFont));
                PdfPCell karabs5 = new PdfPCell(new Phrase(cell_Text_5, DocFont));
                PdfPCell karabs6 = new PdfPCell(new Phrase(cell_Text_6, DocFont));
                PdfPCell karabs7 = new PdfPCell(new Phrase(cell_Text_7, DocFont));
                DocTable.AddCell(karabs1); //不能再指定font
                DocTable.AddCell(karabs2); //不能再指定函數
                DocTable.AddCell(karabs3);//要指定font和函數請事先指定
                DocTable.AddCell(karabs4); //不能再指定font
                DocTable.AddCell(karabs5); //不能再指定函數
                DocTable.AddCell(karabs6);//要指定font和函數請事先指定
                DocTable.AddCell(karabs7);//要指定font和函數請事先指定
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