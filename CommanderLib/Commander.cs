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
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            if (p.ExitCode != 0)
            {
                throw new Exception($"{output}\r\n{arguments}");
            }

            return output;

        }

        public static void Flash(IEnumerable<string> files, string mfgstring, bool massErase = false)
        {

        }

        public static string GetEUIStr()
        {
            return GetEUI().ToString("X16");
        }

        public static long GetEUI()
        {
            string args = $"tokendump --tokengroup znet --token \"MFG_EMBER_EUI_64\" --device {_device}";
            string poutput = commanderRunner(args);

            Regex regx = new Regex("MFG_EMBER_EUI_64: ([0-9,A-F]{16})"); // i.e = 0784A7FEFF570B00
            Match m = regx.Match(poutput);

            if (!m.Success || m.Groups.Count < 2)
                return -1;

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
