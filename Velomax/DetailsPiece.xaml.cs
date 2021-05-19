using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Velomax.modules;

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
        private SortedList<int, string> categories;
        private SortedList<string, string> fournisseurs;
        
        public DetailsPiece(MySqlConnection maConnexion, StockPieces parent, string idPiece)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            this.parent = parent;
            this.idPiece = idPiece;
            InitCategories();
            InitFournisseurs();
            InitInfosPiece();
        }

        private void InitCategories()
        {
            categories = new SortedList<int, string>();
            string requete = "SELECT id_categorie, lib_categorie FROM categorie;";
            RemplirComboBox<int, string>.RemplirCategories(maConnexion, requete, cbCategories, categories);
        }

        private void InitFournisseurs()
        {
            ///liste des fournisseurs qui produisent cette pièce
            fournisseurs = new SortedList<string, string>();
            string requete = "SELECT DISTINCT siret_fourn, nom_fourn FROM fournit NATURAL JOIN fournisseur WHERE id_piece = '" + idPiece + "';";
            RemplirComboBox<string, string>.RemplirFournisseurs(maConnexion, requete, cbFournisseurs, fournisseurs);
        }

        private void InitInfosPiece()
        {
            try
            {
                maConnexion.Open();

                MySqlParameter id_piece = new MySqlParameter("@id_piece", MySqlDbType.VarChar);
                id_piece.Value = idPiece;

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_categorie, prix_piece, AVG(DISTINCT prix_fournit), stock_piece, dateIntro_piece, dateDisc_piece " +
                                        "FROM piece NATURAL JOIN fournit " +
                                        "WHERE id_piece = @id_piece;";
                command.Parameters.Add(id_piece);

                MySqlDataReader reader = command.ExecuteReader();


                if (reader.Read())   // parcours ligne par ligne
                {
                    int categorie = reader.GetInt32(0);
                    double prixVeloMax = reader.GetDouble(1);
                    double moyennePrixFourn = reader.GetDouble(2);
                    int stock = reader.GetInt32(3);
                    DateTime dateIntro = reader.GetDateTime(4);
                    DateTime dateDisc = reader.GetDateTime(5);

                    tbIdPiece.Text = idPiece;
                    cbCategories.SelectedIndex = categories.IndexOfKey(categorie);
                    tbPrixVeloMax.Text = prixVeloMax.ToString("F", System.Globalization.CultureInfo.CurrentCulture);
                    tbMoyPrixFourn.Text = moyennePrixFourn.ToString("F", System.Globalization.CultureInfo.CurrentCulture);
                    tbStock.Text = stock.ToString();
                    dpDateIntro.SelectedDate = dateIntro;
                    dpDateDisc.SelectedDate = dateDisc;
                }

                reader.Close();
                command.Dispose();

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

        private void GenerateIdAuto()
        {
            int indexCategorie = cbCategories.SelectedIndex;

            if (indexCategorie == -1)
            {
                MessageBox.Show("Sélectionnez une catégorie");
                cbAuto.IsChecked = false;
            }
            else
            {
                tbIdPiece.Text = GenererId.GenerateIdAuto(maConnexion, cbAuto, categories.Keys[indexCategorie]);
            }
        }

        private void cbAuto_Click(object sender, RoutedEventArgs e)
        {
            if (cbAuto.IsChecked == true)
            {
                GenerateIdAuto();
            }
        }

        private void cbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAuto.IsChecked == true)
            {
                GenerateIdAuto();
            }
        }

        private void cbFournisseurs_DropDownClosed(object sender, EventArgs e)
        {
            int indexFournisseur = cbFournisseurs.SelectedIndex;

            if (indexFournisseur != -1)
            {
                try
                {
                    maConnexion.Open();

                    MySqlParameter id_piece = new MySqlParameter("@id_piece", MySqlDbType.VarChar);
                    id_piece.Value = idPiece;
                    MySqlParameter siret_fourn = new MySqlParameter("@siret_fourn", MySqlDbType.VarChar);
                    siret_fourn.Value = fournisseurs.Keys[indexFournisseur];

                    // ORDER BY date_fournit DESC pour avoir le N° produit le plus récent s'il a changé
                    MySqlCommand command = maConnexion.CreateCommand();
                    command.CommandText = "SELECT DISTINCT numCatalogue_fournit FROM fournit WHERE id_piece = @id_piece AND siret_fourn = @siret_fourn ORDER BY date_fournit DESC;";
                    command.Parameters.Add(id_piece);
                    command.Parameters.Add(siret_fourn);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string numCatalogue = reader.GetString(0);
                        tbNumCatalogue.Text = numCatalogue;
                    }

                    reader.Close();

                    command.CommandText = "SELECT AVG(delai_fournit) FROM fournit WHERE id_piece = @id_piece AND siret_fourn = @siret_fourn;";
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        double moyDelais = reader.GetDouble(0);
                        tbDelai.Text = moyDelais.ToString("F1", System.Globalization.CultureInfo.CurrentCulture);
                    }

                    reader.Close();
                    command.Dispose();

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
        }

        private void bUp_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbStock.Text, out int stock))
            {
                MessageBox.Show("Quantité en stock invalide");
            }
            else
            {
                stock++;
                tbStock.Text = stock.ToString();
            }
        }

        private void bDown_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbStock.Text, out int stock))
            {
                MessageBox.Show("Quantité en stock invalide");
            }
            else
            {
                if (stock > 0)
                {
                    stock--;
                    tbStock.Text = stock.ToString();
                }
            }
        }
        
        private void bEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            string nvIdPiece = tbIdPiece.Text;

            if (nvIdPiece == "")
            {
                MessageBox.Show("Entrez l'id de la pièce");
            }
            else
            {
                int indexCategorie = cbCategories.SelectedIndex;

                if (indexCategorie == -1)
                {
                    MessageBox.Show("Sélectionnez une catégorie");
                }
                else
                {
                    bool saisieOK = double.TryParse(tbPrixVeloMax.Text, out double prix);

                    if (!saisieOK)
                    {
                        MessageBox.Show("Entrez un prix valide");
                    }
                    else
                    {
                        saisieOK = int.TryParse(tbStock.Text, out int stock);

                        if (!saisieOK)
                        {
                            MessageBox.Show("Entrez une quantité en stock valide");
                        }
                        else
                        {
                            int indexFournisseur = cbFournisseurs.SelectedIndex;

                            if (dpDateIntro.SelectedDate == null)
                            {
                                MessageBox.Show("Entrez la date de début de production");
                            }
                            else
                            {
                                DateTime dateIntro = (DateTime)dpDateIntro.SelectedDate;

                                if (dpDateDisc.SelectedDate == null)
                                {
                                    MessageBox.Show("Entrez la date de fin de production");
                                }
                                else
                                {
                                    DateTime dateDisc = (DateTime)dpDateDisc.SelectedDate;

                                    try
                                    {
                                        maConnexion.Open();

                                        MySqlParameter id_piece = new MySqlParameter("@id_piece", MySqlDbType.VarChar);
                                        id_piece.Value = this.idPiece;
                                        
                                        MySqlParameter nv_id_piece = new MySqlParameter("@nv_id_piece", MySqlDbType.VarChar);
                                        nv_id_piece.Value = nvIdPiece;

                                        MySqlParameter prix_piece = new MySqlParameter("@prix_piece", MySqlDbType.Float);
                                        prix_piece.Value = prix;

                                        MySqlParameter dateIntro_piece = new MySqlParameter("@dateIntro_piece", MySqlDbType.Date);
                                        dateIntro_piece.Value = dateIntro;

                                        MySqlParameter dateDisc_piece = new MySqlParameter("@dateDisc_piece", MySqlDbType.Date);
                                        dateDisc_piece.Value = dateDisc;

                                        MySqlParameter stock_piece = new MySqlParameter("@stock_piece", MySqlDbType.Int32);
                                        stock_piece.Value = stock;

                                        MySqlParameter id_categorie = new MySqlParameter("@id_categorie", MySqlDbType.Int32);
                                        id_categorie.Value = categories.Keys[indexCategorie];

                                                                                
                                        MySqlCommand command = maConnexion.CreateCommand();
                                        command.CommandText = "UPDATE piece SET id_piece = @nv_id_piece, prix_piece = @prix_piece, dateIntro_piece = @dateIntro_piece, " + 
                                            "dateDisc_piece = @dateDisc_piece, stock_piece = @stock_piece, id_categorie = @id_categorie WHERE id_piece = @id_piece;";
                                        command.Parameters.Add(id_piece);
                                        command.Parameters.Add(nv_id_piece);
                                        command.Parameters.Add(prix_piece);
                                        command.Parameters.Add(dateIntro_piece);
                                        command.Parameters.Add(dateDisc_piece);
                                        command.Parameters.Add(stock_piece);
                                        command.Parameters.Add(id_categorie);

                                        
                                        command.ExecuteNonQuery();
                                        MessageBox.Show("Pièce modifiée !");

                                        command.Dispose();
                                        this.Close();
                                    }
                                    catch (MySqlException erreur)
                                    {
                                        MessageBox.Show("Erreur lors de la modification des informations :\n" + erreur);
                                    }
                                    finally
                                    {
                                        maConnexion.Close();
                                        this.parent.LoadInfosPieces(); // actualisation de la liste
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
