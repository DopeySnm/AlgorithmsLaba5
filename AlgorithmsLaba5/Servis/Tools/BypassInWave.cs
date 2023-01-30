using AlgorithmsLaba5.DataStructures;
using AlgorithmsLaba5.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace AlgorithmsLaba5.Servis.Tools
{
    internal class BypassInWave : ITools
    {
        private Grid grid;
        private Node node;
        private Canvas canvas;
        private Graph graph;
        private ListView listView;
        public BypassInWave(Canvas canvas, Graph graph, ListView listView)
        {
            this.canvas = canvas;
            this.graph = graph;
            this.listView = listView;
        }
        async public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            CheckMouseItems checkMouseItems = new CheckMouseItems(canvas);
            Node node = checkMouseItems.node;
            if (node != null)
            {
                string queueItemStr = "";
                List<Vertex> result = new List<Vertex>();
                var vertex = node.vertex;
                Queue<Vertex> queue = new Queue<Vertex>();
                var current = vertex;
                queue.Enqueue(vertex);
                listView.Items.Add($"Положили в очередь вершину {vertex.Data}");
                do
                {
                    listView.Items.Add("Состояние очереди");
                    queueItemStr = "";
                    foreach (var item in queue)
                    {
                        queueItemStr = queueItemStr + $" {item.Data},";
                    }
                    listView.Items.Add($"Состояние очереди начало очереди [ {queueItemStr} ] конец очереди");
                    current = queue.Dequeue();
                    result.Add(current);
                    listView.Items.Add($"Достали из очереди вершину {current.Data}");
                    current.Backlight = true;
                    UpdateCanvas();
                    listView.Items.Add($"Отметили вершину {current.Data}");
                    await Task.Delay(1000);
                    var adjacentVertices = graph.GetVertexList(current);
                    listView.Items.Add($"Взяли все смежные вершины {current.Data}");
                    for (int i = 0; i < adjacentVertices.Count; i++)
                    {
                        if (!adjacentVertices[i].Backlight)
                        {
                            listView.Items.Add($"Вершина {adjacentVertices[i].Data} не отмечена");
                            if (!queue.Contains(adjacentVertices[i]))
                            {
                                listView.Items.Add($"Вершины {adjacentVertices[i].Data} нет в очереди");
                                queue.Enqueue(adjacentVertices[i]);
                                listView.Items.Add($"Положили в очередь вершину {adjacentVertices[i].Data}");
                            }
                            else
                            {
                                listView.Items.Add($"Вершина {adjacentVertices[i].Data} уже есть в стеке, не добавляем в очередь");
                            }
                        }
                        else
                        {
                            listView.Items.Add($"Вершина {adjacentVertices[i].Data} отмечена, не добавляем в очередь");
                        }
                    }
                } while (queue.Count != 0);
                current.Backlight = true;
                UpdateCanvas();
                listView.Items.Add($"Отметили вершину {current.Data}");
                queueItemStr = "";
                foreach (var item in result)
                {
                    queueItemStr = queueItemStr + $" {item.Data},";
                }
                listView.Items.Add($"Результат {queueItemStr}");
                listView.Items.Add($"Очередь пустая выходим, конец алгоритма");
            }
        }
        private void UpdateCanvas()
        {
            foreach (var item in canvas.Children)
            {
                if (item is Node node1)
                {
                    node1.UpdateColor();
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
