using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Cash;

namespace whostpos.MenuCommands
{
    public class ViewInitialBalanceCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new InitialBalanceView();
        }

        public string CommandName
        {
            get { return "View Initial Balance"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewInitialBalanceCommand
            {
                ContentPre = contentPresenter
            };
        }
    }
}
