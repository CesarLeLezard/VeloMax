DROP DATABASE veloMax;
#DROP TABLE adresse;
#DROP TABLE contact;
#DROP TABLE fournisseur;
#DROP TABLE fidelio_programme;
#DROP TABLE bicyclette;
#DROP TABLE piece;
#DROP TABLE ind_client;
#DROP TABLE boutique;
#DROP TABLE commande;
#DROP TABLE contient;
#DROP TABLE passe;


CREATE DATABASE veloMax;
USE veloMax;

CREATE TABLE vélo(
no_modele INT PRIMARY KEY auto_increment,
nom_vélo VARCHAR(40),
grandeur VARCHAR(40),
prix_vélo DOUBLE, 
date_intro_vélo DATETIME,
date_disc_vélo DATETIME,
ligne_produit ENUM('VTT', 'Vélo de course', 'Classique','BMX')
);
alter table vélo auto_increment=100;


CREATE TABLE adresse(
id_adresse INT PRIMARY KEY NOT NULL auto_increment,
rue VARCHAR(40),
ville VARCHAR(40),
code_postal INT,
province VARCHAR(40),
no_commande INT,
siret_fournisseur INT,
id_ind INT,
id_boutique INT
);
alter table adresse auto_increment=1000;

CREATE TABLE contact(
id_contact INT PRIMARY KEY NOT NULL auto_increment,
nom_contact VARCHAR(40),
prenom_contact VARCHAR(40),
telephone_contact VARCHAR(40),
mail_contact VARCHAR(40),
siret_fournisseur INT,
id_boutique INT
);
alter table contact auto_increment=50;

CREATE TABLE fournisseur(
siret_fournisseur INT PRIMARY KEY NOT NULL auto_increment,
id_adresse INT,
id_contact INT,
nom_entreprise VARCHAR(40),
libelle ENUM('1','2','3','4'),
FOREIGN KEY (id_adresse) REFERENCES adresse(id_adresse) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (id_contact) REFERENCES contact(id_contact) ON DELETE CASCADE ON UPDATE CASCADE
);
alter table fournisseur auto_increment=500;

CREATE TABLE fidelio_programme(
id_programme INT PRIMARY KEY NOT NULL auto_increment,
description_programme VARCHAR(40),
duree INT,
cout INT,
rabais INT
);
alter table fidelio_programme auto_increment=1;

CREATE TABLE stock(
id_stock INT PRIMARY KEY NOT NULL auto_increment,
quantite INT
);
alter table stock auto_increment=5000;

CREATE TABLE piece(
no_piece INT PRIMARY KEY auto_increment,
siret_fournisseur INT,
no_modele INT,
description_piece VARCHAR(40),
no_produit INT,
prix_piece DOUBLE,
date_intro_piece DATETIME,
date_discon_piece DATETIME,
delai_approvisionnement INT,
id_stock INT,
FOREIGN KEY (id_stock) REFERENCES stock(id_stock) ON DELETE CASCADE ON UPDATE CASCADE
);
alter table piece auto_increment=300;

CREATE TABLE estEn(
id_estEn INT PRIMARY KEY NOT NULL auto_increment,
no_modele INT,
id_stock INT,
no_piece INT,
FOREIGN KEY (no_modele) REFERENCES vélo(no_modele) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (id_stock) REFERENCES stock(id_stock) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (no_piece) REFERENCES piece(no_piece) ON DELETE CASCADE ON UPDATE CASCADE
);
alter table estEn auto_increment=6000;

CREATE TABLE ind_client(
id_ind INT PRIMARY KEY NOT NULL auto_increment,
id_programme INT,
id_adresse INT,
nom_client VARCHAR(40),
prenom_client VARCHAR(40),
mail_client VARCHAR(40),
telephone_client VARCHAR(40),
FOREIGN KEY (id_programme) REFERENCES fidelio_programme(id_programme) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (id_adresse) REFERENCES adresse(id_adresse) ON DELETE CASCADE ON UPDATE CASCADE
);
alter table ind_client auto_increment=3000;

CREATE TABLE rabais_boutique_client(
id_rabais INT PRIMARY KEY NOT NULL auto_increment,
rabais_boutique INT,
volume_achat INT
);
alter table rabais_boutique_client auto_increment=7000;

CREATE TABLE boutique(
id_boutique INT PRIMARY KEY NOT NULL auto_increment,
id_adresse INT,
id_contact INT,
id_rabais INT,
telephone_boutique VARCHAR(40),
nom_compagnie VARCHAR(40),
mail_boutique VARCHAR(40),
FOREIGN KEY (id_adresse) REFERENCES adresse(id_adresse) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (id_rabais) REFERENCES rabais_boutique_client(id_rabais) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (id_contact) REFERENCES contact(id_contact) ON DELETE CASCADE ON UPDATE CASCADE
);
alter table boutique auto_increment=4000;

CREATE TABLE commande(
no_commande INT PRIMARY KEY NOT NULL auto_increment,
id_ind INT,
id_boutique INT,
id_adresse INT,
date_commande DATETIME,
date_livraison DATETIME,
FOREIGN KEY (id_ind) REFERENCES ind_client(id_ind) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (id_boutique) REFERENCES boutique(id_boutique) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (id_adresse) REFERENCES adresse(id_adresse) ON DELETE CASCADE ON UPDATE CASCADE
);
alter table commande auto_increment=15000;


