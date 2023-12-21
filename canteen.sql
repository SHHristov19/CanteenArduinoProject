-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 21, 2023 at 12:54 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `canteen`
--

-- --------------------------------------------------------

--
-- Table structure for table `menu`
--

CREATE TABLE `menu` (
  `MenuId` varchar(20) NOT NULL,
  `Appetizer` varchar(255) DEFAULT NULL,
  `Main` varchar(255) DEFAULT NULL,
  `Dessert` varchar(255) DEFAULT NULL,
  `Day` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `menu`
--

INSERT INTO `menu` (`MenuId`, `Appetizer`, `Main`, `Dessert`, `Day`) VALUES
('1', 'Супа от тиква с карамел', 'Печено пиле със зеленчуци', 'Шоколадов мус с ягоди', 'Понеделник'),
('10', 'Крем супа от броколи', 'Мекици с пилешко филе', 'Панна кота с плодове', 'Петък'),
('11', 'Карпачо от телешко', 'Лазаня с бял сос', 'Ванилово сладоледено изкушение', NULL),
('2', 'Капрезе салата', 'Паста \"Болонезе\"', 'Тирамису', 'Вторник'),
('3', 'Крокети с пикантен сос', 'Сьомга с лимонов сос', 'Панакота с карамел', 'Сряда'),
('4', 'Гъби на скара с чесън', 'Мусака', 'Крем брюле', 'Четвъртък'),
('5', 'Ролца с прясно мляко', 'Пиле \"Корма\" с ориз и зеленчуци', 'Плодове в шоколад', 'Петък'),
('6', 'Салата \"Цезар\"', 'Теляти стек върху картофен пюре', 'Чийзкейк с плодове', 'Понеделник'),
('7', 'Печени чушки със сирене', 'Оризото с пиле и зеленчуци', 'Тирамису', 'Вторник'),
('8', 'Прошуто и маслини', 'Паста със сьомга и сос от лимон', 'Шоколадов брауни', 'Сряда'),
('9', 'Авокадо салата', 'Свинско филе на скара', 'Черен шоколадов мус', 'Четвъртък');

-- --------------------------------------------------------

--
-- Table structure for table `reader`
--

CREATE TABLE `reader` (
  `Uid` varchar(20) NOT NULL,
  `Time` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `reader`
--

INSERT INTO `reader` (`Uid`, `Time`) VALUES
('9035d1ba', '2023-12-20 23:49:38');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `Id` varchar(80) NOT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Password` varchar(20) DEFAULT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`Id`, `Username`, `Password`, `FirstName`, `LastName`) VALUES
('12a3f015', 'user2', 'password2', 'Petar', 'Petrov'),
('2131152b', 'User4', 'pass', 'Dragan', 'Draganov'),
('9035d1ba', 'user1', 'password1', 'Ivan', 'Ivanov'),
('bd31152b', 'admin', 'adminpass', 'admin', 'admin'),
('c1131141', 'user3', 'password3', 'Stefan', 'Hristov');

-- --------------------------------------------------------

--
-- Table structure for table `usermenu`
--

CREATE TABLE `usermenu` (
  `UserId` varchar(20) NOT NULL,
  `MenuId` varchar(20) NOT NULL,
  `Date` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `usermenu`
--

INSERT INTO `usermenu` (`UserId`, `MenuId`, `Date`) VALUES
('9035d1ba', '1', '2023-12-18'),
('12a3f015', '6', '2023-12-18'),
('c1131141', '1', '2023-12-18'),
('9035d1ba', '2', '2023-12-19'),
('12a3f015', '2', '2023-12-19'),
('c1131141', '7', '2023-12-19'),
('9035d1ba', '8', '2023-12-20'),
('12a3f015', '8', '2023-12-20'),
('c1131141', '3', '2023-12-20'),
('9035d1ba', '4', '2023-12-21'),
('12a3f015', '4', '2023-12-21'),
('c1131141', '4', '2023-12-21'),
('9035d1ba', '5', '2023-12-22'),
('12a3f015', '10', '2023-12-22'),
('c1131141', '5', '2023-12-22');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `menu`
--
ALTER TABLE `menu`
  ADD PRIMARY KEY (`MenuId`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `usermenu`
--
ALTER TABLE `usermenu`
  ADD KEY `MenuId` (`MenuId`),
  ADD KEY `UserId` (`UserId`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `usermenu`
--
ALTER TABLE `usermenu`
  ADD CONSTRAINT `usermenu_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`),
  ADD CONSTRAINT `usermenu_ibfk_2` FOREIGN KEY (`MenuId`) REFERENCES `menu` (`MenuId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
