using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Bank;

namespace whostpos.MenuCommands
{
    public class SetupBankAccountsCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new BankAccountView();
        }

        public string CommandName
        {
            get { return "Setup Bank Accounts"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new SetupBankAccountsCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}