CREATE TABLE contient(
id_contient INT PRIMARY KEY NOT NULL auto_increment,
no_commande INT NOT NULL,
no_modele INT,
no_piece INT,
quantite_bicyclette INT,
quantite_piece INT,
FOREIGN KEY (no_piece) REFERENCES piece(no_piece) ON DELETE CASCADE,
FOREIGN KEY (no_modele) REFERENCES vélo(no_modele) ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY (no_commande) REFERENCES commande(no_commande) ON DELETE CASCADE ON UPDATE CASCADE
);
alter table contient auto_increment=30;

CREATE TABLE fournit(
siret_fournisseur INT,
no_piece INT,
PRIMARY KEY (siret_fournisseur,no_piece)
);

CREATE TABLE est_compose_de(
no_piece INT,
no_modele INT,
PRIMARY KEY (no_piece,no_modele)
);

CREATE TABLE adhere(
date_adhesion DATETIME,
id_ind INT,
id_programme INT,
PRIMARY KEY (id_ind,id_programme)
);

# On modifie la table adresse maintenant que les tables commande,fournisseur,ind_client et boutique sont créees
ALTER TABLE adresse ADD FOREIGN KEY (no_commande) REFERENCES commande(no_commande) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE adresse ADD FOREIGN KEY (siret_fournisseur) REFERENCES fournisseur(siret_fournisseur) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE adresse ADD FOREIGN KEY (id_ind) REFERENCES ind_client(id_ind) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE adresse ADD FOREIGN KEY (id_boutique) REFERENCES boutique(id_boutique) ON DELETE CASCADE ON UPDATE CASCADE;

# On modifie la table contact maintenant que les tables fournisseur et boutique sont créees
ALTER TABLE contact ADD FOREIGN KEY (siret_fournisseur) REFERENCES fournisseur(siret_fournisseur) ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE contact ADD FOREIGN KEY (id_boutique) REFERENCES boutique(id_boutique) ON DELETE CASCADE ON UPDATE CASCADE;


-- INSERTION DES EXEMPLES --
insert into rabais_boutique_client (rabais_boutique,volume_achat) values ('5','50');
insert into rabais_boutique_client (rabais_boutique,volume_achat) values ('8','100');
insert into rabais_boutique_client (rabais_boutique,volume_achat) values ('10','150');
insert into rabais_boutique_client (rabais_boutique,volume_achat) values ('12','250');

insert into contact (nom_contact,prenom_contact,mail_contact,telephone_contact,siret_fournisseur,id_boutique) values ('Dupond', 'Jules', 'julesdupond@gmail.com','0123456789',NULL,NULL);
insert into contact (nom_contact,prenom_contact,mail_contact,telephone_contact,siret_fournisseur,id_boutique) values ('Ronand', 'Grégoire', 'gregou@orange.fr','0643468976',NULL,NULL);
insert into contact (nom_contact,prenom_contact,mail_contact,telephone_contact,siret_fournisseur,id_boutique) values ('Saillaud', 'Astride', 'astridesaillaud@gmail.com','0696425678',NULL,NULL);
insert into contact (nom_contact,prenom_contact,mail_contact,telephone_contact,siret_fournisseur,id_boutique) values ('Chaval', 'Léa', 'chavallea@gmail.com','0614253665',NULL,NULL);
insert into contact (nom_contact,prenom_contact,mail_contact,telephone_contact,siret_fournisseur,id_boutique) values ('Colaba', 'Lucas', 'colabalucas@orange.fr','0245568596',NULL,NULL);
insert into contact (nom_contact,prenom_contact,mail_contact,telephone_contact,siret_fournisseur,id_boutique) values ('Manef', 'Camille', 'manefcamille@gmail.com','0244562521',NULL,NULL);
insert into contact (nom_contact,prenom_contact,mail_contact,telephone_contact,siret_fournisseur,id_boutique) values ('Tulipe', 'Joe', 'tulipejoe@gmail.com','0125477391',NULL,NULL);
insert into contact (nom_contact,prenom_contact,mail_contact,telephone_contact,siret_fournisseur,id_boutique) values ('Rose', 'Maxim', 'rosemaxim@orange.fr','0645454545',NULL,NULL);
insert into contact (nom_contact,prenom_contact,mail_contact,telephone_contact,siret_fournisseur,id_boutique) values ('Chat', 'Amandine', 'chatamandine@gmail.com','0632323232',NULL,NULL);


insert into adresse (rue,ville,code_postal,province) values ('3 rue des Peupliers','Nantes','44000','Pays de la Loire');
insert into adresse (rue,ville,code_postal,province) values ('15 avenue Charles de Gaulle','Paris','75016','Ile de France');
insert into adresse (rue,ville,code_postal,province) values ('6 rue Maréchal','Nantes','44592','Pays de la Loire');
insert into adresse (rue,ville,code_postal,province) values ('68 rue des lilas','Paris','75005','Ile de France');
insert into adresse (rue,ville,code_postal,province) values ('34 boulevard de la Victoire','Toulouse','31200','Occitanie');

insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Kilimandjaro','Adultes','569','VTT','12/02/20','02/04/20');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Kilimandjaro','Garçons','569','VTT','12/04/20','02/05/20');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('NorthPole','Hommes','129','Vélo de course','12/04/20','02/05/20');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('NorthPole','Adultes','359','Classique','10/10/20','20/10/20');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('NorthPole','Adultes','329','VTT','10/10/20','20/10/20');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Hooligan','Jeunes','329','Vélo de course','01/10/20','05/06/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Orléans','Dames','359','BMX','01/10/20','05/06/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('BlueJay','Jeunes','569','Classique','21/06/20','12/12/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('BlueJay','Hommes','569','BMX','21/06/20','12/12/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Trail Explorer','Jeunes','129','Vélo de course','21/06/20','12/12/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Trail Explorer','Filles','229','Classique','21/06/20','12/12/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Trail Explorer','Garçons','229','VTT','01/10/20','05/06/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Night Hawk','Adultes','189','Classique','01/10/20','05/06/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Night Hawk','Filles','195','Vélo de course','01/10/20','05/06/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Tierra Verde','Adultes','89','Vélo de course','01/10/20','05/06/21');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Mud Zinger I','Jeunes','189','Classique','05/05/20','02/06/20');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Mud Zinger I','Jeunes','199','VTT','05/05/20','02/06/20');
insert into vélo (nom_vélo,grandeur,prix_vélo,ligne_produit,date_intro_vélo,date_disc_vélo) values ('Mud Zinger II','Hommes','569','BMX','05/05/20','02/06/20');

insert into fournisseur (nom_entreprise,libelle,id_contact,id_adresse) values ('AXA','1','52','1000');
insert into fournisseur (nom_entreprise,libelle,id_contact,id_adresse) values ('Renauld','2','50','1001');
insert into fournisseur (nom_entreprise,libelle,id_contact,id_adresse) values ('Naturalia','3','55','1004');
insert into fournisseur (nom_entreprise,libelle,id_contact,id_adresse) values ('Decathlon','4','51','1004');
insert into fournisseur (nom_entreprise,libelle,id_contact,id_adresse) values ('VeloPlus','1','54','1003');
insert into fournisseur (nom_entreprise,libelle,id_contact,id_adresse) values ('BikeStore','2','53','1002');
insert into fournisseur (nom_entreprise,libelle,id_contact,id_adresse) values ('serdg','2',NULL,NULL);

insert into fidelio_programme (description_programme,cout,rabais,duree) values ('Fidélio','15','5','1');
insert into fidelio_programme (description_programme,cout,rabais,duree) values ('Fidélio Or','25','8','2');
insert into fidelio_programme (description_programme,cout,rabais,duree) values ('Fidélio Platine','60','10','2');
insert into fidelio_programme (description_programme,cout,rabais,duree) values ('Fidélio Max','100','12','3');

insert into stock (quantite) values ('0');
insert into stock (quantite) values ('40');
insert into stock (quantite) values ('30');
insert into stock (quantite) values ('35');
insert into stock (quantite) values ('45');

insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('500', '2000','Cadre','37','15','04/04/20','10/04/21','2','5000');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('500', '2000','Guidon','12','12','04/04/20','10/04/21','2','5002');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('500', '2000','Freins','15','15','04/04/20','10/04/21','2','5001');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('500', '2000','Selle','18','36','04/04/20','10/04/21','2','5000');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('500', '2000','Dérailleur Avant','48','44','04/04/20','10/04/21','2','5003');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('500', '2000','Dérailleur Arrière','45','41','04/04/20','10/04/21','2','5000');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('501', '2005','Guidon','20','556','15/10/20','15/11/21','30','5000');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('501', '2015','Freins','37','20','21/03/20','10/06/21','5','5001');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('502', '2003','Selle','3457','20','14/12/20','10/04/21','5','5001');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('502', '2002','Dérailleur Avant','45','50','04/02/20','11/11/21','6','5002');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('502', '2001','Dérailleur Arrière','56','35','09/09/20','15/09/21','8','5002');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('503', '2003','Roue Avant','5','34.9','16/09/20','22/12/21','13','5002');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('504', '2003','Roue Arrière','8','15.5','20/10/20','14/04/21','13','5003');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('504', '2004','Réflecteurs','15','16.99','17/07/20','02/04/21','13','5003');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('505', '2005','Pédalier','15','25','01/02/20','02/04/20','2','5003');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('505', '2015','Ordinateur','8','16.99','07/03/20','12/12/20','4','5004');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('505', '2005','Pédalier','15','30','01/02/20','02/04/20','2','5003');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('505', '2015','Roue Avant','8','18.99','07/03/20','12/12/20','4','5004');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('505', '2015','Roue Arrière','8','12.99','07/03/20','12/12/20','4','5004');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('504', '2014','Petit panier','13','45','11/03/20','25/11/20','4','5004');
insert into piece (siret_fournisseur,no_modele,description_piece,no_produit,prix_piece,date_intro_piece,date_discon_piece,delai_approvisionnement,id_stock) values ('504', '2013','Grand panier','9','33','24/11/20','26/12/20','4','5004');

