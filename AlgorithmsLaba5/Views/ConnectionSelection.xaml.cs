using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using static AlgorithmsLaba5.Models.Connection;

namespace AlgorithmsLaba5.Views
{
    public partial class ConnectionSelection : Window
    {
        public ConnectionType connectionType;
        public bool featBack;
        public int WightEdge = 1;
        public bool Oreinted = false;
        public ConnectionSelection()
        {
            InitializeComponent();
        }
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            featBack = false;
            this.Close();
        }
        private void Button_Click_SelectSlement(object sender, RoutedEventArgs e)
        {
            featBack = true;
            connectionType = ConnectionType.ParentsToСhild;
            Oreinted = (bool)lever_Oreinted.IsChecked;
            if (this.WightText.Text != "")
            {
                WightEdge = int.Parse(this.WightText.Text);
            }
            this.Close();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Grid_MouseDown_MovingWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
