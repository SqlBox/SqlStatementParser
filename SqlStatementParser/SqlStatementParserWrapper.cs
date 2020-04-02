using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser
{
    public class SqlStatementParserWrapper
    {
        public readonly string sql;
        private readonly DbType dbType;
        public SqlStatementParserWrapper(string sql, DbType dbType)
        {
            this.sql = sql;
            this.dbType = dbType;
        }

        // returns a list<struct::StatementRange> with the ranges only without the sql!!.
        // This method is fast and capable of handling millions of lines of code
        public List<StatementRange> Parse()
        {
            unsafe
            {
                List<StatementRange> ranges = new List<StatementRange>();
                SqlStatementParser p = SqlStatementParserFactory.createSqlStatementParser(sql, dbType);
                fixed (char* s = sql)
                {
                    p.determineStatementRanges(s, sql.Length,";", ranges, "\n");
                    return ranges;
                }
            }
        }

        // In case that someone need to convert a List<StatementRange> to ready to use list<string> with the sql statements!
        // The two methods are seperated and optional 
        // for the need of speed that provides the Parse method(capable of handling hundreds of thousands of lines of sql)
        // optional trim default to true, trim the statements the start and end
        public static List<string> convert(string sql,List<StatementRange> ranges, bool trim = true)
        {
            List<string> pairs = new List<string>();
            foreach (StatementRange p in ranges)
            {
                string statement = sql.Substring((int)p.start, (int)p.end);
                if (trim)
                {
                    statement = statement.Trim();
                }
                pairs.Add(statement);
            }
            return pairs;
        }


    }
}
