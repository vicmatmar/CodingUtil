using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace CodingUtil
{
    [Verb("flash", HelpText = "use to code device")]
    class FlashOptions
    {
        [Option('f', "inputfiles", Required = true,
           HelpText = "Input files")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('d', "device", Required = false, Default = "EFR32MG13P732F512GM48", HelpText = "Device")]
        public string Device { get; set; }

        [Option('m', "mfgstring", Required = false, HelpText = "Mfg String")]
        public string MFGString { get; set; }

        [Option(Required = false, Default = false, HelpText = "Mass Erase")]
        public bool MassErase { get; set; }

    }

    [Verb("test", HelpText = "dummy test verb")]
    class TestOptions
    {
        [Option(Required = false, Default = false)]
        public bool Test { get; set; }

    }

}
