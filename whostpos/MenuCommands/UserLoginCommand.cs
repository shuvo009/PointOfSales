using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.UserAcount;

namespace whostpos.MenuCommands
{
    public class UserLoginCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new UserLoginView();
        }

        public string CommandName
        {
            get { return "User Login"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new UserLoginCommand
            {
                ContentPre = contentPresenter
            };
        }
    }
}
