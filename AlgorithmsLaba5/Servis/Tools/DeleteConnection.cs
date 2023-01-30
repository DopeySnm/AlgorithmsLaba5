using AlgorithmsLaba5.DataStructures;
using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AlgorithmsLaba5.Servis.Tools
{
    internal class DeleteConnection : ITools
    {
        private Canvas canvas;
        public Node node;
        public Node firstConnectionNode;
        private Connection connection;
        private CheckMouseItems checkMouseItem;
        private Graph graph;
        public DeleteConnection(Canvas canvas, Graph graph)
        {
            this.canvas = canvas;
            checkMouseItem = new CheckMouseItems(canvas);
            this.node = checkMouseItem.node;
            this.graph = graph;
        }
        public void Load()
        {
            canvas.MouseRightButtonUp += MouseUp;
            canvas.MouseRightButtonDown += MouseDown;
            canvas.MouseMove += MouseMove;
        }

        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkMouseItem.CheckObjectUnderMouse();
            node = checkMouseItem.node;
            if (node != null)
            {
                connection = new Connection();
                connection.polyline.Stroke = Brushes.Red;
                connection.polyline.Points.Add(new Point(Canvas.GetLeft(node) + node.grid.Width / 2, Canvas.GetTop(node) + node.grid.Height / 2));
                connection.polyline.Points.Add(new Point(Canvas.GetLeft(node) + node.grid.Width / 2, Canvas.GetTop(node) + node.grid.Height / 2));
                connection.AddGrid(node.grid, node);
                node.AddConnect(connection);
                firstConnectionNode = node;
                canvas.Children.Add(connection.polyline);
                Panel.SetZIndex(connection.polyline, 2);
            }
        }
        public void MouseUp(object sender, MouseButtonEventArgs e)
        {
            checkMouseItem.CheckObjectUnderMouse();
            node = checkMouseItem.node;
            if (connection != null && node != null)
            {
                connection.polyline.Points[1] = new Point(Canvas.GetLeft(node) + node.grid.Width / 2, Canvas.GetTop(node) + node.grid.Height / 2);
                connection.AddGrid(node.grid, node);
                node.AddConnect(connection);

                DeleteHelp deleteHelp = new DeleteHelp(canvas, firstConnectionNode, node, graph);
                deleteHelp.DeleteConnect(connection);
                deleteHelp.DeleteConnectId();

                canvas.Children.Remove(connection.polyline);
                firstConnectionNode.DeletConnect(connection);
                node.DeletConnect(connection);
                connection = null;
                firstConnectionNode = null;
            }
            else
            {
                if (connection != null)
                {
                    canvas.Children.Remove(connection.polyline);
                    firstConnectionNode.DeletConnect(connection);
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
        public void Unload()
        {
            canvas.MouseRightButtonUp -= MouseUp;
            canvas.MouseRightButtonDown -= MouseDown;
            canvas.MouseMove -= MouseMove;
        }
    }
}
