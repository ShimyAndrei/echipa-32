-- --------------------------------------------------------
-- Server:                       127.0.0.1
-- Versiune server:              10.1.22-MariaDB - mariadb.org binary distribution
-- SO server:                    Win64
-- HeidiSQL Versiune:            9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Descarcă structura bazei de date pentru asociatia_mame_db
CREATE DATABASE "mamedb" /*!40100 DEFAULT CHARACTER SET latin1 */;
USE "mamedb";

-- Descarcă structura pentru tabelă asociatia_mame_db.beneficiari
CREATE TABLE  "beneficiari" (
  "id_beneficiar" int(10)  NOT NULL ,
  "nume" char(20) NOT NULL,
  "prenume" char(20) DEFAULT NULL,
  "CNP" char(13) NOT NULL,
  "serie_CI" char(2) DEFAULT NULL,
  "numar_CI" int(6)  DEFAULT NULL,
  "data_nastere" date NOT NULL,
  "tip_beneficiar" char(10) DEFAULT NULL,
  "id_locatie" int(10)  DEFAULT NULL,
  "id_tutori" int(11)  DEFAULT NULL,
  "IBAN" char(24) DEFAULT NULL,
  PRIMARY KEY ("id_beneficiar"),
  --KEY "LOCATIE" ("id_locatie"),
  CONSTRAINT "LOCATIE" FOREIGN KEY ([id_locatie]) REFERENCES [locatie] ([id_locatie])
) ;
--ENGINE=InnoDB =2 DEFAULT CHARSET=latin1;

-- Exportarea datelor nu a fost selectată.
-- Descarcă structura pentru tabelă asociatia_mame_db.beneficii
CREATE TABLE  "beneficii" (
  "id_beneficiu" int(10)  NOT NULL ,
  "tip_sprijin" char(20) DEFAULT NULL,
  "locatie sprijin" char(50) DEFAULT NULL,
  PRIMARY KEY ("id_beneficiu")
) ;
--ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Exportarea datelor nu a fost selectată.
-- Descarcă structura pentru tabelă asociatia_mame_db.descriere_caz
CREATE TABLE  "descriere_caz" (
  "id_beneficiar" int(10)  NOT NULL,
  "data_solicitare_sprijin" date NOT NULL,
  "descriere_caz" text,
  PRIMARY KEY ("id_beneficiar","data_solicitare_sprijin"),
  CONSTRAINT "DESCRIERE CAZ" FOREIGN KEY ("id_beneficiar") REFERENCES "beneficiari" ("id_beneficiar")
) ;
--ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Exportarea datelor nu a fost selectată.
-- Descarcă structura pentru tabelă asociatia_mame_db.istoric
CREATE TABLE  "istoric" (
  "id_beneficiar" int(10)  NOT NULL,
  "start_beneficiu" date NOT NULL,
  "sfarsit_beneficiu" date DEFAULT NULL,
  "costuri" int(10)  DEFAULT NULL,
  "id_beneficiu" int(10)  NOT NULL,
  "observatii" text NOT NULL,
  PRIMARY KEY ("id_beneficiar","start_beneficiu"),
 -- KEY "BENEFICII" ("id_beneficiu"),
  CONSTRAINT "BENEFICII" FOREIGN KEY ("id_beneficiu") REFERENCES "beneficii" ("id_beneficiu"),
  CONSTRAINT "ISTORIC" FOREIGN KEY ("id_beneficiar") REFERENCES "beneficiari" ("id_beneficiar")
) ;
--ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Exportarea datelor nu a fost selectată.
-- Descarcă structura pentru tabelă asociatia_mame_db.locatie
CREATE TABLE  "locatie" (
  "id_locatie" int(10)  NOT NULL ,
  "tip_locatie" char(10) NOT NULL DEFAULT 'de drept',
  "oras" char(20) NOT NULL,
  "judet" char(20) NOT NULL,
  "strada" char(20) DEFAULT NULL,
  "bloc" char(5) DEFAULT NULL,
  "scara" char(5) DEFAULT NULL,
  "numar" int(10)  DEFAULT NULL,
  "apartament" int(4)  DEFAULT NULL,
  "etaj" int(2) DEFAULT NULL,
  "cod_postal" int(6)  DEFAULT NULL,
  PRIMARY KEY ("id_locatie","tip_locatie")
);
--ENGINE=InnoDB =2 DEFAULT CHARSET=latin1;

-- Exportarea datelor nu a fost selectată.
-- Descarcă structura pentru tabelă asociatia_mame_db.tutori
CREATE TABLE  "tutori" (
  "id_tutor" int(10)  NOT NULL ,
  "tip_tutor" char(10) NOT NULL DEFAULT 'mama',
  "nume" char(20) NOT NULL,
  "prenume" char(20) NOT NULL,
  "CNP" char(13) DEFAULT NULL,
  "serie_CI" char(2) DEFAULT NULL,
  "numar_CI" int(6)  DEFAULT NULL,
  "telefon" char(20) DEFAULT NULL,
  "e-mail" char(50) DEFAULT NULL,
  "id_locatie" int(10)  DEFAULT NULL,
  PRIMARY KEY ("id_tutor","tip_tutor"),
  --KEY "LOCATIE_TUTORI" ("id_locatie"),
  CONSTRAINT "LOCATIE_TUTORI" FOREIGN KEY ("id_locatie") REFERENCES "locatie" ("id_locatie") ON DELETE NO ACTION ON UPDATE NO ACTION
);
-- ENGINE=InnoDB =133 DEFAULT CHARSET=latin1;

-- Exportarea datelor nu a fost selectată.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
