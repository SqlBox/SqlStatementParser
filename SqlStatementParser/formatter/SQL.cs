using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser.formatter
{
    // Standard sql vocabulary
    // There is still missing sql std ascii words
    sealed public class SQL
    {
        public static readonly string[] SQL_FUNCTIONS = { "min", "max", "sum", "count",
                "charindex", "upper", "lower", "replace", "len", "left", "right", "rtrim", "ltrim", "substring",
                "getdate", "dateadd", "datediff", "datepart", "year", "month", "day", "hour", "minute", "second",
                "coalesce", "cast", "convert", "avg", "abs","power", "round" };


        public static readonly string[] SQL_WORDS = {"select","from","where","and","in", "not", "in","exists","like",
            "join", "inner", "left", "right","outer", "union","all","commit","rollback", "use", "set","insert",
            "alter","drop","update","delete","except","create","table","null","key","default","constraint","foreign",
            "references"};

    }
}
