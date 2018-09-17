using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projectname.CORE_ADMIN
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["nameofCookie"] == null) // Pas de témoin de connexion
            {
                Response.Redirect("otherpage.aspx"); // On revient a la page de connexion
            }
        }
    }
}