using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Products;

namespace whostpos.MenuCommands
{
    internal class ViewProductReportCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new ProductReportView();
        }

        public string CommandName
        {
            get { return "View Product Report"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewProductReportCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}