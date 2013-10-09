using System;
using System.Windows;
using DevExpress.Xpf.Core;
using whostpos.Core.Interface;
using MessageBoxImage = whostpos.Core.Helpers.MessageBoxImage;

namespace whostpos.Core.Classes
{
    public class DxShowMessage : IShowMessage
    {
        public void ShowMessage(string message, MessageBoxImage messageBoxImage)
        {
            System.Windows.MessageBoxImage messageBoxPic;

            switch (messageBoxImage)
            {
                case MessageBoxImage.Ok:
                    messageBoxPic = System.Windows.MessageBoxImage.Information;
                    break;
                case MessageBoxImage.Error:
                    messageBoxPic = System.Windows.MessageBoxImage.Error;
                    break;
                case MessageBoxImage.Question:
                    messageBoxPic = System.Windows.MessageBoxImage.Question;
                    break;
                default:
                    messageBoxPic = System.Windows.MessageBoxImage.None;
                    break;
            }

            Application.Current.Dispatcher.Invoke(new Action(() => DXMessageBox.Show(message, "Whost POS", MessageBoxButton.OK, messageBoxPic)));
        }

        public bool ShowYesNoMessage(string message)
        {
            var result = false;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                result = DXMessageBox.Show(message, "Whost POS", MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) ==
                         MessageBoxResult.Yes;
            }));
            return result;
        }
    }
}
