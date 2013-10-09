using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Customer;

namespace whostpos.MenuCommands
{
    public class ViewCustomerBalanceCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new CustomerBalanceView();
        }

        public string CommandName
        {
            get { return "View Customer Balance"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewCustomerBalanceCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}