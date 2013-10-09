using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Cash;

namespace whostpos.MenuCommands
{
    public class ViewCashReportCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new CashReportView();
        }

        public string CommandName
        {
            get { return "View Cash Report"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewCashReportCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}