using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View.Database;

namespace whostpos.MenuCommands
{
    public class ViewDatabaseRestoreAndBackupCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new DatabaseBackupRestoreView();
        }

        public string CommandName
        {
            get { return "View Database Restore And Backup"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new ViewDatabaseRestoreAndBackupCommand
            {
                ContentPre = contentPresenter
            };
        }
    }
}
