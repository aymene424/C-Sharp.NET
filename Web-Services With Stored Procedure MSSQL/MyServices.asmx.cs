using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CashAssurancesWebSite.Web_Service
{
    /// <summary>
    /// Description résumée de myclassname
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    [System.Web.Script.Services.ScriptService]
    public class myclassname : System.Web.Services.WebService
    {
    
        /*
            
            IN .ASPX.CS you should add static before the name of the function.
            IN .ASMX.CS You don't need to make it.
            
        */
        
        [WebMethod]
        public static string construction_string_connection()
        {

            string chaine_connection = "Data Source=......;Initial Catalog=......;Integrated Security=False;User ID=.......;Password=........;Connect Timeout=15;Encrypt=False;Packet Size=4096";

            return chaine_connection;
        }
        
        [WebMethod]
        public static string SQLToJson(DataTable dt)
        {
            // Creation d'un outil qui permet de Convertir la table SQL en Json Format
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); // Creation
            serializer.MaxJsonLength = Int32.MaxValue; // On utilise la taille maximal pour le stockage puisque on ne sait pas y-a combien de ligne dans le résultat SQL
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>(); // On Créer une liste "Key":Value pour le format JSon
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows) // On parcour la table SQL reçu
            {
                row = new Dictionary<string, object>();  // On créer une ligne dans le Dictionary pour chaque ligne SQL
                foreach (DataColumn col in dt.Columns) // On Parcour la ligne colonne par colonne 
                {
                    row.Add(col.ColumnName, dr[col]); // On affect le nom de la colonne et sa valeur dans le Dictionary
                }
                rows.Add(row); // On ajoute le dictionnary dans la liste
            }
            return serializer.Serialize(rows); // On retourn le Json
        }


        [WebMethod]
        public string MyFunction(string id = null)
        {
            SqlConnection con = new SqlConnection(construction_string_connection());
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("dbo.NameOFMSSQLProcedure", con);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                sda.Fill(dt);

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
