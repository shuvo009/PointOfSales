using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Transction;

namespace whostpos.MenuCommands
{
    public class ViewSalesReportCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new SalesReportView();
        }

        public string CommandName
        {
            get { return "View Sales Report"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewSalesReportCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}