using AlgorithmsLaba5.Models;
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
using System.Windows.Shapes;

namespace AlgorithmsLaba5.Views
{
    public partial class AddVertex : Window
    {
        public Vertex vertex;
        public AddVertex()
        {
            InitializeComponent();
        }
        private void Button_Click_AddVertex(object sender, RoutedEventArgs e)
        {
            if (nameInput.Text == "")
            {
                vertex = new Vertex("нет имени");
            }
            else
            {
                vertex = new Vertex(nameInput.Text);

            }
            this.Close();
        }
        private void Exti(object sender, MouseButtonEventArgs e)
        {
            this.Close();
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
