using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using Velomax.modules;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour DetailsModele.xaml
    /// </summary>
    public partial class DetailsModele : Window
    {
        private MySqlConnection maConnexion;

        private StockVelos parent;
        private int idModele;
        private SortedList<int, string> grandeurs;
        private SortedList<int, string> lignesProduits;

        public DetailsModele(MySqlConnection maConnexion, StockVelos parent, int idModele)
        {
            this.maConnexion = maConnexion;
            InitializeComponent();

            this.parent = parent;
            this.idModele = idModele;
            InitGrandeurs();
            InitLignesProduits();
            InitInfosModele();
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


        private void InitInfosModele()
        {
            try
            {
                maConnexion.Open();

                MySqlParameter id_modele = new MySqlParameter("@id_modele", MySqlDbType.Int32);
                id_modele.Value = idModele;

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT * FROM modele WHERE id_modele = @id_modele;";
                command.Parameters.Add(id_modele);

                MySqlDataReader reader = command.ExecuteReader();


                if (reader.Read())   // parcours ligne par ligne
                {
                    int idModele = reader.GetInt32(0);
                    string nomModele = reader.GetString(1);
                    double prixModele = reader.GetFloat(2);
                    DateTime dateIntro = reader.GetDateTime(3);
                    DateTime dateDisc = reader.GetDateTime(4);
                    int stock = reader.GetInt32(5);
                    int idGrandeur = reader.GetInt32(6);
                    int idLigne = reader.GetInt32(7);

                    tbIdModele.Text = idModele.ToString();
                    tbNomModele.Text = nomModele;
                    tbPrix.Text = prixModele.ToString("F", System.Globalization.CultureInfo.CurrentCulture);
                    dpDateIntro.SelectedDate = dateIntro;
                    dpDateDisc.SelectedDate = dateDisc;
                    tbStock.Text = stock.ToString();
                    cbGrandeurs.SelectedIndex = grandeurs.IndexOfKey(idGrandeur);
                    cbLignesProduits.SelectedIndex = lignesProduits.IndexOfKey(idLigne);
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

        private void bEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            string nvIdModele = tbIdModele.Text;

            if (nvIdModele == "")
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
                        saisieOK = int.TryParse(tbStock.Text, out int stock);

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

                                            
                                            try
                                            {
                                                maConnexion.Open();

                                                MySqlParameter id_modele = new MySqlParameter("@id_modele", MySqlDbType.Int32);
                                                id_modele.Value = idModele;

                                                MySqlParameter nv_id_modele = new MySqlParameter("@nv_id_modele", MySqlDbType.Int32);
                                                nv_id_modele.Value = nvIdModele;

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
                                                id_ligne.Value = lignesProduits.Keys[indexLigneProduit]; ;


                                                MySqlCommand command = maConnexion.CreateCommand();
                                                command.CommandText = "UPDATE modele SET id_modele = @nv_id_modele, nom_modele = @nom_modele, prix_modele = @prix_modele, dateIntro_modele = @dateIntro_modele," +
                                                    " dateDisc_modele = @dateDisc_modele, stock_modele = @stock_modele, id_grandeur = @id_grandeur, id_ligne = @id_ligne WHERE id_modele = @id_modele;";
                                                command.Parameters.Add(id_modele);
                                                command.Parameters.Add(nv_id_modele);
                                                command.Parameters.Add(nom_modele);
                                                command.Parameters.Add(prix_modele);
                                                command.Parameters.Add(dateIntro_modele);
                                                command.Parameters.Add(dateDisc_modele);
                                                command.Parameters.Add(stock_modele);
                                                command.Parameters.Add(id_grandeur);
                                                command.Parameters.Add(id_ligne);

                                                command.ExecuteNonQuery();
                                                MessageBox.Show("Le modèle a bien été modifié !");

                                                command.Dispose();
                                                this.Close();
                                            }
                                            catch (MySqlException erreur)
                                            {
                                                MessageBox.Show("Erreur :\n" + erreur);
                                            }
                                            finally
                                            {
                                                maConnexion.Close();
                                                this.parent.LoadInfosModeles(); // actualisation de la liste
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

        private void bModifierPieces_Click(object sender, RoutedEventArgs e)
        {
            AjoutPiecesModele windowListePieces = new AjoutPiecesModele(maConnexion, idModele.ToString());
            windowListePieces.Show();
        }
    }
}
