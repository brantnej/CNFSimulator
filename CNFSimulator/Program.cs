using CNFSimulator;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

Grammar G = JsonConvert.DeserializeObject<Grammar>(File.ReadAllText("Input.json"));
Console.WriteLine("Done");