insert into estEn (no_modele,id_stock,no_piece) values ('100','5000',NULL);
insert into estEn (no_modele,id_stock,no_piece) values ('101','5001','300');
insert into estEn (no_modele,id_stock,no_piece) values ('102','5003','301');
insert into estEn (no_modele,id_stock,no_piece) values ('103','5004',NULL);
insert into estEn (no_modele,id_stock,no_piece) values ('104','5001',NULL);
insert into estEn (no_modele,id_stock,no_piece) values ('105','5003',NULL);
insert into estEn (no_modele,id_stock,no_piece) values ('106','5000',NULL);
insert into estEn (no_modele,id_stock,no_piece) values ('107','5001','302');
insert into estEn (no_modele,id_stock,no_piece) values ('108','5003','303');
insert into estEn (no_modele,id_stock,no_piece) values ('109','5004',NULL);
insert into estEn (no_modele,id_stock,no_piece) values ('110','5001',NULL);
insert into estEn (no_modele,id_stock,no_piece) values ('111','5003',NULL);
insert into estEn (no_modele,id_stock,no_piece) values ('112','5000','304');
insert into estEn (no_modele,id_stock,no_piece) values ('113','5001','305');
insert into estEn (no_modele,id_stock,no_piece) values ('114','5003',NULL);
insert into estEn (no_modele,id_stock,no_piece) values ('115','5000','306');
insert into estEn (no_modele,id_stock,no_piece) values ('116','5004','307');
insert into estEn (no_modele,id_stock,no_piece) values ('117','5003','312');
insert into estEn (no_modele,id_stock,no_piece) values (NULL,'5003','308');
insert into estEn (no_modele,id_stock,no_piece) values (NULL,'5000','309');
insert into estEn (no_modele,id_stock,no_piece) values (NULL,'5004','310');
insert into estEn (no_modele,id_stock,no_piece) values (NULL,'5001','311');

insert into ind_client (id_programme,id_adresse,nom_client,prenom_client,mail_client,telephone_client) values ('1','1002','Marine','Leo','marine.leo@gmail.com','0214253665');
insert into ind_client (id_programme,id_adresse,nom_client,prenom_client,mail_client,telephone_client) values ('2','1003','Poireau','Julie','poireau.julie@gmail.com','0632654578');
insert into ind_client (id_programme,id_adresse,nom_client,prenom_client,mail_client,telephone_client) values ('3','1002','Pomme','Manon','pomme.manon@gmail.com','0714583625');
insert into ind_client (id_programme,id_adresse,nom_client,prenom_client,mail_client,telephone_client) values ('4','1004','Poire','Alex','poire.alex@gmail.com','0645484741');
insert into ind_client (id_programme,id_adresse,nom_client,prenom_client,mail_client,telephone_client) values ('4','1004','Fraise','Théo','fraise.theo@gmail.com','0736353236');
insert into ind_client (id_programme,id_adresse,nom_client,prenom_client,mail_client,telephone_client) values ('4','1003','Framboise','Emie','framboise.emie@gmail.com','0795969895');

insert into boutique (id_adresse,id_contact,telephone_boutique,nom_compagnie,mail_boutique,id_rabais) values ('1001','51','0225589663','Velille','velille@gmail.com','7000');
insert into boutique (id_adresse,id_contact,telephone_boutique,nom_compagnie,mail_boutique,id_rabais) values ('1004','51','0245153685','VeloAtout','veloatout@gmail.com','7001');
insert into boutique (id_adresse,id_contact,telephone_boutique,nom_compagnie,mail_boutique,id_rabais) values ('1003','51','0249763128','Transcors','transcors@gmail.com','7002');
insert into boutique (id_adresse,id_contact,telephone_boutique,nom_compagnie,mail_boutique,id_rabais) values ('1002','51','0465153585','Nanticlette','nanticlette@gmail.com','7003');

insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values ('3000',NULL,'1000','12/02/20','16/02/20');
insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values (NULL,'4000','1002','04/03/20','16/05/20');
insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values ('3003','4001','1003','05/03/20','15/02/20');
insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values (NULL,'4002','1002','06/04/20','16/04/20');
insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values (NULL,'4002','1003','07/11/20','21/02/21');
insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values (NULL,'4003','1002','08/10/20','02/01/21');
insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values ('3005',NULL,'1002','09/09/20','17/10/21');
insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values ('3005','4002','1004','10/09/20','19/09/21');
insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values ('3004',NULL,'1002','11/06/20','28/05/21');
insert into commande (id_ind,id_boutique,id_adresse,date_commande,date_livraison) values ('3000','4001','1001','12/08/20','02/05/21');

insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15000',NULL,'300','0','5');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15001','101',NULL,'2','0');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15002','117','305','1','7');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15003',NULL,'302','0','2');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15004','111','302','3','15');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15009','102',NULL,'1','0');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15008','103',NULL,'1','0');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15007',NULL,'302','0','4');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15009','103',NULL,'1','0');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15008','104',NULL,'1','0');
insert into contient (no_commande,no_modele,no_piece,quantite_bicyclette,quantite_piece) values ('15007',NULL,'302','0','4');

