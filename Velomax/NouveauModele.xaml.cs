using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using Velomax.modules;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour NouveauModele.xaml
    /// </summary>
    public partial class NouveauModele : Window
    {
        private MySqlConnection maConnexion;
        private StockVelos parent;

        private SortedList<int, string> grandeurs;
        private SortedList<int, string> lignesProduits;

        public NouveauModele(MySqlConnection maConnexion, StockVelos parent)
        {
            this.maConnexion = maConnexion;
            InitializeComponent();

            this.parent = parent;
            InitGrandeurs();
            InitLignesProduits();
        }

        private void InitGrandeurs()
        {
            grandeurs = new SortedList<int, string>();
            string requete = "SELECT * FROM grandeur";
            RemplirComboBox<int, string>.RemplirGrandeurs(maConnexion, requete, cbGrandeurs, grandeurs);
        }

        private void InitLignesProduits()
        {
            lignesProduits = new SortedList<int, string>();
            string requete = "SELECT * FROM ligneProduit;";
            RemplirComboBox<int, string>.RemplirLignesProduits(maConnexion, requete, cbLignesProduits, lignesProduits);
        }

        private void bValider_Click(object sender, RoutedEventArgs e)
        {
            string idModele = tbIdModele.Text;

            if (idModele == "")
            {
                MessageBox.Show("Entrez l'id du modèle");
            }
            else
            {
                string nomModele = tbNomModele.Text;

                if (nomModele == "")
                {
                    MessageBox.Show("Entrez le nom du modèle");
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
                        saisieOK = int.TryParse(tbQuantite.Text, out int stock);

                        if (!saisieOK)
                        {
                            MessageBox.Show("Entrez une quantité valide");
                        }
                        else
                        {
                            int indexGrandeur = cbGrandeurs.SelectedIndex;

                            if (indexGrandeur == -1)
                            {
                                MessageBox.Show("Sélectionnez une grandeur");
                            }
                            else
                            {
                                int indexLigneProduit = cbLignesProduits.SelectedIndex;

                                if (indexLigneProduit == -1)
                                {
                                    MessageBox.Show("Sélectionnez une ligne produit");
                                }
                                else
                                {
                                    if (dpDateIntro.SelectedDate == null)
                                    {
                                        MessageBox.Show("Entrez la date d'introduction sur le marché");
                                    }
                                    else
                                    {
                                        DateTime dateIntro = (DateTime)dpDateIntro.SelectedDate;

                                        if (dpDateDisc.SelectedDate == null)
                                        {
                                            MessageBox.Show("Entrez la date de discontinuation de production");
                                        }
                                        else
                                        {
                                            DateTime dateDisc = (DateTime)dpDateDisc.SelectedDate;

                                            MessageBoxResult result = MessageBoxResult.No;

                                            try
                                            {
                                                maConnexion.Open();

                                                MySqlParameter id_modele = new MySqlParameter("@id_modele", MySqlDbType.Int32);
                                                id_modele.Value = idModele;

                                                MySqlParameter nom_modele = new MySqlParameter("@nom_modele", MySqlDbType.VarChar);
                                                nom_modele.Value = nomModele;

                                                MySqlParameter prix_modele = new MySqlParameter("@prix_modele", MySqlDbType.Float);
                                                prix_modele.Value = prix;

                                                MySqlParameter dateIntro_modele = new MySqlParameter("@dateIntro_modele", MySqlDbType.Date);
                                                dateIntro_modele.Value = dateIntro;

                                                MySqlParameter dateDisc_modele = new MySqlParameter("@dateDisc_modele", MySqlDbType.Date);
                                                dateDisc_modele.Value = dateDisc;

                                                MySqlParameter stock_modele = new MySqlParameter("@stock_modele", MySqlDbType.Int32);
                                                stock_modele.Value = stock;

                                                MySqlParameter id_grandeur = new MySqlParameter("@id_grandeur", MySqlDbType.Int32);
                                                id_grandeur.Value = grandeurs.Keys[indexGrandeur];

                                                MySqlParameter id_ligne = new MySqlParameter("@id_ligne", MySqlDbType.Int32);
                                                id_ligne.Value = lignesProduits.Keys[indexLigneProduit];


                                                MySqlCommand command = maConnexion.CreateCommand();
                                                command.CommandText = "INSERT INTO velomax.modele VALUES (@id_modele, @nom_modele, @prix_modele," + 
                                                    " @dateIntro_modele, @dateDisc_modele, @stock_modele, @id_grandeur, @id_ligne);";
                                                command.Parameters.Add(id_modele);
                                                command.Parameters.Add(nom_modele);
                                                command.Parameters.Add(prix_modele);
                                                command.Parameters.Add(dateIntro_modele);
                                                command.Parameters.Add(dateDisc_modele);
                                                command.Parameters.Add(stock_modele);
                                                command.Parameters.Add(id_grandeur);
                                                command.Parameters.Add(id_ligne);

                                                command.ExecuteNonQuery();
                                                result = MessageBox.Show("Modèle ajouté! \nSouhaitez-vous renseigner la liste des pièces détachées du modèle ?", "Liste des pièces", MessageBoxButton.YesNo, MessageBoxImage.Question);

                                                command.Dispose();
                                                this.Close();
                                            }
                                            catch (MySqlException erreur)
                                            {
                                                MessageBox.Show("Erreur à l'ajout de la pièce :\n" + erreur);
                                            }
                                            finally
                                            {
                                                maConnexion.Close();
                                                this.parent.LoadInfosModeles(); // actualisation de la liste
                                            }

                                            if (result == MessageBoxResult.Yes)
                                            {
                                                AjoutPiecesModele windowAjoutPieces = new AjoutPiecesModele(maConnexion, idModele);
                                                windowAjoutPieces.Show();
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
            tbIdModele.IsEnabled = false;
            tbIdModele.Text = GenererId.GenerateIdAuto(maConnexion, cbAuto).ToString();
        }

        private void cbAuto_Checked(object sender, RoutedEventArgs e)
        {
            GenerateIdAuto();
            tbIdModele.IsEnabled = false;
        }

        private void cbAuto_Unchecked(object sender, RoutedEventArgs e)
        {
            tbIdModele.IsEnabled = true;
        }
    }
}
