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
    /// Logique d'interaction pour ModifierFidelio.xaml
    /// </summary>
    public partial class ModifierFidelio : Window
    {
        private MySqlConnection maConnexion;

        private DetailsClientInd parentInd;
        private DetailsClientBou parentBou;
        private int idClient;
        private SortedList<int, string> fidelios;

        public ModifierFidelio(MySqlConnection maConnexion, DetailsClientInd parentInd, DetailsClientBou parentBou, int idClient)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            this.parentInd = parentInd;
            this.parentBou = parentBou;
            this.idClient = idClient;
            InitFidelio();
        }

        private void InitFidelio()
        {
            fidelios = new SortedList<int, string>();
            string requete = "SELECT id_fidelio, lib_fidelio FROM fidelio";
            RemplirComboBox<int, string>.RemplirFidelios(maConnexion, requete, cbFidelio, fidelios);
        }

        private void bOk(object sender, RoutedEventArgs e)
        {
            int indexFidelio = cbFidelio.SelectedIndex;

            if (indexFidelio != -1)
            {
                try
                {
                    maConnexion.Open();
                    
                    MySqlParameter id_fidelio = new MySqlParameter("@id_fidelio", MySqlDbType.Int32);
                    id_fidelio.Value = fidelios.Keys[indexFidelio];

                    if (parentInd != null)
                    {
                        MySqlParameter id_clientInd = new MySqlParameter("@id_clientInd", MySqlDbType.Int32);
                        id_clientInd.Value = idClient;

                        MySqlParameter date_adhereInd = new MySqlParameter("@date_adhereInd", MySqlDbType.Date);
                        date_adhereInd.Value = DateTime.Now.Date;

                        MySqlCommand command = maConnexion.CreateCommand();
                        command.CommandText = "UPDATE velomax.adhereInd SET id_fidelio = @id_fidelio, date_adhereInd = @date_adhereInd WHERE id_clientInd = @id_clientInd;";
                        command.Parameters.Add(id_fidelio);
                        command.Parameters.Add(id_clientInd);
                        command.Parameters.Add(date_adhereInd);

                        command.ExecuteNonQuery();
                        command.Dispose();
                    }
                    else
                    {
                        MySqlParameter id_clientBou = new MySqlParameter("@id_clientBou", MySqlDbType.Int32);
                        id_clientBou.Value = idClient;

                        MySqlParameter date_adhereBou = new MySqlParameter("@date_adhereBou", MySqlDbType.Date);
                        date_adhereBou.Value = DateTime.Now.Date;

                        MySqlCommand command = maConnexion.CreateCommand();
                        command.CommandText = "UPDATE velomax.adhereBou SET id_fidelio = @id_fidelio, date_adhereBou = @date_adhereBou WHERE id_clientBou = @id_clientBou;";
                        command.Parameters.Add(id_fidelio);
                        command.Parameters.Add(id_clientBou);
                        command.Parameters.Add(date_adhereBou);

                        command.ExecuteNonQuery();
                        command.Dispose();
                    }

                    
                    this.Close();
                }
                catch(MySqlException erreur)
                {
                    MessageBox.Show("Erreur SQL :\n" + erreur);
                }
                finally
                {
                    maConnexion.Close();

                    if (parentBou == null)
                    {
                        this.parentInd.RefreshFidelio();
                    }
                    else
                    {
                        this.parentBou.RefreshFidelio();
                    }
                }
                
            }
        }
    }
}
