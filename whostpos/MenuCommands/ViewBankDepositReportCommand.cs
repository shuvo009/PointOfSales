using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Bank;

namespace whostpos.MenuCommands
{
    public class ViewBankDepositReportCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new BankDepositReportView();
        }

        public string CommandName
        {
            get { return "View Bank Deposit Report"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewBankDepositReportCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}