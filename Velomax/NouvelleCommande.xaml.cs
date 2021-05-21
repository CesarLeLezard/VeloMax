using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using Velomax.modules;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour NouvelleCommande.xaml
    /// </summary>
    public partial class NouvelleCommande : Window
    {
        private MySqlConnection maConnexion;
        private Commandes parent;

        public NouvelleCommande(MySqlConnection maConnexion, Commandes parent)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            this.parent = parent;
        }

        public Commandes Parentc
        {
            get { return parent; }
        }

        private void LoadClientsInd()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_clientInd, nom_clientInd, prenom_clientInd, tel_clientInd FROM clientInd ORDER BY nom_clientInd;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgClients.ItemsSource = dt.DefaultView;
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

        private void LoadClientsBou()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_clientBou, nom_clientBou, nomContact_clientBou, tel_clientBou FROM clientBou ORDER BY nom_clientBou;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgClients.ItemsSource = dt.DefaultView;
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

        private void bValider_Click(object sender, RoutedEventArgs e)
        {
            string adresse = tbAdresse.Text;

            if (adresse == "")
            {
                MessageBox.Show("Entrez une adresse");
            }
            else
            {
                string codePostal = tbCodePostal.Text;

                if (codePostal == "")
                {
                    MessageBox.Show("Entrez un code postal");
                }
                else
                {
                    string ville = tbVille.Text;

                    if (ville == "")
                    {
                        MessageBox.Show("Entrez une ville");
                    }
                    else
                    {
                        if (dpLivraison.SelectedDate == null)
                        {
                            MessageBox.Show("Entrez la date de livraison");
                        }
                        else
                        {
                            DateTime dateLivraison = (DateTime)dpLivraison.SelectedDate;

                            if (dgClients.SelectedItems.Count == 0)
                            {
                                MessageBox.Show("Sélectionnez un client");
                            }
                            else
                            {
                                int idClient = 0;

                                try
                                {
                                    foreach (DataRowView ligne in dgClients.SelectedItems)
                                    {
                                        idClient = ligne.Row.Field<int>(0); // récupération de l'id_client
                                    }

                                    int idCommande = GenererId.GenerateIdAuto(maConnexion, "SELECT MAX(id_commande) FROM commande;");

                                    try
                                    {
                                        maConnexion.Open();

                                        MySqlParameter id_commande = new MySqlParameter("@id_commande", MySqlDbType.Int32);
                                        id_commande.Value = idCommande;

                                        MySqlParameter date_commande = new MySqlParameter("@date_commande", MySqlDbType.DateTime);
                                        date_commande.Value = DateTime.Now;

                                        MySqlParameter adresseLivraison_commande = new MySqlParameter("@adresseLivraison_commande", MySqlDbType.VarChar);
                                        adresseLivraison_commande.Value = adresse;

                                        MySqlParameter codePLivraison_commande = new MySqlParameter("@codePLivraison_commande", MySqlDbType.VarChar);
                                        codePLivraison_commande.Value = codePostal;

                                        MySqlParameter villeLivraison_commande = new MySqlParameter("@villeLivraison_commande", MySqlDbType.VarChar);
                                        villeLivraison_commande.Value = ville;

                                        MySqlParameter dateLivraison_commande = new MySqlParameter("@dateLivraison_commande", MySqlDbType.DateTime);
                                        dateLivraison_commande.Value = dateLivraison;

                                        MySqlParameter id_client = new MySqlParameter("@id_client", MySqlDbType.Int32);
                                        id_client.Value = idClient;


                                        MySqlCommand command = maConnexion.CreateCommand();
                                        string requete = "";

                                        if ((bool)rbClientInd.IsChecked)
                                        {
                                            requete = "INSERT INTO velomax.commande VALUES (@id_commande, @date_commande, @adresseLivraison_commande, @codePLivraison_commande, @villeLivraison_commande, @dateLivraison_commande, @id_client, NULL);";
                                        }
                                        else
                                        {
                                            requete = "INSERT INTO velomax.commande VALUES (@id_commande, @date_commande, @adresseLivraison_commande, @codePLivraison_commande, @villeLivraison_commande, @dateLivraison_commande, NULL, @id_client);";
                                        }
                                        command.CommandText = requete;
                                        command.Parameters.Add(id_commande);
                                        command.Parameters.Add(date_commande);
                                        command.Parameters.Add(adresseLivraison_commande);
                                        command.Parameters.Add(codePLivraison_commande);
                                        command.Parameters.Add(villeLivraison_commande);
                                        command.Parameters.Add(dateLivraison_commande);
                                        command.Parameters.Add(id_client);

                                        command.ExecuteNonQuery();
                                        command.Dispose();
                                        this.Close();

                                    }
                                    catch (MySqlException erreur)
                                    {
                                        MessageBox.Show("Erreur à l'ajout de la commande :\n" + erreur);
                                    }
                                    finally
                                    {
                                        maConnexion.Close();
                                        this.parent.LoadInfosCommandes(); // actualisation de la liste
                                    }
                                    CommanderPiecesModeles window = new CommanderPiecesModeles(maConnexion, idCommande, this);
                                    window.Show();
                                }
                                catch (InvalidCastException)
                                {
                                    MessageBox.Show("Erreur : élément vide");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            LoadClientsInd();
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            LoadClientsBou();
        }
    }
}
