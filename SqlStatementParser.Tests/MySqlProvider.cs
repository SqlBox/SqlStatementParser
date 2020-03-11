using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SqlStatementParser.Tests
{
    public class MySqlProvider
    {
        public static IEnumerable mysqlStatementProvider()
        {
            yield return new TestCaseData(@"Select * from table1; 
                                COMMIT;
                DELIMITER   $$   
                USE `moviecould`;
                CREATE DEFINER=`root`@`localhost` PROCEDURE `ptest3`()
                BEGIN

	                select * from fasts limit 3;
                    select * from fasts limit 3;
                END     $$      
                DELIMITER ;  
                ;
                D D d asd
                insert t set something;
                DELIM;", 5);
            yield return new TestCaseData(@"Select * from table1;
                DELIMITER   / 
                USE `moviecould`;
                CREATE DEFINER=`root`@`localhost` PROCEDURE `ptest3`()
                BEGIN

	                select * from fasts limit 3;
                    select * from fasts limit 3;
                END     /    
                DELIMITER ;  ; ", 2);
            yield return new TestCaseData(@"Select * from `table1`;
                DELIMITER   //
                USE `moviecould`;
                CREATE DEFINER=`root`@`localhost` PROCEDURE `ptest3`()
                BEGIN
                    
	                select * from `fasts` limit 3;
                    select * from 'fasts' limit 3;
                END     // DELIMITER ;;
                select 1", 3);
            yield return new TestCaseData(@"Select * from `table1`;
                DELIMITER   //
                USE `moviecould`;
                CREATE DEFINER=`root`@`localhost` PROCEDURE `ptest3`()
                BEGIN
                    
	                select * from `fasts` limit 3;
                    select * from 'fasts' limit 3;
                END     // DELIMITER ;;
                -- select * from table;
                select 1", 3);
            yield return new TestCaseData(@"Select * from `table1`;
                DELIMITER   //
                USE `moviecould`;
                CREATE DEFINER=`root`@`localhost` PROCEDURE `ptest3`()
                BEGIN
                    
	                select * from `fasts` limit 3;
                    select * from 'fasts' limit 3;
                END     // DELIMITER ;;
                --select * from table;
                # select * from table;
                /*
                select 1;
                */
                select 1", 3);
            yield return new TestCaseData(@"DELIMITER $$
                DROP PROCEDURE IF EXISTS insert_ten_rows $$ //first
                CREATE PROCEDURE insert_ten_rows ()  // second from here to end
                    BEGIN
                        DECLARE crs INT DEFAULT 0;
                        WHILE crs < 10 DO
                            INSERT INTO `continent`(`name`) VALUES ('cont'+crs);
                            SET crs = crs + 1;
                        END WHILE;
                    END $$
                DELIMITER ;", 2);
            yield return new TestCaseData(@"DELIMITER Ω
                DROP PROCEDURE IF EXISTS insert_ten_rows Ω //first
                CREATE PROCEDURE insert_ten_rows ()  // second from here to end
                    BEGIN
                        DECLARE crs INT DEFAULT 0;
                        WHILE crs < 10 DO
                            INSERT INTO `continent`(`name`) VALUES ('cont'+crs);
                            SET crs = crs + 1;
                        END WHILE;
                    END Ω
                DELIMITER ;", 2);
            yield return new TestCaseData(@"DELIMITER /
                CREATE DEFINER=`root`@`localhost` PROCEDURE `ptest11`()
                BEGIN
                SELECT SLEEP(2);
	                select * from casts limit 3;
                    select * from casts limit 3;
	                END   /
                DELIMITER ;
                CREATE TABLE `movie` (
                  `movieid` int(11) NOT NULL,
                  `imdbid` varchar(8) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL,
                  `title` varchar(50) DEFAULT NULL,
                  `plot` text,
                  `altplot` text,
                  `date` text,
                  `year` int(11) DEFAULT NULL,
                  `month` int(11) DEFAULT NULL,
                  `day` int(11) DEFAULT NULL,
                  `genre` varchar(50) DEFAULT NULL,
                  `ratings` int(11) DEFAULT NULL,
                  `ratingvalue` float DEFAULT NULL,
                  `contentrating` varchar(45) DEFAULT NULL,
                  `poster` text,
                  PRIMARY KEY (`movieid`,`imdbid`),
                  UNIQUE KEY `id_UNIQUE` (`movieid`),
                  UNIQUE KEY `imdbid_UNIQUE` (`imdbid`),
                  KEY `index_year` (`year`) USING BTREE,
                  KEY `index_ratings` (`ratings`) USING BTREE,
                  KEY `index_genres` (`genre`) USING BTREE,
                  KEY `index_title` (`title`) USING BTREE,
                  KEY `content_rating_index` (`contentrating`)
                ) set something else as well here in one create statement
                   like index(mpla mpla)

                ENGINE=InnoDB DEFAULT CHARSET=utf8;", 2);
        }
    }
}
