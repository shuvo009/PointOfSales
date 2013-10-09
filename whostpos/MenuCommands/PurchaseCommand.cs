using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Transction;

namespace whostpos.MenuCommands
{
    public class PurchaseCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new PurchaseView();
        }

        public string CommandName
        {
            get { return "Purchase"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new PurchaseCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}