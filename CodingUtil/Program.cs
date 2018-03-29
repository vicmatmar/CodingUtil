using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommanderLib;

namespace CodingUtil
{
    class Program
    {
        static int Main(string[] args)
        {
            string eui = Commander.GetEUI();

            Console.WriteLine(eui);

            Console.Read();
            return 0;
        }
    }
}
