SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [Style_snowboard] (
  [Id_style] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Style] text NOT NULL
);
CREATE TABLE [Parametre_application] (
  [Id_parametre] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [device] text NOT NULL
);
CREATE TABLE [Niveau_snowboard] (
  [Id_niveau] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Niveau] text NOT NULL
);
CREATE TABLE [Marque_snowboard] (
  [Id_marque] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Marque] text NOT NULL
);
CREATE TABLE [Genre_snowboard] (
  [Id_genre] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Genre] text NOT NULL
);
CREATE TABLE [Planche_snowboard] (
  [Id_planche] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Stock] bigint NOT NULL
, [Prix_euro] numeric(53,0) NOT NULL
, [Prix_dollar] numeric(53,0) NOT NULL
, [Fk_niveau] bigint NOT NULL
, [Fk_marque] bigint NOT NULL
, [Fk_genre] bigint NOT NULL
, [Fk_style] bigint NOT NULL
, [Nom_modele] text DEFAULT test NOT NULL
, CONSTRAINT [FK_Planche_snowboard_0_0] FOREIGN KEY ([Fk_style]) REFERENCES [Style_snowboard] ([Id_style]) ON DELETE NO ACTION ON UPDATE NO ACTION
, CONSTRAINT [FK_Planche_snowboard_1_0] FOREIGN KEY ([Fk_genre]) REFERENCES [Genre_snowboard] ([Id_genre]) ON DELETE NO ACTION ON UPDATE NO ACTION
, CONSTRAINT [FK_Planche_snowboard_2_0] FOREIGN KEY ([Fk_marque]) REFERENCES [Marque_snowboard] ([Id_marque]) ON DELETE NO ACTION ON UPDATE NO ACTION
, CONSTRAINT [FK_Planche_snowboard_3_0] FOREIGN KEY ([Fk_niveau]) REFERENCES [Niveau_snowboard] ([Id_niveau]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
CREATE TABLE [Employe] (
  [Id_employe] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Nom] text NOT NULL
, [Prenom] text NOT NULL
, [Login] text NOT NULL
, [Password] image NOT NULL
, [Groupe] text NOT NULL
, [Salt] image NOT NULL
);
CREATE TABLE [Client] (
  [Id_client] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Nom] text NOT NULL
, [Prenom] text NOT NULL
, [Numero_telephone] text NOT NULL
);
CREATE TABLE [Location] (
  [Id_location] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Moyen_paiement] text NOT NULL
