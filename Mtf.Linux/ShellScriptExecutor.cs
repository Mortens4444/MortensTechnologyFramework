using System;
using System.Diagnostics;

namespace Mtf.Linux
{
    public class ShellScriptExecutor
    {
        public CommndExecutionResult GetCommandResult(string command, string arguments = "")
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "awk",
                    Arguments = String.Concat("'BEGIN{system(\"", command, "\")}'"),
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };
            process.Start();
            process.WaitForExit();
            return new CommndExecutionResult(process.StandardError.ReadToEnd(), process.StandardOutput.ReadToEnd());
        }
    }
}