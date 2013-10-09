using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Customer;

namespace whostpos.MenuCommands
{
    public class SetupCustomerCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new CustomerView();
        }

        public string CommandName
        {
            get { return "Setup Customer"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new SetupCustomerCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}