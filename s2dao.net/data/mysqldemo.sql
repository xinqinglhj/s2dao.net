CREATE DATABASE `s2demo` /*!40100 DEFAULT CHARACTER SET utf8 */;
use s2demo;
CREATE TABLE `s2demo`.`EMP2` (
  `EMPNO` INTEGER UNSIGNED NOT NULL DEFAULT 0,
  `ENAME` VARCHAR(10) DEFAULT '',
  `DEPTNUM` SMALLINT UNSIGNED DEFAULT 0,
  PRIMARY KEY(`EMPNO`)
)
ENGINE = InnoDB;

INSERT INTO EMP2 VALUES(7369,'SMITH',20);
INSERT INTO EMP2 VALUES(7499,'ALLEN',30);