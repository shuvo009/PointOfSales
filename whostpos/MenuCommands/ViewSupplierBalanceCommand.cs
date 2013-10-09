using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Supplier;

namespace whostpos.MenuCommands
{
    public class ViewSupplierBalanceCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new SupplierBalanceView();
        }

        public string CommandName
        {
            get { return "View Supplier Balance"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewSupplierBalanceCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}