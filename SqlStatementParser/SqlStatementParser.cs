using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser
{
    public abstract class SqlStatementParser
    {
        private readonly string originalSql;

        public SqlStatementParser(string originalSqlRef)
        {
            originalSql = originalSqlRef;
        }

        unsafe internal virtual bool isLineBreak(char* head, char* line_break)
        {
            if (*line_break == '\0')
                return false;


            while (*head != '\0' && *line_break != '\0' && *head == *line_break)
            {
                head++;
                line_break++;
            }
            return *line_break == '\0';
        }

        unsafe internal virtual char* skip_leading_whitespace(char* head, char* tail)
        {
            while (head < tail && *head <= ' ')
                head++;
            return head;
        }

        // checks if its only one char(usually happens with multi semicolons)
        unsafe internal virtual bool includeInRange(long start, long end)
        {
            return (originalSql.Substring((int)start, (int)end).Trim().Length > 1);
        }

        unsafe abstract internal void determineStatementRanges(char* sql, int length, string initial_delimiter, List<StatementRange> ranges, string line_break);

    }
}
