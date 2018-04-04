using System;
using System.Linq;
using System.Threading;
using CommanderLib;
using CommandLine;
using RangeTester;

namespace CodingUtil
{
    class Program
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        static int RunCode(CodeOptions opts)
        {

            _logger.Info("Code Device");
            Commander.Device = opts.Device;
            Commander.WorkingDir = opts.WorkingDir;
            Commander.IP = opts.Client_Host;
            Commander.SetDgbMode("OUT");
            Commander.Flash(opts.InputFiles, opts.MFGString, opts.MassErase);

            _logger.Info("Range Test");
            IRangeTester rangeTester = new EFR32xRT();
            rangeTester.Server_Host = opts.Server_Host;
            rangeTester.Server_Port = opts.Server_Port;
            rangeTester.Client_Host = opts.Client_Host;
            rangeTester.Client_Port = opts.Client_Port;
            rangeTester.Channel = opts.Channel;

            rangeTester.Server_Init();
            Thread.Sleep(500);
            RtPingResults ping = rangeTester.Ping(Properties.Settings.Default.PingCount);

            string msg = $"\r\n";
            msg += $"TxLqi:{ping.TxLqi} ({Properties.Settings.Default.TxLqi})\r\n";
            msg += $"TxSsi:{ping.TxRssi} ({Properties.Settings.Default.TxRssi})\r\n";
            msg += $"RxLqi:{ping.RxLqi} ({Properties.Settings.Default.RxLqi})\r\n";
            msg += $"RxRssi:{ping.RxRssi} ({Properties.Settings.Default.RxRssi})";

            _logger.Info(msg);

            if (ping.TxLqi >= Properties.Settings.Default.TxLqi && ping.TxRssi >= Properties.Settings.Default.TxRssi &&
                ping.RxLqi >= Properties.Settings.Default.RxLqi && ping.RxRssi >= Properties.Settings.Default.RxRssi)
            {
                _logger.Info("Range Test Passed");
            }
            else
            {
                _logger.Info("Range Test Failed");
                return -1;
            }


            _logger.Info("GetEUI");
            long eui = Commander.GetEUI();
            _logger.Info($"EUI = {eui.ToString("X16")}");

            _logger.Info("Insert EUI");
            int euiid = DatabaseUtils.InsertEUI(eui);
            _logger.Info($"EUI id = {euiid}");

            return 0;
        }

        static int RunTest(TestOptions opts)
        {
            return 0;
        }

        static int Main(string[] args)
        {
            int rc = 0;
            _logger.Info("Program started");
            try
            {
                rc = Parser.Default.ParseArguments<CodeOptions, TestOptions>(args)
                    .MapResult(
                        (CodeOptions opts) => RunCode(opts),
                        (TestOptions opts) => RunTest(opts),
                        errs => 1);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                return -1;
            }

            return rc;
        }
    }
}
