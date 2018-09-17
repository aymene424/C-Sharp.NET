using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace nameofyourproject.Web_Service
{
    /// <summary>
    /// Description résumée de log
    /// </summary>
    [WebService(Namespace = "http://localhost:57270/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    [System.Web.Script.Services.ScriptService]
    public class logoff : System.Web.Services.WebService
    {
        
        /* Call it with Ajax */

        [WebMethod]
        public void disconnect_user()
        {
            HttpCookie cookie_connection = new HttpCookie("yourcookiename"); // On créer un témoins de connexion avec le même nom de connexion
            cookie_connection.Expires = DateTime.Now.AddDays(-1d); // Qui date d'hier du coup ce n'est pas possible, donc sa va détruire le témoin automatiquement
            HttpContext.Current.Response.Cookies.Add(cookie_connection); // on valide la cookie
        }
    }
}
