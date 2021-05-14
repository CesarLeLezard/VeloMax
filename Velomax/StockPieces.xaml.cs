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


        public void LoadInfosPieces()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_piece, lib_categorie, prix_piece, dateIntro_piece, dateDisc_piece, stock_piece FROM piece NATURAL JOIN categorie ORDER BY id_piece;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
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
        }

        private void bNvPiece_Click(object sender, RoutedEventArgs e)
        {
            NouvellePiece windowNvPiece = new NouvellePiece(maConnexion, this);
            windowNvPiece.Show();
        }

        private void bSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgPieces.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez une pièce");
            }
            else
            {
                string idPiece = "";

                try
                {
                    maConnexion.Open();

                    foreach (DataRowView ligne in dgPieces.SelectedItems)
                    {
                        idPiece = ligne.Row.Field<string>(0); // récupération de l'id pièce à suppr

                        MySqlParameter id_piece = new MySqlParameter("@id_piece", MySqlDbType.VarChar);
                        id_piece.Value = idPiece;

                        MySqlCommand command = maConnexion.CreateCommand();
                        command.CommandText = "DELETE FROM piece WHERE id_piece = @id_piece;";
                        command.Parameters.Add(id_piece);

                        command.ExecuteNonQuery();
                        MessageBox.Show("La pièce " + idPiece + " a bien été supprimée");
                    }
                }
                catch (MySqlException erreur)
                {
                    MessageBox.Show("Erreur à la suppression de la pièce " + idPiece + " :\n" + erreur);
                }
                finally
                {
                    maConnexion.Close();
                    this.LoadInfosPieces(); // actualisation de la liste des pièces
                }
            }

        }

    }
}
