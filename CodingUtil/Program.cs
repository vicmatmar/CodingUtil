using System;
using System.Linq;
using CommanderLib;
using CommandLine;

namespace CodingUtil
{
    class Program
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        static int RunFlash(FlashOptions opts)
        {
            _logger.Info("GetEUI");
            long eui = Commander.GetEUI();
            _logger.Info( $"EUI = {eui.ToString("X16")}");

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
                rc = Parser.Default.ParseArguments<FlashOptions, TestOptions>(args)
                    .MapResult(
                        (FlashOptions opts) => RunFlash(opts),
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
