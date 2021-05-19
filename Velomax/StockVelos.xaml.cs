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
    /// Logique d'interaction pour StockVelos.xaml
    /// </summary>
    public partial class StockVelos : Window
    {
        private MySqlConnection maConnexion;

        public StockVelos(MySqlConnection maConnexion)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            LoadInfosModeles();
        }


        // public pour pouvoir appeler cette fonction à partir d'autres fenetres,
        // afin d'actualiser la liste
        public void LoadInfosModeles()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_modele, nom_modele, lib_grandeur, prix_modele, dateIntro_modele, dateDisc_modele, stock_modele, lib_ligne FROM modele NATURAL JOIN grandeur NATURAL JOIN ligneProduit ORDER BY id_modele;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgVelos.ItemsSource = dt.DefaultView;
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

        private void bSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgVelos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez un modèle", "Supprimer le modèle", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Êtes vous sur ? Cette action est irréversible", "Supprimer le modèle", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        maConnexion.Open();

                        MySqlParameter id_modele = new MySqlParameter("@id_modele", MySqlDbType.Int32);
                        MySqlCommand command = maConnexion.CreateCommand();
                        command.Parameters.Add(id_modele);

                        foreach (DataRowView ligne in dgVelos.SelectedItems)
                        {
                            id_modele.Value = ligne.Row.Field<int>(0); // récupération de l'id modèle à suppr

                            command.CommandText = "DELETE FROM modele WHERE id_modele = @id_modele;";
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
                        this.LoadInfosModeles(); // actualisation de la liste des modèles
                    }
                }
            }
        }

        private void bNvVelo_Click(object sender, RoutedEventArgs e)
        {
            NouveauModele windowNvModele = new NouveauModele(maConnexion, this);
            windowNvModele.Show();
        }

        private void bDetailsModele_Click(object sender, RoutedEventArgs e)
        {
            if (dgVelos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez un modèle");
            }
            else
            {
                int idModele = 0;

                try
                {
                    foreach (DataRowView ligne in dgVelos.SelectedItems)
                    {
                        idModele = ligne.Row.Field<int>(0); // récupération de l'id modèle

                        DetailsModele windowDetailsModele = new DetailsModele(maConnexion, this, idModele);
                        windowDetailsModele.Show();
                    }
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Erreur : élément vide");
                }
            }
        }

        
        private void bListePieces_Click(object sender, RoutedEventArgs e)
        {
            ListePiecesModele windowListePieces = new ListePiecesModele(maConnexion);
            windowListePieces.Show();
        }
    }
}
