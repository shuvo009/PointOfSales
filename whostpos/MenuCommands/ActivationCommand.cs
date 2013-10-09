using System.Windows.Controls;
using whost.License.Validator.View;
using whostpos.Core.Interface;

namespace whostpos.MenuCommands
{
    public class ActivationCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new LicenseActivisionLogin();
        }

        public string CommandName
        {
            get { return "Activation"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ActivationCommand
                   {
                       ContentPre = contentPresenter
                   };
        }
    }
}