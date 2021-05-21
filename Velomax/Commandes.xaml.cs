using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour Commandes.xaml
    /// </summary>
    public partial class Commandes : Window
    {
        private MySqlConnection maConnexion;

        public Commandes(MySqlConnection maConnexion)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            LoadInfosCommandes();
        }


        private void LoadInfosCommandes()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT commande.*, SUM(sous_tot) AS total FROM (" +
                                        "SELECT commande.*, SUM(prix_modele * qte_contientModele) AS sous_tot FROM commande NATURAL JOIN contientModele NATURAL JOIN modele GROUP BY id_commande " +
                                        "UNION " +
                                        "SELECT commande.*, SUM(prix_piece * qte_contientPiece) AS sous_tot FROM commande NATURAL JOIN contientPiece NATURAL JOIN piece GROUP BY id_commande) AS commande " +
                                        "GROUP BY id_commande ORDER BY id_commande;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgCommandes.ItemsSource = dt.DefaultView;
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
