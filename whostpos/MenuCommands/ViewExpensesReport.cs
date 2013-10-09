using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Expenses;

namespace whostpos.MenuCommands
{
    public class ViewExpensesReport :  ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new ExpenseReportView();
        }

        public string CommandName
        {
            get { return "View Expenses Report"; }
        }
        public ContentPresenter ContentPre { get; set; }
        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewExpensesReport
            {
                ContentPre = contentPresenter
            };
        }
    }
}
