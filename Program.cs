using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleTables;
// using ConsoleTables;

namespace ApiClient
{
    class Joke
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("setup")]
        public string Setup { get; set; }

        [JsonPropertyName("punchline")]
        public string PunchLine { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var responseAsStream = await client.GetStreamAsync("https://official-joke-api.appspot.com/jokes/programming/ten");

            var jokes = await JsonSerializer.DeserializeAsync<List<Joke>>(responseAsStream);

            // foreach (var joke in jokes)
            // {
            //     Console.WriteLine($"The joke type is {joke.Type}, has a setup line of {joke.Setup}, and has a punchline of {joke.PunchLine}");
            // }

            var table = new ConsoleTable("Type", "Setup", "Punchline");

            foreach (var joke in jokes)
            {
                table.AddRow(joke.Type, joke.Setup, joke.PunchLine);
            }

            table.Write();
        }
    }
}

