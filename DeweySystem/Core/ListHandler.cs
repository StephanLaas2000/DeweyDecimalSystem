using System.Collections.Generic;

namespace DeweySystem.Core
{
    internal class ListHandler
    {
        public static List<string> deweyDecimalList = new List<string>();
        public static List<string> shuffled = new List<string>();
        public static Dictionary<string, string> callnumberDic = new Dictionary<string, string>();
        public static Dictionary<string, string> callnumberDic2 = new Dictionary<string, string>();
        public static List<TreeRoot<CallNumberLevels>> treeList = new List<TreeRoot<CallNumberLevels>>();
    }
}
