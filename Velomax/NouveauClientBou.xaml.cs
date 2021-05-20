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
    /// Logique d'interaction pour NouveauClientBou.xaml
    /// </summary>
    public partial class NouveauClientBou : Window
    {
        private MySqlConnection maConnexion;
        private Clients parent;

        private SortedList<int, string> fidelios;

        public NouveauClientBou(MySqlConnection maConnexion, Clients parent)
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
                                        int indexFidelio = cbFidelio.SelectedIndex;

                                        int idClient = GenererId.GenerateIdAuto(maConnexion, "SELECT MAX(id_clientBou) FROM clientBou");

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


                                            MySqlCommand command1 = maConnexion.CreateCommand();
                                            command1.CommandText = "INSERT INTO velomax.clientBou VALUES (@id_clientBou, @nom_clientBou, @adresse_clientBou,  @codeP_clientBou,  @ville_clientBou,  @tel_clientBou,  @mail_clientBou, @nomContact_clientBou);";
                                            command1.Parameters.Add(id_clientBou);
                                            command1.Parameters.Add(nom_clientBou);
                                            command1.Parameters.Add(nomContact_clientBou);
                                            command1.Parameters.Add(tel_clientBou);
                                            command1.Parameters.Add(adresse_clientBou);
                                            command1.Parameters.Add(codeP_clientBou);
                                            command1.Parameters.Add(ville_clientBou);
                                            command1.Parameters.Add(mail_clientBou);

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

                                            MySqlParameter date_adhereBou = new MySqlParameter("@date_adhereBou", MySqlDbType.Date);
                                            date_adhereBou.Value = DateTime.Now.Date;

                                            MySqlCommand command2 = maConnexion.CreateCommand();
                                            command2.CommandText = "INSERT INTO velomax.adhereBou VALUES (@id_clientBou, @id_fidelio, @date_adhereBou);";
                                            command2.Parameters.Add(id_clientBou);
                                            command2.Parameters.Add(id_fidelio);
                                            command2.Parameters.Add(date_adhereBou);

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
    }
}
