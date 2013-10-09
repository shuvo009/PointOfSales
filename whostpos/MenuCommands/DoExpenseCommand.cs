using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Expenses;

namespace whostpos.MenuCommands
{
    public class DoExpenseCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new ExpenseView();
        }

        public string CommandName
        {
            get { return "Do Expense"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new DoExpenseCommand
            {
                ContentPre = contentPresenter
            };
        }
    }
}