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
using Velomax.modules;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour NouvellePiece.xaml
    /// </summary>
    public partial class NouvellePiece : Window
    {
        private MySqlConnection maConnexion;

        private StockPieces parent;

        private SortedList<int, string> categories;
        private SortedList<string, string> fournisseurs;

        public NouvellePiece(MySqlConnection maConnexion, StockPieces parent)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            this.parent = parent;
            InitCategories();
            InitFournisseurs();
        }

        private void InitCategories()
        {
            categories = new SortedList<int, string>();
            string requete = "SELECT id_categorie, lib_categorie FROM categorie;";
            RemplirComboBox<int, string>.RemplirCategories(maConnexion, requete, cbCategories, categories);
        }

        private void InitFournisseurs()
        {
            fournisseurs = new SortedList<string, string>();
            string requete = "SELECT siret_fourn, nom_fourn FROM fournisseur";
            RemplirComboBox<string, string>.RemplirFournisseurs(maConnexion, requete, cbFournisseurs, fournisseurs);
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
                int indexCategorie = cbCategories.SelectedIndex;

                if (indexCategorie == -1)
                {
                    MessageBox.Show("Sélectionnez une catégorie");
                }
                else
                {
                    bool saisieOK = double.TryParse(tbPrix.Text, out double prix);

                    if (!saisieOK)
                    {
                        MessageBox.Show("Entrez un prix valide");
                    }
                    else
                    {
                        saisieOK = int.TryParse(tbQuantite.Text, out int quantite);

                        if (!saisieOK)
                        {
                            MessageBox.Show("Entrez une quantité valide");
                        }
                        else
                        {
                            int indexFournisseur = cbFournisseurs.SelectedIndex;

                            if (indexFournisseur == -1)
                            {
                                MessageBox.Show("Sélectionnez un fournisseur");
                            }
                            else
                            {
                                string numCatalogue = tbNumCatalogue.Text;

                                if (numCatalogue == "")
                                {
                                    MessageBox.Show("Entrez le N° produit dans le catalogue du fournisseur");
                                }
                                else
                                {
                                    saisieOK = double.TryParse(tbPrixFourn.Text, out double prixFournisseur);

                                    if (!saisieOK)
                                    {
                                        MessageBox.Show("Entrez un prix fournisseur valide");
                                    }
                                    else
                                    {
                                        saisieOK = int.TryParse(tbDelai.Text, out int delai);

                                        if (!saisieOK)
                                        {
                                            MessageBox.Show("Entrez un délai valide");
                                        }
                                        else
                                        {
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
                                                        id_piece.Value = idPiece;

                                                        MySqlParameter prix_piece = new MySqlParameter("@prix_piece", MySqlDbType.Float);
                                                        prix_piece.Value = prix;

                                                        MySqlParameter dateIntro_piece = new MySqlParameter("@dateIntro_piece", MySqlDbType.Date);
                                                        dateIntro_piece.Value = dateIntro;

                                                        MySqlParameter dateDisc_piece = new MySqlParameter("@dateDisc_piece", MySqlDbType.Date);
                                                        dateDisc_piece.Value = dateDisc;

                                                        MySqlParameter stock_piece = new MySqlParameter("@stock_piece", MySqlDbType.Int32);
                                                        stock_piece.Value = quantite;

                                                        MySqlParameter id_categorie = new MySqlParameter("@id_categorie", MySqlDbType.Int32);
                                                        id_categorie.Value = categories.Keys[indexCategorie];

                                                        MySqlParameter siret_fourn = new MySqlParameter("@siret_fourn", MySqlDbType.VarChar);
                                                        siret_fourn.Value = fournisseurs.Keys[indexFournisseur];

                                                        MySqlParameter prix_fournit = new MySqlParameter("@prix_fournit", MySqlDbType.Float);
                                                        prix_fournit.Value = prixFournisseur;

                                                        MySqlParameter delai_fournit = new MySqlParameter("@delai_fournit", MySqlDbType.Int32);
                                                        delai_fournit.Value = delai;

                                                        MySqlParameter numCatalogue_fournit = new MySqlParameter("@numCatalogue_fournit", MySqlDbType.VarChar);
                                                        numCatalogue_fournit.Value = numCatalogue;

                                                        MySqlParameter date_fournit = new MySqlParameter("@date_fournit", MySqlDbType.Date);
                                                        date_fournit.Value = DateTime.Now.Date;

                                                        MySqlParameter qte_fournit = new MySqlParameter("@qte_fournit", MySqlDbType.Int32);
                                                        qte_fournit.Value = quantite;


                                                        MySqlCommand command1 = maConnexion.CreateCommand();
                                                        command1.CommandText = "INSERT INTO velomax.piece VALUES (@id_piece, @prix_piece, @dateIntro_piece, @dateDisc_piece, @stock_piece, @id_categorie);";
                                                        command1.Parameters.Add(id_piece);
                                                        command1.Parameters.Add(prix_piece);
                                                        command1.Parameters.Add(dateIntro_piece);
                                                        command1.Parameters.Add(dateDisc_piece);
                                                        command1.Parameters.Add(stock_piece);
                                                        command1.Parameters.Add(id_categorie);

                                                        MySqlCommand command2 = maConnexion.CreateCommand();
                                                        command2.CommandText = "INSERT INTO velomax.fournit VALUES (@id_piece, @siret_fourn, @prix_fournit, @delai_fournit, @numCatalogue_fournit, @date_fournit, @qte_fournit);";
                                                        command2.Parameters.Add(id_piece);
                                                        command2.Parameters.Add(siret_fourn);
                                                        command2.Parameters.Add(prix_fournit);
                                                        command2.Parameters.Add(delai_fournit);
                                                        command2.Parameters.Add(numCatalogue_fournit);
                                                        command2.Parameters.Add(date_fournit);
                                                        command2.Parameters.Add(qte_fournit);


                                                        command1.ExecuteNonQuery();
                                                        command2.ExecuteNonQuery();
                                                        MessageBox.Show("Pièce ajoutée !");

                                                        command1.Dispose();
                                                        command2.Dispose();
                                                        this.Close();
                                                    }
                                                    catch (MySqlException erreur)
                                                    {
                                                        MessageBox.Show("Erreur à l'ajout de la pièce :\n" + erreur);
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


        private void cbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAuto.IsChecked == true)
            {
                GenerateIdAuto();
            }
        }


        private void cbAuto_Click(object sender, RoutedEventArgs e)
        {
            if (cbAuto.IsChecked == true)
            {
                GenerateIdAuto();
            }
        }
    }
}
