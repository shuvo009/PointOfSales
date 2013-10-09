using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Transction;

namespace whostpos.MenuCommands
{
    public class ViewSalesReturnCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new SalesReturnView();
        }

        public string CommandName
        {
            get { return "View Sales Return"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewSalesReturnCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}