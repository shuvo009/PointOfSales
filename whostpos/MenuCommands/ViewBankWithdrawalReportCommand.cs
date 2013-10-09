using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Bank;

namespace whostpos.MenuCommands
{
    public class ViewBankWithdrawalReportCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new BankWithdrawalReportView();
        }

        public string CommandName
        {
            get { return "View Bank Withdrawal Report"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewBankWithdrawalReportCommand
            {
                ContentPre = contentPresenter
            };
        }
    }
}