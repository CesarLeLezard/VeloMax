-- Probleme BDD VeloMax
-- GROSSE Alexandre, GUENARD Antoine
-- Groupe : S

-- création de la base de données
DROP DATABASE IF EXISTS velomax;
CREATE DATABASE IF NOT EXISTS velomax;
USE velomax;


-- création des tables
DROP TABLE IF EXISTS grandeur;
CREATE TABLE IF NOT EXISTS grandeur (
    id_grandeur INTEGER PRIMARY KEY,
    lib_grandeur VARCHAR(40)
);


DROP TABLE IF EXISTS ligneProduit;
CREATE TABLE IF NOT EXISTS ligneProduit (
    id_ligne INTEGER PRIMARY KEY,
    lib_ligne VARCHAR(40)
);


DROP TABLE IF EXISTS categorie;
CREATE TABLE IF NOT EXISTS categorie (
    id_categorie INTEGER PRIMARY KEY,
    lib_categorie VARCHAR(40)
);


DROP TABLE IF EXISTS piece;
CREATE TABLE IF NOT EXISTS piece (
    id_piece VARCHAR(8) PRIMARY KEY,
    dateIntro_piece DATE,
    dateDisc_piece DATE,
    stock_piece INTEGER,
    id_categorie INTEGER, FOREIGN KEY (id_categorie) REFERENCES categorie (id_categorie)
);


DROP TABLE IF EXISTS modele;
CREATE TABLE IF NOT EXISTS modele (
    id_modele INTEGER PRIMARY KEY,
    nom_modele VARCHAR(40),
    prix_modele FLOAT,
    dateIntro_modele DATE,
    dateDisc_modele DATE,
    stock_modele INTEGER,
    id_grandeur INTEGER, FOREIGN KEY (id_grandeur) REFERENCES grandeur (id_grandeur),
    id_ligne INTEGER, FOREIGN KEY (id_ligne) REFERENCES ligneProduit (id_ligne)
);


DROP TABLE IF EXISTS fournisseur;
CREATE TABLE IF NOT EXISTS fournisseur (
	siret_fourn VARCHAR(14) PRIMARY KEY,
    nom_fourn VARCHAR(40),
    tel_fourn VARCHAR(10),
    mail_fourn VARCHAR(40),
    adresse_fourn VARCHAR(60),
    codeP_fourn VARCHAR(5),
    ville_fourn VARCHAR(40),
    react_fourn ENUM ('1', '2', '3', '4')
);


DROP TABLE IF EXISTS fidelio;
CREATE TABLE IF NOT EXISTS fidelio (
	id_fidelio INTEGER PRIMARY KEY,
    lib_fidelio VARCHAR(40),
    cout_fidelio FLOAT,
    duree_fidelio INTEGER,
    rabais_fidelio FLOAT
);


DROP TABLE IF EXISTS clientInd;
CREATE TABLE IF NOT EXISTS clientInd (
    id_clientInd INTEGER PRIMARY KEY,
    nom_clientInd VARCHAR(40),
    prenom_clientInd VARCHAR(40),
    adresse_clientInd VARCHAR(60),
    codeP_clientInd VARCHAR(5),
    ville_clientInd VARCHAR(40),
    tel_clientInd VARCHAR(10),
    mail_clientInd VARCHAR(40),
    id_fidelio INTEGER, FOREIGN KEY (id_fidelio) REFERENCES fidelio (id_fidelio)
);


DROP TABLE IF EXISTS clientBou;
CREATE TABLE IF NOT EXISTS clientBou (
    id_clientBou INTEGER PRIMARY KEY,
    nom_clientBou VARCHAR(40),
    adresse_clientBou VARCHAR(60),
    codeP_clientBou VARCHAR(5),
    ville_clientBou VARCHAR(40),
    tel_clientBou VARCHAR(10),
    mail_clientBou VARCHAR(40),
    nomContact_clientBou VARCHAR(40), 
    id_fidelio INTEGER, FOREIGN KEY (id_fidelio) REFERENCES fidelio (id_fidelio)
);


DROP TABLE IF EXISTS commande;
CREATE TABLE IF NOT EXISTS commande (
	id_commande INTEGER PRIMARY KEY,
    date_commande DATE,
    adresseLivraison_commande VARCHAR(60),
    codePLivraison_commande VARCHAR(5),
    villeLivraison_commande VARCHAR(40),
    dateLivraison_commande DATETIME,
    id_clientind INTEGER, FOREIGN KEY (id_clientind) REFERENCES clientInd (id_clientind),
    id_clientbou INTEGER, FOREIGN KEY (id_clientbou) REFERENCES clientBou (id_clientbou)
);


DROP TABLE IF EXISTS fournit;
CREATE TABLE IF NOT EXISTS fournit (
    id_piece VARCHAR(8), FOREIGN KEY (id_piece) REFERENCES piece (id_piece),
    siret_fourn VARCHAR(14), FOREIGN KEY (siret_fourn) REFERENCES fournisseur (siret_fourn),
    prix_fournit FLOAT,
    delai_fournit INTEGER,
    numCatalogue_fournit VARCHAR(8),
    date_fournit DATE,
    qte_fournit INTEGER
);


DROP TABLE IF EXISTS compose;
CREATE TABLE IF NOT EXISTS compose (
    id_modele INTEGER, FOREIGN KEY (id_modele) REFERENCES modele (id_modele),
    id_piece VARCHAR(8), FOREIGN KEY (id_piece) REFERENCES piece (id_piece)
);


DROP TABLE IF EXISTS contientPiece;
CREATE TABLE IF NOT EXISTS contientPiece (
	id_commande INTEGER, FOREIGN KEY (id_commande) REFERENCES commande (id_commande),
    id_piece VARCHAR(8), FOREIGN KEY (id_piece) REFERENCES piece (id_piece),
    qte_contientPiece INTEGER
);


DROP TABLE IF EXISTS contientModele;
CREATE TABLE IF NOT EXISTS contientModele (
    id_commande INTEGER, FOREIGN KEY (id_commande) REFERENCES commande (id_commande),
    id_modele INTEGER, FOREIGN KEY (id_modele) REFERENCES modele (id_modele),
    qte_contientModele INTEGER
);


-- remplissage des programmes de fidélité dans la table fidelio
INSERT INTO velomax.fidelio VALUES (1, 'Fidélio', 15, 1, 0.05);
INSERT INTO velomax.fidelio VALUES (2, 'Fidélio Or', 25, 2, 0.08);
INSERT INTO velomax.fidelio VALUES (3, 'Fidélio Platine', 60, 2, 0.10);
INSERT INTO velomax.fidelio VALUES (4, 'Fidélio Max', 100, 3, 0.12);


-- remplissage des lignes produit
INSERT INTO velomax.ligneProduit VALUES (1, 'Classique');
INSERT INTO velomax.ligneProduit VALUES (2, 'Vélo de course');
INSERT INTO velomax.ligneProduit VALUES (3, 'VTT');
INSERT INTO velomax.ligneProduit VALUES (4, 'BMX');


