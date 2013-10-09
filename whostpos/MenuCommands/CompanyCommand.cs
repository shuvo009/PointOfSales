using System.Windows.Controls;
using whostpos.Core.Interface;
using whostpos.View;

namespace whostpos.MenuCommands
{
    public class CompanyCommand : ICommand, ICommandFactory
    {
        public void Execute()
        {
            ContentPre.Content = new CompanyInformationView();
        }

        public string CommandName
        {
            get { return "Company"; }
        }

        public ContentPresenter ContentPre { get; set; }

        public ICommand MakeCommand(ContentPresenter contentPresenter)
        {
            return new CompanyCommand
                {
                    ContentPre = contentPresenter
                };
        }
    }
}