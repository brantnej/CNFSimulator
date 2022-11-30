using CNFSimulator;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

Grammar G = JsonConvert.DeserializeObject<Grammar>(File.ReadAllText("Input.json"));
bool inGrammar = G.DetermineMembership("aaabbb");
Console.WriteLine("Done");