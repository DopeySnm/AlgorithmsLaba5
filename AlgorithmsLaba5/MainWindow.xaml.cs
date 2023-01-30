using AlgorithmsLaba5.DataStructures;
using AlgorithmsLaba5.Models;
using AlgorithmsLaba5.Servis;
using AlgorithmsLaba5.Servis.Tools;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlgorithmsLaba5
{
    public partial class MainWindow : Window
    {
        private ITools tools { get; set; }
        private Graph graph;
        public MainWindow()
        {
            InitializeComponent();
            tools = new MoveNode(canvas);
            tools.Load();
            ReadDataJson readDataJson = new ReadDataJson();
            graph = readDataJson.ReadGraph("..\\..\\..\\..\\graph.json");
            if (graph != null)
            {
                DrawingFromFile drawingFromFile = new DrawingFromFile(canvas, graph);
                drawingFromFile.Rendering();
            }
            else
            {
                graph = new Graph();
            }
        }
        private void Btn_Click_PeopleAdd(object sender, RoutedEventArgs e)
        {
            OutputLog.Items.Clear();
            if (tools != null)
            {
                tools.Unload();
            }
            tools = new AddNode(canvas, graph);
            tools.Load();
        }
        private void Btn_Click_LineMode(object sender, RoutedEventArgs e)
        {
            OutputLog.Items.Clear();
            if (tools != null)
            {
                tools.Unload();
            }
            tools = new AddConnection(canvas, graph);
            tools.Load();
        }
        private void Btn_Click_RectangleDelete(object sender, RoutedEventArgs e)
        {
            OutputLog.Items.Clear();
            if (tools != null)
            {
                tools.Unload();
            }
            tools = new DeleteNode(canvas, graph);
            tools.Load();
        }
        private void Btn_Click_LineDelete(object sender, RoutedEventArgs e)
        {
            OutputLog.Items.Clear();
            if (tools != null)
            {
                tools.Unload();
            }
            tools = new DeleteConnection(canvas, graph);
            tools.Load();
        }
        private void Btn_Depth(object sender, RoutedEventArgs e)
        {
            OutputLog.Items.Clear();
            graph.BleachingEdges();
            graph.BleachingVertices();
            UpdateCanvas();
            if (tools != null)
            {
                tools.Unload();
            }
            tools = new BypassInDepth(canvas, graph, OutputLog);
            tools.Load();
        }
        private void Btn_Wave(object sender, RoutedEventArgs e)
        {
            OutputLog.Items.Clear();
            graph.BleachingEdges();
            graph.BleachingVertices();
            UpdateCanvas();
            if (tools != null)
            {
                tools.Unload();
            }
            tools = new BypassInWave(canvas, graph, OutputLog);
            tools.Load();
        }
        private void Btn_PrimsAlgorithm(object sender, RoutedEventArgs e)
        {
            OutputLog.Items.Clear();
            graph.BleachingEdges();
            graph.BleachingVertices();
            UpdateCanvas();
            if (tools != null)
            {
                tools.Unload();
            }
            tools = new PrimsAlgorithm(canvas, graph, OutputLog);
            tools.Load();
        }
        private void Btn_ShortestWay(object sender, RoutedEventArgs e)
        {
            OutputLog.Items.Clear();
            graph.BleachingEdges();
            graph.BleachingVertices();
            UpdateCanvas();
            if (tools != null)
            {
                tools.Unload();
            }
            tools = new ShortestWay(canvas, graph, OutputLog);
            tools.Load();
        }
        private void Btn_MaximumFlow(object sender, RoutedEventArgs e)
        {
            OutputLog.Items.Clear();
            graph.BleachingEdges();
            graph.BleachingVertices();
            UpdateCanvas();
            if (tools != null)
            {
                tools.Unload();
            }
            tools = new MaximumFlow(canvas, graph, OutputLog);
            tools.Load();
        }
        private void Btn_Click_Save(object sender, RoutedEventArgs e)
        {
            WritingDataJson readDataJson = new WritingDataJson();
            readDataJson.WriteGraoh(graph);
        }
        private void UpdateCanvas()
        {
            foreach (var item in graph.Edges)
            {
                item.WayFrom = item.Weight;
                item.WayTo = 0;
            }
            foreach (var item in graph.Vertices)
            {
                item.PathCost = "";
            }
            foreach (var item in canvas.Children)
            {
                if (item is Node node1)
                {
                    foreach (var connection in node1.connections)
                    {
                        connection.UpdateWight();
                    }
                    node1.UpdateLine();
                }
            }
        }
    }
}
