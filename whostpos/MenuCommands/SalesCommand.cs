using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Transction;

namespace whostpos.MenuCommands
{
    public class SalesCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new SalesView();
        }

        public string CommandName
        {
            get { return "Sales"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new SalesCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}