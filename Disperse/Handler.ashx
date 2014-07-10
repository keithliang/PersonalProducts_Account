<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.SessionState;

public class Handler : IHttpHandler,IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        if (context.Session["myFile"] != null)
        {
            HttpPostedFile myFile = (HttpPostedFile)context.Session["myFile"];
            int nFileLen = myFile.ContentLength;
            // Allocate a buffer for reading of the file
            
            byte[] myData = new byte[nFileLen];
            myFile.InputStream.Read(myData, 0, nFileLen);
            context.Response.Write(context.Session["myFile"].ToString());
            context.Response.Clear();
            context.Response.ContentType = "image/jpg";
            context.Response.BinaryWrite(myData);

        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}