using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Supplier;

namespace whostpos.MenuCommands
{
    public class SupplierDuePaymentCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new SupplierDuePaymentView();
        }

        public string CommandName
        {
            get { return "Supplier Due Payment"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new SupplierDuePaymentCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}