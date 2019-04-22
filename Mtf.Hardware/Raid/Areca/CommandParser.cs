using System;
using System.Collections.Generic;
using System.Text;
using Mtf.Reflection;

namespace Mtf.Hardware.Raid.Areca
{
    public abstract class CommandParser : ProgramRunner
    {
        protected const int NotFound = -1;
        protected static string CliPath;
        protected Dictionary<string, Tuple<string, Type>> information;

        protected string ExcecuteCommand(string command)
        {
            return RunProgramOrFile(CliPath, command, false, true, 30);
        }

        protected string GetLineValue(string line)
        {
            return line.Substring(line.IndexOf(':') + 2).Trim();
        }

        protected int GetStartLine(string[] lines)
        {
            var lineIndex = 0;
            while (lineIndex < lines.Length && lines[lineIndex].IndexOf("==", StringComparison.Ordinal) == NotFound)
            {
                lineIndex++;
            }
            lineIndex++;
            return lineIndex;
        }

        // TODO Do it with generic function
        private void SetPropertyFromParsedLine(string parsedLine)
        {
            foreach (var info in information)
            {
                if (info.Value.Item2 == typeof(string))
                {
                    if (SetStringProperty(parsedLine, info.Key, info.Value.Item1))
                    {
                        return;
                    }
                }
                else if (info.Value.Item2 == typeof(int))
                {
                    if (SetIntProperty(parsedLine, info.Key, info.Value.Item1))
                    {
                        return;
                    }
                }
                else if (info.Value.Item2 == typeof(double))
                {
                    if (SetDoubleProperty(parsedLine, info.Key, info.Value.Item1))
                    {
                        return;
                    }
                }
            }
        }

        private bool SetDoubleProperty(string line, string caption, string propertyName)
        {
            if (line.IndexOf(caption, StringComparison.Ordinal) > NotFound)
            {
                this.SetPropertyValue(propertyName, Convert.ToDouble(GetLineValue(line)));
                return true;
            }
            return false;
        }

        private bool SetIntProperty(string line, string caption, string propertyName)
        {
            if (line.IndexOf(caption, StringComparison.Ordinal) > NotFound)
            {
                this.SetPropertyValue(propertyName, Convert.ToInt32(GetLineValue(line)));
                return true;
            }
            return false;
        }

        private bool SetStringProperty(string line, string caption, string propertyName)
        {
            if (line.IndexOf(caption, StringComparison.Ordinal) > NotFound)
            {
                this.SetPropertyValue(propertyName, GetLineValue(line));
                return true;
            }
            return false;
        }

        protected void ProcessInfo(string commandOutput)
        {
            var lines = GetLines(commandOutput);
            var i = 0;
            while (i < lines.Length)
            {
                SetPropertyFromParsedLine(lines[i]);
                i++;
            }
        }

        protected string[] GetLines(string text)
        {
            return text.Replace(Environment.NewLine, "\n").Split('\n');
        }

        protected void AppendIntValue(StringBuilder stringBuilder, string valueName, int? value)
        {
            if (value.HasValue)
            {
                stringBuilder.Append($" {valueName}={value.Value}");
            }
        }
    }
}