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
    /// Logique d'interaction pour DetailsClientInd.xaml
    /// </summary>
    public partial class DetailsClientInd : Window
    {
        private MySqlConnection maConnexion;
        private Clients parent;

        private int idClient;

        public DetailsClientInd(MySqlConnection maConnexion, Clients parent, int idClient)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            this.parent = parent;
            this.idClient = idClient;
            InitInfosClient();
        }


        private void InitInfosClient()
        {
            try
            {
                maConnexion.Open();

                MySqlParameter id_clientInd = new MySqlParameter("@id_clientInd", MySqlDbType.Int32);
                id_clientInd.Value = idClient;

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT clientInd.*, lib_fidelio, date_adhereInd, ADDDATE(date_adhereInd, INTERVAL duree_fidelio YEAR) FROM clientInd NATURAL JOIN adhereInd NATURAL JOIN fidelio WHERE id_clientInd = @id_clientInd;";
                command.Parameters.Add(id_clientInd);

                MySqlDataReader reader = command.ExecuteReader();


                if (reader.Read())   // parcours ligne par ligne
                {
                    int idClient = reader.GetInt32(0);
                    string nomClient = reader.GetString(1);
                    string prenomClient = reader.GetString(2);
                    string adresse = reader.GetString(3);
                    string codeP = reader.GetString(4);
                    string ville = reader.GetString(5);
                    string tel = reader.GetString(6);
                    string mail = reader.GetString(7);
                    string fidelio = reader.GetString(8);
                    DateTime dateAdhesion = reader.GetDateTime(9);
                    DateTime dateExpiration = reader.GetDateTime(10);

                    tbIdClient.Text = idClient.ToString();
                    tbNom.Text = nomClient;
                    tbPrenom.Text = prenomClient;
                    tbAdresse.Text = adresse;
                    tbCodePostal.Text = codeP;
                    tbVille.Text = ville;
                    tbTel.Text = tel;
                    tbMail.Text = mail;
                    tbFidelio.Text = fidelio;
                    tbDateAdhesion.Text = dateAdhesion.ToString("dd/MM/yyyy");
                    tbDateExpiration.Text = dateExpiration.ToString("dd/MM/yyyy");
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

        public void RefreshFidelio()
        {
            try
            {
                maConnexion.Open();

                MySqlParameter id_clientInd = new MySqlParameter("@id_clientInd", MySqlDbType.Int32);
                id_clientInd.Value = idClient;

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT lib_fidelio, date_adhereInd, ADDDATE(date_adhereInd, INTERVAL duree_fidelio YEAR) FROM adhereInd NATURAL JOIN fidelio WHERE id_clientInd = @id_clientInd;";
                command.Parameters.Add(id_clientInd);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())   // parcours ligne par ligne
                {
                    string fidelio = reader.GetString(0);
                    DateTime dateAdhesion = reader.GetDateTime(1);
                    DateTime dateExpiration = reader.GetDateTime(2);

                    tbFidelio.Text = fidelio;
                    tbDateAdhesion.Text = dateAdhesion.ToString("dd/MM/yyyy");
                    tbDateExpiration.Text = dateExpiration.ToString("dd/MM/yyyy");
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
            string nom = tbNom.Text;

            if (nom == "")
            {
                MessageBox.Show("Entrez un nom");
            }
            else
            {
                string prenom = tbPrenom.Text;

                if (prenom == "")
                {
                    MessageBox.Show("Entrez un prénom");
                }
                else
                {
                    string tel = tbTel.Text;

                    if (tel == "")
                    {
                        MessageBox.Show("Entrez un numéro de téléphone");
                    }
                    else
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
                                    string mail = tbMail.Text;

                                    if (mail == "")
                                    {
                                        MessageBox.Show("Entrez un mail");
                                    }
                                    else
                                    {
                                        
                                        try
                                        {
                                            maConnexion.Open();

                                            MySqlParameter id_clientInd = new MySqlParameter("@id_clientInd", MySqlDbType.Int32);
                                            id_clientInd.Value = idClient;

                                            MySqlParameter nom_clientInd = new MySqlParameter("@nom_clientInd", MySqlDbType.VarChar);
                                            nom_clientInd.Value = nom;

                                            MySqlParameter prenom_clientInd = new MySqlParameter("@prenom_clientInd", MySqlDbType.VarChar);
                                            prenom_clientInd.Value = prenom;

                                            MySqlParameter tel_clientInd = new MySqlParameter("@tel_clientInd", MySqlDbType.VarChar);
                                            tel_clientInd.Value = tel;

                                            MySqlParameter adresse_clientInd = new MySqlParameter("@adresse_clientInd", MySqlDbType.VarChar);
                                            adresse_clientInd.Value = adresse;

                                            MySqlParameter codeP_clientInd = new MySqlParameter("@codeP_clientInd", MySqlDbType.VarChar);
                                            codeP_clientInd.Value = codePostal;

                                            MySqlParameter ville_clientInd = new MySqlParameter("@ville_clientInd", MySqlDbType.VarChar);
                                            ville_clientInd.Value = ville;

                                            MySqlParameter mail_clientInd = new MySqlParameter("@mail_clientInd", MySqlDbType.VarChar);
                                            mail_clientInd.Value = mail;


                                            MySqlCommand command = maConnexion.CreateCommand();
                                            command.CommandText = "UPDATE clientInd SET nom_clientInd = @nom_clientInd, prenom_clientInd = @prenom_clientInd, tel_clientInd = @tel_clientInd, adresse_clientInd = @adresse_clientInd," +
                                                " codeP_clientInd = @codeP_clientInd, ville_clientInd = @ville_clientInd, mail_clientInd = @mail_clientInd WHERE id_clientInd = @id_clientInd;";
                                            command.Parameters.Add(id_clientInd);
                                            command.Parameters.Add(nom_clientInd);
                                            command.Parameters.Add(prenom_clientInd);
                                            command.Parameters.Add(tel_clientInd);
                                            command.Parameters.Add(adresse_clientInd);
                                            command.Parameters.Add(codeP_clientInd);
                                            command.Parameters.Add(ville_clientInd);
                                            command.Parameters.Add(mail_clientInd);

                                            command.ExecuteNonQuery();
                                            command.Dispose();

                                            MessageBox.Show("Le client a bien été modifié");
                                            
                                            this.Close();

                                        }
                                        catch (MySqlException erreur)
                                        {
                                            MessageBox.Show("Erreur :\n" + erreur);
                                        }
                                        finally
                                        {
                                            maConnexion.Close();
                                            this.parent.LoadInfosClientsInd(); // actualisation de la liste
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void bModifierFidelio_Click(object sender, RoutedEventArgs e)
        {
            ModifierFidelio window = new ModifierFidelio(maConnexion, this, null, idClient);
            window.Show();
        }
    }
}
