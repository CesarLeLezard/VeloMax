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
    /// Logique d'interaction pour CommanderPiecesModeles.xaml
    /// </summary>
    public partial class CommanderPiecesModeles : Window
    {
        private MySqlConnection maConnexion;
        private int idCommande;
        private NouvelleCommande parent;

        public CommanderPiecesModeles(MySqlConnection maConnexion, int idCommande, NouvelleCommande parent)
        {
            InitializeComponent();

            this.maConnexion = maConnexion;
            this.idCommande = idCommande;
            this.parent = parent;
            LoadInfosPieces();
            LoadInfosModeles();
        }

        private void LoadInfosPieces()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_piece, lib_categorie, prix_piece, dateIntro_piece, dateDisc_piece, stock_piece FROM piece NATURAL JOIN categorie ORDER BY id_piece;";

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

        private void LoadInfosModeles()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_modele, nom_modele, lib_grandeur, prix_modele, dateIntro_modele, dateDisc_modele, stock_modele, lib_ligne FROM modele NATURAL JOIN grandeur NATURAL JOIN ligneProduit ORDER BY id_modele;";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgVelos.ItemsSource = dt.DefaultView;
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

        private void LoadInfosModelesAjoutes()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_modele, nom_modele, prix_modele, qte_contientModele FROM modele NATURAL JOIN contientModele WHERE id_commande = " + idCommande.ToString() + ";";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgVelosCommandes.ItemsSource = dt.DefaultView;
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


        private void LoadInfosPiecesAjoutees()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT id_piece, lib_categorie, prix_piece, qte_contientPiece FROM piece NATURAL JOIN categorie NATURAL JOIN contientPiece WHERE id_commande = " + idCommande.ToString() + ";";

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                command.Dispose();
                dgPiecesCommandes.ItemsSource = dt.DefaultView;
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

        private void CalculMontantTotal()
        {
            try
            {
                maConnexion.Open();

                MySqlCommand command = maConnexion.CreateCommand();
                command.CommandText = "SELECT SUM(sous_tot) AS total FROM(" +
                            "SELECT SUM(prix_modele * qte_contientModele) AS sous_tot FROM commande NATURAL JOIN contientModele NATURAL JOIN modele WHERE id_commande = " + idCommande.ToString() + " UNION " +
                            "SELECT SUM(prix_piece * qte_contientPiece) AS sous_tot FROM commande NATURAL JOIN contientPiece NATURAL JOIN piece WHERE id_commande = " + idCommande.ToString() + ") AS commande; ";

                MySqlDataReader reader = command.ExecuteReader();


                if (reader.Read())   // parcours ligne par ligne
                {
                    double montant = reader.GetDouble(0);
                    tbMontantTotal.Text = montant.ToString("F", System.Globalization.CultureInfo.CurrentCulture);
                }

                reader.Close();
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

        private void bCommanderModele_Click(object sender, RoutedEventArgs e)
        {
            bool saisieOK = int.TryParse(tbQteModele.Text, out int quantite);

            if (!saisieOK)
            {
                MessageBox.Show("Entrez une quantité valide");
            }
            else
            {

                if (dgVelos.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Sélectionnez une modèle", "Ajouter un modèle", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    try
                    {
                        maConnexion.Open();

                        MySqlParameter id_commande = new MySqlParameter("@id_commande", MySqlDbType.Int32);
                        id_commande.Value = idCommande;

                        MySqlParameter qte_contientModele = new MySqlParameter("@qte_contientModele", MySqlDbType.Int32);
                        qte_contientModele.Value = quantite;

                        MySqlParameter id_modele = new MySqlParameter("@id_modele", MySqlDbType.Int32);

                        MySqlCommand command = maConnexion.CreateCommand();
                        command.Parameters.Add(id_commande);
                        command.Parameters.Add(id_modele);
                        command.Parameters.Add(qte_contientModele);

                        foreach (DataRowView ligne in dgVelos.SelectedItems)
                        {
                            id_modele.Value = ligne.Row.Field<int>(0); // récupération de l'id modèle à ajouter

                            command.CommandText = "INSERT INTO velomax.contientModele VALUES(@id_commande, @id_modele, @qte_contientModele);";
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
                        LoadInfosModelesAjoutes();
                        CalculMontantTotal();
                        this.parent.Parentc.LoadInfosCommandes();
                    }
                }
            }
        }

        private void bUp_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbQteModele.Text, out int stock))
            {
                MessageBox.Show("Quantité en stock invalide");
            }
            else
            {
                stock++;
                tbQteModele.Text = stock.ToString();
            }
        }

        private void bDown_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbQteModele.Text, out int stock))
            {
                MessageBox.Show("Quantité en stock invalide");
            }
            else
            {
                if (stock > 0)
                {
                    stock--;
                    tbQteModele.Text = stock.ToString();
                }
            }
        }

        private void bUp_2_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbQtePiece.Text, out int stock))
            {
                MessageBox.Show("Quantité en stock invalide");
            }
            else
            {
                stock++;
                tbQtePiece.Text = stock.ToString();
            }
        }

        private void bDown_2_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbQtePiece.Text, out int stock))
            {
                MessageBox.Show("Quantité en stock invalide");
            }
            else
            {
                if (stock > 0)
                {
                    stock--;
                    tbQtePiece.Text = stock.ToString();
                }
            }
        }

        private void bCommanderPiece_Click(object sender, RoutedEventArgs e)
        {
            bool saisieOK = int.TryParse(tbQtePiece.Text, out int quantite);

            if (!saisieOK)
            {
                MessageBox.Show("Entrez une quantité valide");
            }
            else
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

                        MySqlParameter id_commande = new MySqlParameter("@id_commande", MySqlDbType.Int32);
                        id_commande.Value = idCommande;

                        MySqlParameter qte_contientPiece = new MySqlParameter("@qte_contientPiece", MySqlDbType.Int32);
                        qte_contientPiece.Value = quantite;

                        MySqlParameter id_piece = new MySqlParameter("@id_piece", MySqlDbType.VarChar);

                        MySqlCommand command = maConnexion.CreateCommand();
                        command.Parameters.Add(id_commande);
                        command.Parameters.Add(id_piece);
                        command.Parameters.Add(qte_contientPiece);

                        foreach (DataRowView ligne in dgPieces.SelectedItems)
                        {
                            id_piece.Value = ligne.Row.Field<string>(0); // récupération de l'id pièce à ajouter

                            command.CommandText = "INSERT INTO velomax.contientPiece VALUES(@id_commande, @id_piece, @qte_contientPiece);";
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
                        LoadInfosPiecesAjoutees();
                        CalculMontantTotal();
                        this.parent.Parentc.LoadInfosCommandes();
                    }
                }
            }
        }
    }
}
