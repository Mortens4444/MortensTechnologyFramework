namespace Mtf.Hardware.Raid.Areca
{
    public class ArecaController : CommandParser
    {
        private readonly int controllerId;

        public ArecaController(int controllerId)
        {
            this.controllerId = controllerId;
        }

        public void SetCurrentController()
        {
            ExcecuteCommand($"set curctrl={controllerId}");
        }

        public void SetPassword(string password)
        {
            ExcecuteCommand($"set curctrl={controllerId} password={password}");
        }

        public void SaveControllerInformation(string relativePath)
        {
            ExcecuteCommand($"set curctrl={controllerId} savecfg path={relativePath}");
        }
    }
}