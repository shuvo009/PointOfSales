using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Products;

namespace whostpos.MenuCommands
{
    public class SetupProductCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new ProductSetupView();
        }

        public string CommandName
        {
            get { return "Setup Product"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new SetupProductCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}