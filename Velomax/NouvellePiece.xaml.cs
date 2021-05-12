using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour NouvellePiece.xaml
    /// </summary>
    public partial class NouvellePiece : Window
    {
        private MySqlConnection maConnexion;

        public NouvellePiece(MySqlConnection maConnexion)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            Init();
        }

        private void Init()
        {
            List<string> liste = new List<string>();
            liste.Add("bli");
            liste.Add("bla");
            cbCategories.ItemsSource = liste;
        }

        private void bValider_Click(object sender, RoutedEventArgs e)
        {
            string idPiece = tbIdPiece.Text;

            if (idPiece == "")
            {
                MessageBox.Show("Entrez l'id de la pièce");
            }
            else
            {
                //string categorie = cbCategories.G

                maConnexion.Open();


                maConnexion.Close();
            }
        }
    }
}
