using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Transction;

namespace whostpos.MenuCommands
{
    public class ViewSalesReturnReportCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new SalesReturnReportView();
        }

        public string CommandName
        {
            get { return "View Sales Return Report"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewSalesReturnReportCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}