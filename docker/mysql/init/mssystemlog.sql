CREATE DATABASE /*!32312 IF NOT EXISTS*/`mssystemlog` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `mssystemlog`;

/*Table structure for table `log` */

DROP TABLE IF EXISTS `log`;

CREATE TABLE `log` (
  `Id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `Application` varchar(50) DEFAULT NULL,
  `Logged` datetime DEFAULT NULL,
  `Level` varchar(50) DEFAULT NULL,
  `Message` text,
  `Logger` varchar(250) DEFAULT NULL,
  `Callsite` varchar(512) DEFAULT NULL,
  `Exception` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=124169 DEFAULT CHARSET=utf8;
