namespace Mtf.Hardware.Raid.Amcc3Ware
{
    public class CLI_and_API_Version
    {
        public string CLI_version;
        public string API_version;

        public override string ToString()
        {
            return $"CLI version: {CLI_version}, API version: {API_version}";
        }
    }
}