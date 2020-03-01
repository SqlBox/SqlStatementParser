using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser
{
    class PostgreSqlStatementParser : SqlStatementParser
    {
        public PostgreSqlStatementParser(string originalSqlRef) : base(originalSqlRef)
        {
        }

        internal override unsafe void determineStatementRanges(char* sql, int length, string initial_delimiter, List<StatementRange> ranges, string line_break)
        {
            throw new NotImplementedException();
        }
    }
}