-- remplissage des grandeurs de modèles de vélo
INSERT INTO velomax.grandeur VALUES (1, 'Adultes');
INSERT INTO velomax.grandeur VALUES (2, 'Jeunes');
INSERT INTO velomax.grandeur VALUES (3, 'Hommes');
INSERT INTO velomax.grandeur VALUES (4, 'Dames');
INSERT INTO velomax.grandeur VALUES (5, 'Filles');
INSERT INTO velomax.grandeur VALUES (6, 'Garçons');


-- remplissage des descpritions des pièces
INSERT INTO velomax.categorie VALUES (1, 'Cadre');
INSERT INTO velomax.categorie VALUES (2, 'Guidon');
INSERT INTO velomax.categorie VALUES (3, 'Freins');
INSERT INTO velomax.categorie VALUES (4, 'Selle');
INSERT INTO velomax.categorie VALUES (5, 'Dérailleur avant');
INSERT INTO velomax.categorie VALUES (6, 'Dérailleur arrière');
INSERT INTO velomax.categorie VALUES (7, 'Roue avant');
INSERT INTO velomax.categorie VALUES (8, 'Roue arrière');
INSERT INTO velomax.categorie VALUES (9, 'Réflecteurs');
INSERT INTO velomax.categorie VALUES (10, 'Pédalier');
INSERT INTO velomax.categorie VALUES (11, 'Ordinateur');
INSERT INTO velomax.categorie VALUES (12, 'Panier');


-- remplissage des modèles de vélo
INSERT INTO velomax.modele VALUES (101, 'Kilimandjaro', 569, '2017-08-20', '2028-11-02', 30, 1, 3);
INSERT INTO velomax.modele VALUES (102, 'NorthPole', 329, '2018-11-23', '2026-10-09', 10, 1, 3);
INSERT INTO velomax.modele VALUES (103, 'MontBlanc', 399, '2019-12-11', '2023-02-08', 31, 2, 3);
INSERT INTO velomax.modele VALUES (104, 'Hooligan', 199, '2014-11-12', '2024-10-10', 4, 2, 3);
INSERT INTO velomax.modele VALUES (105, 'Orléans', 229, '2020-11-06', '2022-06-10', 6, 3, 2);
INSERT INTO velomax.modele VALUES (106, 'Orléans', 229, '2016-10-23', '2028-11-10', 18, 4, 2);
INSERT INTO velomax.modele VALUES (107, 'BlueJay', 349, '2014-02-02', '2021-12-13', 13, 3, 2);
INSERT INTO velomax.modele VALUES (108, 'BlueJay', 349, '2014-06-01', '2027-10-26', 0, 4, 2);
INSERT INTO velomax.modele VALUES (109, 'Trail Explorer', 129, '2016-06-18', '2028-11-12', 2, 5, 1);
INSERT INTO velomax.modele VALUES (110, 'Trail Explorer', 129, '2018-04-26', '2022-06-25', 16, 6, 1);
INSERT INTO velomax.modele VALUES (111, 'Night Hawk', 189, '2018-09-21', '2026-07-31', 24, 2, 1);
INSERT INTO velomax.modele VALUES (112, 'Tierra Verde', 199, '2015-05-28', '2021-07-24', 1, 3, 1);
INSERT INTO velomax.modele VALUES (113, 'Tierra Verde', 199, '2018-02-19', '2027-11-28', 0, 4, 1);
INSERT INTO velomax.modele VALUES (114, 'Mud Zinger I', 279, '2014-03-03', '2021-10-23', 5, 2, 4);
INSERT INTO velomax.modele VALUES (115, 'Mud Zinger II', 359, '2014-04-21', '2028-07-27', 8, 1, 4);


-- remplissage pièces de vélo
INSERT INTO velomax.piece VALUES ('C01', '2014-05-04', '2026-11-22', 10, 1);
INSERT INTO velomax.piece VALUES ('C02', '2013-02-24', '2027-01-09', 1, 1);
INSERT INTO velomax.piece VALUES ('C15', '2018-06-23', '2022-04-07', 5, 1);
INSERT INTO velomax.piece VALUES ('C25', '2016-11-27', '2023-03-17', 9, 1);
INSERT INTO velomax.piece VALUES ('C26', '2018-08-16', '2025-10-30', 0, 1);
INSERT INTO velomax.piece VALUES ('C32', '2014-06-20', '2024-10-18', 11, 1);
INSERT INTO velomax.piece VALUES ('C34', '2019-10-31', '2022-08-24', 1, 1);
INSERT INTO velomax.piece VALUES ('C43', '2013-01-15', '2025-01-16', 5, 1);
INSERT INTO velomax.piece VALUES ('C43f', '2017-08-25', '2028-11-22', 1, 1);
INSERT INTO velomax.piece VALUES ('C44f', '2014-09-03', '2028-02-17', 2, 1);
INSERT INTO velomax.piece VALUES ('C76', '2018-06-17', '2022-02-18', 15, 1);
INSERT INTO velomax.piece VALUES ('C87', '2017-06-20', '2022-01-26', 0, 1);
INSERT INTO velomax.piece VALUES ('C87f', '2020-02-06', '2027-03-30', 14, 1);

INSERT INTO velomax.piece VALUES ('G7', '2020-01-11', '2026-10-24', 20, 2);
INSERT INTO velomax.piece VALUES ('G9', '2015-02-27', '2028-11-29', 14, 2);
INSERT INTO velomax.piece VALUES ('G12', '2014-11-27', '2026-01-16', 3, 2);

INSERT INTO velomax.piece VALUES ('F3', '2017-10-29', '2023-03-22', 17, 3);
INSERT INTO velomax.piece VALUES ('F9', '2019-06-25', '2022-06-26', 7, 3);

INSERT INTO velomax.piece VALUES ('S88', '2017-07-17', '2028-07-08', 19, 4);
INSERT INTO velomax.piece VALUES ('S37', '2014-04-24', '2028-01-03', 20, 4);
INSERT INTO velomax.piece VALUES ('S35', '2019-11-07', '2021-10-21', 2, 4);
INSERT INTO velomax.piece VALUES ('S02', '2016-04-13', '2027-04-15', 9, 4);
INSERT INTO velomax.piece VALUES ('S03', '2016-11-08', '2026-05-20', 1, 4);
INSERT INTO velomax.piece VALUES ('S36', '2020-11-02', '2023-12-01', 15, 4);
INSERT INTO velomax.piece VALUES ('S34', '2013-05-24', '2023-02-10', 0, 4);
INSERT INTO velomax.piece VALUES ('S87', '2020-09-27', '2026-10-01', 17, 4);

