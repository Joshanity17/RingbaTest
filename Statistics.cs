using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ringba_test.Shared;

namespace ringba_test
{
    class Statistics
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fething Pre-requisite data");
            Task<string> rumbledWords = Task.Run(async () =>await Http.GetString("https://ringba-test-html.s3-us-west-1.amazonaws.com/TestQuestions/output.txt"));
            rumbledWords.Wait();

            Task<string> englishWords = Task.Run(async () => await Http.GetString("https://raw.githubusercontent.com/dwyl/english-words/master/words_dictionary.json"));
            englishWords.Wait();
            Console.WriteLine("Data retrieved. Processing...");

            Console.WriteLine("Total Letter Count: " + DataHelper.GetLength(rumbledWords.Result));
            Console.WriteLine("Total Uppercase Count: " + DataHelper.GetUppercaseCount(rumbledWords.Result));

            KeyValuePair<string, int> mostUsedWord = DataHelper.GetMostUsedWord(rumbledWords.Result);
            Console.WriteLine("Most used word: " + mostUsedWord.Key);
            Console.WriteLine("Usage Count: " + mostUsedWord.Value);

            KeyValuePair<string, int> mostUsedPrefix = DataHelper.GetMostUsedPrefix(rumbledWords.Result, englishWords.Result);
            Console.WriteLine("Most used prefix: " + mostUsedPrefix.Key);
            Console.WriteLine("Usage Count: " + mostUsedPrefix.Value);
        }
    }
}
