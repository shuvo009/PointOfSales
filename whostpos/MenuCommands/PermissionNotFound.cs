using whostpos.Core.Classes;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;

namespace whostpos.MenuCommands
{
    public class PermissionNotFound : ICommand
    {
        public void Execute()
        {
            IShowMessage showMessage = new DxShowMessage();
            showMessage.ShowMessage(string.Format("Permission Required."), MessageBoxImage.Ok);
        }
    }
}
