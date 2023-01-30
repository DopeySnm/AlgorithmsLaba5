using AlgorithmsLaba5.DataStructures;
using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlgorithmsLaba5.Servis
{
    internal class DrawingFromFile
    {
        private Canvas canvas;
        private List<Node> nodes;
        private List<Connection> connections;
        private Graph graph;
        public DrawingFromFile(Canvas canvas, Graph graph)
        {
            this.canvas = canvas;
            nodes = new List<Node>();
            connections = new List<Connection>();
            this.graph = graph;
        }
        public void Rendering()
        {
            RenderingNode();
            RenderingConnection();
            graph.countEdges = graph.Edges.Count;
            graph.countVertices = graph.Vertices.Count;
        }
        //private void GetVertex()
        //{
        //    List<Vertex> vertices = new List<Vertex>();
        //    foreach (var item in graph.Edges)
        //    {
        //        if (!vertices.Contains(item.From))
        //        {
        //            vertices.Add(item.From);
        //        }
        //        if (!vertices.Contains(item.To))
        //        {
        //            vertices.Add(item.To);
        //        }
        //    }
        //    graph.Vertices.Clear();
        //    graph.Vertices = vertices;
        //}
        private void RenderingNode()
        {
            //GetVertex();
            for (int i = 0; i < graph.Vertices.Count; i++)
            {
                Grid grid = new Grid { Height = 70, Width = 70 };
                Node node = new Node(grid, graph.Vertices[i])
                {
                    //Fill = new SolidColorBrush(Color.FromArgb(100, 51, 67, 79)),
                    Fill = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)),
                    Size = 70,
                    Stroke = Brushes.Black,
                };
                canvas.Children.Add(node);
                canvas.Children.Add(node.grid);
                Panel.SetZIndex(node, 3);
                Canvas.SetLeft(grid, graph.Vertices[i].XCordinate);
                Canvas.SetTop(grid, graph.Vertices[i].YCordinate);
                Canvas.SetLeft(node, graph.Vertices[i].XCordinate);
                Canvas.SetTop(node, graph.Vertices[i].YCordinate);
                nodes.Add(node);
            }
        }
        private void CreateConnection(Edge edge, Node firstNode, Node lastNode, ConnectionType connectionType)
        {
            Connection connection = new Connection();
            connection.connectionType = connectionType;
            connection.AddGrid(firstNode.grid, firstNode);
            firstNode.AddConnect(connection);
            connection.AddGrid(lastNode.grid, lastNode);
            lastNode.AddConnect(connection);
            canvas.Children.Add(connection.polyline);
            Panel.SetZIndex(connection.polyline, 2);
            connection.edge = edge;
            AddConnectHelp addConnectHelp = new AddConnectHelp(firstNode, lastNode, connection);
            addConnectHelp.DistributionAndAddingLinks();
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
            //if (Check(connection))
            //{
            //    connections.Add(connection);
            //}
            //else
            //{
            //    canvas.Children.Remove(connection.polyline);
            //    firstNode.DeletConnect(connection);
            //    lastNode.DeletConnect(connection);
            //    connection = null;
            //}
        }
        private bool Check(Connection connection)
        {
            bool check = true;
            for (int i = 0; i < connections.Count; i++)
            {
                if (connection.polyline.Points[0] == connections[i].polyline.Points[0]
                    && connection.polyline.Points[1] == connections[i].polyline.Points[1]
                    || connection.polyline.Points[1] == connections[i].polyline.Points[0]
                    && connection.polyline.Points[0] == connections[i].polyline.Points[1])
                {
                    check = false;
                }
            }
            return check;
        }
        private Node SearchNode(Vertex vertex)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].vertex.Id.Equals(vertex.Id))
                {
                    return nodes[i];
                }
            }
            throw new Exception("Не найдено вершины");
        }
        private void RenderingConnection()
        {
            for (int i = 0; i < graph.Edges.Count; i++)
            {
                Node node1 = SearchNode(graph.Edges[i].From);
                graph.Edges[i].From = node1.vertex;
                Node node2 = SearchNode(graph.Edges[i].To);
                graph.Edges[i].To = node2.vertex;
                CreateConnection(graph.Edges[i], node1, node2, ConnectionType.ParentsToСhild);
            }
            //Connection connection = new Connection();
            //for (int i = 0; i < nodes.Count; i++)
            //{
            //    //Node lastNode;
            //    //lastNode = SearchNodeById(nodes[i].person.IDPartner);
            //    //if (lastNode != null)
            //    //{
            //    //    CreateConnection(nodes[i], lastNode, ConnectionType.Partner);
            //    //}
            //    //for (int j = 0; j < nodes[i].person.IDPerents.Count; j++)
            //    //{
            //    //    lastNode = SearchNodeById(nodes[i].person.IDPerents[j]);
            //    //    if (lastNode != null)
            //    //    {
            //    //        CreateConnection(nodes[i], lastNode, ConnectionType.ChildToParents);
            //    //    }
            //    //}
            //    //for (int j = 0; j < nodes[i].Count; j++)
            //    //{
            //    //    lastNode = SearchNodeById(nodes[i].person.IDChildren[j]);
            //    //    if (lastNode != null)
            //    //    {
            //    //        CreateConnection(nodes[i], lastNode, ConnectionType.ParentsToСhild);
            //    //    }
            //    //}
            //}
        }
        private void CheckCnavasSize()
        {

        }
    }
}
