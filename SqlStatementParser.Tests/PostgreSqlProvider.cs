using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SqlStatementParser.Tests
{
    public class PostgreSqlProvider
    {
        public static IEnumerable postgreSqlstatementProvider()
        {
/*            yield return new TestCaseData(@"
                SELECT * FROM TABLE1;
                DO $$ 
                <<first_block>>
                DECLARE
                  counter integer := 0;
                BEGIN 
                   counter := counter + 1;
                   RAISE NOTICE 'The current value of counter is %', counter;
                END first_block $$", 2);
            yield return new TestCaseData(@"
                SELECT * FROM TABLE1;
                DO  $$   
                <<first_block>>
                DECLARE
                  counter integer := 0;
                BEGIN 
                   counter := counter + 1;
                   RAISE NOTICE 'The current value of counter is %', counter;
                END first_block  $$   select 1;", 3);
            yield return new TestCaseData(@"select 1;$$ my block of code; $$", 2);
            yield return new TestCaseData(@"$$ my block of code; $$;select 1", 2);*/
            yield return new TestCaseData(@"$f$ c; $f$", 1);
        }
    }
}
