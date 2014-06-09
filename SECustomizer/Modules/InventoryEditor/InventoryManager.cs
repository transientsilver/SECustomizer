using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SECustomizer.Modules.InventoryEditor
{
    class InventoryManager
    {
        private XDocument xdoc;
        public List<InventoryCapableItem> blocks { get; set; }
        public List<Item> stockItems { get; set; }
        private string filename;
        private XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";        

        public InventoryManager(string filepath)
        {
            filename = filepath;
            loadData();
            stockItems = new List<Item>();
            this.loadStockItems("Components.sbc", "Component");
            this.loadStockItems("PhysicalItems.sbc", "PhysicalItem");
            this.loadStockItems("AmmoMagazines.sbc", "AmmoMagazine");
        }

        private void loadData()
        {
            xdoc = XDocument.Load(filename);

            var allblocks = from b in xdoc.Descendants("MyObjectBuilder_CubeBlock")
                            where b.Element("Inventory") != null && (b.Attribute(ns + "type").Value == "MyObjectBuilder_Reactor" || b.Attribute(ns + "type").Value == "MyObjectBuilder_CargoContainer")
                            select new InventoryCapableItem(b);

            blocks = allblocks.OrderBy(b => b.Type).ToList();
        }

        private void loadStockItems(string itemfilename, string nodetype)
        {
            var file = FileUtility.quickFind(itemfilename);

            if (file != null)
            {
                XDocument itemsdoc = XDocument.Load(file);

                var items = from b in itemsdoc.Descendants(nodetype)
                            select new Item
                            {
                                Name = b.Element("Id").Element("SubtypeId").Value,
                                Type = b.Element("Id").Element("TypeId").Value
                            };

               List<Item> itemlist = items.OrderBy(b => b.Name).ToList();
               stockItems.AddRange(itemlist);               

            }

        }
        
        public void resetBlocks()
        {
            xdoc = null;
            blocks = null;

            loadData();
        }

        public void addItemToInventory(InventoryCapableItem inventoryitem, Item newitem)
        {
            XElement itemel = new XElement("MyObjectBuilder_InventoryItem",
                 new XElement("ItemId", 0),
                 new XElement("AmountDecimal", 0),
                 new XElement("PhysicalContent", new XAttribute(ns + "type", "MyObjectBuilder_" + newitem.Type), new XElement("SubtypeName", newitem.Name))
            );

            inventoryitem.addItem(itemel);
        }

        public void Save()
        {
            xdoc.Save(filename);
        }

    }
}
