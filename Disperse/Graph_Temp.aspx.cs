using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Graph_Temp : System.Web.UI.Page
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
                DropDownList1.DataValueField = "Expr1"; //沒有指定名稱直接沿用expr1
                DropDownList1.DataTextField = "Expr1"; //沒有指定名稱直接沿用expr1
                DropDownList1.DataSource = dr_Month;
                DropDownList1.DataBind();
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
    protected void Button1_Click1(object sender, EventArgs e)
    {
        SqlConnection conn_graph = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //SqlCommand cmd_graph = new SqlCommand("SELECT Account_name, COUNT(Account_name) AS count FROM [Account_Order_M_View] WHERE (Order_date like '%" + DropDownList1.Text + "%') GROUP BY Account_name", conn_graph);
        SqlCommand cmd_graph = new SqlCommand("SELECT Account_name, COUNT(Account_name) AS count FROM [Account_Order_M_View] WHERE (Order_date like @Order_date) GROUP BY Account_name", conn_graph);
        cmd_graph.Parameters.AddWithValue("@order_date", DropDownList1.Text.Trim() + "%");
        SqlDataReader dr_graph = null;
        try //==== 以下程式，只放「執行期間」的指令！=====================
        {
            conn_graph.Open();//---- 這時候才連結DB
            dr_graph = cmd_graph.ExecuteReader();//---- 這時候執行SQL指令，取出資料
            Chart1.DataSource = dr_graph;
            Chart1.DataBind();
        }
        catch (Exception ex_graph) //---- 如果程式有錯誤或是例外狀況，將執行這一段
        {
            Response.Write("error" + ex_graph.ToString());
        }
        finally //---- 關掉資料連結
        {
            if (dr_graph == null)
            {
                cmd_graph.Cancel();
            }
            if (conn_graph.State == ConnectionState.Open)
            {
                conn_graph.Close();
                conn_graph.Dispose();
            }
        }

       
    }
}