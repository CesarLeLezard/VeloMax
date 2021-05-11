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
    /// Logique d'interaction pour Stock.xaml
    /// </summary>
    public partial class StockPieces : Window
    {
        private MySqlConnection maConnexion;

        public StockPieces(MySqlConnection maConnexion)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            LoadInfosPieces();
        }


        private void LoadInfosPieces()
        {
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT id_piece, lib_categorie, dateIntro_piece, dateDisc_piece, stock_piece FROM piece NATURAL JOIN categorie ORDER BY id_piece;";

            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dgPieces.ItemsSource = dt.DefaultView;

            // maConnexion.Close();
        }

        private void bNvPiece_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
