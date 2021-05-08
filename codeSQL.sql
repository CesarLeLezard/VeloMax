-- Probleme BDD VeloMax
-- GROSSE Alexandre, GUENARD Antoine
-- Groupe : S

-- création de la base de données
DROP DATABASE IF EXISTS velomax;
CREATE DATABASE IF NOT EXISTS velomax;
USE velomax;


-- création des tables
DROP TABLE IF EXISTS qualite;
CREATE TABLE IF NOT EXISTS qualite (
    note_qualite INTEGER PRIMARY KEY,
    libelle_qualite VARCHAR(40)
);


DROP TABLE IF EXISTS grandeur;
CREATE TABLE IF NOT EXISTS grandeur (
    id_grandeur INTEGER PRIMARY KEY,
    libelle_grandeur VARCHAR(40)
);


DROP TABLE IF EXISTS ligneProduit;
CREATE TABLE IF NOT EXISTS ligneProduit (
    id_ligne INTEGER PRIMARY KEY,
    libelle_ligne VARCHAR(40)
);


DROP TABLE IF EXISTS descript;
CREATE TABLE IF NOT EXISTS descript (
    id_descript INTEGER PRIMARY KEY,
    libelle_descript VARCHAR(40)
);


DROP TABLE IF EXISTS piece;
CREATE TABLE IF NOT EXISTS piece (
    id_piece VARCHAR(8) PRIMARY KEY,
    dateIntro_piece DATE,
    dateDisc_piece DATE,
    stock_piece INTEGER,
    id_descript INTEGER, FOREIGN KEY (id_descript) REFERENCES descript (id_descript)
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
    note_qualite INTEGER, FOREIGN KEY (note_qualite) REFERENCES qualite (note_qualite)
);


DROP TABLE IF EXISTS fidelio;
CREATE TABLE IF NOT EXISTS fidelio (
	type_fidelio INTEGER PRIMARY KEY,
    description_fidelio VARCHAR(40),
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
    type_fidelio INTEGER, FOREIGN KEY (type_fidelio) REFERENCES fidelio (type_fidelio)
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
    type_fidelio INTEGER, FOREIGN KEY (type_fidelio) REFERENCES fidelio (type_fidelio)
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
    numCatalogue_fournit VARCHAR(8)
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
    quantite_contientPiece INTEGER
);


DROP TABLE IF EXISTS contientModele;
CREATE TABLE IF NOT EXISTS contientModele (
    id_commande INTEGER, FOREIGN KEY (id_commande) REFERENCES commande (id_commande),
    id_modele INTEGER, FOREIGN KEY (id_modele) REFERENCES modele (id_modele),
    quantite_contientPiece INTEGER
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


-- remplissage qualité de la réactivité des fournisseurs
INSERT INTO velomax.qualite VALUES (1, 'Très bon');
INSERT INTO velomax.qualite VALUES (2, 'Bon');
INSERT INTO velomax.qualite VALUES (3, 'Moyen');
INSERT INTO velomax.qualite VALUES (4, 'Mauvais');


-- remplissage des grandeurs de modèles de vélo
INSERT INTO velomax.grandeur VALUES (1, 'Adultes');
INSERT INTO velomax.grandeur VALUES (2, 'Jeunes');
INSERT INTO velomax.grandeur VALUES (3, 'Hommes');
INSERT INTO velomax.grandeur VALUES (4, 'Dames');
INSERT INTO velomax.grandeur VALUES (5, 'Filles');
INSERT INTO velomax.grandeur VALUES (6, 'Garçons');


-- remplissage des descpritions des pièces
INSERT INTO velomax.descript VALUES (1, 'Cadre');
INSERT INTO velomax.descript VALUES (2, 'Guidon');
INSERT INTO velomax.descript VALUES (3, 'Freins');
INSERT INTO velomax.descript VALUES (4, 'Selle');
INSERT INTO velomax.descript VALUES (5, 'Dérailleur avant');
INSERT INTO velomax.descript VALUES (6, 'Dérailleur arrière');
INSERT INTO velomax.descript VALUES (7, 'Roue avant');
INSERT INTO velomax.descript VALUES (8, 'Roue arrière');
INSERT INTO velomax.descript VALUES (9, 'Réflecteurs');
INSERT INTO velomax.descript VALUES (10, 'Pédalier');
INSERT INTO velomax.descript VALUES (11, 'Ordinateur');
INSERT INTO velomax.descript VALUES (12, 'Panier');


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





