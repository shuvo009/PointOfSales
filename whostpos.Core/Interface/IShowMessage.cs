using whostpos.Core.Helpers;

namespace whostpos.Core.Interface
{
    public interface IShowMessage
    {
        void ShowMessage(string message, MessageBoxImage messageBoxImage);
        bool ShowYesNoMessage(string message);
    }
}
