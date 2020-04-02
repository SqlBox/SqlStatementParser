# C# SQL Statement Parser library
## basic usage <br>
The following sql code should break down into three seperate statements.<br>
```sql
-- First Statement
SELECT * FROM table1; 
		-- Second Statement
                DELIMITER $$ 
                USE `mydatabaser`;
                CREATE DEFINER=`root`@`localhost` PROCEDURE `my_procedure`()
                BEGIN --comment
	                SELECT * FROM table;
                    UPDATE THIS AND SET VAL = 3 ;
                END $$ /*
                    multiline comments
                    select 1;;;
                */
                DELIMITER ;  
                ;
		-- Third Statement
                INSERT INTO T VALUES; 
```
##### Get a list of ranges from every statement found

```csharp
var parser = new SqlStatementParserWrapper(sql, DbType.MYSQL);
 
List<StatementRange> ranges = parser.Parse();
```
- every <b>range</b> contains a <b>start</b> and <b>end</b>.
- <b>start</b> is the index where the statement starts
- <b>end</b> is the length from the start till the end of the statement.
- This is very useful and powerful since its able to handle million lines of code very fast <br>

### Get a list of string statements
```csharp
var parser = new SqlStatementParserWrapper(sql, DbType.MYSQL);
List<string> statements = SqlStatementParserWrapper.convert(parser.sql,parser.Parse());
```

##### Experimental Sql Formatter
```csharp
string myformattedSQL = new Formatter().Format(sql);
```
## Getting started

**SqlStatementParser** is redistributed as a <b> [NuGet package](https://www.nuget.org/packages/protectsoft.SqlStatementParser)</b>. All the code is managed and doesn't have any native dependencies, therefore you are ready to go after referencing the package. This also means the library works on **Windows**, **Linux** and **MacOS X**.

##### namespaces
```csharp
using com.protectsoft.SqlStatementParser;
using com.protectsoft.SqlStatementParser.formatter;
```
