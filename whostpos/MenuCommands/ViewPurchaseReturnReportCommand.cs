using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Transction;

namespace whostpos.MenuCommands
{
    public class ViewPurchaseReturnReportCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new PurchaseReturnReportView();
        }

        public string CommandName
        {
            get { return "View Purchase Return Report"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewPurchaseReturnReportCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}