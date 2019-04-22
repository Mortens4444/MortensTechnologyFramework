using System;
using System.Text;

namespace Mtf.Linux
{
    public class CommndExecutionResult
    {
        public string Error { get; set; }

        public string Output { get; set; }

        public CommndExecutionResult(string error, string output)
        {
            Error = error;
            Output = output;
        }

        public bool HasSucceeded => !String.IsNullOrEmpty(Output);

        public override string ToString()
        {
            var result = new StringBuilder(Output);
            if (!String.IsNullOrEmpty(Error))
            {
                result.Append(Error);
            }
            return result.ToString();
        }
    }
}