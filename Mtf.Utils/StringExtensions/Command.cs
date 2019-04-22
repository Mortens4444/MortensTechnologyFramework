using System;
using System.Collections.Generic;
using System.Text;

namespace Mtf.Utils.StringExtensions
{
    public static class Command
    {
        public static List<string> GetProgramAndParameters(this string command)
        {
            var result = new List<string>();
            var nextElement = new StringBuilder();

            var i = 0;
            while (i < command.Length)
            {
                switch (command[i])
                {
                    case '"':
                        do
                        {
                            i++;
                            if (i >= command.Length)
                            {
                                throw new Exception("Could not found closing \"");
                            }

                            if (command[i] != '"')
                            {
                                nextElement.Append(command[i]);
                            }
                        }
                        while (command[i] != '"');
                        AddNextElement(nextElement, result);
                        nextElement = new StringBuilder();
                        break;
                    case ' ':
                        AddNextElement(nextElement, result);
                        nextElement = new StringBuilder();
                        break;
                    default:
                        nextElement.Append(command[i]);
                        break;
                }
                i++;
            }
            AddNextElement(nextElement, result);
            return result;
        }

        private static void AddNextElement(StringBuilder nextElement, ICollection<string> result)
        {
            if (nextElement.ToString() != String.Empty)
            {
                result.Add(nextElement.ToString());
            }
        }
    }
}