using AlgorithmsLaba5.DataStructures;
using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlgorithmsLaba5.Servis.Tools
{
    internal class DeleteNode : ITools
    {
        private Canvas canvas;
        private Graph graph;
        public DeleteNode(Canvas canvas, Graph graph)
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

        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            CheckMouseItems checkMouseItems = new CheckMouseItems(canvas);
            Node node = checkMouseItems.node;
            if (node != null)
            {
                //List<Node> lastNodes = SheareAllNodeConnect(node);
                //for (int i = 0; i < lastNodes.Count; i++)
                //{
                //    DeleteHelp deleteHelp = new DeleteHelp(canvas, node, lastNodes[i], graph);
                //    deleteHelp.DeleteConnectId();
                //}
                DeleteHelp deleteHelp;
                while (node.connections.Count != 0)
                {
                    if (!node.connections[0].nodes[0].Equals(node))
                    {
                        deleteHelp = new DeleteHelp(canvas, node, node.connections[0].nodes[0], graph);
                        deleteHelp.DeleteConnect(node.connections[0]);
                    }
                    else if (!node.connections[0].grids[1].Equals(node))
                    {
                        deleteHelp = new DeleteHelp(canvas, node, node.connections[0].nodes[1], graph);
                        deleteHelp.DeleteConnect(node.connections[0]);
                    }
                }

                canvas.Children.Remove(node);
                canvas.Children.Remove(node.grid);
                //canvas.Children.Clear();
                graph.RemoveVertex(node.vertex);
                //WritingDataJson writingDataJson = new WritingDataJson();
                //writingDataJson.WritePeople(people);
                //ReadFileJson readFileJson = new ReadFileJson();
                //this.people = readFileJson.ReadPeople("..\\..\\..\\..\\people.json");
                //if (people == null)
                //{
                //    people = new List<Person>();
                //}
                //DrawingFromFile drawingFromFile = new DrawingFromFile(canvas, graph);
                //drawingFromFile.Rendering();
            }
        }
        private List<Node> SheareAllNodeConnect(Node node)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < node.connections.Count; i++)
            {
                for (int j = 0; j < node.connections[i].nodes.Length; j++)
                {
                    if (node.connections[i].nodes[j] != node)
                    {
                        nodes.Add(node.connections[i].nodes[j]);
                    }
                }
            }
            return nodes;
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {

        }

        public void MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        public void Unload()
        {
            canvas.MouseRightButtonUp -= MouseUp;
            canvas.MouseRightButtonDown -= MouseDown;
            canvas.MouseMove -= MouseMove;
        }
    }
}
