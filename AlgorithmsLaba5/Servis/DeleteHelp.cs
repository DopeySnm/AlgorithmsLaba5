using AlgorithmsLaba5.DataStructures;
using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AlgorithmsLaba5.Servis
{
    internal class DeleteHelp
    {
        private Canvas canvas;
        private Node firstNode;
        private Node lastNode;
        private Graph graph;
        public DeleteHelp(Canvas canvas, Node firstNode, Node lastNode, Graph graph)
        {
            this.firstNode = firstNode;
            this.lastNode = lastNode;
            this.canvas = canvas;
            this.graph = graph;
        }
        private ConnectionType GetConnectionTypeDelete()
        {
            ConnectionType connectionType = ConnectionType.ParentsToСhild;
            //for (int i = 0; i < firstNode.person.IDChildren.Count; i++)
            //{
            //    if (firstNode.person.IDChildren[i] == lastNode.person.Id)
            //    {
            //        connectionType = ConnectionType.ParentsToСhild;
            //    }
            //}
            //for (int i = 0; i < firstNode.person.IDPerents.Count; i++)
            //{
            //    if (lastNode.person.Id == firstNode.person.IDPerents[i])
            //    {
            //        connectionType = ConnectionType.ChildToParents;
            //    }
            //}
            //if (firstNode.person.IDPartner == lastNode.person.Id)
            //{
            //    connectionType = ConnectionType.Partner;
            //}
            return connectionType;
        }
        public void DeleteConnectId()
        {
            switch (GetConnectionTypeDelete())
            {
                //case ConnectionType.ParentsToСhild:
                //    firstNode.person.IDChildren.Remove(lastNode.person.Id);
                //    lastNode.person.IDPerents.Remove(firstNode.person.Id);
                //    break;
                //case ConnectionType.ChildToParents:
                //    firstNode.person.IDPerents.Remove(lastNode.person.Id);
                //    lastNode.person.IDChildren.Remove(firstNode.person.Id);
                //    break;
                //case ConnectionType.Partner:
                //    firstNode.person.IDPartner = 0;
                //    lastNode.person.IDPartner = 0;
                //    break;
            }
        }
        public void DeleteConnect(Connection connectionDelete)
        {
            for (int i = 0; i < lastNode.connections.Count; i++)
            {
                if (lastNode.connections[i].nodes[0] == connectionDelete.nodes[0]
                    && lastNode.connections[i].nodes[1] == connectionDelete.nodes[1]
                    || lastNode.connections[i].nodes[0] == connectionDelete.nodes[1]
                    && lastNode.connections[i].nodes[1] == connectionDelete.nodes[0])
                {
                    if (lastNode.connections[i].edge != null)
                    {
                        graph.RemoveEdge(lastNode.connections[i].edge);
                    }
                    canvas.Children.Remove(lastNode.connections[i].rectangle);
                    canvas.Children.Remove(lastNode.connections[i].textBlock);
                    canvas.Children.Remove(lastNode.connections[i].polyline);
                    lastNode.DeletConnect(lastNode.connections[i]);
                }
            }
            for (int i = 0; i < firstNode.connections.Count; i++)
            {
                if (firstNode.connections[i].nodes[0] == connectionDelete.nodes[0]
                    && firstNode.connections[i].nodes[1] == connectionDelete.nodes[1]
                    || firstNode.connections[i].nodes[0] == connectionDelete.nodes[1]
                    && firstNode.connections[i].nodes[1] == connectionDelete.nodes[0])
                {
                    //canvas.Children.Remove(firstConnectionNode.connections[i].polyline);
                    firstNode.DeletConnect(firstNode.connections[i]);
                }
            }
        }
    }
}
