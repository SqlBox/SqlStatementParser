using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SqlStatementParser.Tests
{
    //Generic standard sql statements
    public class SqlProvider
    {

        public static IEnumerable statementProvider()
        {
            yield return new TestCaseData(@"select * from table c; ; ; ; ", 1);
            yield return new TestCaseData(@"select * from table c", 1);
            yield return new TestCaseData(@"select * from mytable; create table2(`id` not null /* ;ignored semicolon;*/ );;    ;     ;; ;; ;    ;   " +
                "; \n select * from table1 where t.prop like 'commnet;' and id = 3; -- trailing; comment;;;;;", 3);
            yield return new TestCaseData(@"select * from something      update asd set asd = 3", 1);
            yield return new TestCaseData(@"SET @crs = 0; // declaration
                --here your query
                @crs := @crs+1 // assignment", 2);
        }

    }
}
