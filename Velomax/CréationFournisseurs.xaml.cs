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
using MySql.Data.MySqlClient;

namespace Velomax
{

    /// <summary>
    /// Logique d'interaction pour CréationFournisseurs.xaml
    /// </summary>
    public partial class CréationFournisseurs : Window
    {
        private MySqlConnection maConnexion;
        private Fournisseurs parent;

        public CréationFournisseurs(MySqlConnection maConnexion, Fournisseurs parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.maConnexion = maConnexion;
        }
        public long LongRandom(long min, long max)
        {
            Random randd = new Random();
            byte[] buf = new byte[8];
            randd.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
        private void bValider_Click(object sender, RoutedEventArgs e)
        {
            maConnexion.Open();
            //1 On demande les informations à l'utilisateur
            string nom_fournisseur = tbNomFournisseur.Text;
            string addresse_fournisseur = tbAdresseFournisseur.Text;
            string tel_fournisseur = tbTelFournisseur.Text;
            string mail_fournisseur = tbMailFournisseur.Text;
            string cp_fournisseur = tbCPFournisseur.Text;
            string ville_fournisseur = tbVilleFournisseur.Text;
            int réactivité_fournisseur = Convert.ToInt32(tbRéactivité.Text);
            long siret_fournisseur = 0;

            if ((bool)siretAuto.IsChecked)
            {
                siret_fournisseur = LongRandom(10000000000000, 100000000000000);
            }
            else
            {
                siret_fournisseur = Convert.ToInt64(tbSiretFournisseur.Text);
            }
            //2 On les insère
            string insertTable = "insert into velomax.fournisseur values ('" + siret_fournisseur + "','" + nom_fournisseur + "','" + tel_fournisseur + "','" + mail_fournisseur + "','" + addresse_fournisseur + "','" + cp_fournisseur + "','" + ville_fournisseur + "'," + réactivité_fournisseur + "); ";
            MySqlCommand command = maConnexion.CreateCommand();
            command.CommandText = insertTable;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MessageBox.Show("Une erreur s'est produite:", Convert.ToString(err));
            }
            command.Dispose();
            this.Close();
            maConnexion.Close();
            this.parent.LoadInfosFournisseurs();
        }
    }
}
