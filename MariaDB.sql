-- MySQL Script generated by MySQL Workbench
-- Mon Aug 17 21:43:44 2020
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema MariaDB
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema MariaDB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `MariaDB` DEFAULT CHARACTER SET utf8 COLLATE utf8_danish_ci ;
USE `MariaDB` ;

-- -----------------------------------------------------
-- Table `MariaDB`.`Table`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `MariaDB`.`Table` (
  `idTable` INT NOT NULL AUTO_INCREMENT,
  `LongURL` VARCHAR(100) NULL,
  `ShortURL` VARCHAR(50) NULL,
  `DateAdd` DATE NULL,
  `Count` INT NULL,
  PRIMARY KEY (`idTable`))
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;