INSERT INTO velomax.piece VALUES ('DV133', '2020-03-22', '2023-10-21', 4, 5);
INSERT INTO velomax.piece VALUES ('DV17', '2017-12-18', '2023-05-21', 8, 5);
INSERT INTO velomax.piece VALUES ('DV87', '2020-03-12', '2021-09-16', 23, 5);
INSERT INTO velomax.piece VALUES ('DV57', '2019-03-02', '2024-09-20', 18, 5);
INSERT INTO velomax.piece VALUES ('DV15', '2016-08-12', '2023-06-04', 16, 5);
INSERT INTO velomax.piece VALUES ('DV41', '2019-08-13', '2023-09-23', 1, 5);
INSERT INTO velomax.piece VALUES ('DV132', '2020-12-28', '2023-05-18', 22, 5);

INSERT INTO velomax.piece VALUES ('DR56', '2013-02-05', '2026-12-02', 4, 6);
INSERT INTO velomax.piece VALUES ('DR87', '2019-10-01', '2024-02-11', 11, 6);
INSERT INTO velomax.piece VALUES ('DR86', '2020-04-24', '2026-10-12', 14, 6);
INSERT INTO velomax.piece VALUES ('DR23', '2020-03-17', '2028-08-07', 17, 6);
INSERT INTO velomax.piece VALUES ('DR76', '2018-09-19', '2024-03-13', 23, 6);
INSERT INTO velomax.piece VALUES ('DR52', '2018-04-02', '2026-08-26', 14, 6);

INSERT INTO velomax.piece VALUES ('R45', '2019-11-18', '2024-02-04', 3, 7);
INSERT INTO velomax.piece VALUES ('R48', '2019-10-13', '2028-11-30', 0, 7);
INSERT INTO velomax.piece VALUES ('R12', '2019-01-27', '2024-11-14', 6, 7);
INSERT INTO velomax.piece VALUES ('R19', '2013-02-01', '2025-02-17', 22, 7);
INSERT INTO velomax.piece VALUES ('R1', '2021-01-31', '2023-12-06', 18, 7);
INSERT INTO velomax.piece VALUES ('R11', '2013-05-31', '2025-12-04', 11, 7);
INSERT INTO velomax.piece VALUES ('R44', '2015-10-19', '2023-07-05', 22, 7);

INSERT INTO velomax.piece VALUES ('R46', '2019-11-15', '2023-04-29', 20, 8);
INSERT INTO velomax.piece VALUES ('R47', '2020-03-19', '2023-04-29', 19, 8);
INSERT INTO velomax.piece VALUES ('R32', '2017-02-09', '2028-04-25', 1, 8);
INSERT INTO velomax.piece VALUES ('R18', '2020-02-07', '2026-09-16', 3, 8);
INSERT INTO velomax.piece VALUES ('R2', '2013-09-13', '2028-02-27', 15, 8);

INSERT INTO velomax.piece VALUES ('R02', '2014-05-29', '2024-04-30', 23, 9);
INSERT INTO velomax.piece VALUES ('R09', '2015-10-06', '2024-12-30', 21, 9);
INSERT INTO velomax.piece VALUES ('R10', '2017-09-13', '2023-06-13', 25, 9);

INSERT INTO velomax.piece VALUES ('P12', '2016-05-24', '2027-02-21', 15, 10);
INSERT INTO velomax.piece VALUES ('P34', '2016-09-30', '2026-05-21', 5, 10);
INSERT INTO velomax.piece VALUES ('P1', '2014-07-11', '2024-02-02', 3, 10);
INSERT INTO velomax.piece VALUES ('P15', '2017-10-13', '2026-01-26', 12, 10);

INSERT INTO velomax.piece VALUES ('O2', '2019-12-15', '2024-07-30', 1, 11);
INSERT INTO velomax.piece VALUES ('O4', '2014-01-13', '2027-10-22', 15, 11);

INSERT INTO velomax.piece VALUES ('S01', '2016-05-24', '2021-07-26', 12, 12);
INSERT INTO velomax.piece VALUES ('S05', '2019-05-06', '2023-10-31', 6, 12);
INSERT INTO velomax.piece VALUES ('S73', '2016-01-23', '2026-05-27', 19, 12);
INSERT INTO velomax.piece VALUES ('S74', '2017-07-3', '2027-02-21', 2, 12);


-- remplissage compose (liaison entre un modèle de vélo et ses pièces)
INSERT INTO velomax.compose VALUES (101, 'C32');
INSERT INTO velomax.compose VALUES (101, 'G7');
INSERT INTO velomax.compose VALUES (101, 'F3');
INSERT INTO velomax.compose VALUES (101, 'S88');
INSERT INTO velomax.compose VALUES (101, 'DV133');
INSERT INTO velomax.compose VALUES (101, 'DR56');
INSERT INTO velomax.compose VALUES (101, 'R45');
INSERT INTO velomax.compose VALUES (101, 'R46');
INSERT INTO velomax.compose VALUES (101, 'P12');
INSERT INTO velomax.compose VALUES (101, 'O2');

INSERT INTO velomax.compose VALUES (102, 'C34');
INSERT INTO velomax.compose VALUES (102, 'G7');
INSERT INTO velomax.compose VALUES (102, 'F3');
INSERT INTO velomax.compose VALUES (102, 'S88');
INSERT INTO velomax.compose VALUES (102, 'DV17');
INSERT INTO velomax.compose VALUES (102, 'DR87');
INSERT INTO velomax.compose VALUES (102, 'R48');
INSERT INTO velomax.compose VALUES (102, 'R47');
INSERT INTO velomax.compose VALUES (102, 'P12');

INSERT INTO velomax.compose VALUES (103, 'C76');
INSERT INTO velomax.compose VALUES (103, 'G7');
INSERT INTO velomax.compose VALUES (103, 'F3');
INSERT INTO velomax.compose VALUES (103, 'S88');
INSERT INTO velomax.compose VALUES (103, 'DV17');
INSERT INTO velomax.compose VALUES (103, 'DR87');
INSERT INTO velomax.compose VALUES (103, 'R48');
INSERT INTO velomax.compose VALUES (103, 'R47');
INSERT INTO velomax.compose VALUES (103, 'P12');
INSERT INTO velomax.compose VALUES (103, 'O2');

INSERT INTO velomax.compose VALUES (104, 'C76');
INSERT INTO velomax.compose VALUES (104, 'G7');
INSERT INTO velomax.compose VALUES (104, 'F3');
INSERT INTO velomax.compose VALUES (104, 'S88');
INSERT INTO velomax.compose VALUES (104, 'DV87');
INSERT INTO velomax.compose VALUES (104, 'DR86');
INSERT INTO velomax.compose VALUES (104, 'R12');
INSERT INTO velomax.compose VALUES (104, 'R32');
INSERT INTO velomax.compose VALUES (104, 'P12');

