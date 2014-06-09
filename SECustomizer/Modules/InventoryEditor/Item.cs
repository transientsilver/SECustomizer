using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECustomizer.Modules.InventoryEditor
{
    class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public Item()
        { }

        public string Description
        {
            get
            {
                if (Type == "Ore")
                {
                    return Name + " " + Type;
                }
                else
                {
                    return Name;
                }
            }

        }
    }
}
