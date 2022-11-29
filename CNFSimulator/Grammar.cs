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
        public IEnumerable<string> Inputs { get; set; }
        public void DetermineMembership()
        {

        }
    }
}
