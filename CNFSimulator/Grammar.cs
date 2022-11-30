using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFSimulator
{
    public class Grammar
    {
        public IEnumerable<string> Variables { get; set; }
        public IEnumerable<string> Terminals { get; set; }
        public IEnumerable<Rule> Rules { get; set; }
        public string Start { get; set; }
        public bool DetermineMembership(string input)
        {
            IEnumerable<string>[,] Table = new IEnumerable<string>[input.Length + 1, input.Length + 1];
            for (int i = 1; i <= input.Length; i++)
            {
                Table[i, i] = Rules.Where(r => 
                    r.Productions.Any(p => 
                        p.Count() == 1 && p.First() == input.ToCharArray()[i - 1].ToString()))
                    .Select(r => r.Start).ToList();
            }
            for (int len = 2; len <= input.Length; len++)
            {
                for (int i = 1; i <= input.Length - len + 1; i++)
                {
                    int j = i + len - 1;
                    Table[i, j] = new List<string>() { };
                    for (int k = i; k <= j - 1; k++)
                    {
                        IEnumerable<string> firstVariables = Table[i, k];
                        IEnumerable<string> secondVariables = Table[k + 1, j];
                        IEnumerable<string> possibleStarts = Rules.Where(r => 
                            r.Productions.Any(p => 
                                firstVariables.Contains(p.First()) && secondVariables.Contains(p.Last())))
                            .Select(r => r.Start);
                        Table[i, j] = Table[i,j].Union(possibleStarts).ToList();
                    }
                }
            }
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