insert into fournit (siret_fournisseur,no_piece) values ('500','300');
insert into fournit (siret_fournisseur,no_piece) values ('501','300');
insert into fournit (siret_fournisseur,no_piece) values ('500','301');
insert into fournit (siret_fournisseur,no_piece) values ('502','301');
insert into fournit (siret_fournisseur,no_piece) values ('504','301');
insert into fournit (siret_fournisseur,no_piece) values ('501','302');
insert into fournit (siret_fournisseur,no_piece) values ('505','303');
insert into fournit (siret_fournisseur,no_piece) values ('505','304');
insert into fournit (siret_fournisseur,no_piece) values ('504','304');
insert into fournit (siret_fournisseur,no_piece) values ('502','304');
insert into fournit (siret_fournisseur,no_piece) values ('501','305');
insert into fournit (siret_fournisseur,no_piece) values ('500','306');
insert into fournit (siret_fournisseur,no_piece) values ('500','307');
insert into fournit (siret_fournisseur,no_piece) values ('500','308');
insert into fournit (siret_fournisseur,no_piece) values ('502','308');
insert into fournit (siret_fournisseur,no_piece) values ('503','309');
insert into fournit (siret_fournisseur,no_piece) values ('504','309');
insert into fournit (siret_fournisseur,no_piece) values ('504','310');
insert into fournit (siret_fournisseur,no_piece) values ('504','311');
insert into fournit (siret_fournisseur,no_piece) values ('500','312');
insert into fournit (siret_fournisseur,no_piece) values ('505','312');
insert into fournit (siret_fournisseur,no_piece) values ('502','311');
insert into fournit (siret_fournisseur,no_piece) values ('503','310');

insert into est_compose_de (no_modele,no_piece) values ('100','300');
insert into est_compose_de (no_modele,no_piece) values ('100','301');
insert into est_compose_de (no_modele,no_piece) values ('100','307');
insert into est_compose_de (no_modele,no_piece) values ('100','303');
insert into est_compose_de (no_modele,no_piece) values ('100','304');
insert into est_compose_de (no_modele,no_piece) values ('100','310');
insert into est_compose_de (no_modele,no_piece) values ('100','317');
insert into est_compose_de (no_modele,no_piece) values ('100','312');
insert into est_compose_de (no_modele,no_piece) values ('100','316');

insert into est_compose_de (no_modele,no_piece) values ('101','300');
insert into est_compose_de (no_modele,no_piece) values ('101','301');
insert into est_compose_de (no_modele,no_piece) values ('101','307');
insert into est_compose_de (no_modele,no_piece) values ('101','303');
insert into est_compose_de (no_modele,no_piece) values ('101','304');
insert into est_compose_de (no_modele,no_piece) values ('101','310');
insert into est_compose_de (no_modele,no_piece) values ('101','317');
insert into est_compose_de (no_modele,no_piece) values ('101','312');
insert into est_compose_de (no_modele,no_piece) values ('101','314');
insert into est_compose_de (no_modele,no_piece) values ('101','315');

insert into est_compose_de (no_modele,no_piece) values ('102','300');
insert into est_compose_de (no_modele,no_piece) values ('102','301');
insert into est_compose_de (no_modele,no_piece) values ('102','307');
insert into est_compose_de (no_modele,no_piece) values ('102','303');
insert into est_compose_de (no_modele,no_piece) values ('102','304');
insert into est_compose_de (no_modele,no_piece) values ('102','310');
insert into est_compose_de (no_modele,no_piece) values ('102','317');
insert into est_compose_de (no_modele,no_piece) values ('102','312');
insert into est_compose_de (no_modele,no_piece) values ('102','314');
insert into est_compose_de (no_modele,no_piece) values ('102','315');

insert into est_compose_de (no_modele,no_piece) values ('103','300');
insert into est_compose_de (no_modele,no_piece) values ('103','301');
insert into est_compose_de (no_modele,no_piece) values ('103','307');
insert into est_compose_de (no_modele,no_piece) values ('103','303');
insert into est_compose_de (no_modele,no_piece) values ('103','304');
insert into est_compose_de (no_modele,no_piece) values ('103','310');
insert into est_compose_de (no_modele,no_piece) values ('103','317');
insert into est_compose_de (no_modele,no_piece) values ('103','312');
insert into est_compose_de (no_modele,no_piece) values ('103','314');
insert into est_compose_de (no_modele,no_piece) values ('103','315');

insert into est_compose_de (no_modele,no_piece) values ('104','300');
insert into est_compose_de (no_modele,no_piece) values ('104','306');
insert into est_compose_de (no_modele,no_piece) values ('104','307');
insert into est_compose_de (no_modele,no_piece) values ('104','303');
insert into est_compose_de (no_modele,no_piece) values ('104','304');
insert into est_compose_de (no_modele,no_piece) values ('104','310');
insert into est_compose_de (no_modele,no_piece) values ('104','317');
insert into est_compose_de (no_modele,no_piece) values ('104','312');
insert into est_compose_de (no_modele,no_piece) values ('104','314');
insert into est_compose_de (no_modele,no_piece) values ('104','319');

