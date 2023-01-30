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
    internal class MaximumFlow : ITools
    {
        private Grid grid;
        private Node node;
        private Canvas canvas;
        private Graph graph;
        private Node[] nodes = new Node[2];
        private ListView listView;

        public MaximumFlow(Canvas canvas, Graph graph, ListView listView)
        {
            this.canvas = canvas;
            this.graph = graph;
            this.listView = listView;
        }

        public async void MouseDown(object sender, MouseButtonEventArgs e)
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
                    int resultCount = 0;
                    do
                    {
                        int currentCount = 0;
                        var current = nodes[0].vertex;
                        var currentEdges = graph.GetOreintedFromEdgeList(current);
                        List<Edge> banList = new List<Edge>();
                        List<Edge> way = new List<Edge>();
                        List<Vertex> wayVertex = new List<Vertex>();
                        listView.Items.Add($"Находим ребро с самым большим весом");
                        currentCount = currentEdges[0].WayFrom;
                        var currentEdge = currentEdges[0];
                        for (int i = 1; i < currentEdges.Count; i++)
                        {
                            if (currentEdges[i].WayFrom > currentCount)
                            {
                                currentCount = currentEdges[i].WayFrom;
                                currentEdge = currentEdges[i];
                            }
                        }
                        listView.Items.Add($"Ребро с самым большим весом {currentEdge.From} -> {currentEdge.To} = {currentCount}");
                        banList.Add(currentEdge);
                        way.Add(currentEdge);
                        wayVertex.Add(current);
                        if (currentEdge.From.Equals(current))
                        {
                            current = currentEdge.To;
                        }
                        else if (currentEdge.To.Equals(current))
                        {
                            current = currentEdge.From;
                        }
                        if (currentCount != 0)
                        {
                            listView.Items.Add($"Находим путь");
                            do
                            {
                                //Находим путь
                                currentEdge = ShearchMaxStream(current, banList, wayVertex);
                                listView.Items.Add($"Берём ребро ребро с самым большим весом вершины {current}");
                                if (currentEdge == null)
                                {//если currentEdge == null мы отходим на одну вершину назад и удаляем из бана все пути current кроме
                                 //того из которого мы пришли
                                    listView.Items.Add($"Не нашли отходим назад");
                                    if (way.Count == 0)
                                    {
                                        break;
                                    }
                                    var edge = way[way.Count - 1];
                                    way.Remove(edge);
                                    //var deleteBanListEdge = graph.GetEdgeList(current);
                                    //for (int i = 0; i < banList.Count; i++)
                                    //{
                                    //    if (deleteBanListEdge.Contains(banList[i]))
                                    //    {
                                    //        if (!banList[i].Equals(edge))
                                    //        {
                                    //            banList.Remove(banList[i]);
                                    //        }
                                    //    }
                                    //}
                                    if (edge.From.Equals(current))
                                    {
                                        current = edge.To;
                                    }
                                    else if (edge.To.Equals(current))
                                    {
                                        current = edge.From;
                                    }
                                    wayVertex.Remove(current);
                                    for (int i = 0; i < way.Count; i++)
                                    {
                                        if (way[i].WayFrom > 0)
                                        {
                                            listView.Items.Add($"{way[i].From} -> {way[i].To} = {way[i].WayFrom}");
                                        }
                                        else
                                        {
                                            listView.Items.Add($"{way[i].From} -> {way[i].To} = {way[i].WayTo}");
                                        }
                                    }
                                    continue;
                                }
                                else
                                {
                                    listView.Items.Add($"Нашли ребро с самым большим весом {currentEdge.From} -> {currentEdge.To} = {currentCount}");
                                    banList.Add(currentEdge);
                                    way.Add(currentEdge);
                                    wayVertex.Add(current);
                                    if (currentEdge.From.Equals(current))
                                    {
                                        current = currentEdge.To;
                                    }
                                    else if (currentEdge.To.Equals(current))
                                    {
                                        current = currentEdge.From;
                                    }
                                    for (int i = 0; i < way.Count; i++)
                                    {
                                        if (way[i].WayFrom > 0)
                                        {
                                            listView.Items.Add($"{way[i].From} -> {way[i].To} = {way[i].WayFrom}");
                                        }
                                        else
                                        {
                                            listView.Items.Add($"{way[i].From} -> {way[i].To} = {way[i].WayTo}");
                                        }
                                    }
                                }
                            } while (!current.Equals(nodes[1].vertex));
                            if (way.Count == 0)
                            {
                                listView.Items.Add($"Не нашли путь выходим");
                                break;
                            }
                            //вычисляем минимальный проход
                            wayVertex.Add(nodes[1].vertex);
                            currentCount = graph.GetEdgeTwoVertex(wayVertex[0], wayVertex[1]).WayFrom;
                            for (int i = 1; i < wayVertex.Count - 1; i++)
                            {
                                currentEdge = graph.GetEdgeTwoVertex(wayVertex[i], wayVertex[i + 1]);
                                if (currentEdge.From.Equals(wayVertex[i]))
                                {
                                    if (currentEdge.WayFrom < currentCount)
                                    {
                                        currentCount = currentEdge.WayFrom;
                                    }
                                }
                                else if (currentEdge.From.Equals(wayVertex[i + 1]))
                                {
                                    if (currentEdge.WayTo < currentCount)
                                    {
                                        if (currentEdge.WayTo != 0)
                                        {
                                            currentCount = currentEdge.WayTo;
                                        }
                                    }
                                }
                            }
                            //строим и вычитаем данные из пути
                            for (int i = 0; i < wayVertex.Count - 1; i++)
                            {
                                currentEdge = graph.GetEdgeTwoVertex(wayVertex[i], wayVertex[i + 1]);
                                if (currentEdge.From.Equals(wayVertex[i]))
                                {
                                    currentEdge.WayFrom = currentEdge.WayFrom - currentCount;
                                    currentEdge.WayTo = currentEdge.WayTo + currentCount;
                                }
                                else if (currentEdge.From.Equals(wayVertex[i + 1]))
                                {
                                    currentEdge.WayTo = currentEdge.WayTo - currentCount;
                                    currentEdge.WayFrom = currentEdge.WayFrom + currentCount;
                                }
                                currentEdge.Backlight = true;
                                currentEdge.From.Backlight = true;
                                currentEdge.To.Backlight = true;
                                UpdateCanvas();
                                await Task.Delay(1000);
                            }
                            if (currentCount == 0)
                            {
                                break;
                            }
                            string wayStr = "";
                            foreach (var item in wayVertex)
                            {
                                wayStr = wayStr + item.Data + ", ";
                            }
                            listView.Items.Add($"Путь: [ {wayStr} ] = " + currentCount);
                            resultCount = resultCount + currentCount;
                            graph.BleachingEdges();
                            graph.BleachingVertices();
                            UpdateCanvas();
                        }
                        else
                        {
                            listView.Items.Add($"Не нашли путь выходим");
                            break;
                        }
                    } while (true);
                    listView.Items.Add(resultCount);
                }
            }
        }

        private Edge ShearchMaxStream(Vertex current, List<Edge> banEdges, List<Vertex> vertices)
        {
            var edges = graph.GetEdgeList(current);
            int currentCount = 0;
            Edge currentEdge = null;
            for (int i = 0; i < edges.Count; i++)
            {
                if (currentEdge == null)
                {
                    if (!banEdges.Contains(edges[i]))
                    {
                        if (!(vertices.Contains(edges[i].From) || vertices.Contains(edges[i].To)))
                        {
                            if (edges[i].From.Equals(current))
                            {
                                currentCount = edges[i].WayFrom;
                                currentEdge = edges[i];
                            }
                        }
                    }
                }
                else
                {
                    if (edges[i].WayFrom > currentCount)
                    {
                        if (!banEdges.Contains(edges[i]))
                        {
                            if (!(vertices.Contains(edges[i].From) || vertices.Contains(edges[i].To)))
                            {
                                if (edges[i].From.Equals(current))
                                {
                                    currentCount = edges[i].WayFrom;
                                    currentEdge = edges[i];
                                }
                            }
                        }
                    }
                }
            }
            if (currentCount == 0)
            {
                for (int i = 0; i < edges.Count; i++)
                {
                    if (currentEdge == null)
                    {
                        if (!banEdges.Contains(edges[i]))
                        {
                            if (!(vertices.Contains(edges[i].From) || vertices.Contains(edges[i].To)))
                            {
                                if (edges[i].To.Equals(current))
                                {
                                    currentCount = edges[i].WayTo;
                                    currentEdge = edges[i];
                                }
                            }
                        }
                    }
                    else
                    {
                        if (edges[i].WayTo > currentCount)
                        {
                            if (!banEdges.Contains(edges[i]))
                            {
                                if (!(vertices.Contains(edges[i].From) || vertices.Contains(edges[i].To)))
                                {
                                    if (edges[i].To.Equals(current))
                                    {
                                        currentCount = edges[i].WayTo;
                                        currentEdge = edges[i];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (currentCount == 0)
            {
                //banEdges.Add(currentEdge);
                return null;
            }
            return currentEdge;
        }
        private void UpdateCanvas()
        {
            foreach (var item in canvas.Children)
            {
                if (item is Node node1)
                {
                    foreach (var connection in node1.connections)
                    {
                        connection.UpdateWay();
                    }
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
