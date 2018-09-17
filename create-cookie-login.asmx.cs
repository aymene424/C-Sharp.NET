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
    public class log : System.Web.Services.WebService
    {

        // Fonction de création de chaine de connection
        [WebMethod]
        public static string construction_string_connection()
        {

            string chaine_connection = "Data Source=localhost\\MSSQLSERVER2012;Initial Catalog=database_name;Integrated Security=SSPI;MultipleActiveResultSets=True;";
            return chaine_connection;
        }


        public SqlConnection con = new SqlConnection(construction_string_connection());
        // Fonction de connection de l'utilisateur ADMIN 
        
        /* Call it with Ajax */
        
        [WebMethod]
        public void connection_user(string _user, string _pwd)
        {
            
            SqlConnection con = new SqlConnection(construction_string_connection());
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("dbo.NameOFMSSQLProcedure", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@user", SqlDbType.VarChar).Value = user;
                command.Parameters.Add("@pwd", SqlDbType.VarChar).Value = pwd;

                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                sda.Fill(dt);
                
                // Si le nombre de ligne est = 1 c'est à dire que L'user et pwd existe donc on a une selection correcte
                if (dt.Rows.Count.ToString() == "1")
                {

                    HttpCookie cookie_connection = new HttpCookie("Yourcookiename"); // Création d'un témoins de connexion (Coockie)
                    cookie_connection["idforexemple"] = dt.Rows[0]["id"].ToString();  // On insert ce qu'on veut dans la Cookie ici c'est L'id de l'user
                    cookie_connection.Expires = DateTime.Now.AddDays(1d); // Temps d'expiration de la Cookie
                    HttpContext.Current.Response.Cookies.Add(cookie_connection); // On valide la Cookie

                }

                con.Close();
                return SQLToJson(dt);

            }
            else
            {
                con.Close();
                return "Erreur de connexion";
            }
           
        }


    }
}