insert into est_compose_de (no_modele,no_piece) values ('105','300');
insert into est_compose_de (no_modele,no_piece) values ('105','306');
insert into est_compose_de (no_modele,no_piece) values ('105','307');
insert into est_compose_de (no_modele,no_piece) values ('105','303');
insert into est_compose_de (no_modele,no_piece) values ('105','304');
insert into est_compose_de (no_modele,no_piece) values ('105','310');
insert into est_compose_de (no_modele,no_piece) values ('105','317');
insert into est_compose_de (no_modele,no_piece) values ('105','312');
insert into est_compose_de (no_modele,no_piece) values ('105','314');
insert into est_compose_de (no_modele,no_piece) values ('105','315');

insert into est_compose_de (no_modele,no_piece) values ('106','300');
insert into est_compose_de (no_modele,no_piece) values ('106','306');
insert into est_compose_de (no_modele,no_piece) values ('106','307');
insert into est_compose_de (no_modele,no_piece) values ('106','303');
insert into est_compose_de (no_modele,no_piece) values ('106','304');
insert into est_compose_de (no_modele,no_piece) values ('106','310');
insert into est_compose_de (no_modele,no_piece) values ('106','317');
insert into est_compose_de (no_modele,no_piece) values ('106','312');
insert into est_compose_de (no_modele,no_piece) values ('106','314');
insert into est_compose_de (no_modele,no_piece) values ('106','320');

insert into est_compose_de (no_modele,no_piece) values ('107','300');
insert into est_compose_de (no_modele,no_piece) values ('107','306');
insert into est_compose_de (no_modele,no_piece) values ('107','307');
insert into est_compose_de (no_modele,no_piece) values ('107','303');
insert into est_compose_de (no_modele,no_piece) values ('107','304');
insert into est_compose_de (no_modele,no_piece) values ('107','310');
insert into est_compose_de (no_modele,no_piece) values ('107','317');
insert into est_compose_de (no_modele,no_piece) values ('107','312');
insert into est_compose_de (no_modele,no_piece) values ('107','314');
insert into est_compose_de (no_modele,no_piece) values ('107','320');

insert into est_compose_de (no_modele,no_piece) values ('108','300');
insert into est_compose_de (no_modele,no_piece) values ('108','306');
insert into est_compose_de (no_modele,no_piece) values ('108','302');
insert into est_compose_de (no_modele,no_piece) values ('108','303');
insert into est_compose_de (no_modele,no_piece) values ('108','304');
insert into est_compose_de (no_modele,no_piece) values ('108','310');
insert into est_compose_de (no_modele,no_piece) values ('108','317');
insert into est_compose_de (no_modele,no_piece) values ('108','318');
insert into est_compose_de (no_modele,no_piece) values ('108','313');
insert into est_compose_de (no_modele,no_piece) values ('108','314');

insert into est_compose_de (no_modele,no_piece) values ('109','300');
insert into est_compose_de (no_modele,no_piece) values ('109','306');
insert into est_compose_de (no_modele,no_piece) values ('109','302');
insert into est_compose_de (no_modele,no_piece) values ('109','303');
insert into est_compose_de (no_modele,no_piece) values ('109','304');
insert into est_compose_de (no_modele,no_piece) values ('109','310');
insert into est_compose_de (no_modele,no_piece) values ('109','317');
insert into est_compose_de (no_modele,no_piece) values ('109','318');
insert into est_compose_de (no_modele,no_piece) values ('109','314');
insert into est_compose_de (no_modele,no_piece) values ('109','319');

insert into est_compose_de (no_modele,no_piece) values ('110','300');
insert into est_compose_de (no_modele,no_piece) values ('110','306');
insert into est_compose_de (no_modele,no_piece) values ('110','302');
insert into est_compose_de (no_modele,no_piece) values ('110','303');
insert into est_compose_de (no_modele,no_piece) values ('110','304');
insert into est_compose_de (no_modele,no_piece) values ('110','310');
insert into est_compose_de (no_modele,no_piece) values ('110','317');
insert into est_compose_de (no_modele,no_piece) values ('110','318');
insert into est_compose_de (no_modele,no_piece) values ('110','314');
insert into est_compose_de (no_modele,no_piece) values ('110','319');

insert into est_compose_de (no_modele,no_piece) values ('111','300');
insert into est_compose_de (no_modele,no_piece) values ('111','306');
insert into est_compose_de (no_modele,no_piece) values ('111','302');
insert into est_compose_de (no_modele,no_piece) values ('111','303');
insert into est_compose_de (no_modele,no_piece) values ('111','304');
insert into est_compose_de (no_modele,no_piece) values ('111','310');
insert into est_compose_de (no_modele,no_piece) values ('111','317');
insert into est_compose_de (no_modele,no_piece) values ('111','318');
insert into est_compose_de (no_modele,no_piece) values ('111','314');
insert into est_compose_de (no_modele,no_piece) values ('111','319');

