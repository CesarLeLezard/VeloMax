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

        private void LoadInfosModeles()
        {
            maConnexion.Open();

            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = "SELECT id_modele, nom_modele, lib_grandeur, prix_modele, dateIntro_modele, dateDisc_modele, stock_modele, lib_ligne FROM modele NATURAL JOIN grandeur NATURAL JOIN ligneProduit ORDER BY id_modele;";

            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dgVelos.ItemsSource = dt.DefaultView;

            maConnexion.Close();
        }
    }
}
