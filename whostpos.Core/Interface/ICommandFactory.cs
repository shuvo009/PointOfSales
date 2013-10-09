using System.Windows.Controls;

namespace whostpos.Core.Interface
{
    public interface ICommandFactory
    {
        string CommandName { get; }
        ContentPresenter ContentPre { get; set; }

        ICommand MakeCommand(ContentPresenter contentPresenter);
    }
}