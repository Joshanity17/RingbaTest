using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using ringba_test.Resource;

namespace ringba_test.Shared
{
    public static class DataHelper
    {
        public static int GetLength(string _text)
        {
            return _text.Length;
        }

        public static int GetUppercaseCount(string _text)
        {
            IEnumerable<char> _upperCases = _text.Where(char.IsUpper);
            return _upperCases.Count();
        }

        public static KeyValuePair<string, int> GetMostUsedWord(string _text)
        {
            Regex regex = new Regex(@"([A-Z][a-z]+)");
            MatchCollection wordList = regex.Matches(_text);

            KeyValuePair<string, int> _mostUsedWord = wordList.GroupBy(x => x.Value)
                .ToDictionary(y => y.Key, y => y.Count())
                .OrderByDescending(x => x.Value)
                .First();

            return _mostUsedWord;
        }

        public static KeyValuePair<string, int> GetMostUsedPrefix(string _text, string _englishWords)
        {
            List<MatchCollection> prefixWordList = new List<MatchCollection>();
            dynamic englishWordList = JsonConvert.DeserializeObject(_englishWords);

            foreach (string prefix in Enum.GetNames(typeof(Data.Prefixes)))
            {
                Regex regex = new Regex(@"(" + prefix + @"[a-z]+)");
                prefixWordList.Add(regex.Matches(_text));
            }

            IDictionary<string, int> prefixUsageDetails = new Dictionary<string, int>();

            foreach (MatchCollection prefixSpecificList in prefixWordList)
            {
                int _prefixUsageCount = 0;
                string _currentPrefix = prefixSpecificList.First().Value.Substring(0, 2);
                foreach(Match _word in prefixSpecificList)
                {
                    if (englishWordList[_word.Value.Substring(2)] == 1)
                        _prefixUsageCount++;
                }
                prefixUsageDetails.Add(_currentPrefix, _prefixUsageCount);

            }

            KeyValuePair<string, int> mostUsedPrefix = prefixUsageDetails.OrderByDescending(x => x.Value).First();

            return mostUsedPrefix;
        }

        
    }
}
