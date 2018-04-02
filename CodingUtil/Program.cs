using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommanderLib;
using Centralite.Database;

namespace CodingUtil
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                long eui = Commander.GetEUI();
                DatabaseUtils.InsertEUI(eui);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);

#if DEBUG
                Console.Read();
#endif

                return -1;
            }

#if DEBUG
            Console.Read();
#endif
            return 0;
        }
    }
}
