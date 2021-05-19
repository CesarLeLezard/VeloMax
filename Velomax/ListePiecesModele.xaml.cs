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
    /// Logique d'interaction pour ListePiecesModele.xaml
    /// </summary>
    public partial class ListePiecesModele : Window
    {
        private MySqlConnection maConnexion;

        public ListePiecesModele(MySqlConnection maConnexion)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            // Init();
        }
        /*
        private void Init()
        {
            // Add columns
            GridView gridView = new GridView();
            lvPieces.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "ID modèle",
                DisplayMemberBinding = new Binding("id_modele"),
                Width = 100
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Name",
                DisplayMemberBinding = new Binding("Name"),
                Width = 100
            });


            lvPieces.A

        }

        
        private void Init1()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ID modèle", typeof(int)));

                maConnexion.Open();

                // on créé tout d'abord les colonnes de catégorie de la DataTable
                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT lib_categorie FROM categorie;";

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string categorie = reader.GetString(0);
                    dt.Columns.Add(new DataColumn(categorie, typeof(string)));
                }





                //dt.Load(command.ExecuteReader());
                command.Dispose();
                dgPieces.ItemsSource = dt.DefaultView;
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("Erreur de requête SQL :\n" + erreur);
            }
            finally
            {
                maConnexion.Close();
            }

        }*/
    }
}
