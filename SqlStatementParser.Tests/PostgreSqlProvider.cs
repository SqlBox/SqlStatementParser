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
            yield return new TestCaseData(@"
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
                DO $$
                <<first_block>>
                DECLARE
                  counter integer := 0;
                BEGIN 
                   counter := counter + 1;
                   RAISE NOTICE 'The current value of counter is %', counter;
                END first_block ;$$Language sql;
                select 2;", 3);
            yield return new TestCaseData(@"select 1;$$ my block of code; $$;select 2", 3);
            yield return new TestCaseData(@"$$ my block of code; $$ select 1;", 1);
            yield return new TestCaseData(@"DO BEGIN $func$ c; $func$ Language sql", 1);
            yield return new TestCaseData(@"$$ c; $$", 1);
            yield return new TestCaseData(@"commit$$ commit; $$", 1);
            yield return new TestCaseData(@"commit$func$ $inner$ select 1;$inner$; $func$", 1);
            yield return new TestCaseData(@"DO $block$ $ sad $$$ s$bloccc$ #$434// #$`` 'CODE'   $block$commit;", 1);
            yield return new TestCaseData(@"DO $block$ $ sad $$$ s$bloccc$ #$434// #$`` 'CODE'   $block$;commit;", 2);
        }
    }
}
