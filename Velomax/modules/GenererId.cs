using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Velomax.modules
{
    class GenererId
    {

        public static string GenerateIdAuto(MySqlConnection maConnexion, CheckBox cbAuto, int idCategorie)
        {
            try
            {
                maConnexion.Open();

                #region Récupération du code catégorie correspondant à la catégorie

                MySqlParameter id_categorie = new MySqlParameter("@id_categorie", MySqlDbType.Int32);
                id_categorie.Value = idCategorie;

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT code_categorie FROM categorie WHERE id_categorie = @id_categorie;";
                command.Parameters.Add(id_categorie);

                MySqlDataReader reader = command.ExecuteReader();

                string codeCategorie = "";

                if (reader.Read())
                {
                    codeCategorie = reader.GetString(0);
                }

                reader.Close();
                command.Dispose();

                #endregion

                #region Récupération du dernier id correspondant au code catégorie

                MySqlParameter code_categorie = new MySqlParameter("@code_categorie", MySqlDbType.VarChar);
                code_categorie.Value = codeCategorie;

                command.CommandText = "SELECT id_piece FROM piece NATURAL JOIN categorie WHERE code_categorie = @code_categorie ORDER BY id_piece DESC;";
                command.Parameters.Add(code_categorie);

                reader = command.ExecuteReader();

                string dernierIdPiece = "";

                if (reader.Read())
                {
                    dernierIdPiece = reader.GetString(0);
                }

                reader.Close();
                command.Dispose();

                #endregion

                string dernierNumString = "";

                foreach (char c in dernierIdPiece)
                {
                    if (Char.IsDigit(c))
                    {
                        dernierNumString += c;
                    }
                }

                int dernierNum = Convert.ToInt32(dernierNumString);
                dernierNum++;

                string idAuto = codeCategorie + dernierNum.ToString();

                return idAuto;

            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("Erreur de requête SQL :\n" + erreur);
                cbAuto.IsChecked = false;
                return "";
            }
            finally
            {
                maConnexion.Close();
            }
        }
    }
}
