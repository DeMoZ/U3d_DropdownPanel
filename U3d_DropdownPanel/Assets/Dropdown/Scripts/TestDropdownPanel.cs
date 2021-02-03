using System.Collections.Generic;
using UnityEngine;

namespace DropdownPanel
{
    public class TestDropdownPanel : MonoBehaviour
    {
        public DropdownPanel dropdownPanel;

        private void Start()
        {
            List<Structs.Stage> stages = new List<Structs.Stage>();

            stages.Add(new Structs.Stage()
            {
                name = "Dropdown 1",
                id = 1,
                floors = new List<Structs.Floor>
                {
                    new Structs.Floor {name = "Item 1", id = 101},
                    new Structs.Floor {name = "Item 2", id = 102},
                    new Structs.Floor {name = "Item 3", id = 103},
                }
            });
            stages.Add(new Structs.Stage()
            {
                name = "Dropdown 2",
                id = 2,
                floors = new List<Structs.Floor>
                {
                    new Structs.Floor {name = "Item 4", id = 104},
                    new Structs.Floor {name = "Item 5", id = 105},
                    new Structs.Floor {name = "Item 6", id = 106},
                    new Structs.Floor {name = "Item 7", id = 107},
                    new Structs.Floor {name = "Item 8", id = 108},
                    new Structs.Floor {name = "Item 9", id = 109},
                }
            });
            stages.Add(new Structs.Stage()
            {
                name = "Dropdown 3",
                id = 3,
                floors = new List<Structs.Floor>
                {
                    new Structs.Floor {name = "Item 10", id = 110},
                    new Structs.Floor {name = "Item 11", id = 111},
                    new Structs.Floor {name = "Item 12", id = 112},
                    new Structs.Floor {name = "Item 13", id = 113},
                }
            });
            stages.Add(new Structs.Stage()
            {
                name = "Dropdown 4",
                id = 4,
                floors = new List<Structs.Floor>
                {
                    new Structs.Floor {name = "Item 14", id = 114},
                }
            });
            stages.Add(new Structs.Stage()
            {
                name = "Dropdown 5",
                id = 5
            });

            dropdownPanel.Init(stages);
        }
    }
}