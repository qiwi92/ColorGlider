using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlowGlider.Shared;

namespace HighScoreTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var production = "https://glowglider.azurewebsites.net";
            var debug = "http://localhost:52004";
            var api = new HighScoreApi(production);

            var top10Entries = await api.GetTop10Async();

            foreach (var entry in top10Entries)
            {
                Console.WriteLine(entry);
            }

            Console.WriteLine();

            var random = new Random();
            var publishTasks = new List<Task>();
            Guid newGuid = Guid.NewGuid();
            for (int i = 0; i < 5; i++)
            {
                newGuid = Guid.NewGuid();
                var nameChars = Enumerable.Range(0, random.Next(4, 8))
                    .Select(x => (char) random.Next(65, 90))
                    .ToArray();

                var request = new PublishRequest(newGuid.ToString(), new string(nameChars), random.Next(20, 120));
                var publishTask = api.PublishScore(request);
                await publishTask;
            }

            await Task.WhenAll(publishTasks);

            var aroundEntries = await api.GetRanksAroundPlayer(newGuid);

            foreach (var highScoreData in aroundEntries)
            {
                Console.WriteLine(highScoreData);
            }

            Console.ReadLine();
        }
    }
}
