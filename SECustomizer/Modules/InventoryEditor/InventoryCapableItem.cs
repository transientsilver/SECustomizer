using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SECustomizer.Modules.InventoryEditor
{
    class InventoryCapableItem
    {
        private XElement xe;
        private XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
        public List<InventoryItemEntry> items { get; set; }

        public InventoryCapableItem(XElement e)
        {
            xe = e;
            this.loadItems();
        }

        public string Id
        {
            get
            {
                return xe.Element("EntityId").Value;
            }
        }

        public string TypeName
        {
            get
            {
                if (xe.Element("SubtypeName").Value != "")
                {
                    return xe.Element("SubtypeName").Value;
                }
                else
                {
                    string val = (string)xe.Attribute(ns + "type");
                    return val.Split(new Char[] { '_' })[1];
                }
            }
        }

        public string Type
        {
            get
            {
                return (string)xe.Attribute(ns + "type");
            }
        }

        public void addItem(XElement item)
        {
            int newId = Convert.ToInt32(xe.Element("Inventory").Element("nextItemId").Value);
            item.SetElementValue("ItemId", newId);
            xe.Element("Inventory").SetElementValue("nextItemId", newId + 1);
            xe.Element("Inventory").Element("Items").Add(item);
            loadItems();
        }

        private void loadItems()
        {
            var inventoryitems = from b in xe.Descendants("MyObjectBuilder_InventoryItem")
                                 select new InventoryItemEntry(b);

            items = inventoryitems.OrderBy(b => b.Id).ToList();
        }

    }
}
