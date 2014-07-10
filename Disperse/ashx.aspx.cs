using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ashx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.PostedFile != null)
        //PostedFile:單檔上傳
        //PostedFiles:多檔上傳
        {
            HttpPostedFile myFile = FileUpload1.PostedFile;
            Session["myFile"] = myFile;
            Image1.ImageUrl = "Handler.ashx";
        }
    }
}