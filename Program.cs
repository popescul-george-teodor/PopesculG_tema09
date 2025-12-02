using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema_EGC_Slots
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (Window3D window = new Window3D())
            {
                window.Run(60.0);
            }   
        }
    }
}
