using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SECustomizer.Modules.InventoryEditor
{
    class InventoryItemEntry
    {
        private XElement xe;
        private XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";

        public InventoryItemEntry(XElement e)
        {
            xe = e;
        }

        public string Id
        {
            get
            {
                return xe.Element("ItemId").Value;
            }
            set
            {
                xe.SetElementValue("ItemId", value);
            }
        }

        public string ItemName
        {
            get
            {
                return xe.Element("PhysicalContent").Element("SubtypeName").Value;
            }
        }

        public string ItemType
        {
            get
            {
                string val = (string) xe.Element("PhysicalContent").Attribute(ns + "type");
                return val.Split(new Char[] { '_' })[1];
            }
        }

        public decimal Amount
        {
            get
            {
                return Convert.ToDecimal(xe.Element("AmountDecimal").Value);
            }
            set
            {
                xe.SetElementValue("AmountDecimal", value);
            }
        }
        
    }
}
