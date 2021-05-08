-- Probleme BDD VeloMax
-- GROSSE Alexandre, GUENARD Antoine
-- Groupe : S

-- création de la base de données
DROP DATABASE IF EXISTS velomax;
CREATE DATABASE IF NOT EXISTS velomax;
USE velomax;


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
    id_ligneProduit INTEGER PRIMARY KEY,
    libelle_ligneProduit VARCHAR(40)
);


DROP TABLE IF EXISTS piece;
CREATE TABLE IF NOT EXISTS piece (
    id_piece VARCHAR(8) PRIMARY KEY,
    description_piece VARCHAR(40),
    dateIntro_piece DATE,
    dateDisc_piece DATE,
    stock_piece INTEGER
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
    id_ligneProduit INTEGER, FOREIGN KEY (id_ligneProduit) REFERENCES ligneProduit (id_ligneProduit)
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
    id_piece VARCHAR(8), FOREIGN KEY (id_piece) REFERENCES piece (id_piece),
    id_modele INTEGER, FOREIGN KEY (id_modele) REFERENCES modele (id_modele)
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