, [Date_debut] datetime NOT NULL
, [Date_fin] datetime NOT NULL
, [Tva] numeric(53,0) NOT NULL
, [En_cours] text DEFAULT "Location" NOT NULL
, [Fk_Client] bigint NOT NULL
, CONSTRAINT [FK_Location_0_0] FOREIGN KEY ([Fk_Client]) REFERENCES [Client] ([Id_client]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
CREATE TABLE [Planche_louee] (
  [Id_planche_louee] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Prix_location_euro] numeric(53,0) NOT NULL
, [Prix_location_dollar] numeric(53,0) NOT NULL
, [Fk_planche] bigint NOT NULL
, [Fk_location] bigint NULL
, [Quantite] bigint DEFAULT 3 NOT NULL
, CONSTRAINT [FK_Planche_louee_0_0] FOREIGN KEY ([Fk_location]) REFERENCES [Location] ([Id_location]) ON DELETE NO ACTION ON UPDATE NO ACTION
, CONSTRAINT [FK_Planche_louee_1_0] FOREIGN KEY ([Fk_planche]) REFERENCES [Planche_snowboard] ([Id_planche]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
CREATE TRIGGER [fki_Planche_snowboard_Fk_style_Style_snowboard_Id_style] BEFORE Insert ON [Planche_snowboard] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Planche_snowboard violates foreign key constraint FK_Planche_snowboard_0_0') WHERE (SELECT Id_style FROM Style_snowboard WHERE  Id_style = NEW.Fk_style) IS NULL; END;
CREATE TRIGGER [fku_Planche_snowboard_Fk_style_Style_snowboard_Id_style] BEFORE Update ON [Planche_snowboard] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Planche_snowboard violates foreign key constraint FK_Planche_snowboard_0_0') WHERE (SELECT Id_style FROM Style_snowboard WHERE  Id_style = NEW.Fk_style) IS NULL; END;
CREATE TRIGGER [fki_Planche_snowboard_Fk_genre_Genre_snowboard_Id_genre] BEFORE Insert ON [Planche_snowboard] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Planche_snowboard violates foreign key constraint FK_Planche_snowboard_1_0') WHERE (SELECT Id_genre FROM Genre_snowboard WHERE  Id_genre = NEW.Fk_genre) IS NULL; END;
CREATE TRIGGER [fku_Planche_snowboard_Fk_genre_Genre_snowboard_Id_genre] BEFORE Update ON [Planche_snowboard] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Planche_snowboard violates foreign key constraint FK_Planche_snowboard_1_0') WHERE (SELECT Id_genre FROM Genre_snowboard WHERE  Id_genre = NEW.Fk_genre) IS NULL; END;
CREATE TRIGGER [fki_Planche_snowboard_Fk_marque_Marque_snowboard_Id_marque] BEFORE Insert ON [Planche_snowboard] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Planche_snowboard violates foreign key constraint FK_Planche_snowboard_2_0') WHERE (SELECT Id_marque FROM Marque_snowboard WHERE  Id_marque = NEW.Fk_marque) IS NULL; END;
CREATE TRIGGER [fku_Planche_snowboard_Fk_marque_Marque_snowboard_Id_marque] BEFORE Update ON [Planche_snowboard] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Planche_snowboard violates foreign key constraint FK_Planche_snowboard_2_0') WHERE (SELECT Id_marque FROM Marque_snowboard WHERE  Id_marque = NEW.Fk_marque) IS NULL; END;
CREATE TRIGGER [fki_Planche_snowboard_Fk_niveau_Niveau_snowboard_Id_niveau] BEFORE Insert ON [Planche_snowboard] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Planche_snowboard violates foreign key constraint FK_Planche_snowboard_3_0') WHERE (SELECT Id_niveau FROM Niveau_snowboard WHERE  Id_niveau = NEW.Fk_niveau) IS NULL; END;
CREATE TRIGGER [fku_Planche_snowboard_Fk_niveau_Niveau_snowboard_Id_niveau] BEFORE Update ON [Planche_snowboard] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Planche_snowboard violates foreign key constraint FK_Planche_snowboard_3_0') WHERE (SELECT Id_niveau FROM Niveau_snowboard WHERE  Id_niveau = NEW.Fk_niveau) IS NULL; END;
CREATE TRIGGER [fki_Location_Fk_Client_Client_Id_client] BEFORE Insert ON [Location] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Location violates foreign key constraint FK_Location_0_0') WHERE (SELECT Id_client FROM Client WHERE  Id_client = NEW.Fk_Client) IS NULL; END;
CREATE TRIGGER [fku_Location_Fk_Client_Client_Id_client] BEFORE Update ON [Location] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Location violates foreign key constraint FK_Location_0_0') WHERE (SELECT Id_client FROM Client WHERE  Id_client = NEW.Fk_Client) IS NULL; END;
CREATE TRIGGER [fki_Planche_louee_Fk_location_Location_Id_location] BEFORE Insert ON [Planche_louee] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Planche_louee violates foreign key constraint FK_Planche_louee_0_0') WHERE NEW.Fk_location IS NOT NULL AND(SELECT Id_location FROM Location WHERE  Id_location = NEW.Fk_location) IS NULL; END;
CREATE TRIGGER [fku_Planche_louee_Fk_location_Location_Id_location] BEFORE Update ON [Planche_louee] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Planche_louee violates foreign key constraint FK_Planche_louee_0_0') WHERE NEW.Fk_location IS NOT NULL AND(SELECT Id_location FROM Location WHERE  Id_location = NEW.Fk_location) IS NULL; END;
CREATE TRIGGER [fki_Planche_louee_Fk_planche_Planche_snowboard_Id_planche] BEFORE Insert ON [Planche_louee] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Planche_louee violates foreign key constraint FK_Planche_louee_1_0') WHERE (SELECT Id_planche FROM Planche_snowboard WHERE  Id_planche = NEW.Fk_planche) IS NULL; END;
CREATE TRIGGER [fku_Planche_louee_Fk_planche_Planche_snowboard_Id_planche] BEFORE Update ON [Planche_louee] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Planche_louee violates foreign key constraint FK_Planche_louee_1_0') WHERE (SELECT Id_planche FROM Planche_snowboard WHERE  Id_planche = NEW.Fk_planche) IS NULL; END;
COMMIT;

