using AlgorithmsLaba5.DataStructures;
using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace AlgorithmsLaba5.Servis.Tools
{
    internal class ShortestWay : ITools
    {
        private Grid grid;
        private Node node;
        private Canvas canvas;
        private Graph graph;
        private Node[] nodes = new Node[2];
        private ListView listView;
        public ShortestWay(Canvas canvas, Graph graph, ListView listView)
        {
            this.canvas = canvas;
            this.graph = graph;
            this.listView = listView;
        }
        private class VertexInt
        {
            public Vertex Vertex;
            public int Weight;
            public VertexInt(int weight, Vertex vertex)
            {
                Weight = weight;
                Vertex = vertex;
            }
        }
        async public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            graph.BleachingVertices();
            graph.BleachingEdges();
            CheckMouseItems checkMouseItems = new CheckMouseItems(canvas);
            Node node = checkMouseItems.node;
            if (node != null)
            {
                if (nodes[0] == null)
                {
                    nodes[0] = node;
                }
                else
                {
                    nodes[1] = node;
                    var current = nodes[0].vertex;
                    Dictionary<Vertex, VertexInt> dictionaryVertex = new Dictionary<Vertex, VertexInt>();
                    foreach (var item in graph.Vertices)
                    {
                        if (item.Equals(current))
                        {
                            dictionaryVertex.Add(item, new VertexInt(0, item));
                            listView.Items.Add($"Добавляем состояния {current.Data} и ставил ему метку {0}");
                        }
                        else
                        {
                            dictionaryVertex.Add(item, new VertexInt(int.MaxValue, item));
                            listView.Items.Add($"Добавляем состояния {item.Data} и ставил ему метку {int.MaxValue} это местка бесконечности для удобства");
                        }
                    }
                    listView.Items.Add($"Заполнили состояния");
                    int exit = 0;
                    do
                    {
                        exit++;
                        List<Edge> currentEdge = graph.GetOreintedFromEdgeList(current);
                        listView.Items.Add($"Берём все смежные вершины, вершины - {current.Data}");
                        int weight = dictionaryVertex[current].Weight;
                        listView.Items.Add($"Заполняем состояния");
                        for (int i = 0; i < currentEdge.Count; i++)
                        {
                            await Task.Delay(1000);
                            if (!currentEdge[i].To.Equals(current))
                            {
                                if (!currentEdge[i].To.Backlight)
                                {
                                    if (dictionaryVertex[currentEdge[i].To].Weight > weight + currentEdge[i].Weight)
                                    {
                                        listView.Items.Add($"Вес {weight + currentEdge[i].Weight} меньше чем {dictionaryVertex[currentEdge[i].To].Weight}");
                                        dictionaryVertex[currentEdge[i].To].Weight = weight + currentEdge[i].Weight;
                                        dictionaryVertex[currentEdge[i].To].Vertex = current;
                                        listView.Items.Add($"Добавляем вершину {dictionaryVertex[currentEdge[i].To].Vertex.Data} и вес {weight + currentEdge[i].Weight} в состояния вершины {currentEdge[i].To}");
                                        listView.Items.Add($"Получается мы переходим из вершины {dictionaryVertex[currentEdge[i].To].Vertex.Data} с весом {weight + currentEdge[i].Weight} в состояния вершины {currentEdge[i].To}");
                                    }
                                }
                            }
                            else if (!currentEdge[i].From.Equals(current))
                            {
                                if (!currentEdge[i].To.Backlight)
                                {
                                    if (dictionaryVertex[currentEdge[i].From].Weight > weight + currentEdge[i].Weight)
                                    {
                                        listView.Items.Add($"Вес {weight + currentEdge[i].Weight} меньше чем {dictionaryVertex[currentEdge[i].From].Weight}");
                                        dictionaryVertex[currentEdge[i].From].Weight = weight + currentEdge[i].Weight;
                                        dictionaryVertex[currentEdge[i].From].Vertex = current;
                                        listView.Items.Add($"Добавляем вершину {dictionaryVertex[currentEdge[i].From].Vertex.Data} и вес {weight + currentEdge[i].Weight} в состояния вершины {currentEdge[i].From}");
                                        listView.Items.Add($"Получается мы переходим из вершины {dictionaryVertex[currentEdge[i].From].Vertex.Data} с весом {weight + currentEdge[i].Weight} в состояния вершины {currentEdge[i].From}");
                                    }
                                }
                            }
                            foreach (var item in dictionaryVertex)
                            {
                                item.Key.PathCost = $"От {nodes[0].vertex.Data} = " + item.Value.Weight.ToString();
                            }
                            UpdateCanvas();
                        }
                        current.Backlight = true;
                        listView.Items.Add($"Отмечаем как минимальный вес, вершину {current.Data}");
                        //UpdateCanvas();
                        //await Task.Delay(1000);
                        VertexInt currentVertexInt = null;
                        listView.Items.Add($"Ищим минимальный переход");
                        foreach (var item in dictionaryVertex)
                        {
                            if (currentVertexInt == null)
                            {
                                if (!item.Key.Backlight)
                                {
                                    if (item.Value.Weight != int.MaxValue)
                                    {
                                        currentVertexInt = item.Value;
                                        current = item.Key;
                                    }
                                }
                            }
                            else
                            {
                                if (!item.Key.Backlight)
                                {
                                    if (item.Value.Weight != int.MaxValue)
                                    {
                                        if (currentVertexInt.Weight > item.Value.Weight)
                                        {
                                            currentVertexInt = item.Value;
                                            current = item.Key;
                                        }
                                    }
                                }
                            }
                        }
                        listView.Items.Add($"Минимальный переход в вершину {current.Data}");
                        //if (exit > graph.countVertices)
                        //{
                        //    graph.BleachingVertices();
                        //    graph.BleachingEdges();
                        //    nodes = new Node[2];
                        //    return;
                        //}
                    } while (/*!current.Equals(nodes[1].vertex)*/exit < graph.countVertices);
                    graph.BleachingVertices();
                    current = nodes[1].vertex; // всё что закоментированно, это мы проходили только до нужной вершины,
                                               // а это и то что на месте комментария это мы проходим всё но строим только путь только до нужного
                    listView.Items.Add($"Отмечаем путь");
                    do
                    {
                        foreach (var item in dictionaryVertex)
                        {
                            if (item.Key.Equals(current))
                            {
                                current.Backlight = true;
                                var currentEdge = graph.GetEdgeTwoVertex(item.Value.Vertex, current);
                                currentEdge.Backlight = true;
                                current = item.Value.Vertex;
                                listView.Items.Add($"Отмечаем вершину {current.Data}");
                                listView.Items.Add($"Отмечаем ребро - из {currentEdge.From} в {currentEdge.To}");
                                //UpdateCanvas();
                                //await Task.Delay(1000);
                                break;
                            }
                        }
                    } while (!current.Equals(nodes[0].vertex));
                    nodes[0].vertex.Backlight = true;
                    listView.Items.Add($"Отмечаем вершину {nodes[0].vertex.Data}");
                    UpdateCanvas();
                    listView.Items.Add($"Подсвечиваем весь путь, конец алгоритма");
                    nodes = new Node[2];
                }
            }
        }
        private void UpdateCanvas()
        {
            foreach (var item in canvas.Children)
            {
                if (item is Node node1)
                {
                    node1.UpdateLine();
                }
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
        public void MouseMove(object sender, MouseEventArgs e)
        {

        }
        public void MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
