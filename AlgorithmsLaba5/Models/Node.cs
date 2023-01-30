using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using AlgorithmsLaba5.DataStructures;
using System.Xml.Linq;
using System.Windows.Markup;

namespace AlgorithmsLaba5.Models
{
    internal class Node : Shape
    {
        public Graph graph;
        public Grid grid;
        public List<Connection> connections;
        public Vertex vertex;
        public TextBlock Data;
        public TextBlock PathCost;
        public Node(Grid grid, Vertex vertex)
        {
            this.grid = grid;
            this.vertex = vertex;
            StackPanel stackPanel = new StackPanel();
            Data = new TextBlock() { Text = vertex.ToString(), Height = 16, TextWrapping = TextWrapping.Wrap, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 12 };
            stackPanel.Children.Add(Data);
            PathCost = new TextBlock() { Text = vertex.PathCost, Height = 16, TextWrapping = TextWrapping.Wrap, FontSize = 12 };
            stackPanel.Children.Add(PathCost);
            grid.Children.Add(stackPanel);
            connections = new List<Connection>();
        }
        public void DeletConnect(Connection connection)
        {
            connections.Remove(connection);
        }
        public void AddConnect(Connection connection)
        {
            connections.Add(connection);
        }
        public void UpdateData()
        {
            Data.Text = vertex.Data;
            PathCost.Text = vertex.PathCost;
        }
        public void UpdateColor()
        {
            if (vertex.Backlight)
            {
                Fill = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
            }
            else
            {
                Fill = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
            }
        }
        public void UpdateLine()
        {
            UpdateData();
            UpdateColor();
            connections.ForEach(x => x.Update());
        }
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(Double), typeof(Node));
        public double Size
        {
            get { return (double)this.GetValue(SizeProperty); }
            set { this.SetValue(SizeProperty, value); }
        }
        protected override Geometry DefiningGeometry
        {
            get
            {
                Point p1 = new Point(0.0d, 0.0d);
                Point p2 = new Point(this.Size, 0.0d);
                Point p3 = new Point(this.Size, this.Size);
                Point p4 = new Point(0.0d, this.Size);

                List<PathSegment> segments = new List<PathSegment>(4);
                segments.Add(new LineSegment(p1, true));
                segments.Add(new LineSegment(p2, true));
                segments.Add(new LineSegment(p3, true));
                segments.Add(new LineSegment(p4, true));

                List<PathFigure> figures = new List<PathFigure>(1);
                PathFigure pf = new PathFigure(p1, segments, true);
                figures.Add(pf);

                Geometry g = new PathGeometry(figures, FillRule.EvenOdd, null);

                return g;
            }
        }
    }
}
