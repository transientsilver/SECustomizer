﻿using System;
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
using SECustomizer.Modules.InventoryEditor;

namespace SECustomizer
{

    public partial class InventoryEditor : UserControl
    {
        private InventoryManager manager;
        
        public InventoryEditor()
        {
            InitializeComponent();
            initManager();
        }

        private void initManager()
        {
            var savefile = pickSave();

            if (savefile != null)
            {
                manager = new InventoryManager(savefile);

                cboEntities.ItemsSource = manager.entities;
                cboEntities.DisplayMemberPath = "DisplayName";

                cboAvailableItems.ItemsSource = manager.stockItems;
                cboAvailableItems.DisplayMemberPath = "Description";
            }
            else
            {
                MessageBox.Show("Unable to locate CubeBlocks file");
            }
        }

        private string pickSave()
        {
             return FileUtility.quickFind("SANDBOX_0_0_0_.sbs", @"\", Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Roaming\SpaceEngineers\Saves");
        }

        private void cboInventoryCapable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InventoryCapableItem selected = (InventoryCapableItem) cboInventoryCapable.SelectedItem;
            this.refreshItemGrid(selected);
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            InventoryCapableItem selectedInventory = (InventoryCapableItem) cboInventoryCapable.SelectedItem;
            Item newItem = (Item) cboAvailableItems.SelectedItem;
            manager.addItemToInventory(selectedInventory, newItem);
            this.refreshItemGrid(selectedInventory);
        }

        private void refreshItemGrid(InventoryCapableItem selected)
        {
            if (selected != null)
            {
                inventoryDataGrid.ItemsSource = selected.items;
                cboAvailableItems.IsEnabled = true;
                btnAddItem.IsEnabled = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            manager.Save();
            MessageBox.Show("Saved.");
        }

        private void cboEntities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboInventoryCapable.ItemsSource = null;
            inventoryDataGrid.ItemsSource = null;

            Entity selectedEntity = (Entity)cboEntities.SelectedItem;
            cboInventoryCapable.ItemsSource = selectedEntity.blocks;
            cboInventoryCapable.DisplayMemberPath = "TypeName";
        }

    }
}
