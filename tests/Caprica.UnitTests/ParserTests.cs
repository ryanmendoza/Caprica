using Caprica.GOLDEngine.Parsing;
using Xunit;

namespace Caprica.UnitTests
{
    public class ParserTests
    {
        [Fact]
        public void ParserLoadTablesLoadsGrammarFile()
        {
            var parser = new Parser();
            
            var loaded = parser.LoadTables(GetType(), "Caprica.UnitTests.Grammar.Rules.egt");

            Assert.True(loaded);
        }
        
        [Fact]
        public void ParserReturnsTokenReadParseMessageOnParseOfRuleText()
        {
            var parser = new Parser();
            
            parser.LoadTables(GetType(), "Caprica.UnitTests.Grammar.Rules.egt");
            
            parser.Open("@UserId == 123456");

            var result = parser.Parse();

            Assert.Equal(ParseMessageType.TokenRead, result);
        }
    }
}