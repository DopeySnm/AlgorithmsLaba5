using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlgorithmsLaba5.Models
{
    internal interface ITools
    {
        public void Load();
        public void Unload();
        public void MouseMove(object sender, MouseEventArgs e);
        public void MouseDown(object sender, MouseButtonEventArgs e);
        public void MouseUp(object sender, MouseButtonEventArgs e);
    }
}