insert into est_compose_de (no_modele,no_piece) values ('112','300');
insert into est_compose_de (no_modele,no_piece) values ('112','306');
insert into est_compose_de (no_modele,no_piece) values ('112','302');
insert into est_compose_de (no_modele,no_piece) values ('112','303');
insert into est_compose_de (no_modele,no_piece) values ('112','304');
insert into est_compose_de (no_modele,no_piece) values ('112','310');
insert into est_compose_de (no_modele,no_piece) values ('112','311');
insert into est_compose_de (no_modele,no_piece) values ('112','312');
insert into est_compose_de (no_modele,no_piece) values ('112','313');
insert into est_compose_de (no_modele,no_piece) values ('112','314');

insert into est_compose_de (no_modele,no_piece) values ('113','300');
insert into est_compose_de (no_modele,no_piece) values ('113','301');
insert into est_compose_de (no_modele,no_piece) values ('113','302');
insert into est_compose_de (no_modele,no_piece) values ('113','308');
insert into est_compose_de (no_modele,no_piece) values ('113','304');
insert into est_compose_de (no_modele,no_piece) values ('113','310');
insert into est_compose_de (no_modele,no_piece) values ('113','311');
insert into est_compose_de (no_modele,no_piece) values ('113','312');
insert into est_compose_de (no_modele,no_piece) values ('113','316');
insert into est_compose_de (no_modele,no_piece) values ('113','320');

insert into est_compose_de (no_modele,no_piece) values ('114','300');
insert into est_compose_de (no_modele,no_piece) values ('114','301');
insert into est_compose_de (no_modele,no_piece) values ('114','302');
insert into est_compose_de (no_modele,no_piece) values ('114','308');
insert into est_compose_de (no_modele,no_piece) values ('114','304');
insert into est_compose_de (no_modele,no_piece) values ('114','305');
insert into est_compose_de (no_modele,no_piece) values ('114','311');
insert into est_compose_de (no_modele,no_piece) values ('114','312');
insert into est_compose_de (no_modele,no_piece) values ('114','313');
insert into est_compose_de (no_modele,no_piece) values ('114','316');

insert into est_compose_de (no_modele,no_piece) values ('115','300');
insert into est_compose_de (no_modele,no_piece) values ('115','301');
insert into est_compose_de (no_modele,no_piece) values ('115','302');
insert into est_compose_de (no_modele,no_piece) values ('115','308');
insert into est_compose_de (no_modele,no_piece) values ('115','304');
insert into est_compose_de (no_modele,no_piece) values ('115','310');
insert into est_compose_de (no_modele,no_piece) values ('115','311');
insert into est_compose_de (no_modele,no_piece) values ('115','312');
insert into est_compose_de (no_modele,no_piece) values ('115','316');
insert into est_compose_de (no_modele,no_piece) values ('115','320');

insert into est_compose_de (no_modele,no_piece) values ('116','300');
insert into est_compose_de (no_modele,no_piece) values ('116','301');
insert into est_compose_de (no_modele,no_piece) values ('116','302');
insert into est_compose_de (no_modele,no_piece) values ('116','308');
insert into est_compose_de (no_modele,no_piece) values ('116','309');
insert into est_compose_de (no_modele,no_piece) values ('116','310');
insert into est_compose_de (no_modele,no_piece) values ('116','311');
insert into est_compose_de (no_modele,no_piece) values ('116','312');
insert into est_compose_de (no_modele,no_piece) values ('116','313');
insert into est_compose_de (no_modele,no_piece) values ('116','316');

insert into est_compose_de (no_modele,no_piece) values ('117','300');
insert into est_compose_de (no_modele,no_piece) values ('117','301');
insert into est_compose_de (no_modele,no_piece) values ('117','302');
insert into est_compose_de (no_modele,no_piece) values ('117','308');
insert into est_compose_de (no_modele,no_piece) values ('117','309');
insert into est_compose_de (no_modele,no_piece) values ('117','310');
insert into est_compose_de (no_modele,no_piece) values ('117','311');
insert into est_compose_de (no_modele,no_piece) values ('117','312');
insert into est_compose_de (no_modele,no_piece) values ('117','313');
insert into est_compose_de (no_modele,no_piece) values ('117','316');

insert into adhere (id_ind,id_programme,date_adhesion) values ('3000','1','21/02/20');
insert into adhere (id_ind,id_programme,date_adhesion) values ('3001','2','12/10/20');
insert into adhere (id_ind,id_programme,date_adhesion) values ('3002','1','14/05/20');
insert into adhere (id_ind,id_programme,date_adhesion) values ('3003','3','02/12/20');



-- On modifie les valeurs--
UPDATE adresse SET `no_commande`="15000",`siret_fournisseur`="500",`id_ind`="3000" WHERE `id_adresse`=1000;
UPDATE adresse SET `no_commande`="15001",`siret_fournisseur`="501",`id_boutique`="4000" WHERE `id_adresse`=1001;
UPDATE adresse SET `no_commande`="15002",`siret_fournisseur`="501",`id_ind`="3000" WHERE `id_adresse`=1002;
UPDATE adresse SET `no_commande`="15003",`siret_fournisseur`="504",`id_boutique`="4000" WHERE `id_adresse`=1003;
UPDATE adresse SET `no_commande`="15004",`siret_fournisseur`="504",`id_boutique`="4000" WHERE `id_adresse`=1004;
UPDATE adresse SET `no_commande`="15005",`siret_fournisseur`="504",`id_ind`="3000" WHERE `id_adresse`=1005;

