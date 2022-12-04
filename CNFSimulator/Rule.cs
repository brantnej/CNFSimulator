using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFSimulator
{
    /// <summary>
    /// Represents a rule that contains a start symbol 
    /// and a list of items it can go to in a CNF grammar.
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// The start symbol of the rule.
        /// </summary>
        public char Start { get; set; }
        /// <summary>
        /// The list of Productions (each production is a 
        /// list of 1 or 2 chars) that the rule can go to.
        /// </summary>
        public IEnumerable<IEnumerable<char>> Productions { get; set; }
    }
}
