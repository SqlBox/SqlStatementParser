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
            if (dbType == DbType.MYSQL)
            {
                return new MySqlStatementParser(sql, false);
            }
            else if (dbType == DbType.MARIADB)
            {
                return new MySqlStatementParser(sql, true);
            }
            else if (dbType == DbType.SQLITE)
            {
                return new SqlLiteSqlStatementParser(sql);
            }
            else if (dbType == DbType.POSTGRES)
            {
                return new PostgreSqlStatementParser(sql);
            }

            throw new Exception("Database Not Supported");
        }
    }
}
