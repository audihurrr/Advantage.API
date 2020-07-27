using System;
using System.Collections.Generic;

namespace Advantage.API
{
    public class Helpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        private static readonly List<string> _businessPrefix = new List<string>()
        {
            "Happy",
            "Trader",
            "Whole",
            "Wholesome",
            "Enterprise",
            "Family",
            "Children",
            "Whizbang",
        };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        private static readonly List<string> _businessSuffix = new List<string>()
        {
            "Joes",
            "Electronics",
            "Dental",
            "Pizzeria",
            "Cafe",
            "Shack",
            "Diner",
            "Boutique",
        };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        private static readonly List<string> _usStates = new List<string>()
        {
            "AK", "AL","AZ",  "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
        };
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static Random _rand = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        internal static string GenerateRandomUniqueCustomer(List<string> names)
        {
            var maxNumberOfNames = _businessPrefix.Count * _businessSuffix.Count;

            if (names.Count >= maxNumberOfNames)
            {
                throw new AdvantageException("names list greater than max size.");
            }

            string newName = GenerateRandomCustomer();

            if (names.Contains(newName)) 
            {
                GenerateRandomUniqueCustomer(names);
            } 

            return newName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Randomly generated customer name string</returns>
        public static string GenerateRandomCustomer()
        {
            string prefix = GetRandom(_businessPrefix);
            string suffix = GetRandom(_businessSuffix);

            return prefix + " " + suffix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static decimal GetRandomOrderTotal()
        {
            return _rand.Next(100, 5000);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        private static string GetRandom(IList<string> names)
        {
            return names[_rand.Next(names.Count)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static string GenerateRandomEmail(string emailAt)
        {
            return $"contact@{emailAt.Replace(" ", "").ToLower()}_{DateTime.Now:fff}.com";
        }

        internal static DateTime GetRandomOrderPlaced()
        {
            var end  = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);
            
            return start + newSpan;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderPlaced"></param>
        /// <returns></returns>
        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;

            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if (timePassed < minLeadTime)
            {
                return null;
            }

            return orderPlaced.AddDays(_rand.Next(7, 14));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static string GenerateRandomState()
        {
            return GetRandom(_usStates);
        }
    }
}