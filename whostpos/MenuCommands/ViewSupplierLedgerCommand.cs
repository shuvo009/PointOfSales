using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Supplier;

namespace whostpos.MenuCommands
{
    public class ViewSupplierLedgerCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new SpplierLedgerView();
        }

        public string CommandName
        {
            get { return "View Supplier Ledger"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewSupplierLedgerCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}