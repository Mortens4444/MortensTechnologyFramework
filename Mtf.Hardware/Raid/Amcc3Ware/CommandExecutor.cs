using Mtf.Hardware.Raid.Amcc3Ware.Enum;
using Mtf.Utils.EnumExtensions;

namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class CommandExecutor : ProgramRunner
    {
        public string ExecuteCommand(string filename, TW_CLI_ParameterType parameter, int? x = null, int? y = null, int? z = null)
        {
            var arguments = GetTwCliParameter(parameter);
            if (x != null)
            {
                arguments = arguments.Replace("{0}", x.ToString());
            }
            if (y != null)
            {
                arguments = arguments.Replace("{1}", y.ToString());
            }
            if (z != null)
            {
                arguments = arguments.Replace("{2}", z.ToString());
            }
            return RunProgramOrFile(filename, arguments, false, true, 120);
        }

        public string ExecuteCommand(string filename, TW_CLI_ParameterType parameter, int x, string p1 = null, string p2 = null, string p3 = null)
        {
            var arguments = GetTwCliParameter(parameter);
            arguments = arguments.Replace("{0}", x.ToString());
            arguments = arguments.Replace("{1}", p1);
            if (p2 != null)
            {
                arguments = arguments.Replace("{2}", p2);
            }
            if (p3 != null)
            {
                arguments = arguments.Replace("{3}", p3);
            }
            return RunProgramOrFile(filename, arguments, false, true, 30);
        }

        public static string GetTwCliParameter(TW_CLI_ParameterType parameter)
        {
            return parameter.GetDescription();
        }
    }
}