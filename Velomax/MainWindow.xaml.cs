using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Xml.Serialization;
using System.IO;

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
    }
}
