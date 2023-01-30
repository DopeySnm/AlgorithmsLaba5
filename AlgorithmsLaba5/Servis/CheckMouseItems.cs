using AlgorithmsLaba5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AlgorithmsLaba5.Servis
{
    internal class CheckMouseItems
    {
        private Canvas canvas;
        public Node node;
        public CheckMouseItems(Canvas canvas)
        {
            this.canvas = canvas;
            CheckObjectUnderMouse();
        }
        public void CheckObjectUnderMouse()
        {
            node = null;
            for (int i = 0; i < canvas.Children.Count; i++)
            {
                if (canvas.Children[i] is Node shape && shape.IsMouseOver)
                {
                    node = shape;
                }
            }
        }
    }
}
