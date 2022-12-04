using CNFSimulator;
using Newtonsoft.Json;

//Build and validate grammar
Grammar G = new Grammar();
try
{
    G = JsonConvert.DeserializeObject<Grammar>(File.ReadAllText("Input.json"));
    G.ValidateGrammar();
}
catch (Exception e)
{
    Console.WriteLine(e);
    Environment.Exit(1);
}

//Take input from the user and check if it is in the grammar
string input = "";
while (true)
{
    Console.WriteLine("Enter a string to test (/q to quit)...");
    input = Console.ReadLine();
    if (string.Compare(input, "/q") == 0)
    {
        break;
    }
    if (!string.IsNullOrWhiteSpace(input))
    {
        bool inGrammar = G.DetermineMembership(input);
        if (inGrammar) Console.WriteLine($"{input} is in the grammar");
        else Console.WriteLine($"{input} is NOT in the grammar");
    }
}