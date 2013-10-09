using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Transction;

namespace whostpos.MenuCommands
{
    public class ViewStockRegisterCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new PurchaseReportView();
        }

        public string CommandName
        {
            get { return "View Stock Register"; }
        }
        public ContentPresenter ContentPre { get; set; }
        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewStockRegisterCommand
            {
                ContentPre = contentPresenter
            };
        }
    }
}
