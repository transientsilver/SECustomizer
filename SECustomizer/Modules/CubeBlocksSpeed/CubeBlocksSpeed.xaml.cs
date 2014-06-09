using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SECustomizer
{

    public partial class CubeBlocksSpeed : UserControl
    {
        private CubeBlockManger manager;

        public CubeBlocksSpeed()
        {
            InitializeComponent();
            initBlockManager();
        }

        private void initBlockManager()
        {
            var fullpath = FileUtility.quickFind("CubeBlocks.sbc");

            if (fullpath != null)
            {
                manager = new CubeBlockManger(fullpath);
                blockDataGrid.ItemsSource = manager.blocks;
            }
            else
            {
                MessageBox.Show("Unable to locate CubeBlocks file");                
            }
        }

        private void sliderMultiplier_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            decimal currentval = Convert.ToDecimal(sliderMultiplier.Value);
            manager.resetBlocks();

            if (currentval > 1)
            {
                lblInfo.Content = String.Format("Block speed will be {0} times faster", Math.Abs(currentval));
                manager.increaseSpeedByMultiplier(currentval);
            }
            else if (currentval < -1)
            {
                lblInfo.Content = String.Format("Block speed will be {0} times slower", Math.Abs(currentval));
                manager.decreaseSpeedByMultiplier(currentval);
            }

            blockDataGrid.ItemsSource = manager.blocks;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            manager.Save();
            MessageBox.Show("Saved.");
        }

    }
}
