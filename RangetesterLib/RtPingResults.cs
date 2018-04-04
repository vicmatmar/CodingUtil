using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeTester
{
    public class RtPingResults
    {
        public double TxLqi;
        public double TxRssi;
        public double RxLqi;
        public double RxRssi;

        public RtPingResults(double txLqi, double txRssi, double rxLqi, double rxRssi)
        {
            TxLqi = txLqi;
            TxRssi = txRssi;
            RxLqi = rxLqi;
            RxRssi = rxRssi;

        }

    }
}
