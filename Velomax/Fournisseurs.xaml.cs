using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour Fournisseurs.xaml
    /// </summary>
    public partial class Fournisseurs : Window
    {
        private MySqlConnection maConnexion;

        public Fournisseurs(MySqlConnection maConnexion)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            LoadInfosFournisseurs();
        }

        public void LoadInfosFournisseurs()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT siret_fourn, nom_fourn, tel_fourn, mail_fourn, adresse_fourn, codeP_fourn," +
                    " ville_fourn, lib_react FROM fournisseur NATURAL JOIN reactivite ORDER BY nom_fourn;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgFournisseurs.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("Erreur de requête SQL :\n" + erreur);
            }
            finally
            {
                maConnexion.Close();
            }
        }

        private void OuvrirStockPieces(object sender, RoutedEventArgs e)
        {
            if (dgFournisseurs.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez un fournisseur", "Supprimer le fournisseur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Êtes vous sur ? Cette action est irréversible", "Modifier le fournisseur", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        maConnexion.Open();

                        MySqlParameter siret_fourn = new MySqlParameter("@siret_fourn", MySqlDbType.VarChar);
                        MySqlCommand command = maConnexion.CreateCommand();
                        command.Parameters.Add(siret_fourn);
                        //MessageBox.Show(Convert.ToString(siret_fourn));
                        foreach (DataRowView ligne in dgFournisseurs.SelectedItems)
                        {
                            siret_fourn.Value = ligne.Row.Field<string>(0); // récupération de l'id modèle à suppr
                            string siret_fourn2 = ligne.Row.Field<string>(0);
                            //MessageBox.Show(ligne.Row.Field<string>(0));
                            string nom_fournisseur = ligne.Row.Field<string>(1);
                            //MessageBox.Show(ligne.Row.Field<string>(1));
                            string tel_fournisseur = ligne.Row.Field<string>(2);
                            //MessageBox.Show(ligne.Row.Field<string>(2));
                            string mail_fournisseur = ligne.Row.Field<string>(3);
                            //MessageBox.Show(ligne.Row.Field<string>(3));
                            string addresse_fournisseur = ligne.Row.Field<string>(4);
                            //MessageBox.Show(ligne.Row.Field<string>(4));
                            string cp_fournisseur = ligne.Row.Field<string>(5);
                            //MessageBox.Show(ligne.Row.Field<string>(5));
                            string ville_fournisseur = ligne.Row.Field<string>(6);
                            //MessageBox.Show(ligne.Row.Field<string>(6));
                            string react_fournisseur = ligne.Row.Field<string>(7);
                            int react2_fournisseur;
                            //MessageBox.Show(ligne.Row.Field<string>(7));
                            if (react_fournisseur == "Mauvais")
                            {
                                react2_fournisseur = 4;
                            }
                            else if (react_fournisseur == "Moyen")
                            {
                                react2_fournisseur = 3;
                            }
                            else if (react_fournisseur == "Bon")
                            {
                                react2_fournisseur = 2;
                            }
                            else if (react_fournisseur == "Très bon")
                            {
                                react2_fournisseur = 1;
                            }
                            else
                            {
                                MessageBox.Show("Erreur de Notation react mis a Moyen");
                                react2_fournisseur = 3;
                            }
                            command.CommandText = "UPDATE fournisseur SET nom_fourn = '" + nom_fournisseur + "', tel_fourn = '" + tel_fournisseur + "', mail_fourn = '" + mail_fournisseur + "'," +
                                " adresse_fourn = '" + addresse_fournisseur + "', codeP_fourn = '" + cp_fournisseur + "', ville_fourn ='" + ville_fournisseur + "', id_react ='" + react2_fournisseur + "' Where siret_fourn = '" + siret_fourn2 + "';";

                            //MessageBox.Show("UPDATE fournisseur SET nom_fourn = '" + nom_fournisseur + "', tel_fourn = '" + tel_fournisseur + "', mail_fourn = '" + mail_fournisseur + "'," +
                            //    " adresse_fourn = '" + addresse_fournisseur + "', codeP_fourn = '" + cp_fournisseur + "', ville_fourn ='" + ville_fournisseur + "' Where siret_fourn = '" + siret_fourn2 + "';");
                            command.ExecuteNonQuery();
                        }

                        command.Dispose();
                        MessageBox.Show("Modification effectuée !", "Validation de la modif", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (MySqlException erreur)
                    {
                        MessageBox.Show("Erreur :\n" + erreur);
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Erreur : élément vide");
                    }
                    finally
                    {
                        maConnexion.Close();
                        this.LoadInfosFournisseurs(); // actualisation de la liste des modèles
                    }
                }
            }
        }

        private void bSuppressionFournisseurs_Click(object sender, RoutedEventArgs e)
        {

            if (dgFournisseurs.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez un fournisseur", "Supprimer le fournisseur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Êtes vous sur ? Cette action est irréversible", "Supprimer le fournisseur", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        maConnexion.Open();

                        MySqlParameter siret_fourn = new MySqlParameter("@siret_fourn", MySqlDbType.VarChar);
                        MySqlCommand command = maConnexion.CreateCommand();
                        command.Parameters.Add(siret_fourn);

                        foreach (DataRowView ligne in dgFournisseurs.SelectedItems)
                        {
                            siret_fourn.Value = ligne.Row.Field<string>(0); // récupération de l'id modèle à suppr

                            command.CommandText = "DELETE FROM fournisseur WHERE siret_fourn = @siret_fourn;";
                            command.ExecuteNonQuery();
                        }

                        command.Dispose();
                        MessageBox.Show("Suppression effectuée !", "Validation de suppresion", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (MySqlException erreur)
                    {
                        MessageBox.Show("Erreur :\n" + erreur);
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Erreur : élément vide");
                    }
                    finally
                    {
                        maConnexion.Close();
                        this.LoadInfosFournisseurs(); // actualisation de la liste des modèles
                    }
                }
            }
        }

        private void bAjoutFournisseurs_Click(object sender, RoutedEventArgs e)
        {
            CréationFournisseurs WindowCré = new CréationFournisseurs(maConnexion, this);
            WindowCré.Show();
        }
    }
}
