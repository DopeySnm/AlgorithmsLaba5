using AlgorithmsLaba5.DataStructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AlgorithmsLaba5.Servis
{
    internal class WritingDataJson
    {
        public void WriteGraoh(Graph graph)
        {
            string dateWrite = JsonConvert.SerializeObject(graph);
            File.WriteAllText("..\\..\\..\\..\\graph.json", dateWrite);
        }
    }
}
