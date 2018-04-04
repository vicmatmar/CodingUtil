using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace CodingUtil
{
    [Verb("code", HelpText = "use to code device")]
    class CodeOptions
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

        [Option(Required = false, HelpText = "Working Directory")]
        public string WorkingDir { get; set; }

        [Option('s', "server", Required = false,
            HelpText = "Server Range Test Master Address.")]
        public string Server_Host { get; set; }

        [Option("server_port", Required = false, Default = 10001,
            HelpText = "Server port number")]
        public int Server_Port { get; set; }

        [Option('c', "client", Required = false,
            HelpText = "Client DUT ISA Adapter Address.")]
        public string Client_Host { get; set; }

        [Option("client_port", Required = false, Default= 4901,
            HelpText = "Client port number")]
        public int Client_Port { get; set; }

        [Option('l', "channel", Required = false, Default = 17,
            HelpText = "Connection Channel (11-25)")]
        public int Channel { get; set; }

    }

    [Verb("test", HelpText = "dummy test verb")]
    class TestOptions
    {
        [Option(Required = false, Default = false)]
        public bool Test { get; set; }

    }

}
