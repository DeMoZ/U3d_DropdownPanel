using System.Collections.Generic;

namespace DropdownPanel
{
    public struct Structs
    {
        public struct Stage
        {
            public string name;
            public int id;

            public List<Floor> floors;
        }

        public struct Floor
        {
            public string name;
            public int id;
        }
    }
}