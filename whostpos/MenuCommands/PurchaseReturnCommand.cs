using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Transction;

namespace whostpos.MenuCommands
{
    public class PurchaseReturnCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new PurchaseReturnView();
        }

        public string CommandName
        {
            get { return "Purchase Return"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new PurchaseReturnCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}