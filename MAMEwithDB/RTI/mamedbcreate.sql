--Beneficiari
CREATE TABLE [dbo].[beneficiari] (
    [id_beneficiar]  INT       NOT NULL,
    [nume]           CHAR (20) NOT NULL,
    [prenume]        CHAR (20) DEFAULT (NULL) NULL,
    [CNP]            CHAR (13) NOT NULL,
    [serie_CI]       CHAR (2)  DEFAULT (NULL) NULL,
    [numar_CI]       INT       DEFAULT (NULL) NULL,
    [data_nastere]   DATE      NOT NULL,
    [tip_beneficiar] CHAR (10) DEFAULT (NULL) NULL,
    [id_locatie]     INT       DEFAULT (NULL) NULL,
    [id_tutori]      INT       DEFAULT (NULL) NULL,
    [IBAN]           CHAR (24) DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([id_beneficiar] ASC),
    CONSTRAINT [FK_beneficiari_locatie] FOREIGN KEY ([id_locatie]) REFERENCES [dbo].[locatie]([id_locatie]), 

);




--Beneficii
CREATE TABLE [dbo].[beneficii] (
    [id_beneficiu]    INT       NOT NULL,
    [tip_sprijin]     CHAR (20) DEFAULT (NULL) NULL,
    [locatie sprijin] CHAR (50) DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([id_beneficiu] ASC)
);





--Descriere Caz
CREATE TABLE [dbo].[descriere_caz] (
    [id_beneficiar]           INT  NOT NULL,
    [data_solicitare_sprijin] DATE NOT NULL,
    [descriere_caz]           TEXT NULL,
    PRIMARY KEY CLUSTERED ([id_beneficiar] ASC, [data_solicitare_sprijin] ASC),
    CONSTRAINT [FK_descrierecaz_beneficiari] FOREIGN KEY ([id_beneficiar]) REFERENCES [dbo].[beneficiari] ([id_beneficiar])
);




--Istoric
CREATE TABLE [dbo].[istoric] (
    [id_beneficiar]     INT  NOT NULL,
    [start_beneficiu]   DATE NOT NULL,
    [sfarsit_beneficiu] DATE DEFAULT (NULL) NULL,
    [costuri]           INT  DEFAULT (NULL) NULL,
    [id_beneficiu]      INT  NOT NULL,
    [observatii]        TEXT NOT NULL,
    PRIMARY KEY CLUSTERED ([id_beneficiar] ASC, [start_beneficiu] ASC),
    CONSTRAINT [beneficiikeyid] UNIQUE NONCLUSTERED ([id_beneficiu] ASC),
    CONSTRAINT [FK_istoric_beneficiari] FOREIGN KEY ([id_beneficiar]) REFERENCES [dbo].[beneficiari] ([id_beneficiar]),
    CONSTRAINT [FK_istoric_beneficii] FOREIGN KEY ([id_beneficiu]) REFERENCES [dbo].[beneficii] ([id_beneficiu])
);




--Locatie
CREATE TABLE [dbo].[locatie] (
    [id_locatie]  INT       NOT NULL,
    [tip_locatie] CHAR (10) DEFAULT ('de drept') NOT NULL,
    [oras]        CHAR (20) NOT NULL,
    [judet]       CHAR (20) NOT NULL,
    [strada]      CHAR (20) DEFAULT (NULL) NULL,
    [bloc]        CHAR (5)  DEFAULT (NULL) NULL,
    [scara]       CHAR (5)  DEFAULT (NULL) NULL,
    [numar]       INT       DEFAULT (NULL) NULL,
    [apartament]  INT       DEFAULT (NULL) NULL,
    [etaj]        INT       DEFAULT (NULL) NULL,
    [cod_postal]  INT       DEFAULT (NULL) NULL,
 	PRIMARY KEY CLUSTERED ([id_locatie] ASC),
);




--Tutori
CREATE TABLE "tutori" (
  "id_tutor" int NOT NULL,
  "tip_tutor" char(10) NOT NULL DEFAULT 'mama',
  "nume" char(20) NOT NULL,
  "prenume" char(20) NOT NULL,
  "CNP" char(13) DEFAULT NULL,
  "serie_CI" char(2) DEFAULT NULL,
  "numar_CI" int DEFAULT NULL,
  "telefon" char(20) DEFAULT NULL,
  "e-mail" char(50) DEFAULT NULL,
  "id_locatie" int DEFAULT NULL,
  PRIMARY KEY ("id_tutor","tip_tutor"),
  CONSTRAINT [locatietutorkeyid] UNIQUE ([id_locatie]),
  CONSTRAINT [FK_tutori_locatie] FOREIGN KEY ([id_locatie]) REFERENCES [locatie]([id_locatie]) ON DELETE NO ACTION ON UPDATE NO ACTION, 
  );