INSERT INTO velomax.compose VALUES (105, 'C43');
INSERT INTO velomax.compose VALUES (105, 'G9');
INSERT INTO velomax.compose VALUES (105, 'F9');
INSERT INTO velomax.compose VALUES (105, 'S37');
INSERT INTO velomax.compose VALUES (105, 'DV57');
INSERT INTO velomax.compose VALUES (105, 'DR86');
INSERT INTO velomax.compose VALUES (105, 'R19');
INSERT INTO velomax.compose VALUES (105, 'R18');
INSERT INTO velomax.compose VALUES (105, 'R02');
INSERT INTO velomax.compose VALUES (105, 'P34');

INSERT INTO velomax.compose VALUES (106, 'C44f');
INSERT INTO velomax.compose VALUES (106, 'G9');
INSERT INTO velomax.compose VALUES (106, 'F9');
INSERT INTO velomax.compose VALUES (106, 'S35');
INSERT INTO velomax.compose VALUES (106, 'DV57');
INSERT INTO velomax.compose VALUES (106, 'DR86');
INSERT INTO velomax.compose VALUES (106, 'R19');
INSERT INTO velomax.compose VALUES (106, 'R18');
INSERT INTO velomax.compose VALUES (106, 'R02');
INSERT INTO velomax.compose VALUES (106, 'P34');

INSERT INTO velomax.compose VALUES (107, 'C43');
INSERT INTO velomax.compose VALUES (107, 'G9');
INSERT INTO velomax.compose VALUES (107, 'F9');
INSERT INTO velomax.compose VALUES (107, 'S37');
INSERT INTO velomax.compose VALUES (107, 'DV57');
INSERT INTO velomax.compose VALUES (107, 'DR87');
INSERT INTO velomax.compose VALUES (107, 'R19');
INSERT INTO velomax.compose VALUES (107, 'R18');
INSERT INTO velomax.compose VALUES (107, 'R02');
INSERT INTO velomax.compose VALUES (107, 'P34');
INSERT INTO velomax.compose VALUES (107, 'O4');

INSERT INTO velomax.compose VALUES (108, 'C43f');
INSERT INTO velomax.compose VALUES (108, 'G9');
INSERT INTO velomax.compose VALUES (108, 'F9');
INSERT INTO velomax.compose VALUES (108, 'S35');
INSERT INTO velomax.compose VALUES (108, 'DV57');
INSERT INTO velomax.compose VALUES (108, 'DR87');
INSERT INTO velomax.compose VALUES (108, 'R19');
INSERT INTO velomax.compose VALUES (108, 'R18');
INSERT INTO velomax.compose VALUES (108, 'R02');
INSERT INTO velomax.compose VALUES (108, 'P34');
INSERT INTO velomax.compose VALUES (108, 'O4');

INSERT INTO velomax.compose VALUES (109, 'C01');
INSERT INTO velomax.compose VALUES (109, 'G12');
INSERT INTO velomax.compose VALUES (109, 'S02');
INSERT INTO velomax.compose VALUES (109, 'R1');
INSERT INTO velomax.compose VALUES (109, 'R2');
INSERT INTO velomax.compose VALUES (109, 'R09');
INSERT INTO velomax.compose VALUES (109, 'P1');
INSERT INTO velomax.compose VALUES (109, 'S01');

INSERT INTO velomax.compose VALUES (110, 'C02');
INSERT INTO velomax.compose VALUES (110, 'G12');
INSERT INTO velomax.compose VALUES (110, 'S03');
INSERT INTO velomax.compose VALUES (110, 'R1');
INSERT INTO velomax.compose VALUES (110, 'R2');
INSERT INTO velomax.compose VALUES (110, 'R09');
INSERT INTO velomax.compose VALUES (110, 'P1');
INSERT INTO velomax.compose VALUES (110, 'S05');

INSERT INTO velomax.compose VALUES (111, 'C15');
INSERT INTO velomax.compose VALUES (111, 'G12');
INSERT INTO velomax.compose VALUES (111, 'F9');
INSERT INTO velomax.compose VALUES (111, 'S36');
INSERT INTO velomax.compose VALUES (111, 'DV15');
INSERT INTO velomax.compose VALUES (111, 'DR23');
INSERT INTO velomax.compose VALUES (111, 'R11');
INSERT INTO velomax.compose VALUES (111, 'R12');
INSERT INTO velomax.compose VALUES (111, 'R10');
INSERT INTO velomax.compose VALUES (111, 'P15');
INSERT INTO velomax.compose VALUES (111, 'S74');

INSERT INTO velomax.compose VALUES (112, 'C87');
INSERT INTO velomax.compose VALUES (112, 'G12');
INSERT INTO velomax.compose VALUES (112, 'F9');
INSERT INTO velomax.compose VALUES (112, 'S36');
INSERT INTO velomax.compose VALUES (112, 'DV41');
INSERT INTO velomax.compose VALUES (112, 'DR76');
INSERT INTO velomax.compose VALUES (112, 'R11');
INSERT INTO velomax.compose VALUES (112, 'R12');
INSERT INTO velomax.compose VALUES (112, 'R10');
INSERT INTO velomax.compose VALUES (112, 'P15');
INSERT INTO velomax.compose VALUES (112, 'S74');

INSERT INTO velomax.compose VALUES (113, 'C87f');
INSERT INTO velomax.compose VALUES (113, 'G12');
INSERT INTO velomax.compose VALUES (113, 'F9');
INSERT INTO velomax.compose VALUES (113, 'S34');
INSERT INTO velomax.compose VALUES (113, 'DV41');
INSERT INTO velomax.compose VALUES (113, 'DR76');
INSERT INTO velomax.compose VALUES (113, 'R11');
INSERT INTO velomax.compose VALUES (113, 'R12');
INSERT INTO velomax.compose VALUES (113, 'R10');
INSERT INTO velomax.compose VALUES (113, 'P15');
INSERT INTO velomax.compose VALUES (113, 'S73');

INSERT INTO velomax.compose VALUES (114, 'C25');
INSERT INTO velomax.compose VALUES (114, 'G7');
INSERT INTO velomax.compose VALUES (114, 'F3');
INSERT INTO velomax.compose VALUES (114, 'S87');
INSERT INTO velomax.compose VALUES (114, 'DV132');
INSERT INTO velomax.compose VALUES (114, 'DR52');
INSERT INTO velomax.compose VALUES (114, 'R44');
INSERT INTO velomax.compose VALUES (114, 'R47');
INSERT INTO velomax.compose VALUES (114, 'P12');

INSERT INTO velomax.compose VALUES (115, 'C26');
INSERT INTO velomax.compose VALUES (115, 'G7');
INSERT INTO velomax.compose VALUES (115, 'F3');
INSERT INTO velomax.compose VALUES (115, 'S87');
INSERT INTO velomax.compose VALUES (115, 'DV133');
INSERT INTO velomax.compose VALUES (115, 'DR52');
INSERT INTO velomax.compose VALUES (115, 'R44');
INSERT INTO velomax.compose VALUES (115, 'R47');
INSERT INTO velomax.compose VALUES (115, 'P12');


