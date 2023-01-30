using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AlgorithmsLaba5.Servis.Tools
{
    internal class MoveNode : ITools
    {
        Canvas canvas { get; set; }
        Point startPosition { get; set; }
        private bool drag { get; set; }
        private Node node;
        private CheckMouseItems checkMouseItem;
        public MoveNode(Canvas canvas)
        {
            this.canvas = canvas;
            this.checkMouseItem = new CheckMouseItems(canvas);
            this.node = checkMouseItem.node;

        }
        public void Load()
        {
            canvas.MouseLeftButtonDown += MouseDown;
            canvas.MouseLeftButtonUp += MouseUp;
            canvas.MouseMove += MouseMove;
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
            checkMouseItem.CheckObjectUnderMouse();
            node = checkMouseItem.node;
            if (drag && node != null)
            {
                var mp = e.GetPosition(canvas);
                double deltaX = mp.X - startPosition.X;
                double deltaY = mp.Y - startPosition.Y;

                var newX = deltaX + Canvas.GetLeft(node);
                var newY = deltaY + Canvas.GetTop(node);

                if (newX < 0)
                    newX = 0;
                else if (newX + node.ActualWidth > canvas.ActualWidth)
                    newX = canvas.ActualWidth - node.ActualWidth;

                if (newY < 0)
                    newY = 0;
                else if (newY + node.ActualHeight > canvas.ActualHeight)
                    newY = canvas.ActualHeight - node.ActualHeight;

                node.vertex.XCordinate = newX;
                node.vertex.YCordinate = newY;

                Canvas.SetLeft(node, newX);
                Canvas.SetTop(node, newY);
                Canvas.SetLeft(node.grid, newX);
                Canvas.SetTop(node.grid, newY);
                //node.person.XCordinate = newX;
                //node.person.YCordinate = newY;
                node.UpdateLine();

                startPosition = mp;
            }
        }

        public void MouseDown(object sender, MouseButtonEventArgs e)
        {
            checkMouseItem.CheckObjectUnderMouse();
            node = checkMouseItem.node;
            if (node != null)
            {
                drag = true;
                Mouse.OverrideCursor = Cursors.Hand;
                startPosition = e.GetPosition(canvas);
                Mouse.Capture(node);
            }
        }

        public void MouseUp(object sender, MouseButtonEventArgs e)
        {
            checkMouseItem.CheckObjectUnderMouse();
            node = checkMouseItem.node;
            if (node != null)
            {
                drag = false;
                Mouse.OverrideCursor = Cursors.Arrow;
                Mouse.Capture(null);
            }
        }

        public void Unload()
        {

        }
    }
}
