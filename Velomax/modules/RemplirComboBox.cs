using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Velomax.modules
{
    class RemplirComboBox<T1, T2>
    {
        private delegate void Recuperation(MySqlDataReader reader, SortedList<T1, T2> liste);

        
        private static void Remplir(MySqlConnection maConnexion, string requete, ComboBox maComboBox, SortedList<T1, T2> liste, Recuperation r)
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = requete;

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())   // parcours ligne par ligne
                {
                    r(reader, liste);
                }
                maComboBox.ItemsSource = liste.Values;
            }
            catch (MySqlException erreur)
            {
                System.Windows.MessageBox.Show("Erreur de requête SQL :\n" + erreur);
            }
            finally
            {
                maConnexion.Close();
            }
        }


        private static void RecuperationFournisseurs(MySqlDataReader reader, SortedList<T1, T2> fournisseurs)
        {
            T1 siretFournisseur = reader.GetFieldValue<T1>(0); // récupération 1ère colonne contenant les siret des fournisseurs
            T2 nomFournisseur = reader.GetFieldValue<T2>(1); // récupération 2ème colonne contenant les noms des fournisseurs
            fournisseurs.Add(siretFournisseur, nomFournisseur);
        }


        private static void RecuperationCategories(MySqlDataReader reader, SortedList<T1, T2> categories)
        {
            T1 idCategorie = reader.GetFieldValue<T1>(0); // récupération 1ère colonne contenant les id_categorie
            T2 libCategorie = reader.GetFieldValue<T2>(1); // récupération 2ème colonne contenant les libellés des catégories
            categories.Add(idCategorie, libCategorie);
        }


        public static void RemplirFournisseurs(MySqlConnection maConnexion, string requete, ComboBox maComboBox, SortedList<T1, T2> fournisseurs)
        {
            Remplir(maConnexion, requete, maComboBox, fournisseurs, RecuperationFournisseurs);
        }

        public static void RemplirCategories(MySqlConnection maConnexion, string requete, ComboBox maComboBox, SortedList<T1, T2> categories)
        {
            Remplir(maConnexion, requete, maComboBox, categories, RecuperationCategories);
        }

    }
}
