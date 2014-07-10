using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;//縮圖用
using System.Data;

public partial class Bootstrap_ImageSizeZip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //private const string savePath = @"F:\WebDegsin\作品集\According2\Data_Temp\";
    //private const string tempPath = @"F:\WebDegsin\作品集\According2\img\";

    protected void Button1_Click(object sender, EventArgs e)
    {
        string tempPath = Server.MapPath("~/Data_Temp/");
        string savePath = Server.MapPath("~/img/");
        if (FileUpload1.HasFile)
        {
            string tempName = tempPath + FileUpload1.FileName;
            string imageName = savePath + FileUpload1.FileName;

            // 儲存暫存檔
            FileUpload1.SaveAs(tempName);

            // System.Web.UI.WebControls 與 System.Drawing 同時擁有 Image 類別
            // 所以以下程式碼明確指定要使用的是 System.Drawing.Image

            System.Drawing.Image.GetThumbnailImageAbort callBack = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            Bitmap image = new Bitmap(tempName);

            // 計算維持比例的縮圖大小
            int[] thumbnailScale = getThumbnailImageScale(600, 600, image.Width, image.Height);

            // 產生縮圖
            System.Drawing.Image smallImage = image.GetThumbnailImage(thumbnailScale[0], thumbnailScale[1], callBack, IntPtr.Zero);

            // 將縮圖存檔
            smallImage.Save(imageName);

            // 釋放並刪除暫存檔
            image.Dispose();
            System.IO.File.Delete(tempName);
            Response.Write("ok");
        }
        else
        {
            Response.Write("error");
        }
    }

    // 計算維持比例的縮圖大小
    private int[] getThumbnailImageScale(int maxWidth, int maxHeight, int oldWidth, int oldHeight)
    {
        int[] result = new int[] { 0, 0 };
        float widthDividend, heightDividend, commonDividend;

        widthDividend = (float)oldWidth / (float)maxWidth;
        heightDividend = (float)oldHeight / (float)maxHeight;

        commonDividend = (heightDividend > widthDividend) ? heightDividend : widthDividend;
        result[0] = (int)(oldWidth / commonDividend);
        result[1] = (int)(oldHeight / commonDividend);

        return result;
    }

    private bool ThumbnailCallback()
    {
        return false;
    }
}