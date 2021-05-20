using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows;
using Velomax.modules;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour NouveauClientInd.xaml
    /// </summary>
    public partial class NouveauClientInd : Window
    {
        private MySqlConnection maConnexion;
        private Clients parent;

        private SortedList<int, string> fidelios;

        public NouveauClientInd(MySqlConnection maConnexion, Clients parent)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            this.parent = parent;
            InitFidelios();
        }

        private void InitFidelios()
        {
            fidelios = new SortedList<int, string>();
            string requete = "SELECT id_fidelio, lib_fidelio FROM fidelio";
            RemplirComboBox<int, string>.RemplirFidelios(maConnexion, requete, cbFidelio, fidelios);
        }

        private void bValider_Click(object sender, RoutedEventArgs e)
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
                                        int indexFidelio = cbFidelio.SelectedIndex;

                                        int idClient = GenererId.GenerateIdAuto(maConnexion, "SELECT MAX(id_clientInd) FROM clientInd");

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


                                            MySqlCommand command1 = maConnexion.CreateCommand();
                                            command1.CommandText = "INSERT INTO velomax.clientInd VALUES (@id_clientInd, @nom_clientInd,  @prenom_clientInd," +
                                                "  @adresse_clientInd,  @codeP_clientInd,  @ville_clientInd,  @tel_clientInd,  @mail_clientInd);";
                                            command1.Parameters.Add(id_clientInd);
                                            command1.Parameters.Add(nom_clientInd);
                                            command1.Parameters.Add(prenom_clientInd);
                                            command1.Parameters.Add(tel_clientInd);
                                            command1.Parameters.Add(adresse_clientInd);
                                            command1.Parameters.Add(codeP_clientInd);
                                            command1.Parameters.Add(ville_clientInd);
                                            command1.Parameters.Add(mail_clientInd);

                                            command1.ExecuteNonQuery();
                                            command1.Dispose();

                                            MySqlParameter id_fidelio = new MySqlParameter("@id_fidelio", MySqlDbType.Int32);

                                            if (indexFidelio == -1)
                                            {
                                                id_fidelio.Value = 5;
                                            }
                                            else
                                            {
                                                id_fidelio.Value = fidelios.Keys[indexFidelio];
                                            }

                                            MySqlParameter date_adhereInd = new MySqlParameter("@date_adhereInd", MySqlDbType.Date);
                                            date_adhereInd.Value = DateTime.Now.Date;

                                            MySqlCommand command2 = maConnexion.CreateCommand();
                                            command2.CommandText = "INSERT INTO velomax.adhereInd VALUES (@id_clientInd, @id_fidelio, @date_adhereInd);";
                                            command2.Parameters.Add(id_clientInd);
                                            command2.Parameters.Add(id_fidelio);
                                            command2.Parameters.Add(date_adhereInd);

                                            command2.ExecuteNonQuery();
                                            command2.Dispose();

                                            MessageBox.Show("Le client a bien été ajouté");
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
    }
}
