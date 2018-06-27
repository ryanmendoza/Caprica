using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Caprica.Infrastructure.Rules.Parsing;
using Caprica.Infrastructure.Rules.Parsing.Resolvers;
using Caprica.Infrastructure.Security.Interfaces;
using Caprica.Infrastructure.Security.Rules;

namespace PerformanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! Running performance tests on Caprica Rules Engine");

            var ruleTextResolver = new RuleTextResolver { Resolver = Resolver };
            var expressionBuilder = new ExpressionBuilder(ruleTextResolver);
            
            IAuthorizationProvider authorizationProvider = new RuleAuthorizationProvider(expressionBuilder);

            var scenarios = GetTestScenarios();
            
            var random  = new Random();

            Console.WriteLine("Executing 10,000 interations");
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            
            for (var i = 1; i <= 10000; i++)
            {
                var scenario = scenarios.ElementAt(random.Next(scenarios.Count()));               
                authorizationProvider.Authorize(scenario.User.ToRuleAuthorizationContext(), scenario.RuleText);
            }
            
            stopwatch.Stop();
            
            Console.WriteLine($"Completed 10,000 iterations in {stopwatch.Elapsed}");
            
            Console.ReadLine();
        }

        private static string Resolver(string name)
        {
            // This would typically be a DB call for a rule name,
            // but that's not what we are testing here
            switch (name)
            {
                case "Directors":
                    return "(@UserId IN (12345,45678))";
                case "Assistants":
                    return "(@UserId IN (741852,369258))";
                default:
                    return string.Empty;
            }
        }

        private static IEnumerable<TestScenario> GetTestScenarios()
        {
            var user1 = new User(12345, 661448, "Minnesota", "Director");
            var user2 = new User(45678, 101010, "Virginia", "Director");
            var user3 = new User(741852, 248878, "Washington", "Assistant");
            var user4 = new User(369258, 1021420, "Minnesota", "Assistant");
            var user5 = new User(963741, 919058, "Wymoning", "Waterboy");
            var user6 = new User(852123, 6001670, "Minnesota", "Waterboy");
            
            return new List<TestScenario>
            {
                new TestScenario("@UserId == 12345", user1, true),
                new TestScenario("@UserId == 45679", user2, false),
                new TestScenario("@UserId IN (852123)", user6, true),
                new TestScenario("@UserId IN (456369,147789)", user5, false),
                new TestScenario("{{Directors}}", user1, true),
                new TestScenario("{{Directors}}", user3, false),
                new TestScenario("{{Directors}} || {{Assistants}}", user4, true)
            };
        }
    }
}