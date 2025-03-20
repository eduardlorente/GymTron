
DROP DATABASE IF EXISTS `sql7761283`;
CREATE DATABASE sql7761283;

USE sql7761283;

-- CREATE TABLE --

DROP TABLE IF EXISTS `Routines`;
CREATE TABLE `Routines` (
  `Id` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `ExerciseParameters`;
CREATE TABLE `ExerciseParameters` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Pattern` varchar(255) NOT NULL,
  `ReplaysInReserve` int(11) NOT NULL,
  `TypeId` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=48 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `Trainings`;
CREATE TABLE `Trainings` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoutineId` int(11) NOT NULL,
  `DayOfWeek` int(11) NOT NULL,
  `StartedOn` datetime NOT NULL,
  `CompletedOn` datetime DEFAULT NULL,
  `Status` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `RoutineId` (`RoutineId`),
  CONSTRAINT `Trainings_ibfk_1` FOREIGN KEY (`RoutineId`) REFERENCES `Routines` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `RoutineItems`;
CREATE TABLE `RoutineItems` (
  `Id` int(11) NOT NULL,
  `RoutineId` int(11) NOT NULL,
  `DayOfWeek` int(11) NOT NULL,
  `ExerciseParametersId` int(11) NOT NULL,
  `MinRestTimeInSeconds` int(11) DEFAULT NULL,
  `MaxRestTimeInSeconds` int(11) DEFAULT NULL,
  `AlternatingSeries` bit(1) NOT NULL,
  `Active` bit(1) NOT NULL,
  `Order` tinyint(4) DEFAULT '0',
  `Series` int(11) NOT NULL,
  `RepetitionsMin` int(11) DEFAULT NULL,
  `RepetitionsMax` int(11) DEFAULT NULL,
  `Duration` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `ExerciseParametersId` (`ExerciseParametersId`),
  CONSTRAINT `RoutineItems_ibfk_1` FOREIGN KEY (`ExerciseParametersId`) REFERENCES `ExerciseParameters` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `Exercises`;
CREATE TABLE `Exercises` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TrainingId` int(11) NOT NULL,
  `ExerciseParametersId` int(11) NOT NULL,
  `Weight` decimal(10,2) DEFAULT NULL,
  `Duration` int(11) DEFAULT NULL,
  `Repetitions` int(11) DEFAULT NULL,
  `CreatedOn` datetime NOT NULL,
  `Observations` text,
  PRIMARY KEY (`Id`),
  KEY `TrainingId` (`TrainingId`),
  KEY `ExerciseParametersId` (`ExerciseParametersId`),
  CONSTRAINT `Exercises_ibfk_1` FOREIGN KEY (`TrainingId`) REFERENCES `Trainings` (`Id`),
  CONSTRAINT `Exercises_ibfk_2` FOREIGN KEY (`ExerciseParametersId`) REFERENCES `ExerciseParameters` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `BodyWeights`;
CREATE TABLE BodyWeights (
    Id INT NOT NULL AUTO_INCREMENT,
    `Weight` DECIMAL(10,2) NOT NULL,
    IMC DECIMAL(10,2) NOT NULL,
    CreatedOn DATETIME NOT NULL,
    PRIMARY KEY (Id)
) DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `Logs`;
CREATE TABLE Logs (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CreatedOn DATETIME,
    Message VARCHAR(255)
);


-- ENCODING --
ALTER DATABASE sql7761283 CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
ALTER TABLE Routines CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
ALTER TABLE ExerciseParameters CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
ALTER TABLE Trainings CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
ALTER TABLE RoutineItems CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
ALTER TABLE Exercises CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
ALTER TABLE BodyWeights CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
ALTER TABLE Logs CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;



-- NOURISH DATA --

INSERT INTO Routines (Id, `Name`)
VALUES 
	(1, 'MARÇ 2024'),
	(2, 'MAIG 2024'),
	(3, 'OCTUBRE 2024'),
	(4, 'DESEMBRE 2024');
	
INSERT INTO `ExerciseParameters` (`Id`, `Name`, `Pattern`, `ReplaysInReserve`, `TypeId`) VALUES
(1, 'AB ROLL', 'ABD', 2, 1),
(2, 'ABDUCCIONES DE CADERA DESDE POLEA BAJA', 'ABDUC', 2, 1),
(3, 'CRUCES AL FRENTE EN POLEA ALTA-INCLINADO A 45 GRADOS', 'PUSH/PEC', 2, 1),
(4, 'CRUCES DESDE POLEA BAJA EN BANCO A 30 GRADOS', 'PEC', 2, 1),
(5, 'CURL DE BICEPS ALTERNO DE PIE', 'PULL/BIC', 2, 1),
(6, 'CURL DE BICEPS CON BARRA RECTA', 'PULL/BIC', 2, 1),
(7, 'CURL DE BICEPS CON BARRA Z', 'PULL/BIC', 2, 1),
(8, 'CURL FEMORAL SENTADO', 'PULL/ISQ', 2, 1),
(9, 'ELEVACION DE TALONES DE PIE UNILATERAL', 'GEM', 2, 1),
(10, 'ELEVACIONES DE PIERNAS RECTAS TUMBADO BOCARRIBA', 'ABD', 0, 1),
(11, 'ELEVACIONES DE RODILLAS AL PECHO COLGADO EN POLEA', 'ABD', 1, 1),
(12, 'ELEVACIONES FRONTALES EN POLEA BAJA', 'PUSH/DT', 1, 1),
(13, 'ELEVACIONES LATERALES ALTERNAS EN POLEA', 'PUSH/DT', 2, 1),
(14, 'ELEVACIONES LATERALES CON MANCUERNAS', 'PUSH/DT', 2, 1),
(15, 'ENCOGIMIENTOS EN POLEA DE RODILLAS', 'ABD', 2, 1),
(16, 'EXTENSIONES DE CODO EN POLEA ALTA CON BARRA RECTA', 'PULL/TR', 2, 1),
(17, 'HIP THRUST', 'HIP/GLT', 2, 1),
(18, 'LEG EXTENSION SENTADO', 'QUAD', 2, 1),
(19, 'NORDIC CURL', 'HIP/ISQ', 2, 1),
(20, 'PATADA TRASERA DESDE POLEA BAJA', 'GLT', 1, 1),
(21, 'PATADAS DE RANA', 'GEM', 2, 1),
(22, 'PESO MUERTO RUMANO-ATRASAR CADERAS', 'HIP/PULL/ISQ', 2, 1),
(23, 'PESO MUERTO SUMO, CADA REP DESDE EL SUELO', 'HIP/PULL', 4, 1),
(24, 'PESO MUERTO, CADA REP DESDE EL SUELO', 'HIP/PULL', 3, 1),
(25, 'PLANCHA HORIZONTAL', 'CORE', 0, 2),
(26, 'PLANCHA LATERAL', 'CORE', 0, 2),
(27, 'POLEA ALTA AGARRE ANCHO NEUTRO INCLINADO', 'PULL', 2, 1),
(28, 'POLEA ALTA AGARRE ANCHO PRONO INCLINADO', 'PULL', 2, 1),
(29, 'POLEA ALTA AGARRE INVERTIDO CERRADO', 'PULL', 1, 1),
(30, 'POLEA ALTA AGARRE SUPINO CERRADO', 'PULL', 2, 1),
(31, 'POLEA BAJA AGARRE NEUTRO ESTRECHO INCLINADO A 45 GRADOS', 'PULL', 2, 1),
(32, 'PRENSA INCLINADA SENTADO', 'PUSH/QUAD', 3, 1),
(33, 'PRESS BANCA, TOCAR Y SUBIR SIN REBOTAR', 'PUSH PEC', 3, 1),
(34, 'PRESS FRANCÉS EZ', 'PUSH/TR', 2, 1),
(35, 'PRESS MANCUERNAS A 30 GRADOS', 'PUSH PEC', 2, 1),
(36, 'PRESS MANCUERNAS A 75 GRADOS', 'PUSH/DT', 2, 1),
(37, 'PRESS MILITAR SENTADO A 75 GRADOS CON BARRA', 'PUSH/DT', 4, 1),
(38, 'PULL THROUGH EN POLEA ALTURA DE CADERA', 'HIP/GLT', 1, 1),
(39, 'REMO CON MANCUERNA EN BANCO A 30 GRADOS', 'PULL', 2, 1),
(40, 'REMO PENDLAY, ESPALDA PARALELA AL SUELO', 'PUSH PEC', 4, 1),
(41, 'REMO SENTADO EN AGARRE ANCHO NEUTRO', 'PULL', 2, 1),
(42, 'REMO SENTADO EN AGARRE NEUTRO ESTRECHO', 'PULL', 3, 1),
(43, 'SENTADILLA BULGARA PIE DE ATRAS A 15CM', 'QUAD', 2, 1),
(44, 'SENTADILLA CONTRA PARED', 'QUAD', 0, 2),
(45, 'SENTADILLA HACK EN PRENSA', 'QUAD', 2, 1),
(46, 'SENTADILLA LIBRE CON BARRA / SUBPARALELA', 'QUAD/HIP', 3, 1),
(47, 'ZANCADAS INVERTIDAS ALTERNAS, RODILLA AL SUELO', 'QUAD/UNIL', 2, 1);



INSERT INTO `RoutineItems` (
    `Id`, `RoutineId`, `DayOfWeek`, `ExerciseParametersId`, 
    `MinRestTimeInSeconds`, `MaxRestTimeInSeconds`, `AlternatingSeries`, 
    `Active`, `Order`, `Series`, `RepetitionsMin`, 
    `RepetitionsMax`, `Duration`
) VALUES
(1, 1, 1, 46, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(2, 1, 1, 33, 120, 180, 0, 1, 2, 4, 7, 10, NULL),
(3, 1, 1, 41, 120, NULL, 0, 1, 3, 3, 7, 10, NULL),
(4, 1, 1, 32, 120, NULL, 0, 1, 4, 3, 8, 12, NULL),
(5, 1, 1, 8, 60, NULL, 1, 1, 5, 3, 8, 12, NULL),
(6, 1, 1, 14, 60, NULL, 1, 1, 6, 3, 8, 15, NULL),
(7, 1, 1, 9, 30, NULL, 1, 1, 7, 3, 8, 15, NULL),
(8, 1, 1, 25, 30, NULL, 1, 1, 8, 3, NULL, NULL, 30),
(9, 1, 2, 37, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(10, 1, 2, 22, 120, NULL, 0, 1, 2, 3, 7, 10, NULL),
(11, 1, 2, 47, 120, NULL, 0, 1, 3, 3, 8, 12, NULL),
(12, 1, 2, 31, 120, NULL, 0, 1, 4, 3, 8, 12, NULL),
(13, 1, 2, 17, 120, NULL, 0, 1, 5, 3, 8, 12, NULL),
(14, 1, 2, 16, 30, NULL, 1, 1, 6, 3, 8, 12, NULL),
(15, 1, 2, 7, 30, NULL, 1, 1, 7, 3, 8, 15, NULL),
(16, 1, 2, 21, 30, NULL, 1, 1, 8, 3, 8, 15, NULL),
(17, 1, 3, 24, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(18, 1, 3, 43, 120, NULL, 0, 1, 2, 3, 8, 12, NULL),
(19, 1, 3, 27, 120, NULL, 0, 1, 3, 3, 8, 12, NULL),
(20, 1, 3, 36, 120, NULL, 0, 1, 4, 3, 8, 12, NULL),
(21, 1, 3, 18, 60, NULL, 1, 1, 5, 3, 8, 15, NULL),
(22, 1, 3, 4, 60, NULL, 1, 1, 6, 3, 8, 15, NULL),
(23, 1, 3, 9, 30, NULL, 1, 1, 7, 3, 8, 15, NULL),
(24, 1, 3, 26, 30, NULL, 1, 1, 8, 3, NULL, NULL, 30),
(25, 2, 1, 46, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(26, 2, 1, 37, 120, 180, 0, 1, 2, 4, 7, 10, NULL),
(27, 2, 1, 47, 120, NULL, 0, 1, 3, 3, 8, 12, NULL),
(28, 2, 1, 17, 120, 180, 0, 1, 4, 3, 8, 12, NULL),
(29, 2, 1, 35, 60, NULL, 1, 1, 5, 3, 8, 12, NULL),
(30, 2, 1, 14, 60, NULL, 1, 1, 6, 3, 8, 15, NULL),
(31, 2, 1, 2, 60, NULL, 1, 1, 7, 3, 8, 12, NULL),
(32, 2, 1, 21, 60, NULL, 1, 1, 8, 3, 8, 15, NULL),
(33, 2, 2, 23, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(34, 2, 2, 22, 120, 180, 0, 1, 2, 3, 7, 10, NULL),
(35, 2, 2, 42, 120, NULL, 0, 1, 3, 3, 7, 10, NULL),
(36, 2, 2, 8, 120, NULL, 0, 1, 4, 3, 8, 12, NULL),
(37, 2, 2, 28, 60, NULL, 1, 1, 5, 3, 8, 12, NULL),
(38, 2, 2, 8, 60, NULL, 1, 1, 6, 3, 8, 12, NULL),
(39, 2, 2, 5, 30, NULL, 1, 1, 7, 3, 8, 12, NULL),
(40, 2, 2, 25, 30, NULL, 1, 1, 8, 3, NULL, NULL, 30),
(41, 2, 3, 33, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(42, 2, 3, 40, 120, 180, 0, 1, 2, 3, 7, 10, NULL),
(43, 2, 3, 32, 120, 180, 0, 1, 3, 3, 8, 12, NULL),
(44, 2, 3, 36, 120, NULL, 0, 1, 4, 3, 8, 12, NULL),
(45, 2, 3, 18, 120, NULL, 0, 1, 5, 3, 8, 15, NULL),
(46, 2, 3, 16, 30, NULL, 1, 1, 6, 3, 8, 12, NULL),
(47, 2, 3, 9, 30, NULL, 1, 1, 7, 3, 8, 15, NULL),
(48, 2, 3, 15, 30, NULL, 1, 1, 8, 3, 8, 15, NULL),
(49, 3, 1, 46, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(50, 3, 1, 17, 60, NULL, 0, 1, 2, 3, 8, 12, NULL),
(51, 3, 1, 47, 60, NULL, 0, 1, 3, 3, 8, 12, NULL),
(52, 3, 1, 22, 120, 180, 0, 1, 4, 3, 7, 10, NULL),
(53, 3, 1, 18, 60, NULL, 1, 1, 5, 3, 8, 15, NULL),
(54, 3, 1, 19, 60, NULL, 1, 1, 6, 3, 8, 15, NULL),
(55, 3, 1, 20, 30, NULL, 1, 1, 7, 3, 8, 12, NULL),
(56, 3, 1, 1, 30, NULL, 1, 1, 8, 3, 8, 15, NULL),
(57, 3, 2, 37, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(58, 3, 2, 33, 120, 180, 0, 1, 2, 4, 7, 10, NULL),
(59, 3, 2, 29, 60, NULL, 0, 1, 3, 3, 8, 12, NULL),
(60, 3, 2, 35, 60, NULL, 0, 1, 4, 3, 8, 12, NULL),
(61, 3, 2, 16, 60, NULL, 0, 1, 5, 3, 8, 12, NULL),
(62, 3, 2, 12, 60, NULL, 1, 1, 6, 3, 8, 12, NULL),
(63, 3, 2, 15, 30, NULL, 1, 1, 7, 3, 8, 15, NULL),
(64, 3, 2, 9, 30, NULL, 1, 1, 8, 3, 8, 15, NULL),
(65, 3, 3, 23, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(66, 3, 3, 32, 60, NULL, 0, 1, 2, 3, 8, 12, NULL),
(67, 3, 3, 8, 60, NULL, 0, 1, 3, 3, 8, 12, NULL),
(68, 3, 3, 43, 60, NULL, 0, 1, 4, 3, 8, 12, NULL),
(69, 3, 3, 38, 60, NULL, 1, 1, 5, 3, 8, 12, NULL),
(70, 3, 3, 2, 60, NULL, 1, 1, 6, 3, 8, 12, NULL),
(71, 3, 3, 9, 30, NULL, 1, 1, 7, 3, 8, 15, NULL),
(72, 3, 3, 11, 30, NULL, 1, 1, 8, 3, 8, 15, NULL),
(73, 4, 1, 46, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(74, 4, 1, 22, 120, NULL, 0, 1, 2, 3, 7, 10, NULL),
(75, 4, 1, 32, 120, NULL, 0, 1, 3, 3, 8, 12, NULL),
(76, 4, 1, 44, 60, NULL, 1, 1, 4, 3, NULL, NULL, 30),
(77, 4, 1, 19, 60, NULL, 1, 1, 5, 3, 8, 15, NULL),
(78, 4, 1, 21, 30, NULL, 1, 1, 6, 3, 8, 15, NULL),
(79, 4, 1, 9, 30, NULL, 1, 1, 7, 3, 8, 15, NULL),
(80, 4, 2, 33, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(81, 4, 2, 40, 120, NULL, 0, 1, 2, 3, 7, 10, NULL),
(82, 4, 2, 3, 60, NULL, 1, 1, 3, 3, 7, 10, NULL),
(83, 4, 2, 28, 60, NULL, 1, 1, 4, 3, 8, 12, NULL),
(84, 4, 2, 34, 30, NULL, 1, 1, 5, 3, 8, 12, NULL),
(85, 4, 2, 6, 30, NULL, 1, 1, 6, 3, 8, 12, NULL),
(86, 4, 2, 1, 30, NULL, 1, 1, 7, 3, 8, 15, NULL),
(87, 4, 3, 24, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(88, 4, 3, 44, 120, NULL, 0, 1, 2, 3, NULL, NULL, 30),
(89, 4, 3, 17, 120, NULL, 0, 1, 3, 3, 8, 12, NULL),
(90, 4, 3, 45, 120, NULL, 0, 1, 4, 3, 8, 12, NULL),
(91, 4, 3, 8, 30, NULL, 1, 1, 5, 3, 8, 12, NULL),
(92, 4, 3, 9, 30, NULL, 1, 1, 6, 3, 8, 15, NULL),
(93, 4, 3, 15, 30, NULL, 1, 1, 7, 3, 8, 15, NULL),
(94, 4, 4, 37, 120, 180, 0, 1, 1, 4, 7, 10, NULL),
(95, 4, 4, 39, 120, NULL, 0, 1, 2, 3, 8, 12, NULL),
(96, 4, 4, 35, 120, NULL, 0, 1, 3, 3, 8, 12, NULL),
(97, 4, 4, 30, 60, NULL, 1, 1, 4, 3, 8, 12, NULL),
(98, 4, 4, 4, 60, NULL, 1, 1, 5, 3, 8, 15, NULL),
(99, 4, 4, 13, 30, NULL, 1, 1, 6, 3, 7, 10, NULL),
(100, 4, 4, 25, 30, NULL, 1, 1, 7, 3, NULL, NULL, 30);
	
	
	
INSERT INTO BodyWeights (Id, `Weight`, IMC, CreatedOn)
VALUES 
    (1, 108.1, 0, '2024-02-29 18:00:00'),
    (2, 112, 0, '2024-04-03 18:00:00'),
    (3, 110, 0, '2024-05-06 18:00:00'),
    (4, 107.2, 0, '2024-06-06 18:00:00'),
    (5, 104.7, 0, '2024-07-04 18:00:00'),
    (6, 102.6, 0, '2024-09-02 18:00:00'),
    (7, 102.1, 0, '2024-10-02 18:00:00'),
    (8, 102.3, 0, '2024-11-06 18:00:00'),
    (9, 101.3, 0, '2024-12-10 18:00:00'),
    (10, 101.2, 36.1, '2025-02-10 18:00:00');
	