-- remplissage fournisseur
INSERT INTO velomax.fournisseur VALUES ('87256282358708', 'ESPRIT B2B' , '0174968119', 'espritb2b@gmail.com', '21 rue du Château', '44800', 'SAINT-HERBLAIN', '3');
INSERT INTO velomax.fournisseur VALUES ('27057420344630', 'P2R' , '0193934286', 'p2r@outlook.com', '57 rue Descartes', '92150', 'SURESNES', '1');
INSERT INTO velomax.fournisseur VALUES ('04895544921737', 'GEO-NEGOCE' , '0142571366', 'geo-negoce@gmail.com', '19 rue Isambard', '83600', 'FRÉJUS', '2');
INSERT INTO velomax.fournisseur VALUES ('59933584014297', 'GRIMAC' , '0187585556', 'grimac@gmail.com', '73 rue Marguerite', '94300', 'VINCENNES', '1');
INSERT INTO velomax.fournisseur VALUES ('09245180481124', 'DIFFUSION DIRECTE' , '0165557293', 'diffusiondirecte@yahoo.com', '31 Chemin Challet', '59000', 'LILLE', '1');
INSERT INTO velomax.fournisseur VALUES ('42801278980975', 'PIECES2MOBILE' , '0123283045', 'pieces2mobile@gmail.com', '21 boulevard Albin Durand', '73000', 'CHAMBÉRY', '2');
INSERT INTO velomax.fournisseur VALUES ('00077220588546', 'CHRONO PIECES' , '0113197718', 'chronopieces@outlook.com', '66 rue de la Boétie', '86000', 'POITIERS', '1');
INSERT INTO velomax.fournisseur VALUES ('89545452406536', 'WD INTERNATIONAL' , '0122863970', 'wdinternational@hotmail.fr', '25 rue de Penthièvre', '92800', 'PUTEAUX', '1');
INSERT INTO velomax.fournisseur VALUES ('82972092500941', 'FUTUR AGRI' , '0121140824', 'futuragri@yahoo.com', '16 rue Marie de Médicis', '64200', 'BIARRITZ', '4');
INSERT INTO velomax.fournisseur VALUES ('51885528683333', 'BEPCO FRANCE' , '0199329996', 'bepcofrance@gmail.com', "35 avenue de l'Amandier", '92270', 'BOIS-COLOMBES', '2');


-- remplissage transactions entre velomax et les fournisseurs pour les pièces
INSERT INTO velomax.fournit VALUES ('C02', '89545452406536', 188, 6, 'C837', '2020-03-24', 50);
INSERT INTO velomax.fournit VALUES ('C02', '59933584014297', 121, 4, 'C949', '2021-03-12', 100);

INSERT INTO velomax.fournit VALUES ('C15', '89545452406536', 204, 4, 'C158', '2020-05-06', 40);
INSERT INTO velomax.fournit VALUES ('C15', '89545452406536', 204, 4, 'C158', '2020-10-31', 10);
INSERT INTO velomax.fournit VALUES ('C15', '89545452406536', 204, 4, 'C158', '2020-02-26', 50);
INSERT INTO velomax.fournit VALUES ('C15', '00077220588546', 270, 2, 'C527', '2020-04-13', 30);

INSERT INTO velomax.fournit VALUES ('C25', '82972092500941', 59, 4, 'C301', '2020-08-24', 50);
INSERT INTO velomax.fournit VALUES ('C25', '82972092500941', 69, 5, 'C301', '2020-11-06', 10);
INSERT INTO velomax.fournit VALUES ('C25', '27057420344630', 80, 1, 'C876', '2021-02-08', 70);
INSERT INTO velomax.fournit VALUES ('C25', '27057420344630', 80, 1, 'C876', '2020-07-06', 60);

INSERT INTO velomax.fournit VALUES ('C26', '42801278980975', 188, 7, 'C987', '2020-12-18', 80);
INSERT INTO velomax.fournit VALUES ('C26', '82972092500941', 188, 14, 'C145', '2020-10-29', 80);

INSERT INTO velomax.fournit VALUES ('C32', '00077220588546', 126, 2, 'C261', '2020-12-28', 10);
INSERT INTO velomax.fournit VALUES ('C32', '00077220588546', 126, 1, 'C261', '2020-04-29', 40);
INSERT INTO velomax.fournit VALUES ('C32', '00077220588546', 126, 2, 'C261', '2020-04-15', 10);
INSERT INTO velomax.fournit VALUES ('C32', '87256282358708', 181, 9, 'C726', '2020-12-27', 30);
INSERT INTO velomax.fournit VALUES ('C32', '87256282358708', 181, 7, 'C726', '2020-06-12', 30);

INSERT INTO velomax.fournit VALUES ('C34', '42801278980975', 421, 1, 'C981', '2020-11-16', 50);
INSERT INTO velomax.fournit VALUES ('C34', '42801278980975', 421, 1, 'C981', '2020-09-26', 40);
INSERT INTO velomax.fournit VALUES ('C34', '42801278980975', 421, 1, 'C981', '2020-12-07', 50);
INSERT INTO velomax.fournit VALUES ('C34', '27057420344630', 360, 3, 'C896', '2021-03-12', 70);

INSERT INTO velomax.fournit VALUES ('C43', '59933584014297', 347, 10, 'C816', '2021-03-16', 70);
INSERT INTO velomax.fournit VALUES ('C43', '59933584014297', 347, 8, 'C816', '2020-04-19', 60);
INSERT INTO velomax.fournit VALUES ('C43', '59933584014297', 347, 5, 'C816', '2021-02-18', 20);
INSERT INTO velomax.fournit VALUES ('C43', '51885528683333', 358, 6, 'C451', '2020-09-06', 20);
INSERT INTO velomax.fournit VALUES ('C43', '51885528683333', 358, 5, 'C451', '2020-05-09', 70);

INSERT INTO velomax.fournit VALUES ('C43f', '82972092500941', 275, 2, 'C323', '2020-10-27', 100);
INSERT INTO velomax.fournit VALUES ('C43f', '82972092500941', 275, 1, 'C323', '2021-03-06', 10);
INSERT INTO velomax.fournit VALUES ('C43f', '82972092500941', 419, 3, 'C323', '2020-07-03', 60);

INSERT INTO velomax.fournit VALUES ('C44f', '51885528683333', 77, 3, 'C891', '2020-08-17', 20);
INSERT INTO velomax.fournit VALUES ('C44f', '51885528683333', 77, 5, 'C891', '2020-04-23', 30);
INSERT INTO velomax.fournit VALUES ('C44f', '59933584014297', 83, 4, 'C329', '2020-06-11', 80);
INSERT INTO velomax.fournit VALUES ('C44f', '59933584014297', 83, 2, 'C329', '2020-03-17', 100);
INSERT INTO velomax.fournit VALUES ('C44f', '59933584014297', 90, 4, 'C329', '2020-02-09', 60);

