using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser
{
    public enum DbType : int
    {
        MYSQL = 0,
        MARIADB = 1,
        ORACLE = 2,
        POSTGRES = 3,
        SQLSERVER = 4,
        SQLITE = 5,
        DB2 = 6
    }
}
