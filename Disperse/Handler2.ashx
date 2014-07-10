<%@ WebHandler Language="C#" Class="Handler2" %>

using System;
using System.Web;
using System.Drawing;

public class Handler2 : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        HttpResponse Response = context.Response;
        Bitmap bmp = new Bitmap(392, 72);

        Graphics g = Graphics.FromImage(bmp);
        g.Clear(System.Drawing.Color.Gray);
        g.DrawString("This is 32bit png.",
             new Font("verdana bold", 14f),
             Brushes.HotPink, 0f, 0f);
        g.Dispose();

        bmp.MakeTransparent(System.Drawing.Color.Gray);
        System.IO.MemoryStream MemStream = new System.IO.MemoryStream();
        bmp.Save(MemStream, System.Drawing.Imaging.ImageFormat.Png);
        bmp.Dispose();

        Response.Clear();
        Response.ContentType = "image/PNG";
        MemStream.WriteTo(Response.OutputStream);
        MemStream.Close();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}