using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using Velomax.modules;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour RapportStats.xaml
    /// </summary>
    public partial class RapportStats : Window
    {
        private MySqlConnection maConnexion;

        private SortedList<int, string> fidelios;

        public RapportStats(MySqlConnection maConnexion)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            InitFidelio();
            Question1();
            Question2();
            Question3();
            Question5();

            Demo1();
            Demo2();
            Demo3();
        }

        private void InitFidelio()
        {
            fidelios = new SortedList<int, string>();
            string requete = "SELECT id_fidelio, lib_fidelio FROM fidelio";
            RemplirComboBox<int, string>.RemplirFidelios(maConnexion, requete, cbFidelio, fidelios);
        }

        private void Question1()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command1 = maConnexion.CreateCommand();
                command1.CommandText = "SELECT id_modele, SUM(qte_contientModele) AS qte_vendue FROM contientModele GROUP BY id_modele;";

                DataTable dt = new DataTable();
                dt.Load(command1.ExecuteReader());
                command1.Dispose();
                dgModeles1.ItemsSource = dt.DefaultView;

                MySqlCommand command2 = maConnexion.CreateCommand();
                command2.CommandText = "SELECT id_piece, SUM(qte_contientPiece) AS qte_vendue FROM contientPiece GROUP BY id_piece;";
                
                DataTable dt2 = new DataTable();
                dt2.Load(command2.ExecuteReader());
                command2.Dispose();
                dgPieces1.ItemsSource = dt2.DefaultView;
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


        private void Question2()
        {
            try
            {
                maConnexion.Open();

                //SELECT lib_fidelio, id_clientBou, nom_clientBou FROM clientBou NATURAL JOIN adhereBou NATURAL JOIN fidelio WHERE id_fidelio = @id_fidelio;
                int indexFidelio = cbFidelio.SelectedIndex;

                MySqlParameter id_fidelio = new MySqlParameter("@id_fidelio", MySqlDbType.Int32);
                id_fidelio.Value = fidelios.Keys[indexFidelio];

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_clientInd, prenom_clientInd, nom_clientInd FROM clientInd NATURAL JOIN adhereInd WHERE id_fidelio = @id_fidelio;";
                command.Parameters.Add(id_fidelio);

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgClientsInd2.ItemsSource = dt.DefaultView;

                MySqlCommand command2 = maConnexion.CreateCommand();
                command2.CommandText = "SELECT id_clientBou, nom_clientBou FROM clientBou NATURAL JOIN adhereBou WHERE id_fidelio = @id_fidelio;";
                command2.Parameters.Add(id_fidelio);

                DataTable dt2 = new DataTable();
                dt2.Load(command2.ExecuteReader());
                command2.Dispose();
                dgClientsBou2.ItemsSource = dt2.DefaultView;
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

        private void Question3()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nom_clientInd, prenom_clientInd, ADDDATE(date_adhereInd, INTERVAL duree_fidelio YEAR) AS date_expiration FROM clientInd NATURAL JOIN adhereInd NATURAL JOIN fidelio;";
                
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgClientInd3.ItemsSource = dt.DefaultView;

                MySqlCommand command1 = maConnexion.CreateCommand();
                command1.CommandText = "SELECT nom_clientBou, ADDDATE(date_adhereBou, INTERVAL duree_fidelio YEAR) AS date_expiration FROM clientBou NATURAL JOIN adhereBou NATURAL JOIN fidelio;";

                DataTable dt1 = new DataTable();
                dt1.Load(command1.ExecuteReader());
                command1.Dispose();
                dgClientBou3.ItemsSource = dt1.DefaultView;
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

        private void Question5()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_commande, AVG(qte_contientPiece) AS moy_nb_pieces FROM commande NATURAL JOIN contientPiece GROUP BY id_commande;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgPieces5.ItemsSource = dt.DefaultView;

                
                MySqlCommand command1 = maConnexion.CreateCommand();
                command1.CommandText = "SELECT id_commande, AVG(qte_contientModele) AS moy_nb_modeles FROM commande NATURAL JOIN contientModele GROUP BY id_commande;";

                DataTable dt1 = new DataTable();
                dt1.Load(command1.ExecuteReader());
                command1.Dispose();
                dgModeles5.ItemsSource = dt1.DefaultView;
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

        private void cbFidelio_DropDownClosed(object sender, EventArgs e)
        {
            Question2();
        }


        private void Demo1()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT SUM(nb_clients) AS nb_total_clients FROM ( SELECT COUNT(*) AS nb_clients FROM clientInd UNION SELECT COUNT(*) AS nb_clients FROM clientBou) AS clients; ";

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    tb1.Text = reader.GetInt32(0).ToString();
                }

                command.Dispose();
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


        private void Demo2()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_modele FROM modele WHERE stock_modele <= 2 UNION SELECT id_piece FROM piece WHERE stock_piece <= 2; ";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgDemo2.ItemsSource = dt.DefaultView;
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


        private void Demo3()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT nom_fourn, COUNT(DISTINCT id_piece) AS nb_pieces FROM fournit NATURAL JOIN fournisseur GROUP BY siret_fourn;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgDemo3.ItemsSource = dt.DefaultView;
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
    }
}