UPDATE contact SET `siret_fournisseur`="500",`id_boutique`=NULL WHERE `id_contact`=50;
UPDATE contact SET `siret_fournisseur`="501",`id_boutique`=NULL WHERE `id_contact`=51;
UPDATE contact SET `siret_fournisseur`="502",`id_boutique`=NULL WHERE `id_contact`=52;
UPDATE contact SET `siret_fournisseur`="503",`id_boutique`=NULL WHERE `id_contact`=53;
UPDATE contact SET `siret_fournisseur`="504",`id_boutique`=NULL WHERE `id_contact`=54;
UPDATE contact SET `siret_fournisseur`="505",`id_boutique`="4000" WHERE `id_contact`=55;
UPDATE contact SET `siret_fournisseur`=NULL,`id_boutique`="4001" WHERE `id_contact`=56;
UPDATE contact SET `siret_fournisseur`=NULL,`id_boutique`="4002" WHERE `id_contact`=57;
UPDATE contact SET `siret_fournisseur`=NULL,`id_boutique`="4003" WHERE `id_contact`=58;

# MODULE GESTION DES STOCKS: REQUETE
SELECT quantite,no_piece from stock natural join estEn natural join piece group by no_piece;
SELECT * from stock natural join estEn WHERE id_piece IS NOT NULL GROUP BY id_piece;
SELECT quantite,siret_fournisseur FROM stock NATURAL JOIN estEn join piece on estEn.id_piece=piece.id_piece GROUP BY siret_fournisseur ;
SELECT quantite,no_modele FROM stock NATURAL JOIN estEn WHERE no_modele IS NOT NULL GROUP BY no_modele;
SELECT SUM(quantite),nom_vélo FROM stock NATURAL JOIN estEn NATURAL JOIN bicyclette WHERE no_modele IS NOT NULL GROUP BY nom_vélo;
SELECT SUM(quantite),grandeur FROM stock NATURAL JOIN estEn NATURAL JOIN bicyclette WHERE no_modele IS NOT NULL GROUP BY grandeur;
SELECT SUM(quantite),ligne_produit FROM stock NATURAL JOIN estEn NATURAL JOIN bicyclette WHERE no_modele IS NOT NULL GROUP BY ligne_produit;
SELECT SUM(quantite),id_modele FROM stock NATURAL JOIN estEn NATURAL JOIN bicyclette NATURAL JOIn modele WHERE no_modele IS NOT NULL GROUP BY id_modele;
SELECT SUM(quantite),id_modele FROM stock NATURAL JOIN estEn NATURAL JOIN bicyclette GROUP BY prix_vélo;

SELECT id_piece FROM stock NATURAL JOIN estEn WHERE quantite='0' and id_piece IS NOT NULL;
DELETE FROM fournisseur Where siret_fournisseur='506';
select siret_fournisseur,no_piece,id_contact,nom_entreprise,no_modele,prix_piece,delai_approvisionnement,quantite from fournisseur natural join piece natural join stock where quantite<='10';
select siret_fournisseur, id_adresse, id_contact, nom_entreprise, libelle, no_piece, no_modele, description_piece, no_produit, prix_piece, delai_approvisionnement, quantite from fournisseur natural join piece natural join stock where quantite <= '2';
select fournisseur.siret_fournisseur,count(*) from fournisseur, fournit, piece WHERE fournisseur.siret_fournisseur=fournit.siret_fournisseur and fournit.no_piece=piece.no_piece group by fournisseur.siret_fournisseur;
select nom_client,sum(prix_piece),sum(prix_vélo) from ind_client, commande, contient, piece, vélo where commande.id_ind=ind_client.id_ind and contient.no_commande=commande.no_commande and contient.no_modele=vélo.no_modele and piece.no_piece=contient.no_piece group by nom_client;
select nom_compagnie,sum(prix_piece),sum(prix_vélo) from commande, contient, piece, vélo, boutique where boutique.id_boutique=commande.id_boutique and contient.no_commande=commande.no_commande and contient.no_modele=vélo.no_modele and piece.no_piece=contient.no_piece group by boutique.nom_compagnie;
select nom_compagnie,sum(prix_piece*quantite_piece) from commande, contient,piece, boutique where boutique.id_boutique=commande.id_boutique and contient.no_commande=commande.no_commande and piece.no_piece=contient.no_piece and contient.no_piece IS NOT NULL group by boutique.nom_compagnie;
select nom_compagnie,sum(prix_vélo*quantite_bicyclette) from commande, contient,vélo, boutique where boutique.id_boutique=commande.id_boutique and contient.no_commande=commande.no_commande and vélo.no_modele=contient.no_modele and contient.no_modele IS NOT NULL group by boutique.nom_compagnie;



