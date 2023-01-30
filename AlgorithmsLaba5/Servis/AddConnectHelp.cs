using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlgorithmsLaba5.Models.Connection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace AlgorithmsLaba5.Servis
{
    internal class AddConnectHelp
    {
        private Node firstNode;
        private Node lastNode;
        private Connection connection;
        public AddConnectHelp(Node firstNode, Node lastNode, Connection connection)
        {
            this.firstNode = firstNode;
            this.lastNode = lastNode;
            this.connection = connection;
        }
        public void DistributionAndAddingId()
        {
            switch (connection.connectionType)
            {
                case ConnectionType.ParentsToСhild:
                    //firstNode.person.IDChildren.Add(lastNode.person.Id);
                    //lastNode.person.IDPerents.Add(firstNode.person.Id);
                    connection.edge = new Edge(firstNode.vertex, lastNode.vertex);
                    connection.edge.Oreinted = true;
                    //if (lastNode.person.IDPerents[0] == 0) lastNode.person.IDPerents[0] = firstNode.person.Id;
                    //else lastNode.person.IDPerents[1] = firstNode.person.Id;
                    break;
                case ConnectionType.ChildToParents:
                    //lastNode.person.IDChildren.Add(firstNode.person.Id);
                    //firstNode.person.IDPerents.Add(lastNode.person.Id);
                    connection.edge = new Edge(lastNode.vertex, firstNode.vertex);
                    connection.edge.Oreinted = true;
                    //if (firstNode.person.IDPerents[0] == 0) firstNode.person.IDPerents[0] = lastNode.person.Id;
                    //else firstNode.person.IDPerents[1] = lastNode.person.Id;
                    break;
                case ConnectionType.Partner:
                    //firstNode.person.IDPartner = lastNode.person.Id;
                    //lastNode.person.IDPartner = firstNode.person.Id;
                    connection.edge = new Edge(firstNode.vertex, lastNode.vertex);
                    break;
            }
        }
        public void DistributionAndAddingLinks()
        {
            switch (connection.connectionType)
            {
                case ConnectionType.ParentsToСhild:
                    connection.polyline.Points.Clear();
                    //connection.polyline.Points.Add(new Point(Canvas.GetLeft(firstNode) + firstNode.grid.Width / 2, Canvas.GetTop(firstNode) + firstNode.grid.Height));
                    //connection.polyline.Points.Add(new Point(Canvas.GetLeft(lastNode) + lastNode.grid.Width / 2, Canvas.GetTop(lastNode)));
                    connection.polyline.Points.Add(new Point(Canvas.GetLeft(firstNode) + firstNode.grid.Width / 2, Canvas.GetTop(firstNode) + firstNode.grid.Height / 2));
                    connection.polyline.Points.Add(new Point(Canvas.GetLeft(lastNode) + lastNode.grid.Width / 2, Canvas.GetTop(lastNode) + lastNode.grid.Height / 2));
                    break;
                case ConnectionType.ChildToParents:
                    connection.polyline.Points.Clear();
                    connection.polyline.Points.Add(new Point(Canvas.GetLeft(firstNode) + firstNode.grid.Width / 2, Canvas.GetTop(firstNode)));
                    connection.polyline.Points.Add(new Point(Canvas.GetLeft(lastNode) + lastNode.grid.Width / 2, Canvas.GetTop(lastNode) + lastNode.grid.Height));
                    break;
                case ConnectionType.Partner:
                    Point firstRectLeft = new Point(Canvas.GetLeft(firstNode), Canvas.GetTop(firstNode) + firstNode.grid.Height / 2);
                    Point firstRectRight = new Point(Canvas.GetLeft(firstNode) + firstNode.grid.Width, Canvas.GetTop(firstNode) + firstNode.grid.Height / 2);

                    Point lastRectLeft = new Point(Canvas.GetLeft(lastNode), Canvas.GetTop(lastNode) + lastNode.grid.Height / 2);
                    Point lastRectRight = new Point(Canvas.GetLeft(lastNode) + lastNode.grid.Width, Canvas.GetTop(lastNode) + lastNode.grid.Height / 2);
                    if (firstRectLeft.X + firstRectRight.X < lastRectLeft.X + lastRectRight.X)
                    {
                        connection.polyline.Points.Clear();
                        connection.polyline.Points.Add(firstRectRight);
                        connection.polyline.Points.Add(lastRectLeft);
                    }
                    else
                    {
                        connection.polyline.Points.Clear();
                        connection.polyline.Points.Add(lastRectRight);
                        connection.polyline.Points.Add(firstRectLeft);
                    }
                    //connection.polyline.Points[0] = new Point(Canvas.GetLeft(firstNode), Canvas.GetTop(firstNode) + firstNode.grid.Height / 2);
                    //connection.polyline.Points[1] = new Point(Canvas.GetLeft(lastNode) + lastNode.grid.Width, Canvas.GetTop(lastNode) + lastNode.grid.Height / 2);
                    break;
            }
            //connection.AddGrid(firstNode.grid, firstNode);
            //firstNode.AddConnect(connection);
            //connection.AddGrid(lastNode.grid, lastNode);
            //lastNode.AddConnect(connection);
        }
    }
}
