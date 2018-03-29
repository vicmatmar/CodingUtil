using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanderLib
{
    /// <summary>
    /// Helper class used for automation with SI Commander utility
    /// </summary>
    public class Commander
    {
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

        static public string Device { get => _device; set => _device = value; }

        static string _device = "EFR32MG13P732F512GM48";

        public static string GetEUI()
        {
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = Exe_path,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = $"tokendump --tokengroup znet --token \"MFG_EMBER_EUI_64\" --device {_device}"
            };

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return output;
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
