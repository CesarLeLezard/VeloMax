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
    /// Logique d'interaction pour DetailsPiece.xaml
    /// </summary>
    public partial class DetailsPiece : Window
    {
        private MySqlConnection maConnexion;

        private StockPieces parent;
        private string idPiece;

        public DetailsPiece(MySqlConnection maConnexion, StockPieces parent, string idPiece)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            this.parent = parent;
            this.idPiece = idPiece;
            InitInfosPiece();
        }

        private void InitInfosPiece()
        {
            try
            {
                maConnexion.Open();

                MySqlParameter id_piece = new MySqlParameter("@id_piece", MySqlDbType.VarChar);
                id_piece.Value = idPiece;

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT lib_categorie, prix_piece, AVG(DISTINCT prix_fournit), stock_piece, dateIntro_piece, dateDisc_piece " +
                                        "FROM piece NATURAL JOIN categorie NATURAL JOIN fournit " +
                                        "WHERE id_piece = @id_piece;";
                command.Parameters.Add(id_piece);

                MySqlDataReader reader;
                reader = command.ExecuteReader();

                
                if (reader.Read())   // parcours ligne par ligne
                {
                    string categorie = reader.GetString(0);
                    double prixVeloMax = reader.GetDouble(1);
                    double moyennePrixFourn = reader.GetDouble(2);
                    int stock = reader.GetInt32(3);
                    DateTime dateIntro = reader.GetDateTime(4);
                    DateTime dateDisc = reader.GetDateTime(5);

                    tbIdPiece.Text = idPiece;
                    // tbCategorie.Text = categorie;
                    tbPrixVeloMax.Text = prixVeloMax.ToString();
                    tbMoyPrixFourn.Text = moyennePrixFourn.ToString("F", System.Globalization.CultureInfo.CurrentCulture);
                    tbStock.Text = stock.ToString();
                    dpDateIntro.SelectedDate = dateIntro;
                    dpDateDisc.SelectedDate = dateDisc;
                }
            }
            catch (MySqlException erreur)
            {
                MessageBox.Show("Erreur de requête SQL :\n" + erreur);
                this.Close();
            }
            finally
            {
                maConnexion.Close();
            }
        }

        private void cbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
