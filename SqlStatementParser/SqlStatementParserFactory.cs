using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser
{
    class SqlStatementParserFactory
    {
        private SqlStatementParserFactory() { }

        internal static SqlStatementParser createSqlStatementParser(string sql, DbType dbType)
        {
            switch(dbType)
            {
                case DbType.MYSQL:
                case DbType.MARIADB:
                    return new MySqlStatementParser(sql, false);
                case DbType.SQLITE:
                    return new SqlLiteSqlStatementParser(sql);
                case DbType.POSTGRES:
                    return new PostgreSqlStatementParser(sql);
                case DbType.ORACLE:
                    return new OracleStatementParser(sql);
                case DbType.SQLSERVER:
                    return new SqlServerStatementParser(sql);
                case DbType.DB2:
                    return new Db2StatementParser(sql);
            }
            throw new Exception("Database Not Supported");
        }
    }
}