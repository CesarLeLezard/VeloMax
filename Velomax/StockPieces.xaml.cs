using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.IO;
using System.Xml.Serialization;

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


        // public pour pouvoir appeler cette fonction à partir d'autres fenetres,
        // afin d'actualiser la liste
        public void LoadInfosPieces()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_piece, lib_categorie, prix_piece, dateIntro_piece, dateDisc_piece, stock_piece FROM piece NATURAL JOIN categorie ORDER BY id_piece;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
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
                MessageBox.Show("Sélectionnez une pièce", "Supprimer la pièce", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Êtes vous sur ? Cette action est irréversible", "Supprimer la pièce", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        maConnexion.Open();

                        MySqlParameter id_piece = new MySqlParameter("@id_piece", MySqlDbType.VarChar);
                        MySqlCommand command = maConnexion.CreateCommand();
                        command.Parameters.Add(id_piece);

                        foreach (DataRowView ligne in dgPieces.SelectedItems)
                        {
                            id_piece.Value = ligne.Row.Field<string>(0); // récupération de l'id pièce à suppr

                            command.CommandText = "DELETE FROM piece WHERE id_piece = @id_piece;";
                            command.ExecuteNonQuery();
                        }

                        command.Dispose();
                        MessageBox.Show("Suppression effectuée !", "Validation de suppresion", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (MySqlException erreur)
                    {
                        MessageBox.Show("Erreur :\n" + erreur);
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Erreur : élément vide");
                    }
                    finally
                    {
                        maConnexion.Close();
                        this.LoadInfosPieces(); // actualisation de la liste des modèles
                    }
                }
            }

        }

        private void bDetails_Click(object sender, RoutedEventArgs e)
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
                    foreach (DataRowView ligne in dgPieces.SelectedItems)
                    {
                        idPiece = ligne.Row.Field<string>(0); // récupération de l'id pièce

                        DetailsPiece windowDetailsPiece = new DetailsPiece(maConnexion, this, idPiece);
                        windowDetailsPiece.Show();
                    }
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Erreur : élément vide");
                }
            }
        }
        public class Piece
        {
            public string id_piece { get; set; }
            public string categorie { get; set; }
            public DateTime début_prod { get; set; }
            public DateTime fin_prod { get; set; }
            public double prix { get; set; }
            public int stock { get; set; }
            public Piece() { }
            public Piece(string id_piece, string categorie, DateTime debut, DateTime fin, double prix, int stock)
            {
                this.id_piece = id_piece;
                this.categorie = categorie;
                this.début_prod = debut;
                this.fin_prod = fin;
                this.prix = prix;
                this.stock = stock;
            }
        }
        private void bExportStock_Click(object sender, RoutedEventArgs e)
        {
            List<Piece> l = new List<Piece>();

            foreach (DataRowView ligne in dgPieces.ItemsSource)
            {

                if (ligne.Row.Field<int>(5) < 11)
                {
                    //MessageBox.Show(Convert.ToString(ligne.Row.Field<float>(2)));
                    Piece nouv = new Piece(ligne.Row.Field<string>(0), ligne.Row.Field<string>(1), ligne.Row.Field<DateTime>(3), ligne.Row.Field<DateTime>(4), ligne.Row.Field<float>(2), ligne.Row.Field<int>(5));
                    l.Add(nouv);
                    //MessageBox.Show(Convert.ToString(nouv.id_piece));
                }
            }
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Piece>));
                StreamWriter wr = new StreamWriter("StockFaible.xml");
                xs.Serialize(wr, l);
                wr.Close();
                MessageBox.Show("Exportation réussie");
            }
            catch (Exception err)
            {
                MessageBox.Show("Une erreur s'est produite ", Convert.ToString(err));
            }
        }
    }
}
