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
    internal class PrimsAlgorithm : ITools
    {
        private Grid grid;
        private Node node;
        private Canvas canvas;
        private Graph graph;
        private ListView listView;
        public PrimsAlgorithm(Canvas canvas, Graph graph, ListView listView)
        {
            this.canvas = canvas;
            this.graph = graph;
            this.listView = listView;
        }
        private Edge ShearchMin(List<Edge> edgesVert, List<Edge> edgesConect, List<Vertex> verticesConect)
        {
            Edge edge = null;
            int count = 0;
            for (int i = count; i < edgesVert.Count; i++)
            {
                if (!edgesConect.Contains(edge))
                {
                    if (!(verticesConect.Contains(edgesVert[i].From) && verticesConect.Contains(edgesVert[i].To)))
                    {
                        edge = edgesVert[i];
                        break;
                    }
                }
                count++;
            }
            for (int i = count; i < edgesVert.Count; i++)
            {
                if (edgesVert[i].Weight < edge.Weight)
                {
                    if (!edgesConect.Contains(edgesVert[i]))
                    {
                        if (!(verticesConect.Contains(edgesVert[i].From) && verticesConect.Contains(edgesVert[i].To)))
                        {
                            edge = edgesVert[i];
                        }
                    }
                }
            }
            return edge;
        }
        async public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            CheckMouseItems checkMouseItems = new CheckMouseItems(canvas);
            Node node = checkMouseItems.node;
            if (node != null)
            {
                string verticesConectStr = "";
                List<Edge> edgesConect = new List<Edge>();
                List<Vertex> verticesConect = new List<Vertex>();
                listView.Items.Add($"Ищем минимальное ребро графа");
                var edge = ShearchMin(graph.Edges, edgesConect, verticesConect);
                listView.Items.Add($"минимальное ребро - из {edge.From.Data} в {edge.To.Data}");
                edge.Backlight = true;
                edge.From.Backlight = true;
                edge.To.Backlight = true;
                UpdateCanvas();
                listView.Items.Add($"Отмечаем ребро из {edge.From.Data} в {edge.To.Data} и его вершины");
                await Task.Delay(1000);
                verticesConect.Add(edge.From);
                verticesConect.Add(edge.To);
                edgesConect.Add(edge);
                listView.Items.Add($"Добавляем ребро из {edge.From.Data} в {edge.To.Data} и его вершины в отмеченные");
                while (!verticesConect.Count.Equals(graph.Vertices.Count))
                {
                    listView.Items.Add($"Перебераем все смежные рёбра уже отмеченных вершин и ищем минимальное смежно ребро");
                    for (int i = 0; i < verticesConect.Count; i++)
                    {
                        verticesConectStr = "";
                        var listEdge = graph.GetEdgeList(verticesConect[i]);
                        foreach (var item in verticesConect)
                        {
                            verticesConectStr = verticesConectStr + $" {item.Data},";
                        }
                        listView.Items.Add($"Отмеченные вершины: [ {verticesConectStr} ]");
                        listView.Items.Add($"Берём все смежные вершины, вершины - {verticesConect[i].Data}");
                        var temp = ShearchMin(listEdge, edgesConect, verticesConect);
                        listView.Items.Add($"Ищим из смежных вершины, вершины - {verticesConect[i].Data} минимальное ребро, чтобы это ребро и одна из вершин не входили в отмеченные");
                        if (i != 0)
                        {
                            if (edge == null && temp != null)
                            {
                                listView.Items.Add($"Текущее ребро - из {temp.From} в {temp.To}");
                                edge = temp;
                            }
                            if (temp != null)
                            {
                                listView.Items.Add($"сравниваем выбранное ребро - из {edge.From} в {edge.To} вес - {edge.Weight}  с ребром - из {temp.From} в {temp.To} вес - {temp.Weight}");
                                if (temp.Weight < edge.Weight)
                                {
                                    listView.Items.Add($"ребро - из {temp.From} в {temp.To} вес - {temp.Weight} меньше чем текущее ребро - из {edge.From} в {edge.To} вес - {edge.Weight}");
                                    edge = temp;
                                    listView.Items.Add($"Новое терущее ребро - из {temp.From} в {temp.To}");
                                }
                            }
                        }
                        else
                        {
                            if (temp != null)
                            {
                                listView.Items.Add($"Берём в текущее ребро - из {temp.From} в {temp.To}");
                            }
                            else
                            {
                                listView.Items.Add($"Нет подходящих рёбер в вершине {verticesConect[i].Data}");
                            }
                            edge = temp;
                        }
                    }
                    edge.Backlight = true;
                    edge.From.Backlight = true;
                    edge.To.Backlight = true;
                    UpdateCanvas();
                    listView.Items.Add($"Отмечаем ребро из {edge.From.Data} в {edge.To.Data} и его вершины");
                    await Task.Delay(1000);
                    if (!verticesConect.Contains(edge.From))
                    {
                        verticesConect.Add(edge.From);
                        listView.Items.Add($"Добавляем вершину из {edge.From.Data} в отмеченные");
                    }
                    if (!verticesConect.Contains(edge.To))
                    {
                        verticesConect.Add(edge.To);
                        listView.Items.Add($"Добавляем вершину из {edge.To.Data} в отмеченные");
                    }
                    edgesConect.Add(edge);
                    listView.Items.Add($"Добавляем ребро из {edge.From.Data} в {edge.To.Data}");
                }
                verticesConectStr = "";
                foreach (var item in verticesConect)
                {
                    verticesConectStr = verticesConectStr + $" {item.Data},";
                }
                listView.Items.Add($"Отмеченные вершины: [ {verticesConectStr} ]");
                listView.Items.Add($"Отметили все вершины, конец алгоритма");
            }
        }
        private void UpdateCanvas()
        {
            foreach (var item in canvas.Children)
            {
                if (item is Node node1)
                {
                    node1.UpdateColor();
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
