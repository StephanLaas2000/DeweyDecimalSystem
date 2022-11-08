using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweySystem.Core
{
    class TreeModel<CallNumberLevels>
    {
        public CallNumberLevels Data { get; set; }
        public TreeModel<CallNumberLevels> Parent { get; set; }
        public List<TreeModel<CallNumberLevels>> Children { get; set; }
    }
}
