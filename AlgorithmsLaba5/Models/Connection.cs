using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AlgorithmsLaba5.Models
{
    internal class Connection
    {
        public Polyline polyline = new() { StrokeThickness = 3, Stroke = Brushes.Red };
        public Grid[] grids = new Grid[2];
        public Node[] nodes = new Node[2];
        public Edge edge;
        public Rectangle rectangle;
        public TextBlock textBlock;
        public ConnectionType connectionType;
        public void AddGrid(Grid grid, Node node)
        {
            if (grids[0] == null) grids[0] = grid;
            else grids[1] = grid;
            if (nodes[0] == null) nodes[0] = node;
            else nodes[1] = node;
        }
        public void UpdateColor()
        {
            if (edge.Backlight)
            {
                polyline.Stroke = Brushes.Green;
            }
            else
            {
                polyline.Stroke = Brushes.Red;
            }
        }
        public void UpdateWight()
        {
            textBlock.Text = edge.Weight.ToString();
        }
        public void UpdateWay()
        {
            textBlock.Text = $"{edge.WayFrom}/{edge.WayTo}";
        }
        public void UpdateLine()
        {
            switch (connectionType)
            {
                case ConnectionType.ParentsToСhild:
                    polyline.Points.Clear();
                    //polyline.Points.Add(new Point(Canvas.GetLeft(grids[0]) + grids[0].Width / 2, Canvas.GetTop(grids[0]) + grids[0].Height));
                    //polyline.Points.Add(new Point(Canvas.GetLeft(grids[1]) + grids[1].Width / 2, Canvas.GetTop(grids[1])));
                    polyline.Points.Add(new Point(Canvas.GetLeft(grids[0]) + grids[0].Width / 2, Canvas.GetTop(grids[0]) + grids[0].Height / 2));
                    polyline.Points.Add(new Point(Canvas.GetLeft(grids[1]) + grids[1].Width / 2, Canvas.GetTop(grids[1]) + grids[1].Height / 2));
                    //
                    //Canvas.SetLeft(rectangle, ((Canvas.GetLeft(grids[1])/* + grids[1].Width*/ + Canvas.GetLeft(grids[0])) / 2) + Canvas.GetLeft(grids[0]) / 2);
                    //Canvas.SetTop(rectangle, ((Canvas.GetTop(grids[1])/* + grids[1].Width */+ Canvas.GetTop(grids[0])) / 2) + Canvas.GetTop(grids[0]) / 2 );
                    if (edge.Oreinted)
                    {
                        Canvas.SetLeft(rectangle, (((polyline.Points[1].X + polyline.Points[0].X) / 2) + polyline.Points[1].X) / 2);
                        Canvas.SetTop(rectangle, (((polyline.Points[1].Y + polyline.Points[0].Y) / 2) + polyline.Points[1].Y) / 2);
                    }
                    //
                    Canvas.SetLeft(textBlock, (polyline.Points[1].X + polyline.Points[0].X) / 2);
                    Canvas.SetTop(textBlock, (polyline.Points[1].Y + polyline.Points[0].Y) / 2);
                    //Canvas.SetLeft(textBlock, (Canvas.GetLeft(grids[1]) /*+ grids[1].Width*/ + Canvas.GetLeft(grids[0])) / 2);
                    //Canvas.SetTop(textBlock, (Canvas.GetTop(grids[1]) /*+ grids[1].Height*/ + Canvas.GetTop(grids[0])) / 2);
                    break;
                case ConnectionType.ChildToParents:
                    polyline.Points.Clear();
                    polyline.Points.Add(new Point(Canvas.GetLeft(grids[0]) + grids[0].Width / 2, Canvas.GetTop(grids[0])));
                    polyline.Points.Add(new Point(Canvas.GetLeft(grids[1]) + grids[1].Width / 2, Canvas.GetTop(grids[1]) + grids[1].Height));
                    break;
                case ConnectionType.Partner:
                    polyline.Points.Clear();
                    Point firstRectLeft = new Point(Canvas.GetLeft(grids[0]), Canvas.GetTop(grids[0]) + grids[0].Height / 2);
                    Point firstRectRight = new Point(Canvas.GetLeft(grids[0]) + grids[0].Width, Canvas.GetTop(grids[0]) + grids[0].Height / 2);

                    Point lastRectLeft = new Point(Canvas.GetLeft(grids[1]), Canvas.GetTop(grids[1]) + grids[1].Height / 2);
                    Point lastRectRight = new Point(Canvas.GetLeft(grids[1]) + grids[1].Width, Canvas.GetTop(grids[1]) + grids[1].Height / 2);

                    if (firstRectLeft.X + firstRectRight.X < lastRectLeft.X + lastRectRight.X)
                    {
                        polyline.Points.Add(firstRectRight);
                        polyline.Points.Add(lastRectLeft);
                    }
                    else
                    {
                        polyline.Points.Add(lastRectRight);
                        polyline.Points.Add(firstRectLeft);
                    }
                    //polyline.Points.Add(new Point(Canvas.GetLeft(grids[0]), Canvas.GetTop(grids[0]) + grids[0].Height / 2));
                    //polyline.Points.Add(new Point(Canvas.GetLeft(grids[1]) + grids[1].Width, Canvas.GetTop(grids[1]) + grids[1].Height / 2));

                    // Если будут разведённые пары, то нужно поменя логику
                    break;
            }
        }
        public void Update()
        {
            UpdateColor();
            UpdateLine();
        }
    }
}
