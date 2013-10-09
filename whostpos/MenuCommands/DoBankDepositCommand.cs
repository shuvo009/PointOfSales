using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Bank;

namespace whostpos.MenuCommands
{
    public class DoBankDepositCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new BankDepositView();
        }

        public string CommandName
        {
            get { return "Do Bank Deposit"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new DoBankDepositCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}