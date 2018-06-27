namespace PerformanceTests
{
    public class TestScenario
    {
        public TestScenario(string ruleText, User user, bool expected)
        {
            RuleText = ruleText;
            User = user;
            Expected = expected;
            Actual = false;
        }
        
        public string RuleText { get; set; }
        public User User { get; set; }
        public bool Expected { get; set; }
        public bool Actual { get; set; }
    }
}