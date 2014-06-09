using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SECustomizer.Modules.InventoryEditor
{
    class Entity
    {
        private XElement xe;
        public List<InventoryCapableItem> blocks { get; set; }
        private XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";

        public Entity(XElement e)
        {
            xe = e;
            loadData();
        }

        private void loadData()
        {
            var allblocks = from b in xe.Descendants("MyObjectBuilder_CubeBlock")
                            where b.Element("Inventory") != null && (b.Attribute(ns + "type").Value == "MyObjectBuilder_Reactor" || b.Attribute(ns + "type").Value == "MyObjectBuilder_CargoContainer")
                            select new InventoryCapableItem(b);

            blocks = allblocks.OrderBy(b => b.Type).ToList();
        }

        public string Id
        {
            get
            {
                return xe.Element("EntityId").Value;
            }
        }

        public string Size
        {
            get
            {
                return xe.Element("GridSizeEnum").Value;
            }
        }

        public string DisplayName
        {
            get
            {
                string val = xe.Element("GridSizeEnum").Value;

                var beacons = from b in xe.Descendants("MyObjectBuilder_CubeBlock")
                              where b.Attribute(ns + "type") != null && b.Attribute(ns + "type").Value == "MyObjectBuilder_Beacon" && b.Element("CustomName") != null
                              select b.Element("CustomName").Value;

                if (beacons.Any())
                {
                    val = val + " - " + beacons.First();
                }

                return val;
            }
        }

    }
}
