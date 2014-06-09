using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SECustomizer
{
    class CubeBlockManger
    {
        private XDocument xdoc;
        public List<CubeBlock> blocks { get; set; }        
        private string filename;

        public CubeBlockManger(string filepath)
        {
            filename = filepath;
            loadData();
        }

        private void loadData()
        {
            xdoc = XDocument.Load(filename);

            var allblocks = from b in xdoc.Descendants("Definition")
                            select new CubeBlock(b);

            blocks = allblocks.OrderBy(b => b.Name).ToList();
        }

        public void increaseSpeedByMultiplier(decimal multiplier)
        {
            foreach(CubeBlock b in blocks)
            {
                b.BuildTime = b.BuildTime / Math.Abs(multiplier);
            }
        }

        public void decreaseSpeedByMultiplier(decimal multiplier)
        {
            foreach (CubeBlock b in blocks)
            {
                b.BuildTime = b.BuildTime * Math.Abs(multiplier);
            }
        }

        public void resetBlocks()
        {
            xdoc = null;
            blocks = null;

            loadData();            
        }

        public void Save()
        {
            xdoc.Save(filename);
        }

    }
}
