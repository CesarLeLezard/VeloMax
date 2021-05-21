using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Velomax.modules;

namespace Velomax
{
    /// <summary>
    /// Logique d'interaction pour AjoutPiecesModele.xaml
    /// </summary>
    public partial class AjoutPiecesModele : Window
    {
        private MySqlConnection maConnexion;
        private string idModele;
        private SortedList<int, string> categories;

        public AjoutPiecesModele(MySqlConnection maConnexion, string idModele)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            this.idModele = idModele;
            LoadPieces("SELECT id_piece, lib_categorie FROM piece NATURAL JOIN categorie ORDER BY id_piece;");
            LoadPiecesAjoutees();
            InitCategories();
        }


        private void LoadPieces(string requete)
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = requete;

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgPieces.ItemsSource = dt.DefaultView;
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


        private void LoadPiecesAjoutees()
        {
            try
            {
                maConnexion.Open();

                MySqlParameter id_modele = new MySqlParameter("@id_modele", MySqlDbType.Int32);
                id_modele.Value = idModele;

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_piece, lib_categorie FROM compose NATURAL JOIN piece NATURAL JOIN categorie WHERE id_modele = @id_modele";
                command.Parameters.Add(id_modele);

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgPiecesAjoutees.ItemsSource = dt.DefaultView;
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


        private void InitCategories()
        {
            categories = new SortedList<int, string>();
            string requete = "SELECT id_categorie, lib_categorie FROM categorie;";
            RemplirComboBox<int, string>.RemplirCategories(maConnexion, requete, cbCategories, categories);
        }


        private void FiltrageListePieces()
        {
            int indexCategorie = cbCategories.SelectedIndex;
            string idPiece = tbIdPiece.Text.ToLower();

            if (indexCategorie == -1)
            {
                if (idPiece == "")
                {
                    LoadPieces("SELECT id_piece, lib_categorie FROM piece NATURAL JOIN categorie ORDER BY id_piece;");
                }
                else
                {
                    LoadPieces("SELECT id_piece, lib_categorie FROM piece NATURAL JOIN categorie WHERE LOWER(id_piece) LIKE '" + idPiece + "%' ORDER BY id_piece;");
                }
            }
            else
            {
                string idCategorie = categories.Keys[indexCategorie].ToString();
                
                if (idPiece == "")
                {
                    LoadPieces("SELECT id_piece, lib_categorie FROM piece NATURAL JOIN categorie WHERE id_categorie = " + idCategorie + " ORDER BY id_piece;");
                }
                else
                {
                    LoadPieces("SELECT id_piece, lib_categorie FROM piece NATURAL JOIN categorie WHERE id_categorie = " + idCategorie + " AND LOWER(id_piece) LIKE '" + idPiece + "%' ORDER BY id_piece;");
                }
            }
        }


        private void cbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltrageListePieces();
        }

        private void tbIdPiece_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrageListePieces();
        }

        private void bAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (dgPieces.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez une pièce", "Ajouter une pièce", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                try
                {
                    maConnexion.Open();

                    MySqlParameter id_modele = new MySqlParameter("@id_modele", MySqlDbType.Int32);
                    id_modele.Value = idModele;

                    MySqlParameter id_piece = new MySqlParameter("@id_piece", MySqlDbType.VarChar);

                    MySqlCommand command = maConnexion.CreateCommand();
                    command.Parameters.Add(id_modele);
                    command.Parameters.Add(id_piece);

                    foreach (DataRowView ligne in dgPieces.SelectedItems)
                    {
                        id_piece.Value = ligne.Row.Field<string>(0); // récupération de l'id pièce à ajouter

                        command.CommandText = "INSERT INTO velomax.compose VALUES (@id_modele, @id_piece);";
                        command.ExecuteNonQuery();
                    }

                    command.Dispose();
                    MessageBox.Show("Ajout effectué !", "Validation de l'ajout", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (MySqlException erreur)
                {
                    MessageBox.Show("Erreur :\n" + erreur);
                }
                catch (InvalidCastException)
                {
                    MessageBox.Show("Erreur : élément vide");
                }
                finally
                {
                    maConnexion.Close();
                    LoadPiecesAjoutees();
                }
            }
        }

        private void bSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (dgPiecesAjoutees.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez une pièce", "Supprimer la pièce", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Êtes-vous sur de vouloir supprimer cette pièce ?", "Supprimer la pièce", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        maConnexion.Open();

                        MySqlParameter id_modele = new MySqlParameter("@id_modele", MySqlDbType.Int32);
                        id_modele.Value = idModele;

                        MySqlParameter id_piece = new MySqlParameter("@id_piece", MySqlDbType.VarChar);
                        MySqlCommand command = maConnexion.CreateCommand();
                        command.Parameters.Add(id_modele);
                        command.Parameters.Add(id_piece);

                        foreach (DataRowView ligne in dgPiecesAjoutees.SelectedItems)
                        {
                            id_piece.Value = ligne.Row.Field<string>(0); // récupération de l'id pièce à suppr

                            command.CommandText = "DELETE FROM compose WHERE id_modele = @id_modele AND id_piece = @id_piece;";
                            command.ExecuteNonQuery();
                        }

                        command.Dispose();
                        MessageBox.Show("Suppression effectuée !", "Validation de suppresion", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (MySqlException erreur)
                    {
                        MessageBox.Show("Erreur :\n" + erreur);
                    }
                    catch (InvalidCastException)
                    {
                        MessageBox.Show("Erreur : élément vide");
                    }
                    finally
                    {
                        maConnexion.Close();
                        LoadPiecesAjoutees(); // actualisation de la liste des modèles
                    }
                }
            }
        }
    }
}
