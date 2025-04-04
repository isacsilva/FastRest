-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
-- -----------------------------------------------------
-- Schema new_schema1
-- -----------------------------------------------------
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Restaurant`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Restaurant` (
  `id` INT NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `slug` VARCHAR(255) NOT NULL,
  `description` TEXT NOT NULL,
  `avatarImageUrl` VARCHAR(500) NOT NULL,
  `coverImageUrl` VARCHAR(500) NOT NULL,
  `createdAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
  `updatedAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE INDEX (`slug` ASC) VISIBLE);


-- -----------------------------------------------------
-- Table `mydb`.`MenuCategory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`MenuCategory` (
  `id` INT NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `restaurantId` INT NOT NULL,
  `createdAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
  `updatedAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  INDEX `fk_menuCategory_restaurant` (`restaurantId` ASC) VISIBLE,
  CONSTRAINT `fk_menuCategory_restaurant`
    FOREIGN KEY (`restaurantId`)
    REFERENCES `mydb`.`Restaurant` (`id`)
    ON DELETE CASCADE);


-- -----------------------------------------------------
-- Table `mydb`.`Product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Product` (
  `id` INT NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `description` TEXT NOT NULL,
  `price` DECIMAL(10,2) NOT NULL,
  `imageUrl` VARCHAR(500) NOT NULL,
  `restaurantId` INT NOT NULL,
  `menuCategoryId` INT NOT NULL,
  `createdAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
  `updatedAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  INDEX `fk_product_restaurant` (`restaurantId` ASC) VISIBLE,
  INDEX `fk_product_menuCategory` (`menuCategoryId` ASC) VISIBLE,
  CONSTRAINT `fk_product_restaurant`
    FOREIGN KEY (`restaurantId`)
    REFERENCES `mydb`.`Restaurant` (`id`)
    ON DELETE CASCADE,
  CONSTRAINT `fk_product_menuCategory`
    FOREIGN KEY (`menuCategoryId`)
    REFERENCES `mydb`.`MenuCategory` (`id`)
    ON DELETE CASCADE);


-- -----------------------------------------------------
-- Table `mydb`.`OrderTable`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`OrderTable` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `total` DECIMAL(10,2) NOT NULL,
  `status` ENUM('PENDING', 'IN_PREPARATION', 'FINISHED') NOT NULL,
  `consumptionMethod` ENUM('TAKEWAY', 'DINE_IN') NOT NULL,
  `restaurantId` INT NOT NULL,
  `createdAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
  `updatedAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  INDEX `fk_order_restaurant` (`restaurantId` ASC) VISIBLE,
  CONSTRAINT `fk_order_restaurant`
    FOREIGN KEY (`restaurantId`)
    REFERENCES `mydb`.`Restaurant` (`id`)
    ON DELETE CASCADE);


-- -----------------------------------------------------
-- Table `mydb`.`OrderProducts`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`OrderProducts` (
  `id` INT NOT NULL,
  `productId` INT NOT NULL,
  `orderId` INT NOT NULL,
  `quantity` INT NOT NULL,
  `price` DECIMAL(10,2) NOT NULL,
  `createdAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
  `updatedAt` DATETIME NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  INDEX `fk_product_OP` (`productId` ASC) VISIBLE,
  INDEX `fk_OD_OP` (`orderId` ASC) VISIBLE,
  CONSTRAINT `fk_product_OP`
    FOREIGN KEY (`productId`)
    REFERENCES `mydb`.`Product` (`id`)
    ON DELETE CASCADE,
  CONSTRAINT `fk_OD_OP`
    FOREIGN KEY (`orderId`)
    REFERENCES `mydb`.`OrderTable` (`id`)
    ON DELETE CASCADE);


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
