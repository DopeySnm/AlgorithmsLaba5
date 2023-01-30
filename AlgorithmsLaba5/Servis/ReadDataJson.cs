using AlgorithmsLaba5.DataStructures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba5.Servis
{
    internal class ReadDataJson
    {
        public Graph ReadGraph(string path)
        {
            string read = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Graph>(read);
        }
    }
}
