using ConsoleTables;

namespace CNFSimulator
{
    /// <summary>
    /// Represents a CNF grammar. Tracks the variables, terminals, rules, and start symbol.
    /// Can validate that the grammar is in CNF and can check if a string would be accepted
    /// using the CYK algorithm.
    /// </summary>
    public class Grammar
    {
        /// <summary>
        /// The list of Variables in the grammar.
        /// </summary>
        public IEnumerable<char> Variables { get; set; }
        /// <summary>
        /// The list of Terminals in the grammar.
        /// </summary>
        public IEnumerable<char> Terminals { get; set; }
        /// <summary>
        /// The set of rules that are used to create productions in the grammar.
        /// </summary>
        public IEnumerable<Rule> Rules { get; set; }
        /// <summary>
        /// The start symbol that the string must be able to start with to be accepted.
        /// </summary>
        public char Start { get; set; }
        /// <summary>
        /// Checks that the grammar is a valid CNF grammar. If it is not, an exception is thrown.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if Grammar is not in CNF. Describes why it is not valid.</exception>
        public void ValidateGrammar()
        {
            if (Variables.Intersect(Terminals).Count() != 0)
            {
                throw new ArgumentException("Some symbols are both terminals and variables");
            }
            if (!Variables.Contains(Start))
            {
                throw new ArgumentException("Start symbol is not a variable");
            }
            foreach (var rule in Rules)
            {
                if (!Variables.Contains(rule.Start))
                {
                    throw new ArgumentException("Grammar contains production not in variables");
                }
                foreach (var production in rule.Productions)
                {
                    //If production goes to two values, should be two variables
                    if (production.Count() == 2)
                    {
                        if (!Variables.Contains(production.ElementAt(0)) && !Variables.Contains(production.ElementAt(1)))
                        {
                            throw new ArgumentException("Grammar contains production with two values that are not both variables");
                        }
                    }
                    //If production goes to one value, ensure it is a terminal
                    else if (production.Count() == 1)
                    {
                        if (!Terminals.Contains(production.First()))
                        {
                            throw new ArgumentException("Grammar contains production that goes to a single non-terminal value");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Grammar contains production that doesnt have 1 or 2 values");
                    }
                }
            }
        }
        /// <summary>
        /// Performs the CYK algorithm to determine if the string is able to be produced by the grammar.
        /// This is a dynamic algorithm and builds a table that holds the symbols that could produce each substring.
        /// If the entire sting could be produced starting the with start symbol, it is accepted.
        /// </summary>
        /// <param name="input">The string being tested by the algorithm.</param>
        /// <returns><c>True</c> if the string could be built by the grammar. <c>False</c> otherwise.</returns>
        public bool DetermineMembership(string input)
        {
            //Run CYK algorithm
            IEnumerable<char>[,] Table = new IEnumerable<char>[input.Length + 1, input.Length + 1];
            //Build the table, set the diagonal to products that could make the terminal in the string.
            for (int i = 1; i <= input.Length; i++)
            {
                Table[i, i] = Rules.Where(r =>
                    r.Productions.Any(p =>
                        p.Count() == 1 && p.First() == input.ToCharArray()[i - 1]))
                    .Select(r => r.Start).ToList();
            }
            //Iterate through each remaining diagonal up to the upper right corner
            for (int len = 2; len <= input.Length; len++)
            {
                for (int i = 1; i <= input.Length - len + 1; i++)
                {
                    int j = i + len - 1;
                    Table[i, j] = new List<char>() { };
                    //Iterate through each pair of cells that the current cell can create
                    for (int k = i; k <= j - 1; k++)
                    {
                        //Get list of start/end variables, find the set of starts that
                        //goes to anything in the first and then anything in the second.
                        IEnumerable<char> firstVariables = Table[i, k];
                        IEnumerable<char> secondVariables = Table[k + 1, j];
                        IEnumerable<char> possibleStarts = Rules.Where(r =>
                            r.Productions.Any(p =>
                                firstVariables.Contains(p.First()) && secondVariables.Contains(p.Last())))
                            .Select(r => r.Start);
                        Table[i, j] = Table[i, j].Union(possibleStarts).ToList();
                    }
                }
            }

            //Build the output table
            var outputTable = new ConsoleTable(Enumerable.Range(1, input.Length).Select(n => n.ToString()).ToArray());
            for (int i = 1; i <= input.Length; i++)
            {
                outputTable.AddRow(Enumerable.Range(1, input.Length)
                    .Select(x => Table[i, x])
                    .Select(cell =>
                    {
                        if (cell is null) return string.Empty;
                        else if (cell.Any()) return string.Join(",", cell);
                        return "NULL";
                    }).ToArray());
            }
            outputTable.Write();
            return Table[1, input.Length].Contains(Start);
        }
    }
}
