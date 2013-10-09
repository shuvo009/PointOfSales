using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Customer;

namespace whostpos.MenuCommands
{
    public class CustomerDuePaymentCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new CustomerDuePaumentView();
        }

        public string CommandName
        {
            get { return "Customer Due Payment"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new CustomerDuePaymentCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}