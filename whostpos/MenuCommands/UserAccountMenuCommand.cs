using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.UserAcount;

namespace whostpos.MenuCommands
{
    public class UserAccountMenuCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new UserAccountView();
        }

        public string CommandName
        {
            get { return "User Account"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new UserAccountMenuCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}