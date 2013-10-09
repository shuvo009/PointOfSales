using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Stock;

namespace whostpos.MenuCommands
{
    public class ViewStockCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new StockView();
        }

        public string CommandName
        {
            get { return "View Stock"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewStockCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}