using System;
using System.Collections.Generic;
using System.Text;

namespace com.protectsoft.SqlStatementParser
{
    unsafe public struct StatementRange
    {
        public long start;
        public long end;
        public StatementRange(long start, long end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
