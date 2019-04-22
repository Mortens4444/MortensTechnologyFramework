using System.Diagnostics;

namespace Mtf.Hardware.Raid
{
    public class ProgramRunner
    {
        public string RunProgramOrFile(string filename, string arguments, bool shellExecute, bool waitForExit, int? timeoutInSeconds)
        {
            var psi = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = arguments,
                UseShellExecute = shellExecute
            };

            if (waitForExit)
            {
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
            }

            string result = null;
            {
                using (var ps = Process.Start(psi))
                {
                    if (ps == null)
                    {
                        return null;
                    }
                    if (waitForExit)
                    {
                        if (timeoutInSeconds == null)
                        {
                            ps.WaitForExit();
                        }
                        else
                        {
                            ps.WaitForExit(timeoutInSeconds.Value * 1000);
                        }
                        result = ps.StandardOutput.ReadToEnd();
                    }
                }
            }
            return result;
        }

        public static string[] ElliminateCharsAndCreateArray(string input, char ch)
        {
            var eliminate = ch.ToString() + ch;
            while (input.IndexOf(eliminate) > -1)
            {
                input = input.Replace(eliminate, ch.ToString());
            }
            input = input.Trim(ch);
            return input.Split(ch);
        }
    }
}