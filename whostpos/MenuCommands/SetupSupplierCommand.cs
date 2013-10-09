using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Supplier;

namespace whostpos.MenuCommands
{
    public class SetupSupplierCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new SupplierView();
        }

        public string CommandName
        {
            get { return "Setup Supplier"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new SetupSupplierCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}