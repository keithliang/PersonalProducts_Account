//http://phorum.study-area.org/index.php?topic=63766.0;wap

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
//System.Drawing.Imaging 提供對複雜的形狀、區域的支援。
//包括 3 x 3 矩陣 Matrix、線條端點樣式 LineCap、樣式塗刷 HatchBrush
//線形漸層塗刷LinearGradientBrush、直線和曲線的軌道路徑 GraphicsPath等等。 

//System.Drawing.Drawing2D;提供基本的類別、結構
//包括顏色Color、畫筆Pen、塗刷Brush、字型Font、影像Image
//點陣圖Bitmap、2D點Point、矩形Rectangle、寬高 Size等等。 .

public partial class Disperse_ImgMark : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
 
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        //建立圖檔及浮水印檔位置字串
        String filepath_original = Server.MapPath("~\\img\\09.JPG");
        String filepath_watermark = Server.MapPath("~\\img\\Mark.PNG");

        //圖檔的位置
        System.Drawing.Image
        imageOriginal = System.Drawing.Image.FromFile(filepath_original),
        imageWatermark = System.Drawing.Image.FromFile(filepath_watermark);

        //建立已嵌入圖檔
        System.Drawing.Image newImage = new Bitmap(imageOriginal);
        using (Graphics newGraphic = Graphics.FromImage(newImage))
        {
            //浮水印嵌入位置計算
            //原圖檔寬減浮水印檔寬
            Int32 drawx = imageOriginal.Width - imageWatermark.Width;
            Int32 drawy = 0;

            //指定浮水印嵌入位置，產生新的圖片
            newGraphic.DrawImageUnscaled(imageWatermark, new Point(drawx, drawy));
            newImage.Save(Server.MapPath("~\\img\\newImage.png"), ImageFormat.Png);
            //gif,jpeg,png,bmp,tiff
        }

        Image1.ImageUrl = ("~\\img\\newImage.png");
    }
}