using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Customer;

namespace whostpos.MenuCommands
{
    public class ViewCustomerLedgerCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new CustomerLedgerView();
        }

        public string CommandName
        {
            get { return "View Customer Ledger"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewCustomerLedgerCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}