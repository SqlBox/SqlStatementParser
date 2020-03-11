using com.protectsoft.SqlStatementParser;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlStatementParser.Tests
{
    [TestFixture(typeof(DbType), DbType.POSTGRES)]
    public class TestPostgreSqlStatementParser<T>
    {
        private readonly DbType dbType;
        public TestPostgreSqlStatementParser(T t)
        {
            this.dbType = (DbType)(object)t;
        }

        /*[Test, TestCaseSource(typeof(SqlProvider), "statementProvider")]
        public void TestParser(string sql, int expectedStatements)
        {
            Assert.AreEqual(expectedStatements, new SqlStatementParserWrapper(sql, dbType).Parse().Count);
        }*/

        [Test, TestCaseSource(typeof(PostgreSqlProvider), "postgreSqlstatementProvider")]
        public void TestMysqlParser(string sql, int expectedStatements)
        {
            Assert.AreEqual(expectedStatements, new SqlStatementParserWrapper(sql, dbType).Parse().Count);
        }
/*
        [Test, TestCaseSource(typeof(SqlProvider), "statementProvider")]
        public void TestConvert(string sql, int expectedStatements)
        {
            SqlStatementParserWrapper parser = new SqlStatementParserWrapper(sql, dbType);
            Assert.AreEqual(expectedStatements, SqlStatementParserWrapper.convert(parser.sql, parser.Parse()).Count);
        }

        [Test, TestCaseSource(typeof(PostgreSqlProvider), "postgreSqlstatementProvider")]
        public void TestMySqlConvert(string sql, int expectedStatements)
        {
            SqlStatementParserWrapper parser = new SqlStatementParserWrapper(sql, dbType);
            Assert.AreEqual(expectedStatements, SqlStatementParserWrapper.convert(parser.sql, parser.Parse()).Count);
        }*/
    }
}