INSERT INTO velomax.fournit VALUES ('C76', '42801278980975', 480, 7, 'C489', '2020-03-17', 30);
INSERT INTO velomax.fournit VALUES ('C76', '42801278980975', 498, 3, 'C489', '2020-07-05', 100);
INSERT INTO velomax.fournit VALUES ('C76', '42801278980975', 498, 8, 'C489', '2020-11-14', 100);

INSERT INTO velomax.fournit VALUES ('C87', '51885528683333', 102, 3, 'C753', '2020-05-01', 30);
INSERT INTO velomax.fournit VALUES ('C87', '51885528683333', 102, 3, 'C753', '2021-04-18', 20);

INSERT INTO velomax.fournit VALUES ('C87f', '42801278980975', 123, 15, 'C896', '2020-08-01', 50);

INSERT INTO velomax.fournit VALUES ('DR23', '04895544921737', 32, 1, 'D552', '2021-02-22', 100);
INSERT INTO velomax.fournit VALUES ('DR23', '51885528683333', 46, 8, 'D814', '2021-01-16', 60);

INSERT INTO velomax.fournit VALUES ('DR52', '04895544921737', 57, 17, 'D349', '2021-04-02', 100);
INSERT INTO velomax.fournit VALUES ('DR52', '04895544921737', 57, 17, 'D349', '2020-03-12', 60);
INSERT INTO velomax.fournit VALUES ('DR52', '04895544921737', 57, 17, 'D349', '2020-11-09', 30);

INSERT INTO velomax.fournit VALUES ('DR56', '42801278980975', 95, 1, 'D304', '2020-07-09', 80);
INSERT INTO velomax.fournit VALUES ('DR56', '42801278980975', 95, 1, 'D304', '2020-05-12', 100);
INSERT INTO velomax.fournit VALUES ('DR56', '42801278980975', 95, 2, 'D304', '2020-08-09', 50);

INSERT INTO velomax.fournit VALUES ('DR76', '00077220588546', 20, 1, 'D449', '2020-10-25', 60);
INSERT INTO velomax.fournit VALUES ('DR76', '00077220588546', 28, 1, 'D449', '2020-07-02', 90);

INSERT INTO velomax.fournit VALUES ('DR86', '82972092500941', 71, 4, 'D954', '2020-11-26', 30);
INSERT INTO velomax.fournit VALUES ('DR86', '00077220588546', 84, 13, 'D727', '2021-02-06', 60);
INSERT INTO velomax.fournit VALUES ('DR86', '00077220588546', 84, 5, 'D727', '2020-03-23', 70);
INSERT INTO velomax.fournit VALUES ('DR86', '00077220588546', 84, 6, 'D727', '2021-01-25', 60);

INSERT INTO velomax.fournit VALUES ('DR87', '00077220588546', 45, 2, 'D989', '2020-03-08', 50);
INSERT INTO velomax.fournit VALUES ('DR87', '87256282358708', 49, 9, 'D103', '2020-06-22', 100);
INSERT INTO velomax.fournit VALUES ('DR87', '87256282358708', 49, 7, 'D103', '2020-03-06', 50);

INSERT INTO velomax.fournit VALUES ('DV132', '87256282358708', 79, 2, 'D442', '2020-04-15', 90);
INSERT INTO velomax.fournit VALUES ('DV132', '87256282358708', 79, 2, 'D442', '2021-04-02', 30);
INSERT INTO velomax.fournit VALUES ('DV132', '87256282358708', 79, 2, 'D442', '2020-07-22', 100);

INSERT INTO velomax.fournit VALUES ('DV133', '87256282358708', 55, 4, 'D565', '2020-12-06', 80);
INSERT INTO velomax.fournit VALUES ('DV133', '87256282358708', 60, 4, 'D565', '2020-08-26', 60);

INSERT INTO velomax.fournit VALUES ('DV15', '51885528683333', 63, 9, 'D808', '2020-06-05', 70);
INSERT INTO velomax.fournit VALUES ('DV15', '51885528683333', 63, 7, 'D808', '2020-11-23', 20);
INSERT INTO velomax.fournit VALUES ('DV15', '09245180481124', 75, 3, 'D433', '2020-08-22', 10);

INSERT INTO velomax.fournit VALUES ('DV17', '27057420344630', 45, 6, 'D795', '2020-01-29', 100);
INSERT INTO velomax.fournit VALUES ('DV17', '27057420344630', 45, 6, 'D795', '2020-04-09', 10);

INSERT INTO velomax.fournit VALUES ('DV41', '04895544921737', 80, 4, 'D617', '2020-02-06', 80);
INSERT INTO velomax.fournit VALUES ('DV41', '04895544921737', 80, 4, 'D617', '2020-08-08', 70);

INSERT INTO velomax.fournit VALUES ('DV57', '59933584014297', 42, 12, 'D449', '2020-02-28', 70);
INSERT INTO velomax.fournit VALUES ('DV57', '51885528683333', 40, 3, 'D255', '2020-04-23', 50);

INSERT INTO velomax.fournit VALUES ('DV87', '51885528683333', 332, 4, 'D195', '2020-01-03', 90);
INSERT INTO velomax.fournit VALUES ('DV87', '51885528683333', 332, 4, 'D195', '2020-02-19', 70);

INSERT INTO velomax.fournit VALUES ('F3', '09245180481124', 70, 1, 'F616', '2020-08-05', 60);
INSERT INTO velomax.fournit VALUES ('F3', '42801278980975', 71, 4, 'F114', '2020-10-27', 50);

INSERT INTO velomax.fournit VALUES ('F9', '87256282358708', 34, 1, 'F233', '2020-08-31', 30);
INSERT INTO velomax.fournit VALUES ('F9', '87256282358708', 37, 1, 'F233', '2020-01-01', 100);

INSERT INTO velomax.fournit VALUES ('G12', '42801278980975', 104, 25, 'G966', '2020-01-24', 30);
INSERT INTO velomax.fournit VALUES ('G12', '42801278980975', 104, 25, 'G966', '2020-11-18', 40);

INSERT INTO velomax.fournit VALUES ('G7', '51885528683333', 66, 8, 'G814', '2020-05-16', 30);
INSERT INTO velomax.fournit VALUES ('G7', '51885528683333', 66, 10, 'G814', '2020-09-08', 100);
INSERT INTO velomax.fournit VALUES ('G7', '51885528683333', 66, 11, 'G814', '2020-03-16', 50);
INSERT INTO velomax.fournit VALUES ('G7', '42801278980975', 215, 5, 'G749', '2021-03-08', 100);

