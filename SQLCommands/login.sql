
drop table users;
create table users(
steamid VARCHAR(25) primary key,
bantime DATE,
ip VARCHAR(25)
);
drop table HangarTable;
CREATE TABLE `HangarTable` (
	`planeID` INT NOT NULL,
	`steamID_chara` VARCHAR(25),
	`planeName` VARCHAR(25),
	PRIMARY KEY (`planeID`)
);
drop table SpawnPoints;
CREATE TABLE `SpawnPoints` (
	`steamID_chara` VARCHAR(25),
	`PointID1` BOOLEAN,
	`PointID2` BOOLEAN,
	`PointID3` BOOLEAN,
	`PointID4` BOOLEAN,
	`PointID5` BOOLEAN,
	`PointID6` BOOLEAN,
	PRIMARY KEY (`steamID_chara`)
);
drop table CharacterTable;
CREATE TABLE `CharacterTable` (
	`steamID_chara` VARCHAR(25) NOT NULL,
	`steamID` INT NOT NULL,
	`CharaName` VARCHAR(25),
	`isjailed` DATE,
	`canspawnboat` BOOLEAN NOT NULL,
	`canspawnplane` BOOLEAN NOT NULL,
	`moneybank` INT DEFAULT '0',
	`moneyhand` INT(10) DEFAULT '0',
	`job` VARCHAR(25),
	`ispolice` BOOLEAN,
	`canspawntow` BOOLEAN,
	`isEMC` BOOLEAN,
	PRIMARY KEY (`steamID_chara`)
);
drop table CharacterLookTable;
CREATE TABLE `CharacterLookTable` (
	`lookID` INT NOT NULL,
    `active` BOOL,
	`steamID_chara` VARCHAR(25),
	`face` INT,
	`face_variation` INT,
	`head` INT,
	`head_variation` INT,
	`hair` INT,
	`hair_variation` INT,
	`torso` INT,
	`torso_variation` INT,
	`legs` INT,
	`legs_variation` INT,
	`hands` INT,
	`hands_variation` INT,
	`shoes` INT,
	`shoes_variation` INT,
	`special1` INT,
	`special1_variation` INT,
	`special2` INT,
	`special2_variation` INT,
	`special3` INT,
	`special3_variation` INT,
	`textures` INT,
	`textures_variation` INT,
	`torso2_variation` INT,
	`torso2` INT,
	`hats` INT,
	`hats_variation` INT,
	`glasses` INT,
	`glasses_variation` INT,
	`Unknown3` INT,
	`Unknown3_variation` INT,
	`Unknown4` INT,
	`Unknown4_variation` INT,
	`Unknown5` INT,
	`Unknown5_variation` INT,
	`Watches` INT,
	`Watches_variation` INT,
	`Wristbands` INT,
	`Wristbands_variation` INT,
	`Unknown8` INT,
	`Unknown8_variation` INT,
	`Unknown9` INT,
	`Unknown9_variation` INT,
	PRIMARY KEY (`lookID`)
);
drop table InventoryTable;
CREATE TABLE `InventoryTable` (
	`steamID_chara` VARCHAR(25),
	`slot0` INT,
	`slot1` INT,
	`slot2` INT,
	`slot3` INT,
	`slot4` INT,
	`slot5` INT,
	`slot6` INT,
	`slot7` INT,
	`slot8` INT,
	`slot9` INT,
	`slot10` INT,
	`slot11` INT,
	`slot12` INT,
	`slot13` INT,
	`slot14` INT,
	`slot15` INT,
	`slot16` INT,
	`slot17` INT,
	`slot18` INT,
	`slot19` INT,
	PRIMARY KEY (`steamID_chara`)
);
drop table CarTable;
CREATE TABLE `CarTable` (
	`steamID_chara` VARCHAR(25),
    `carID` INT NOT NULL,
    `licensePlate` VARCHAR(25),
	`vehicleName` INT,
	`primaryColor` VARCHAR(25),
	`secondaryColor` VARCHAR(25),
	`pearlColor` VARCHAR(25),
	`wheelType` VARCHAR(25),
	`neonLights` VARCHAR(25) COMMENT 'string of "left:right:back:front"with all permutations of them',
	`windowTint` VARCHAR(25),
	`insert-name` INT,
	`Spoilers` INT ,
	`FrontBumper` INT,
	`RearBumper` INT,
	`SideSkirt` INT,
	`Exhaust` INT,
	`Frame` INT,
	`Grille` INT,
	`Hood` INT,
	`Fender` INT,
	`RightFender` INT,
	`Roof` INT,
	`Engine` INT,
	`Brakes` INT,
	`Transmission` INT,
	`Horns` INT,
	`Suspension` INT,
	`Armor` INT,
	`FrontWheel` INT,
	`RearWheel` INT,
	`PlateHolder` INT,
	`VanityPlates` INT,
	`TrimDesign` INT,
	`Ornaments` INT,
	`Dashboard` INT,
	`DialDesign` INT,
	`DoorSpeakers` INT,
	`Seats` INT,
	`SteeringWheels` INT,
	`ColumnShifterLevers` INT,
	`Plaques` INT,
	`Speakers` INT,
	`Trunk` INT,
	`Hydraulics` INT,
	`EngineBlock` INT,
	`AirFilter` INT,
	`Struts` INT,
	`ArchCover` INT,
	`Aerials` INT,
	`Trim` INT,
	`Tank` INT,
	`Windows` INT,
	`Livery` INT, 
	PRIMARY KEY (`carID`)
);
drop table BoatTable;
CREATE TABLE `BoatTable` (
	`boatID` INT NOT NULL,
	`steamID_chara` VARCHAR(25),
	`boatName` VARCHAR(25),
	PRIMARY KEY (`boatID`)
);
drop table WeaponTable;
CREATE TABLE `WeaponTable` (
	`steamID_chara` VARCHAR(25),
	`melee` INT,
	`offhand` INT,
	`mainweapon` INT,
	PRIMARY KEY (`steamID_chara`)
);