using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba5.Models
{
    public class Vertex
    {
        public string Data { get; set; }
        public string PathCost { get; set; }
        public double XCordinate { get; set; }
        public double YCordinate { get; set; }
        public int Id { get; set; }
        public bool Backlight { get; set; }
        public Vertex(string data)
        {
            Data = data;
        }
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
