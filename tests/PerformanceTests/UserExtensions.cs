using System.Linq;
using System.Reflection;
using Caprica.Helpers;
using Caprica.Infrastructure.Security.Rules;

namespace PerformanceTests
{
    public static class UserExtensions
    {
        /// <summary>
        /// TOOD
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// TOOD
        /// </returns>
        public static RuleAuthorizationContext ToRuleAuthorizationContext(this User user)
        {
            Guard.IsNotNull(user);

            var dictionary = user.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(user, null));

            return new RuleAuthorizationContext(dictionary);
        }
    }
}