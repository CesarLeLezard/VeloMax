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
    /// Logique d'interaction pour Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        private MySqlConnection maConnexion;

        public Clients(MySqlConnection maConnexion)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            LoadInfosClientsInd();
            LoadInfosClientsBou();
        }


        public void LoadInfosClientsInd()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT clientInd.*, lib_fidelio FROM clientInd NATURAL JOIN adhereInd NATURAL JOIN fidelio ORDER BY nom_clientInd;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgClientsInd.ItemsSource = dt.DefaultView;
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

        public void LoadInfosClientsBou()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT clientBou.*, lib_fidelio FROM clientBou NATURAL JOIN adhereBou NATURAL JOIN fidelio ORDER BY nom_clientBou;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgClientsBou.ItemsSource = dt.DefaultView;
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

        private void bNvClientInd_Click(object sender, RoutedEventArgs e)
        {
            NouveauClientInd window = new NouveauClientInd(maConnexion, this);
            window.Show();
        }

        private void bSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientsInd.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez un client", "Supprimer le client", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Êtes vous sur ? Cette action est irréversible", "Supprimer le client", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        maConnexion.Open();

                        MySqlParameter id_clientInd = new MySqlParameter("@id_clientInd", MySqlDbType.Int32);
                        MySqlCommand command = maConnexion.CreateCommand();
                        command.Parameters.Add(id_clientInd);

                        foreach (DataRowView ligne in dgClientsInd.SelectedItems)
                        {
                            id_clientInd.Value = ligne.Row.Field<int>(0); // récupération de l'id client à suppr

                            command.CommandText = "DELETE FROM clientInd WHERE id_clientInd = @id_clientInd;";
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
                        this.LoadInfosClientsInd(); // actualisation de la liste des modèles
                    }
                }
            }
        }

        private void bDetailsModele_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientsInd.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez un client");
            }
            else
            {
                int idClient = 0;

                try
                {
                    foreach (DataRowView ligne in dgClientsInd.SelectedItems)
                    {
                        idClient = ligne.Row.Field<int>(0); // récupération de l'id modèle

                        DetailsClientInd window = new DetailsClientInd(maConnexion, this, idClient);
                        window.Show();
                    }
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Erreur : élément vide");
                }
            }
        }

        private void bNvClientBou_Click(object sender, RoutedEventArgs e)
        {
            NouveauClientBou window = new NouveauClientBou(maConnexion, this);
            window.Show();
        }

        private void bDetailsClientBou_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientsBou.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez un client");
            }
            else
            {
                int idClient = 0;

                try
                {
                    foreach (DataRowView ligne in dgClientsBou.SelectedItems)
                    {
                        idClient = ligne.Row.Field<int>(0); // récupération de l'id modèle

                        DetailsClientBou window = new DetailsClientBou(maConnexion, this, idClient);
                        window.Show();
                    }
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Erreur : élément vide");
                }
            }
        }

        private void bSupprimerClientBou_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientsBou.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez un client", "Supprimer le client", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Êtes vous sur ? Cette action est irréversible", "Supprimer le client", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        maConnexion.Open();

                        MySqlParameter id_clientBou = new MySqlParameter("@id_clientBou", MySqlDbType.Int32);
                        MySqlCommand command = maConnexion.CreateCommand();
                        command.Parameters.Add(id_clientBou);

                        foreach (DataRowView ligne in dgClientsBou.SelectedItems)
                        {
                            id_clientBou.Value = ligne.Row.Field<int>(0); // récupération de l'id client à suppr

                            command.CommandText = "DELETE FROM clientBou WHERE id_clientBou = @id_clientBou;";
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
                        this.LoadInfosClientsBou(); // actualisation de la liste des modèles
                    }
                }
            }
        }
    }
}
