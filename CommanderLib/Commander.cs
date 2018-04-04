using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommanderLib
{
    /// <summary>
    /// Helper class used for automation with SI Commander utility
    /// </summary>
    public class Commander
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        static string[] _possible_paths = new string[]
        {
            @"C:\ Simplicity Commander",
            @"C:\SiliconLabs\SimplicityStudio\v4\developer\adapter_packs\commander"
        };

        const string _exe_name = "commander.exe";

        static string _exe_path;
        public static string Exe_path
        {
            get
            {
                _exe_path = FindEXEPath();
                return _exe_path;
            }
            set => _exe_path = value;
        }

        static string _device = "EFR32MG13P732F512GM48";
        static public string Device { get => _device; set => _device = value; }

        static string _ip = null;
        public static string IP { get => _ip; set => _ip = value; }

        static string _workingDir = null;
        static public string WorkingDir { get => _workingDir; set => _workingDir = value; }

        static string commanderRunner(string arguments)
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo()
            {
                FileName = Exe_path,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = arguments
            };

            if (!string.IsNullOrEmpty(WorkingDir))
                p.StartInfo.WorkingDirectory = WorkingDir;

            _logger.Debug(arguments);

            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            if (p.ExitCode != 0)
            {
                throw new Exception($"Exit code = {p.ExitCode}\r\n{output}\r\n{arguments}");
            }

            _logger.Debug(output);

            return output;

        }

        public static string Flash(IEnumerable<string> files, string mfgstring = null, bool massErase = false)
        {
            // commander flash bootloader-storage-internal-single-512k-combined.s37 Highfin.gbl --masserase --tokengroup znet --token "TOKEN_MFG_STRING:\"4200-C\"" --device EFR32MG13P732F512GM48
            string args = "flash";

            if (!string.IsNullOrEmpty(IP))
                args += $" --ip {IP}";

            foreach (string file in files)
                args += $" {file}";

            if (massErase)
                args += $" --masserase";

            if (!string.IsNullOrEmpty(mfgstring))
                args += $" --tokengroup znet --token \"TOKEN_MFG_STRING:\\\"{mfgstring}\\\"\"";

            if (!string.IsNullOrEmpty(Device))
                args += $" --device {Device}";

            string poutput = commanderRunner(args);
            return poutput;

        }

        public static string TokenDump(string token, string group)
        {
            string args = "tokendump";

            if (!string.IsNullOrEmpty(IP))
                args += $" --ip {IP}";

            if (!string.IsNullOrEmpty(group))
                args += $" --tokengroup {group}";

            if (!string.IsNullOrEmpty(token))
                args += $" --token \"{token}\"";

            if (!string.IsNullOrEmpty(Device))
                args += $" --device {Device}";

            string poutput = commanderRunner(args);
            return poutput;
        }

        public static string SetDgbMode(string mode="OUT")
        {
            string args = $"adapter dbgmode {mode}";
            string poutput = commanderRunner(args);
            return poutput;
        }

        public static string GetEUIStr()
        {
            return GetEUI().ToString("X16");
        }

        public static long GetEUI()
        {
            //string args = $"tokendump --tokengroup znet --token \"MFG_EMBER_EUI_64\" --device {Device}";
            string poutput = TokenDump("MFG_EMBER_EUI_64", "znet");

            Regex regx = new Regex("MFG_EMBER_EUI_64: ([0-9,A-F]{16})"); // i.e = 0784A7FEFF570B00
            Match m = regx.Match(poutput);

            if (!m.Success || m.Groups.Count < 2)
            {
                throw new Exception($"Unable to find EUI in:\r\n{poutput}");
            }

            // Convert to little endian
            long bigeui = Convert.ToInt64(m.Groups[1].Value, 16);
            byte[] bs = BitConverter.GetBytes(bigeui);
            Array.Reverse(bs);
            long eui = BitConverter.ToInt64(bs, 0);

            /*
            char[] chars = m.Groups[1].Value.ToCharArray();
            string[] strs = new string[chars.Length / 2];
            for (int i = 0, b = 0; i < chars.Length / 2; i++, b += 2)
            {
                strs[i] = new string(new char[] { chars[b], chars[b + 1] });
            }
            var reversed = strs.Reverse();
            string eui = string.Join("", reversed.ToArray()).Trim();
            */

            return eui;
        }

        /// <summary>
        /// Checks system for local commander utility
        /// </summary>
        /// <returns>utility full path</returns>
        public static string FindEXEPath()
        {
            string exe_path = _exe_path;
            if (!string.IsNullOrEmpty(exe_path))
                if (File.Exists(exe_path))
                    return exe_path;

            foreach (var p in _possible_paths)
            {
                string pp = Path.Combine(p, _exe_name);
                if (File.Exists(pp))
                    return pp;
            }

            return null;
        }
    }
}
