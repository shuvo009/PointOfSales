using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Supplier;

namespace whostpos.MenuCommands
{
    public class SupplierReportCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new SupplierReportView();
        }

        public string CommandName
        {
            get { return "Supplier Report"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new SupplierReportCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}