using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using AlgorithmsLaba5.Views;
using AlgorithmsLaba5.DataStructures;
using System.Windows.Shapes;
using System.Windows.Media;

namespace AlgorithmsLaba5.Servis.Tools
{
    internal class AddConnection : ITools
    {
        private Canvas canvas;
        private Node node;
        private Node firstConnectionNode;
        private Connection connection;
        private CheckMouseItems checkMouseItem;
        private Graph graph;
        public AddConnection(Canvas canvas, Graph graph)
        {
            this.canvas = canvas;
            checkMouseItem = new CheckMouseItems(canvas);
            this.node = checkMouseItem.node;
            this.graph = graph;
        }
        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkMouseItem.CheckObjectUnderMouse();
            node = checkMouseItem.node;
            if (node != null)
            {
                connection = new Connection();
                connection.polyline.Points.Add(new Point(Canvas.GetLeft(node) + node.grid.Width / 2, Canvas.GetTop(node) + node.grid.Height / 2));
                connection.polyline.Points.Add(new Point(Canvas.GetLeft(node) + node.grid.Width / 2, Canvas.GetTop(node) + node.grid.Height / 2));
                //connection.AddGrid(node.grid, node);
                //node.AddConnect(connection);
                firstConnectionNode = node;
                canvas.Children.Add(connection.polyline);
                Panel.SetZIndex(connection.polyline, 2);
            }
        }
        public void MouseUp(object sender, MouseButtonEventArgs e)
        {
            checkMouseItem.CheckObjectUnderMouse();
            node = checkMouseItem.node;
            ConnectionSelection connectionSelection = new ConnectionSelection();
            connectionSelection.ShowDialog();
            if (connection != null && node != null && connectionSelection.featBack)
            {
                connection.connectionType = connectionSelection.connectionType;
                AddConnectHelp addConnectHelp = new AddConnectHelp(firstConnectionNode, node, connection);
                addConnectHelp.DistributionAndAddingLinks();
                addConnectHelp.DistributionAndAddingId();
                connection.AddGrid(firstConnectionNode.grid, firstConnectionNode);
                firstConnectionNode.AddConnect(connection);
                connection.AddGrid(node.grid, node);
                node.AddConnect(connection);
                connection.edge.Weight = connectionSelection.WightEdge;
                connection.edge.WayFrom = connection.edge.Weight;
                graph.AddEdge(connection.edge);
                connection.edge.Oreinted = connectionSelection.Oreinted;
                if (connection.edge.Oreinted)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = Brushes.Black;
                    rectangle.Height = 10;
                    rectangle.Width = 10;
                    rectangle.Stroke = Brushes.Black;
                    Canvas.SetLeft(rectangle, (((connection.polyline.Points[1].X + connection.polyline.Points[0].X) / 2) + connection.polyline.Points[1].X) / 2);
                    Canvas.SetTop(rectangle, (((connection.polyline.Points[1].Y + connection.polyline.Points[0].Y) / 2) + connection.polyline.Points[1].Y) / 2);
                    Canvas.SetZIndex(rectangle, 5);
                    canvas.Children.Add(rectangle);
                    connection.rectangle = rectangle;
                }
                connection.textBlock = new TextBlock();
                connection.textBlock.FontSize = 20;
                connection.textBlock.Background = Brushes.DarkGray;
                connection.textBlock.Text = connection.edge.Weight.ToString();
                Canvas.SetLeft(connection.textBlock, (connection.polyline.Points[1].X + connection.polyline.Points[0].X) / 2);
                Canvas.SetTop(connection.textBlock, (connection.polyline.Points[1].Y + connection.polyline.Points[0].Y) / 2);
                canvas.Children.Add(connection.textBlock);
                Canvas.SetZIndex(connection.textBlock, 5);
                connection = null;
                firstConnectionNode = null;
            }
            else
            {
                if (connection != null)
                {
                    canvas.Children.Remove(connection.polyline);
                    //firstConnectionNode.DeletConnect(connection);
                    connection = null;
                    firstConnectionNode = null;
                }
            }
        }
        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (connection != null)
            {
                connection.polyline.Points[1] = e.GetPosition(canvas);
            }
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
    }
}
