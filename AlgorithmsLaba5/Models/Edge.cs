using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba5.Models
{
    internal class Edge
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public bool Backlight { get; set; }
        public bool Oreinted { get; set; }
        public int Weight { get; set; }
        public int WayFrom { get; set; }
        public int WayTo { get; set; }
        public int Id { get; set; }
        public Edge(Vertex from, Vertex to)
        {
            From = from;
            To = to;
            WayTo = 0;
        }
        public override string ToString()
        {
            return $"({From}; {To})";
        }
    }
}
