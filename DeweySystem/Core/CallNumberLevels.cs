using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweySystem.Core
{
    public class CallNumberLevels
    {
        public string Number { get; set; }
        public string Description { get; set; }

        public CallNumberLevels()
        {
        }

        public CallNumberLevels(string number, string description)
        {
            Number = number;
            Description = description;
        }

        public string toString()
        {
            return $"{Number}-{Description}";
        }
    }
}