INSERT INTO velomax.fournit VALUES ('G9', '89545452406536', 65, 1, 'G806', '2020-03-08', 40);
INSERT INTO velomax.fournit VALUES ('G9', '89545452406536', 65, 1, 'G806', '2020-01-12', 50);
INSERT INTO velomax.fournit VALUES ('G9', '59933584014297', 70, 7, 'G340', '2021-03-07', 40);
INSERT INTO velomax.fournit VALUES ('G9', '59933584014297', 70, 6, 'G340', '2020-10-24', 80);

INSERT INTO velomax.fournit VALUES ('O2', '42801278980975', 94, 3, 'O875', '2020-06-16', 30);
INSERT INTO velomax.fournit VALUES ('O2', '87256282358708', 101, 2, 'O155', '2021-04-25', 40);
INSERT INTO velomax.fournit VALUES ('O2', '87256282358708', 101, 1, 'O155', '2020-04-18', 10);
INSERT INTO velomax.fournit VALUES ('O2', '87256282358708', 101, 2, 'O155', '2020-06-28', 80);

INSERT INTO velomax.fournit VALUES ('O4', '51885528683333', 119, 4, 'O780', '2020-04-24', 100);
INSERT INTO velomax.fournit VALUES ('O4', '51885528683333', 119, 1, 'O780', '2021-04-09', 60);
INSERT INTO velomax.fournit VALUES ('O4', '51885528683333', 119, 3, 'O780', '2021-01-08', 60);
INSERT INTO velomax.fournit VALUES ('O4', '27057420344630', 150, 2, 'O318', '2020-03-21', 30);

INSERT INTO velomax.fournit VALUES ('P1', '51885528683333', 171, 19, 'P356', '2020-09-08', 100);
INSERT INTO velomax.fournit VALUES ('P1', '59933584014297', 136, 2, 'P965', '2020-03-10', 30);
INSERT INTO velomax.fournit VALUES ('P1', '59933584014297', 136, 1, 'P965', '2021-01-16', 10);

INSERT INTO velomax.fournit VALUES ('P12', '04895544921737', 65, 4, 'P336', '2020-08-08', 40);
INSERT INTO velomax.fournit VALUES ('P12', '27057420344630', 76, 1, 'P788', '2020-08-06', 90);
INSERT INTO velomax.fournit VALUES ('P12', '27057420344630', 76, 2, 'P788', '2020-07-26', 60);
INSERT INTO velomax.fournit VALUES ('P12', '27057420344630', 76, 3, 'P788', '2021-02-24', 80);

INSERT INTO velomax.fournit VALUES ('P15', '27057420344630', 78, 9, 'P192', '2020-08-30', 80);
INSERT INTO velomax.fournit VALUES ('P15', '27057420344630', 78, 9, 'P192', '2020-02-09', 40);
INSERT INTO velomax.fournit VALUES ('P15', '27057420344630', 78, 9, 'P192', '2021-04-19', 10);

INSERT INTO velomax.fournit VALUES ('P34', '42801278980975', 139, 4, 'P599', '2020-05-08', 100);
INSERT INTO velomax.fournit VALUES ('P34', '42801278980975', 139, 4, 'P599', '2020-09-14', 20);
INSERT INTO velomax.fournit VALUES ('P34', '42801278980975', 139, 4, 'P599', '2021-02-02', 40);

INSERT INTO velomax.fournit VALUES ('R02', '04895544921737', 66, 1, 'R884', '2021-02-19', 50);
INSERT INTO velomax.fournit VALUES ('R02', '82972092500941', 66, 4, 'R411', '2020-12-17', 80);

INSERT INTO velomax.fournit VALUES ('R09', '82972092500941', 67, 2, 'R985', '2021-01-28', 40);
INSERT INTO velomax.fournit VALUES ('R09', '82972092500941', 67, 4, 'R985', '2020-01-18', 40);
INSERT INTO velomax.fournit VALUES ('R09', '82972092500941', 67, 7, 'R985', '2020-10-28', 80);
INSERT INTO velomax.fournit VALUES ('R09', '09245180481124', 79, 5, 'R936', '2020-01-01', 100);
INSERT INTO velomax.fournit VALUES ('R09', '09245180481124', 79, 4, 'R936', '2020-10-17', 90);

INSERT INTO velomax.fournit VALUES ('R1', '82972092500941', 74, 7, 'R745', '2020-04-21', 60);
INSERT INTO velomax.fournit VALUES ('R1', '00077220588546', 70, 2, 'R239', '2020-06-07', 40);
INSERT INTO velomax.fournit VALUES ('R1', '00077220588546', 70, 3, 'R239', '2021-03-21', 10);

INSERT INTO velomax.fournit VALUES ('R10', '82972092500941', 56, 2, 'R570', '2020-09-24', 10);
INSERT INTO velomax.fournit VALUES ('R10', '82972092500941', 56, 1, 'R570', '2020-08-19', 20);
INSERT INTO velomax.fournit VALUES ('R10', '00077220588546', 61, 6, 'R580', '2020-03-21', 40);

INSERT INTO velomax.fournit VALUES ('R11', '59933584014297', 185, 4, 'R430', '2020-02-15', 80);
INSERT INTO velomax.fournit VALUES ('R11', '59933584014297', 185, 4, 'R430', '2020-04-11', 40);

INSERT INTO velomax.fournit VALUES ('R12', '42801278980975', 347, 2, 'R811', '2020-01-16', 10);
INSERT INTO velomax.fournit VALUES ('R12', '42801278980975', 347, 2, 'R811', '2020-07-16', 50);

INSERT INTO velomax.fournit VALUES ('R18', '82972092500941', 79, 5, 'R137', '2020-01-01', 60);
INSERT INTO velomax.fournit VALUES ('R18', '82972092500941', 79, 7, 'R137', '2020-08-15', 70);
INSERT INTO velomax.fournit VALUES ('R18', '04895544921737', 79, 1, 'R338', '2020-11-01', 100);
INSERT INTO velomax.fournit VALUES ('R18', '04895544921737', 79, 2, 'R338', '2021-03-06', 80);
INSERT INTO velomax.fournit VALUES ('R18', '04895544921737', 79, 1, 'R338', '2020-05-12', 80);

INSERT INTO velomax.fournit VALUES ('R19', '51885528683333', 65, 16, 'R154', '2020-07-14', 100);
INSERT INTO velomax.fournit VALUES ('R19', '42801278980975', 74, 8, 'R125', '2020-09-02', 40);

INSERT INTO velomax.fournit VALUES ('R2', '51885528683333', 63, 6, 'R791', '2020-07-07', 50);
INSERT INTO velomax.fournit VALUES ('R2', '00077220588546', 82, 3, 'R217', '2021-01-05', 10);
INSERT INTO velomax.fournit VALUES ('R2', '00077220588546', 82, 2, 'R217', '2020-08-25', 90);
INSERT INTO velomax.fournit VALUES ('R2', '00077220588546', 82, 1, 'R217', '2020-10-09', 50);

