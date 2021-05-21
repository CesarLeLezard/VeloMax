using System.Windows;
using MySql.Data.MySqlClient;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySqlConnection maConnexion;

        public MainWindow()
        {
            InitializeComponent();

            #region Test ouverture de connexion

            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=Kb:8E2z?Y);";
                maConnexion = new MySqlConnection(connexionString);
                maConnexion.Open();
                maConnexion.Close();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Erreur : l'application n'a pas pu se connecter au serveur MySQL ");
                this.Close();
            }

            #endregion
        }
        


        private void OuvrirStockPieces(object sender, RoutedEventArgs e)
        {
            StockPieces windowStock = new StockPieces(maConnexion);
            windowStock.Show();
        }

        private void OuvrirStockVelos(object sender, RoutedEventArgs e)
        {
            StockVelos windowStock = new StockVelos(maConnexion);
            windowStock.Show();
        }

        private void bFournisseurs_Click(object sender, RoutedEventArgs e)
        {
            Fournisseurs windowFournisseurs = new Fournisseurs(maConnexion);
            windowFournisseurs.Show();
        }

        private void bClients_Click(object sender, RoutedEventArgs e)
        {
            Clients windowClients = new Clients(maConnexion);
            windowClients.Show();
        }

        private void bCommandes_Click(object sender, RoutedEventArgs e)
        {
            Commandes window = new Commandes(maConnexion);
            window.Show();
        }
    }
}
