using System.Windows;
using MySql.Data.MySqlClient;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private MySqlConnection maConnexion;
        private MySqlConnection maConnexionroot;
        private MySqlConnection maConnexionbozo;

        public MainWindow()
        {
            InitializeComponent();

            #region Test ouverture de connexion

            try
            {
                string connexionString = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=Kb:8E2z?Y);";
                maConnexionroot = new MySqlConnection(connexionString);
                maConnexionroot.Open();
                maConnexionroot.Close();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Erreur : l'application n'a pas pu se connecter au serveur MySQL ");
            }

            /*
            try
            {
                maConnexionroot.Open();

                MySqlCommand command = maConnexionroot.CreateCommand();
                command.CommandText = "CREATE USER 'bozo'@'localhost' IDENTIFIED BY 'bozo'; + " +
                                        "GRANT SELECT ON velomax.* TO 'bozo'@'localhost';";

                command.ExecuteNonQuery();
                maConnexionroot.Close();

                /*
                string connexionString = "SERVER=localhost;PORT=3306;DATABASE=velomax;UID=bozo;PASSWORD=bozo);";
                maConnexionbozo = new MySqlConnection(connexionString);
                maConnexionbozo.Open();
                maConnexionbozo.Close();
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("Erreur : l'application n'a pas pu se connecter au serveur MySQL \n" + erreur);
                maConnexion = maConnexionroot;
            }*/

            #endregion
        }
        


        private void OuvrirStockPieces(object sender, RoutedEventArgs e)
        {
            StockPieces windowStock = new StockPieces(maConnexionroot);
            windowStock.Show();
        }

        private void OuvrirStockVelos(object sender, RoutedEventArgs e)
        {
            StockVelos windowStock = new StockVelos(maConnexionroot);
            windowStock.Show();
        }

        private void bFournisseurs_Click(object sender, RoutedEventArgs e)
        {
            Fournisseurs windowFournisseurs = new Fournisseurs(maConnexionroot);
            windowFournisseurs.Show();
        }

        private void bClients_Click(object sender, RoutedEventArgs e)
        {
            Clients windowClients = new Clients(maConnexionroot);
            windowClients.Show();
        }

        private void bCommandes_Click(object sender, RoutedEventArgs e)
        {
            Commandes window = new Commandes(maConnexionroot);
            window.Show();
        }

        private void bAutre_Click(object sender, RoutedEventArgs e)
        {
            RapportStats window = new RapportStats(maConnexionroot);
            window.Show();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            //maConnexion = maConnexionroot;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //maConnexion = maConnexionbozo;
        }
    }
}