INSERT INTO velomax.fournit VALUES ('R32', '82972092500941', 40, 2, 'R651', '2021-01-17', 80);
INSERT INTO velomax.fournit VALUES ('R32', '82972092500941', 40, 2, 'R651', '2020-08-05', 50);

INSERT INTO velomax.fournit VALUES ('R44', '00077220588546', 278, 3, 'R146', '2021-02-15', 70);
INSERT INTO velomax.fournit VALUES ('R44', '00077220588546', 278, 3, 'R146', '2020-04-05', 90);

INSERT INTO velomax.fournit VALUES ('R45', '27057420344630', 365, 29, 'R514', '2020-04-08', 20);
INSERT INTO velomax.fournit VALUES ('R45', '27057420344630', 365, 29, 'R514', '2020-08-29', 60);

INSERT INTO velomax.fournit VALUES ('R46', '51885528683333', 52, 1, 'R306', '2020-09-11', 40);
INSERT INTO velomax.fournit VALUES ('R46', '04895544921737', 56, 18, 'R137', '2020-07-31', 60);
INSERT INTO velomax.fournit VALUES ('R46', '04895544921737', 56, 8, 'R137', '2021-04-20', 40);
INSERT INTO velomax.fournit VALUES ('R46', '04895544921737', 56, 3, 'R137', '2020-07-27', 50);

INSERT INTO velomax.fournit VALUES ('R47', '42801278980975', 88, 1, 'R171', '2020-06-05', 90);
INSERT INTO velomax.fournit VALUES ('R47', '42801278980975', 88, 2, 'R171', '2020-07-27', 10);
INSERT INTO velomax.fournit VALUES ('R47', '87256282358708', 82, 4, 'R241', '2020-03-08', 70);
INSERT INTO velomax.fournit VALUES ('R47', '87256282358708', 82, 5, 'R241', '2021-02-04', 70);

INSERT INTO velomax.fournit VALUES ('R48', '51885528683333', 99, 1, 'R159', '2020-08-23', 10);
INSERT INTO velomax.fournit VALUES ('R48', '51885528683333', 99, 2, 'R159', '2020-12-02', 20);
INSERT INTO velomax.fournit VALUES ('R48', '89545452406536', 96, 4, 'R202', '2020-01-27', 50);

INSERT INTO velomax.fournit VALUES ('S01', '04895544921737', 10, 3, 'S547', '2021-01-26', 70);
INSERT INTO velomax.fournit VALUES ('S01', '04895544921737', 10, 2, 'S547', '2021-03-24', 90);

INSERT INTO velomax.fournit VALUES ('S02', '82972092500941', 45, 4, 'S243', '2021-01-09', 20);
INSERT INTO velomax.fournit VALUES ('S02', '82972092500941', 45, 4, 'S243', '2020-04-16', 20);
INSERT INTO velomax.fournit VALUES ('S02', '87256282358708', 31, 11, 'S533', '2020-01-08', 20);
INSERT INTO velomax.fournit VALUES ('S02', '87256282358708', 31, 6, 'S533', '2020-01-18', 40);
INSERT INTO velomax.fournit VALUES ('S02', '87256282358708', 31, 4, 'S533', '2020-01-20', 30);

INSERT INTO velomax.fournit VALUES ('S03', '59933584014297', 35, 1, 'S444', '2020-08-09', 80);
INSERT INTO velomax.fournit VALUES ('S03', '59933584014297', 35, 3, 'S444', '2020-08-05', 90);
INSERT INTO velomax.fournit VALUES ('S03', '59933584014297', 35, 2, 'S444', '2021-02-22', 30);
INSERT INTO velomax.fournit VALUES ('S03', '04895544921737', 31, 2, 'S215', '2020-09-10', 40);

INSERT INTO velomax.fournit VALUES ('S05', '09245180481124', 19, 1, 'S231', '2020-12-26', 50);
INSERT INTO velomax.fournit VALUES ('S05', '09245180481124', 19, 1, 'S231', '2020-10-28', 20);
INSERT INTO velomax.fournit VALUES ('S05', '09245180481124', 19, 1, 'S231', '2020-05-09', 20);

INSERT INTO velomax.fournit VALUES ('S34', '87256282358708', 51, 4, 'S706', '2020-01-27', 100);
INSERT INTO velomax.fournit VALUES ('S34', '00077220588546', 60, 2, 'S194', '2020-04-27', 40);
INSERT INTO velomax.fournit VALUES ('S34', '00077220588546', 60, 2, 'S194', '2020-03-14', 50);

INSERT INTO velomax.fournit VALUES ('S35', '04895544921737', 34, 20, 'S424', '2020-02-08', 60);
INSERT INTO velomax.fournit VALUES ('S35', '04895544921737', 34, 20, 'S424', '2020-08-30', 80);
INSERT INTO velomax.fournit VALUES ('S35', '04895544921737', 34, 20, 'S424', '2020-04-13', 50);

INSERT INTO velomax.fournit VALUES ('S36', '59933584014297', 49, 25, 'S609', '2020-08-18', 70);
INSERT INTO velomax.fournit VALUES ('S36', '59933584014297', 49, 25, 'S609', '2021-04-14', 100);

INSERT INTO velomax.fournit VALUES ('S37', '27057420344630', 56, 1, 'S895', '2020-07-31', 70);
INSERT INTO velomax.fournit VALUES ('S37', '27057420344630', 56, 3, 'S895', '2021-03-03', 80);
INSERT INTO velomax.fournit VALUES ('S37', '27057420344630', 56, 2, 'S895', '2020-10-09', 70);

INSERT INTO velomax.fournit VALUES ('S73', '42801278980975', 16, 1, 'S422', '2020-06-25', 80);
INSERT INTO velomax.fournit VALUES ('S73', '04895544921737', 12, 1, 'S471', '2021-03-26', 90);
INSERT INTO velomax.fournit VALUES ('S73', '04895544921737', 12, 3, 'S471', '2020-07-07', 60);

INSERT INTO velomax.fournit VALUES ('S74', '87256282358708', 17, 1, 'S237', '2021-03-11', 20);
INSERT INTO velomax.fournit VALUES ('S74', '59933584014297', 19, 3, 'S946', '2020-10-03', 60);

INSERT INTO velomax.fournit VALUES ('S87', '09245180481124', 44, 1, 'S656', '2020-06-29', 20);
INSERT INTO velomax.fournit VALUES ('S87', '09245180481124', 44, 2, 'S656', '2020-08-12', 50);
INSERT INTO velomax.fournit VALUES ('S87', '09245180481124', 44, 1, 'S656', '2020-06-27', 80);

INSERT INTO velomax.fournit VALUES ('S88', '00077220588546', 59, 3, 'S255', '2020-05-08', 50);
INSERT INTO velomax.fournit VALUES ('S88', '00077220588546', 59, 2, 'S255', '2021-03-28', 90);
INSERT INTO velomax.fournit VALUES ('S88', '00077220588546', 59, 3, 'S255', '2020-10-04', 90);







