using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Bank;

namespace whostpos.MenuCommands
{
    public class DoBankWithdrawalCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new BankWithdrawalView();
        }

        public string CommandName
        {
            get { return "Do Bank Withdrawal"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new DoBankWithdrawalCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}