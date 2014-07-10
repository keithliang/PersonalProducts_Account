//http://blog.csdn.net/lizhiliang06/article/details/8667073

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GoogleColudSave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


}

namespace DrEdit.Models
{
    internal static class ClientCredentials
    {
        /// <summary>
        /// The OAuth2.0 Client ID of your project.
        /// </summary>
        public static readonly string CLIENT_ID = "375601264799.apps.googleusercontent.com"; //"[[YOUR_CLIENT_ID]]";

        /// <summary>
        /// The OAuth2.0 Client secret of your project.
        /// </summary>
        public static readonly string CLIENT_SECRET = "tCW5SB_GDI5gDxH63p25cfQU"; //"[[YOUR_CLIENT_SECRET]]";

        /// <summary>
        /// The OAuth2.0 scopes required by your project.
        /// </summary>
        public static readonly string[] SCOPES = new String[]
        {
            "https://www.googleapis.com/auth/drive.file" ,
            "https://www.googleapis.com/auth/userinfo.email" ,
            "https://www.googleapis.com/auth/userinfo.profile" ,
            "https://www.googleapis.com/auth/drive.install"
        };

        /// <summary>
        /// The Redirect URI of your project.
        /// </summary>
        public static readonly string REDIRECT_URI = "http://127.0.0.1";  //"[[YOUR_REDIRECT_URI]]";
    }
}
