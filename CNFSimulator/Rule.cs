using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNFSimulator
{
    public class Rule
    {
        public string Start { get; set; }
        public IEnumerable<IEnumerable<string>> Productions { get; set; }
    }
}
