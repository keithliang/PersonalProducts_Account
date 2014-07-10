using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Collections;


/// <summary>
/// AutoComplete_WebPage 的摘要描述
/// </summary>
public class AutoComplete_WebPage :System.Web.UI.Page
{
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    //一定要宣告成static才有效果
    public static string[] GetCompletionList(string prefixText, int count)
    {
        //連線字串
        //string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename="
                     //+ System.Web.HttpContext.Current.Server.MapPath("~/App_Data/NorthwindChinese.mdf") + ";Integrated Security=True;User Instance=True";

        ArrayList array = new ArrayList();//儲存撈出來的字串集合

        //using (SqlConnection conn = new SqlConnection(connStr))
        using (SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        {
            DataSet ds = new DataSet();
            string selectStr = @"SELECT Top (" + count + ") Account_numbers FROM Account_Order_M_View Where Account_numbers Like '" + prefixText + "%'";
            SqlDataAdapter da = new SqlDataAdapter(selectStr, conn);
            conn.Open();
            da.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                array.Add(dr["Account_numbers"].ToString());
            }

        }

        return (string[])array.ToArray(typeof(string));

    }
 
}