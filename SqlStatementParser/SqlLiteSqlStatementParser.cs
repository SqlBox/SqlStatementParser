using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser
{
    // At the moment Mysql is a superSet for sqlite and the parser has a complete coverage
    class SqlLiteSqlStatementParser : MySqlStatementParser
    {
        public SqlLiteSqlStatementParser(string originalSqlRef) : base(originalSqlRef, false)
        {
        }
    }
}
