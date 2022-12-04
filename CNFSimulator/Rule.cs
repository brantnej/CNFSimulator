using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFSimulator
{
    public class Rule
    {
        public char Start { get; set; }
        public IEnumerable<IEnumerable<char>> Productions { get; set; }
    }
}
