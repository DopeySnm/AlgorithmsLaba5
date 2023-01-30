using AlgorithmsLaba5.DataStructures;
using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlgorithmsLaba5.Servis.Tools
{
    internal class BypassInDepth : ITools
    {
        private Grid grid;
        private Node node;
        private Canvas canvas;
        private Graph graph;
        private ListView listView;
        public BypassInDepth(Canvas canvas, Graph graph, ListView listView)
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
                List<Vertex> result = new List<Vertex>();
                string stackItemStr = "";
                var vertex = node.vertex;
                Stack<Vertex> stack = new Stack<Vertex>();
                var current = vertex;
                stack.Push(vertex);
                listView.Items.Add($"Пушнули в стек вершину {vertex.Data}");
                do
                {
                    listView.Items.Add("Состояние стека");
                    stackItemStr = "";
                    foreach (var item in stack)
                    {
                        stackItemStr = stackItemStr + $" {item.Data},";
                    }
                    listView.Items.Add($"Состояние стека вершина стека [ {stackItemStr} ] дно стека");
                    current = stack.Pop();
                    result.Add(current);
                    listView.Items.Add($"Достали из стека вершину {current.Data}");
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
                            if (!stack.Contains(adjacentVertices[i]))
                            {
                                listView.Items.Add($"Вершины {adjacentVertices[i].Data} нет в стеке");
                                stack.Push(adjacentVertices[i]);
                                listView.Items.Add($"Пушнули в стек вершину {adjacentVertices[i].Data}");
                            }
                            else
                            {
                                listView.Items.Add($"Вершина {adjacentVertices[i].Data} уже есть в стеке, не добавляем в стек");
                            }
                        }
                        else
                        {
                            listView.Items.Add($"Вершина {adjacentVertices[i].Data} отмечена, не добавляем в стек");
                        }
                    }
                } while (stack.Count != 0);
                current.Backlight = true;
                UpdateCanvas();
                listView.Items.Add($"Отметили вершину {current.Data}");
                stackItemStr = "";
                foreach (var item in result)
                {
                    stackItemStr = stackItemStr + $" {item.Data},";
                }
                listView.Items.Add($"Результат {stackItemStr}");
                listView.Items.Add($"Стек пустой выходим, конец алгоритма");
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
