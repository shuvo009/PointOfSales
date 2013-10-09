using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Other;
using whostpos.View.UserAcount;

namespace whostpos.MenuCommands
{
    public class HomeCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new HomeView();
        }

        public string CommandName
        {
            get { return "Home"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new HomeCommand
            {
                ContentPre = contentPresenter
            };
        }
    }
}
