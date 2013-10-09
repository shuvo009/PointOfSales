using whostpos.Core.Classes;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;

namespace whostpos.MenuCommands
{
    public class NotFoundCommand : ICommand
    {
        public string Name { get; set; }
        public void Execute()
        {
            IShowMessage showMessage = new DxShowMessage();
            showMessage.ShowMessage(string.Format("Couldn`t find command {0}", Name),MessageBoxImage.Error);
        }
    }
}
