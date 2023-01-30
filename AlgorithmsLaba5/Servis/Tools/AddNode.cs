using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using AlgorithmsLaba5.Views;
using AlgorithmsLaba5.DataStructures;

namespace AlgorithmsLaba5.Servis.Tools
{
    internal class AddNode : ITools
    {
        private Grid grid;
        private Node node;
        private Canvas canvas;
        private Graph graph;
        public AddNode(Canvas canvas, Graph graph)
        {
            this.canvas = canvas;
            this.graph = graph;
        }
        public void Load()
        {
            canvas.MouseRightButtonUp += MouseUp;
            canvas.MouseRightButtonDown += MouseDown;
            canvas.MouseMove += MouseMove;
        }
        public void Unload()
        {
            canvas.MouseRightButtonUp -= MouseUp;
            canvas.MouseRightButtonDown -= MouseDown;
            canvas.MouseMove -= MouseMove;
        }
        public void MouseMove(object sender, MouseEventArgs e)
        {

        }
        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = Mouse.GetPosition(canvas);
            do
            {
                AddVertex addVertex = new AddVertex();
                try
                {
                    Vertex vertex;
                    addVertex.ShowDialog();
                    addVertex.Close();
                    vertex = addVertex.vertex;
                    if (vertex != null)
                    {
                        vertex.XCordinate = point.X;
                        vertex.YCordinate = point.Y;
                        graph.AddVertex(vertex);
                        grid = new Grid { Height = 70, Width = 70 };
                        node = new Node(grid, vertex)
                        {
                            //Fill = new SolidColorBrush(Color.FromArgb(100, 51, 67, 79)),
                            Fill = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)),
                            Size = 70,
                            Stroke = Brushes.Black,
                        };
                        canvas.Children.Add(node);
                        canvas.Children.Add(node.grid);
                        Panel.SetZIndex(node, 3);
                        Canvas.SetLeft(grid, point.X);
                        Canvas.SetTop(grid, point.Y);
                        Canvas.SetLeft(node, point.X);
                        Canvas.SetTop(node, point.Y);
                        break;
                    }
                    break;
                }
                catch (Exception)
                {
                    MessageBox.Show("Попробуйте ещё раз или закройте");// что-то придумать с датой рождения так как может быть тип с неизвестной датой рождения
                    continue;
                }
            } while (true);
        }
        public void MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
