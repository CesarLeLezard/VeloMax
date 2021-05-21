using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour Commandes.xaml
    /// </summary>
    public partial class Commandes : Window
    {
        private MySqlConnection maConnexion;

        public Commandes(MySqlConnection maConnexion)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            LoadInfosCommandes();
        }


        public void LoadInfosCommandes()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT commande.*, SUM(sous_tot) AS total FROM (" +
                                        "SELECT commande.*, SUM(prix_modele * qte_contientModele) AS sous_tot FROM commande NATURAL JOIN contientModele NATURAL JOIN modele GROUP BY id_commande " +
                                        "UNION " +
                                        "SELECT commande.*, SUM(prix_piece * qte_contientPiece) AS sous_tot FROM commande NATURAL JOIN contientPiece NATURAL JOIN piece GROUP BY id_commande) AS commande " +
                                        "GROUP BY id_commande ORDER BY id_commande;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgCommandes.ItemsSource = dt.DefaultView;
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

        private void bNvCommande_Click(object sender, RoutedEventArgs e)
        {
            NouvelleCommande window = new NouvelleCommande(maConnexion, this);
            window.Show();
        }
    }
}
