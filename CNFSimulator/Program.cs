using CNFSimulator;
using Newtonsoft.Json;

Grammar G = JsonConvert.DeserializeObject<Grammar>(File.ReadAllText("Input.json"));
while (true)
{
    Console.WriteLine("Enter a string to test...");
    string str = Console.ReadLine();
    bool inGrammar = G.DetermineMembership(str);
    if (inGrammar) Console.WriteLine($"{str} is in the grammar");
    else Console.WriteLine($"{str} is NOT in the grammar");
}
Console.WriteLine("Done");