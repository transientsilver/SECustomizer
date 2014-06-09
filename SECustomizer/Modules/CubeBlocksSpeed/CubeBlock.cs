using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SECustomizer
{
    public class CubeBlock
    {
        private XElement xe;

        public CubeBlock(XElement e)
        {
            xe = e;
        }

        public decimal BuildTime
        {
            get
            {
                return Math.Round(Convert.ToDecimal(xe.Element("BuildTimeSeconds").Value), 1);
            }

            set
            {
                xe.SetElementValue("BuildTimeSeconds", value);
            }
        }

        public string Name
        {
            get
            {
                return xe.Element("BlockPairName").Value;
            }

            set
            {
                xe.SetElementValue("BlockPairName", value);
            }
        }

        public string Size
        {
            get
            {
                return xe.Element("CubeSize").Value;
            }
        }

        public string Description
        {
            get
            {
                return Name + " - " + Size;
            }
        }

    }
}
