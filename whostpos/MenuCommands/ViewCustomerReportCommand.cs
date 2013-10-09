using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Customer;

namespace whostpos.MenuCommands
{
    public class ViewCustomerReportCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new CustomerReportView();
        }

        public string CommandName
        {
            get { return "View Customer Report"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewCustomerReportCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}