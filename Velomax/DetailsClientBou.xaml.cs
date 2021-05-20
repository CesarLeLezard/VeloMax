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
    /// Logique d'interaction pour DetailsClientBou.xaml
    /// </summary>
    public partial class DetailsClientBou : Window
    {
        private MySqlConnection maConnexion;
        private Clients parent;

        private int idClient;

        public DetailsClientBou(MySqlConnection maConnexion, Clients parent, int idClient)
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

                MySqlParameter id_clientBou = new MySqlParameter("@id_clientBou", MySqlDbType.Int32);
                id_clientBou.Value = idClient;

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT clientBou.*, lib_fidelio, date_adhereBou, ADDDATE(date_adhereBou, INTERVAL duree_fidelio YEAR) FROM clientBou NATURAL JOIN adhereBou NATURAL JOIN fidelio WHERE id_clientBou = @id_clientBou;";
                command.Parameters.Add(id_clientBou);

                MySqlDataReader reader = command.ExecuteReader();


                if (reader.Read())   // parcours ligne par ligne
                {
                    int idClient = reader.GetInt32(0);
                    string nomClient = reader.GetString(1);
                    string adresse = reader.GetString(2);
                    string codeP = reader.GetString(3);
                    string ville = reader.GetString(4);
                    string tel = reader.GetString(5);
                    string mail = reader.GetString(6);
                    string nomContact = reader.GetString(7);
                    string fidelio = reader.GetString(8);
                    DateTime dateAdhesion = reader.GetDateTime(9);
                    DateTime dateExpiration = reader.GetDateTime(10);

                    tbIdClient.Text = idClient.ToString();
                    tbNom.Text = nomClient;
                    tbAdresse.Text = adresse;
                    tbCodePostal.Text = codeP;
                    tbVille.Text = ville;
                    tbTel.Text = tel;
                    tbMail.Text = mail;
                    tbNomContact.Text = nomContact;
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

                MySqlParameter id_clientBou = new MySqlParameter("@id_clientBou", MySqlDbType.Int32);
                id_clientBou.Value = idClient;

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT lib_fidelio, date_adhereBou, ADDDATE(date_adhereBou, INTERVAL duree_fidelio YEAR) FROM adhereBou NATURAL JOIN fidelio WHERE id_clientBou = @id_clientBou;";
                command.Parameters.Add(id_clientBou);
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
                string nomContact = tbNomContact.Text;

                if (nomContact == "")
                {
                    MessageBox.Show("Entrez le nom du contact");
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

                                            MySqlParameter id_clientBou = new MySqlParameter("@id_clientBou", MySqlDbType.Int32);
                                            id_clientBou.Value = idClient;

                                            MySqlParameter nom_clientBou = new MySqlParameter("@nom_clientBou", MySqlDbType.VarChar);
                                            nom_clientBou.Value = nom;

                                            MySqlParameter nomContact_clientBou = new MySqlParameter("@nomContact_clientBou", MySqlDbType.VarChar);
                                            nomContact_clientBou.Value = nomContact;

                                            MySqlParameter tel_clientBou = new MySqlParameter("@tel_clientBou", MySqlDbType.VarChar);
                                            tel_clientBou.Value = tel;

                                            MySqlParameter adresse_clientBou = new MySqlParameter("@adresse_clientBou", MySqlDbType.VarChar);
                                            adresse_clientBou.Value = adresse;

                                            MySqlParameter codeP_clientBou = new MySqlParameter("@codeP_clientBou", MySqlDbType.VarChar);
                                            codeP_clientBou.Value = codePostal;

                                            MySqlParameter ville_clientBou = new MySqlParameter("@ville_clientBou", MySqlDbType.VarChar);
                                            ville_clientBou.Value = ville;

                                            MySqlParameter mail_clientBou = new MySqlParameter("@mail_clientBou", MySqlDbType.VarChar);
                                            mail_clientBou.Value = mail;


                                            MySqlCommand command = maConnexion.CreateCommand();
                                            command.CommandText = "UPDATE clientBou SET nom_clientBou = @nom_clientBou, nomContact_clientBou = @nomContact_clientBou, tel_clientBou = @tel_clientBou, adresse_clientBou = @adresse_clientBou," +
                                                " codeP_clientBou = @codeP_clientBou, ville_clientBou = @ville_clientBou, mail_clientBou = @mail_clientBou WHERE id_clientBou = @id_clientBou;";
                                            command.Parameters.Add(id_clientBou);
                                            command.Parameters.Add(nom_clientBou);
                                            command.Parameters.Add(nomContact_clientBou);
                                            command.Parameters.Add(tel_clientBou);
                                            command.Parameters.Add(adresse_clientBou);
                                            command.Parameters.Add(codeP_clientBou);
                                            command.Parameters.Add(ville_clientBou);
                                            command.Parameters.Add(mail_clientBou);

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
                                            this.parent.LoadInfosClientsBou(); // actualisation de la liste
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
            ModifierFidelio window = new ModifierFidelio(maConnexion, null, this, idClient);
            window.Show();
        }
    }
}
